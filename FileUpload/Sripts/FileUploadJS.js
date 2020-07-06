$(document).ready(function () {
    console.log('JS Loaded');

    var max_file_number=3;
    //swapping btnUpload click event to fileUpload
    $('#fuSelect').attr('accept', '.xls,.xlsx.doc,.docx,.pdf');
    $('#btnUpload').on('click', function () {
        $('#fuSelect').click();
        return false;
    });

    //validating files selected
    $('#fuSelect').on('change', function (event) {
        var filesList = $('.dummyUL');
        filesList.empty();
        var number_of_files = $(this)[0].files.length;
        if (number_of_files > max_file_number) {
            filesList.append('<li>Please select files..</li>');
            alert('You can upload maximum ' + max_file_number+' files.');
            $(this).val('');
            return false;
        }
        else if (number_of_files <= 0) {
            filesList.append('<li>Please select files..</li>');
        }
        else {
            var contentSize=0;
            for (i = 0; i < $(this)[0].files.length; i++){
                var fileName = '';
                if ($(this)[0].files[i].name.length > 25) {
                    fileName = $(this)[0].files[i].name.slice(0, 12) + '...' + $(this)[0].files[i].name.slice($(this)[0].files[i].name.length - 10);
                    filesList.append('<li>' + fileName + '</li>');
                }
                else {
                    filesList.append('<li>' + $(this)[0].files[i].name + '</li>');
                }
                contentSize += $(this)[0].files[i].size;
                //2000000$()
            }
            if (contentSize > 2000000) {
                alert('You can upload maximum 2Mb');
                filesList.empty();
                filesList.append('<li>Please select files..</li>');
                $(this).val('');
                return false;
            }
        }
        return true;
    });

    function addFiles() {

    }
});