
function createLeave() {
    debugger;
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);

    var data = {
        Name: $('#leaveName').val(),
        Abbreviations: $('#leaveabbreviation').val(),
        DeductFromAnnualLeave: $('#deductleave').is(":checked"),
        HoursDeductedFromTimesheet: $('#showPendingBalance').is(":checked")
    };

    if (data.Name !== "" && data.Abbreviation !== "") {
        $.ajax({
            type: 'POST',
            url: '/Leave/CreateLeave',
            dataType: 'json',
            data: {
                leaveDetails: JSON.stringify(data)
            },
            success: function (result) {
                $('#submit_btn').html(defaultBtnValue);
                if (!result.isError) {
                    var url = '/Leave/Index';
                    successAlertWithRedirect(result.msg, url);
                } else {
                    $('#submit_btn').attr("disabled", false);
                    errorAlert(result.msg);
                }
            },
            error: function (ex) {
                $('#submit_btn').html(defaultBtnValue);
                $('#submit_btn').attr("disabled", false);
                errorAlert("Network failure, please try again");
            }
        });
    } else {
        shiftValidtion(''); // This function is not defined in your provided code
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
    }
}



