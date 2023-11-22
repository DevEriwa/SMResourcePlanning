
function createLeave() {
    debugger;
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);

    var numberOfDaysValue = $('#numberOfDays').val();
    if (numberOfDaysValue === "") {
        numberOfDaysValue = 0; // Set to null when it's empty
    }

    var data = {
        Name: $('#leaveName').val(),
        ShiftId: $('#shiftId').val(),
        Abbreviations: $('#leaveabbreviation').val(),
        NumberOfDays: numberOfDaysValue,
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

function requestLeaves() {
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


function requestLeave() {
    debugger
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);
    var staffId = $("#submit1").data("staff-id");
    var staffIdString = staffId.toString();

    var leaveData = {
        staffId: staffIdString,
        leaveTypeId: $("#leaveTypeId").val(data.leaveTypeId),
        startDate: $("#leaveStartDate").val(data.startDate),
        endDate: $("#leaveEndDate").val(data.endDate),
        leaveReason: $("#leaveReason").val(data.leaveReason)
    };
    if ($("#ckickRequest").is(":checked")) {
        // Use the attribute selector to capture the value
        leaveData.leaveTypeId = $("input[name='radio']:checked").val();

        // Make an AJAX call with the updated leaveData
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
        // Handle the case where "Others" radio button is selected
        if (leaveData.leaveTypeId !== null && leaveData.startDate !== "" && leaveData.endDate !== "" && leaveData.reason !== "" && leaveData.staffId !== "0") {
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
        //} else {
        //    shiftValidtion(''); // This function is not defined in your provided code
        //    $('#submit_btn').html(defaultBtnValue);
        //    $('#submit_btn').attr("disabled", false);
        }
    }
}


function ajaxCall(leaveData, defaultBtnValue) {
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

function leaveToBeEdited(action) {
    debugger;
    if (action == "APPROVE" || "DECLINE" || "CANCEL") {
        errorAlert("Oops! This leave can't be Edit again");
    };
}

function employeeLeaveToBeEdited(id) {
    debugger;
    $.ajax({
        type: 'Get',
        dataType: 'Json',
        url: '/Leave/EditStaffLeave',
        data: {
            id: id
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                $('#leaveStaffId').val(result.id);
                if (result.name.includes("Annual Leave")) {
                    $('#btns').prop('checked', false);
                    $('#annaulLeaveId').prop('checked', true);
                } else {
                    $('#btns').prop('checked', true);
                    $('#annaulLeaveId').prop('checked', false);
                }
                $('#otherTypeLeaveId').val(result.name);
                $('#startDate').val(dateToInput(result.startDateInString));
                $('#endDate').val(dateToInput(result.endDateInString));
                $('#numberOfDaysRemaining').val(result.numberOfDays);
                $('#remainingLeaveDays').val(result.remainingLeave);
                $('#leaveReason').val(result.leaveReason);
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

$(document).ready(function () {
    debugger;
    $('#dropDowned').hide();
    $("#Leavet").hide();

    $('#othersId').click(function () {
        debugger
        $("#leaveTypeDropDown").show();
        $("#shiftDropDown").show();
        $("#abbrev").show();
        $("#dropDowned").hide();
        $("#LeaveName").attr("disabled", "disabled");
    });

    $('#annualId').click(function () {
        debugger
        $('#dropDowned').show();
        $("#leaveTypeDropDown").show();
        $("#shiftDropDown").show();
        $("#abbrev").show();
        $("#LeaveName").val("Annual Leave");
        $("#LeaveName").val("").removeAttr("disabled");
    });

    // Trigger a click on annualId by default when the page loads
    $('#annualId').click();
});

$(document).ready(function () {
    $("#text").hide();
    $('#dropDown').hide();
    $('#ckickRequest').click(function () {
        debugger
        $("#dropDown").hide();
        $("#remain").show();
    });
    $('#clickOther').click(function () {
        $('#dropDown').show();
        $('#remain').hide();
    });
});

function viewLeaveReason(id) {
    debugger;
    $.ajax({
        type: 'GET',
        url: '@Url.Action("viewLeaveReason", "Admin")',
        data: { leaveId: id },
        success: function (data) {
            $("#myModalBody").html(data);
        }
    });
    $("#myModal").modal('show');
}

function employeeViewLeaveReason(id) {
    debugger;
    $.ajax({
        type: 'GET',
        url:'/Leave/ViewLeaveReason',
        data: { leaveId: id },
        success: function (data) {
            debugger
            $("#employeeViewLeaveModalBody").html(data);
        }
    });
    $("#employeeViewLeaveModal").modal();
};   

$("#leaveEndDate").change(function () {
    let startDate = $("#leaveStartDate").val();
    let endDate = $("#leaveEndDate").val();
    let remainingLeaveDays = parseInt($("#remainingLeaveDays").val()); // Convert to integer

    if (!!startDate && !!endDate && !isNaN(remainingLeaveDays)) {
        const startDateToTime = new Date(startDate).getTime();
        const endDateToTime = new Date(endDate).getTime();
        const timeRange = Math.abs(endDateToTime - startDateToTime);
        const daysRange = Math.round(timeRange / (1000 * 60 * 60 * 24)) + 1;
        // Check if requested days are less than or equal to remaining leave days
        if (daysRange <= remainingLeaveDays) {
            $("#numberOfDays").val(daysRange);
            remainingLeaveDays -= daysRange;
            $("#remainingLeaveDays").val(remainingLeaveDays);

        } else {
            infoAlert("Requested days exceed remaining leave days.");
        }
    } else {
        infoAlert("Please fill the form properly.");
    }
});