$(function() {
    $('#GioiTinhs').change(function() {
        var GioiTinhs= $(this).val();
      //here u can get the selected batch ID
      //pass the batch ID to controller by using ajax request
    });

$("#submit").live("click", function (e) {

                e.preventDefault();
                var data = GioiTinhs;
                $.ajax({
                    type: "POST",
                    url: config.basePath + '/Controllername/ActionMethodName',
                    cache: false,
                    data: data,
                    dataType: this.dataType,
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {

                        if (result.IsSuccess) {
                            $("#error_message").html(result.ErrorMessage);
                            $("#error_message").addClass("success");
                            UTMSGrid.StudentCourses.List.bindGrid($("#grid"));
                        }
                        else {
                            $("#error_message").html(result.ErrorMessage);
                            $("#error_message").addClass("fail");
                        }
                    },
                    error: function (data) {
                        // $("#error_message").html(data);

                    }
                });
});