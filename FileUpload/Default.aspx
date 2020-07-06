<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FileUpload.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />

    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk" crossorigin="anonymous" />
    <link href="Styles/FileUploadCSS.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="upMain">
            <ContentTemplate>
        <div class="container-fluid">
            <div class="row">
                <table class="table table-dark">
                    <tr>
                        <td colspan="2">File Upload Control</td>
                    </tr>
                    <tr>
                        <td>
                            <div class="Drop-Zone">
                                <asp:FileUpload ID="fuSelect" runat="server" ClientIDMode="Static" AllowMultiple="true" Style="display: hidden; height: 0px; width: 0px;" />
                                <asp:Button runat="server" ID="btnUpload" ClientIDMode="Static" CssClass="btn btn-success uploadBtn" Text="Choose to upload" />
                                <br />
                                <ul id="liFiles" style="text-align:left;margin-top:10px;" runat="server" class="dummyUL">
                                </ul>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:10%;">
                            <label for="ddlUser" id="lblUser">User</label>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlUser" Width="150" CssClass="form-control">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="chkCheck" ClientIDMode="Static" Text="check" AutoPostBack="true" CssClass="form-control" width="110"/>
                            <asp:Button runat="server" ID="btnSubmit" Text="Submit" ClientIDMode="Static" OnClick="btnSubmit_Click" CssClass="btn btn-primary" width="110" />
                        </td>
                    </tr>
                </table>
            </div>

        </div>
                </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
                <asp:PostBackTrigger ControlID="ddlUser" />
                <asp:PostBackTrigger ControlID="chkCheck" />
                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
        </asp:UpdatePanel>



    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js" integrity="sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI" crossorigin="anonymous"></script>
    <script src="Sripts/FileUploadJS.js"></script>
    </form>



    </body>
</html>
