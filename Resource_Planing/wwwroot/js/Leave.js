
function createLeave() {
    debugger;
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);

    var data = {
        Name: $('#leaveName').val(),
        ShiftId: $('#shiftId').val(),
        Abbreviations: $('#leaveabbreviation').val(),
        DeductFromAnnualLeave: $('#deductleave').is(":checked"),
        HoursDeductedFromTimesheet: $('#showPendingBalance').is(":checked")
    };

    if (data.Name !== "" && data.Abbreviation !== "" && data.ShiftId != "0") {
        $.ajax({
            type: 'POST',
            url: '/Admin/CreateLeaveType',
            dataType: 'json',
            data: {
                leaveDetails: JSON.stringify(data)
            },
            success: function (result) {
                $('#submit_btn').html(defaultBtnValue);
                if (!result.isError) {
                    var url = '/Admin/LeaveType';
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



$(document).ready(function () {
    debugger;
    $("#text").hide();
    $('#dropDown').hide();
});
$('#Btn').click(function () {
    debugger
    $('#dropDown').show();
    $('#remain').hide();
});
$('#CloseLeave').click(function () {
    debugger
    $('#dropDown').hide();
    $('#remain').show();
});


function requestLeave() {
    debugger;
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);
    var staffId = $("#submit1").data("staff-id");
    var staffIdString = staffId.toString();
    var leaveData = {
        staffId: staffIdString,
        leaveTypeId: $("#leaveTypeId").val(),
        startDate: $("#startDate").val(),
        endDate: $("#endDate").val(),
        reason: $("#leaveReason").val()
    };
    if (leaveData.leaveTypeName !== "" && leaveData.startDate !== "" && leaveData.endDate !== "" && leaveData.reason !== "" && leaveData.staffId !== "0") {
        $.ajax({
            type: 'POST',
            url: '/Leave/StaffRequestLeave',
            dataType: 'json',
            data: {
                leaveDetails: JSON.stringify(leaveData),
                staffId: staffId
            },
            success: function (result) {
                $('#submit_btn').html(defaultBtnValue);
                if (!result.isError) {
                    var url = '/Leave/RequestLeave';
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

function editleaveTypeToBeSave() {
    debugger;
    var data = {};
    data.Id = $('#leaveTypeId').val();
    data.name = $('#editLeaveName').val();
    data.numberOfDays = $('#numberOfDays').val();
    data.companyBranchId = $('#companyBranchId').val();
    let leaveTypeDetail = JSON.stringify(data);
    $.ajax({
        type: 'Post',
        dataType: 'Json',
        url: '/Admin/EditedLeaveType',
        data: {
            leaveTypeDetails: leaveTypeDetail,
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                var url = '/Admin/LeaveType'
                successAlertWithRedirect(result.msg, url)
            }
            else {
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            errorAlert("Error occured try again");
        }
    })
}

function editLeaveType(id) {
    debugger;
    $.ajax({
        type: 'Get',
        dataType: 'Json',
        url: '/Admin/EditLeaveType',
        data: {
            id: id
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                $('#leaveTypeId').val(result.id);
                $('#editLeaveName').val(result.name);
                $('#numberOfDays').val(result.numberOfDays);
                $('#companyBranchId').val(result.companyBranchId);
            }
            else {
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            errorAlert("Error occured try again");
        }
    })
}

function deleteleaveType() {
    var id = $('#deleteleaveTypeId').val();
    debugger;
    $.ajax({
        type: 'Post',
        dataType: 'Json',
        url: '/Admin/LeaveTypeToBeDeleted',
        data: {
            id: id
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                var url = '/Admin/LeaveType'
                successAlertWithRedirect(result.msg, url)
            }
            else {
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            errorAlert("Error occured try again");
        }
    })
}

function leaveTypeToBeDeleted(id) {
    debugger;
    $('#deleteleaveTypeId').val(id);
}
