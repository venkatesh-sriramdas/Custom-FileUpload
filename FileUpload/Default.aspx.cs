using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FileUpload
{
    public partial class Default : System.Web.UI.Page
    {
        DataTable dtFiles = new DataTable("tblEPRAttachments");
        public DataTable getFiles {
            get { return (Session["SelectedFiles"] as DataTable == null?dtFiles: Session["SelectedFiles"] as DataTable); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                dtFiles.Columns.Add("UserId", typeof(int));
                dtFiles.Columns.Add("FileName", typeof(string));
                dtFiles.Columns.Add("FileContentType", typeof(string));
                dtFiles.Columns.Add("FileContent", typeof(byte[]));

                if (IsPostBack)
                {
                    if ((getFiles.Rows.Count <= 0 && (Request.Files.Count >= 1 && Request.Files[0].ContentLength != 0)) || (Request.Files.Count >= 1 && Request.Files[0].ContentLength != 0 && getFiles.Rows.Count > 0))//empty session and request has files
                    {
                        AddFilesToSession();
                    }
                    else if (getFiles.Rows.Count > 0 && Request.Files.Count < 1)
                    {
                        dtFiles = getFiles;
                    }
                    //else if(Request.Files.Count >= 1 && Request.Files[0].ContentLength != 0 && getFiles.Rows.Count > 0)
                    //{
                    //    AddFilesToSession();
                    //}

                    //set files data names
                }
                else
                {
                    Session["SelectedFiles"] = null;
                }
                setFileName();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                throw;
            }
        }

        private void setFileName()
        {
            try
            {
                if (getFiles.Rows.Count > 0)
                {
                    foreach(DataRow dr in getFiles.Rows)
                    {
                        var fileName = dr["FileName"].ToString();
                        if (fileName.Length > 25)
                            fileName = fileName.Substring(0, 12) + "..." + fileName.Substring(fileName.Length - 10);
                        AddListItem(fileName);
                    }
                }
                else
                {
                    AddListItem("Please select files..");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        
        private void InsertToDB(DataTable dt,ref string tErrorMessage)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["FilesDBConnectionString"].ConnectionString);
            try
            {
                SqlCommand sqlCommand = new SqlCommand("sp_InsertAttachment", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                
                SqlParameter tvpParam = sqlCommand.Parameters.AddWithValue("@data", getFiles);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "dbo.attachmentType";

                SqlParameter tResults = new SqlParameter();
                tResults.ParameterName = "@results";
                tResults.Size = 8000;
                tResults.SqlDbType = SqlDbType.NVarChar;
                tResults.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(tResults);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                string outMessage = tResults.Value.ToString();
                if (!outMessage.StartsWith("-"))
                {
                    tErrorMessage = "1:Successfully inserted";
                }
                else
                {
                    tErrorMessage = outMessage;
                }
            }
            catch (Exception ex)
            {
                if (sqlConnection.State != ConnectionState.Open) sqlConnection.Close();
                tErrorMessage = "-4:Exception : " + Environment.NewLine + ex.Message;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (getFiles.Rows.Count <= 0)//no files selected
                {
                    Response.Write("----------------" + Environment.NewLine + " Not selected " + Environment.NewLine + "-----------------");
                }
                else//files selected
                {
                    Response.Write("----------------" + Environment.NewLine + getFiles.Rows.Count + " selected " + Environment.NewLine + "-----------------");
                    string tResponse = "";
                    
                    //insertion to table
                    InsertToDB(getFiles,ref tResponse);
                    if(tResponse.StartsWith("-"))
                    {
                        Response.Write(tResponse);
                    }
                    else
                    {
                        Response.Write("Success");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            
        }
        private void AddListItem(string text)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            li.InnerText = text;
            liFiles.Controls.Add(li);
             
        }
        private void AddFilesToSession()
        {
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile file = Request.Files[i];
                    DataRow dr = dtFiles.NewRow();
                    dr["FileName"] = file.FileName;
                    using(BinaryReader fs = new BinaryReader(file.InputStream))
                    {
                        dr["FileContent"] = fs.ReadBytes(file.ContentLength);
                    }
                    dr["FileContentType"] = file.ContentType;
                    dr["UserId"] = ddlUser.SelectedValue;
                    dtFiles.Rows.Add(dr);
                }
                Session["SelectedFiles"] = dtFiles;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}