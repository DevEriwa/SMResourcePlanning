function addLocation() {

    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);
    var data = {};
    data.Name = $('#locationName').val();
    data.AbbreviatedName = $('#abbreviationlocationName').val();
    if (data.Name != "" && data.AbbreviatedName != "") {
        let locationDetails = JSON.stringify(data);
        $.ajax({
            type: 'Post',
            url: '/Admin/AddLocations',
            dataType: 'json',
            data:
            {
                locationDetails: locationDetails,
            },
            success: function (result) {
                if (!result.isError) {
                    var url = '/Admin/Location';
                    successAlertWithRedirect(result.msg, url);
                    $('#submit_btn').html(defaultBtnValue);
                }
                else {
                    $('#submit_btn').html(defaultBtnValue);
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
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please fill the form Correctly");
    }


}

function locationToBeEdited(id) {
    debugger
    $.ajax({
        type: 'Get',
        dataType: 'Json',
        url: '/Admin/EditLocation',
        data: {
            id: id
        },
        success: function (result) {
            debugger
            if (!result.isError) {
                var date = result.deteCreated.split("T")[0];
                $('#editLocationId').val(result.id);
                $('#editLocation_Name').val(result.name);
                $('#editabbreviationlocation_Name').val(result.abbreviatedName);
                $('#editDateCreate_date').val(date);
            }
            else {
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            errorAlert("Network failure, please try again");
        }
    })
}

function LocationToSave() {
    var defaultBtnValue = $('#submit_Btn').html();
    $('#submit_Btn').html("Please wait...");
    $('#submit_Btn').attr("disabled", true);
    var data = {};
    data.Id = $("#editLocationId").val();
    data.Name = $("#editLocation_Name").val();
    data.AbbreviatedName = $("#editabbreviationlocation_Name").val();
    data.DateCreated = $("#editDateCreate_date").val();
    if (data.Name != "" && data.DateCreated != "" && data.AbbreviatedName != "") {
        let editlocationdetails = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            url: '/Admin/EditedLocation',
            dataType: 'json',
            data:
            {
                locationdetails: editlocationdetails,
            },
            success: function (result) {
                if (!result.isError) {
                    var url = '/Admin/Location'
                    successAlertWithRedirect(result.msg, url)
                    $('#submit_Btn').html(defaultBtnValue);
                }
                else {
                    $('#submit_Btn').html(defaultBtnValue);
                    $('#submit_Btn').attr("disabled", false);
                    errorAlert(result.msg);
                }
            },
            error: function (ex) {
                $('#submit_Btn').html(defaultBtnValue);
                $('#submit_Btn').attr("disabled", false);
                errorAlert(result.msg);
            }
        });
    } else {
        $('#submit_Btn').html(defaultBtnValue);
        $('#submit_Btn').attr("disabled", false);
        errorAlert("Invalid, Please fill the form correctly.");
    }
}

function deleteLocation() {
    var id = $('#deleteLocationId').val();
    $.ajax({
        type: 'Post',
        dataType: 'Json',
        url: '/Admin/DeleteLocation',
        data: {
            id: id
        },
        success: function (result) {
            if (!result.isError) {
                var url = '/Admin/Location'
                successAlertWithRedirect(result.msg, url)
            }
            else {
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            errorAlert("Network failure, please try again");
        }
    })
}

function locationToBeDeleted(id) {
    $('#deleteLocationId').val(id);
}

function addDepartment() {

    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);
    var data = {};
    data.Name = $('#departmentName').val();
    if (data.Name != "") {
        let departmentDetails = JSON.stringify(data);
        $.ajax({
            type: 'Post',
            url: '/Admin/AddDepartments',
            dataType: 'json',
            data:
            {
                departmentDetails: departmentDetails,
            },
            success: function (result) {
                if (!result.isError) {
                    var url = '/Admin/Department';
                    successAlertWithRedirect(result.msg, url);
                    $('#submit_btn').html(defaultBtnValue);
                }
                else {
                    $('#submit_btn').html(defaultBtnValue);
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
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please fill the form Correctly");
    }


}

function departmentToBeEdited(id) {
    debugger
    $.ajax({
        type: 'Get',
        dataType: 'Json',
        url: '/Admin/EditDepartment',
        data: {
            id: id
        },
        success: function (result) {
            if (!result.isError) {
                var date = result.deteCreated.split("T")[0];
                $('#editDepartmentId').val(result.id);
                $('#editDepartment_Name').val(result.name);
                $('#editDate_Created').val(date);
            }
            else {
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            errorAlert("Network failure, please try again");
        }
    })
}

function DepartmentToSave() {
    var defaultBtnValue = $('#submit_Btn').html();
    $('#submit_Btn').html("Please wait...");
    $('#submit_Btn').attr("disabled", true);
    var data = {};
    data.Id = $("#editDepartmentId").val();
    data.Name = $("#editDepartment_Name").val();
    data.DateCreated = $("#editDate_Created").val();
    if (data.Name != "" && data.DateCreated != "" ) {
        let editdepartmentdetails = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            url: '/Admin/EditedDepartment',
            dataType: 'json',
            data:
            {
                departmentdetails: editdepartmentdetails,
            },
            success: function (result) {
                if (!result.isError) {
                    var url = '/Admin/Department'
                    successAlertWithRedirect(result.msg, url)
                    $('#submit_Btn').html(defaultBtnValue);
                }
                else {
                    $('#submit_Btn').html(defaultBtnValue);
                    $('#submit_Btn').attr("disabled", false);
                    errorAlert(result.msg);
                }
            },
            error: function (ex) {
                $('#submit_Btn').html(defaultBtnValue);
                $('#submit_Btn').attr("disabled", false);
                errorAlert(result.msg);
            }
        });
    } else {
        $('#submit_Btn').html(defaultBtnValue);
        $('#submit_Btn').attr("disabled", false);
        errorAlert("Invalid, Please fill the form correctly.");
    }
}

function deleteDepartment() {
    var id = $('#deleteDepartmentId').val();
    $.ajax({
        type: 'Post',
        dataType: 'Json',
        url: '/Admin/DeleteDepartment',
        data: {
            id: id
        },
        success: function (result) {
            if (!result.isError) {
                var url = '/Admin/Department'
                successAlertWithRedirect(result.msg, url)
            }
            else {
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            errorAlert("Network failure, please try again");
        }
    })
}

function departmentToBeDeleted(id) {
    $('#deleteDepartmentId').val(id);
}

$(document).ready(function () {
    debugger
    var rowCount = 1; // Initial number of rows
    // Function to generate a new row
    function generateRow(schedule) {
        var row = '<tr><td>{DATERANGE}</td>';
        row = row.replace("{DATERANGE}", schedule.dateRange)
        var rowI = '<td class="text-center">{TRANGE} <span class="badge bg-success">{LOC}</span></td>';

        var rowII = '<td class="text-center" id="{ID}" onclick="popModal({DATEID})"><span><i class="fa fa-plus-circle"></i></span></td>';
        var hhh = schedule.rotaObject;
        $.each(hhh, function (index, x) {
            var txt = "";
            if (x.shiftId != null) {
                txt = rowI.replace("{LOC}", x.shift.locations.abbreviatedName)
                txt = txt.replace("{TRANGE}", x.tRange)
            } else {
                var vvv = "'" + x.date + "','" + rowCount + "'";
                debugger
                txt = rowII.replace("{DATEID}", vvv)
                txt = txt.replace("{ID}", x.date)
            }
            row += txt;
        });
        var txtEnd = '<td id="plannedHr_{WEEKCOUNT}">{PH}</td><td>19</td><td>#8000</td></tr>'.replace("{PH}", schedule.totalPlannedHour);
        row += txtEnd.replace("{WEEKCOUNT}", rowCount);
        rowCount++;
        return row;
    }
    // Event handler for the button click
    $("#add_items").click(function () {
        debugger
        var tableBody = $("#myShiftTableBody");
        var data = {};
        data.UserId = $("#userId").val();
        data.WeekCount = rowCount;
        data.Date = $("#dateId").val();
        if (data.UserId != "" && data.WeekCount > 0) {
            let passdata = JSON.stringify(data);
            $.ajax({
                type: 'GET',
                url: '/Admin/GetWeeklyUserRota',
                data: { rotaData: passdata,},
                dataType: 'json',
                success: function (data) {

                    if (data != "") {
                        tableBody.append(generateRow(data));
                    }
                }

            });
        }
    });

});


var tzt = "";
function NavigateToRata() {
    debugger
    var data = {};
    data.UserId = $('#inRotaId').val();
    data.Datee = $('#start_DateId').val();

    var url = '/Admin/AllocateShifts?UserId=' + encodeURIComponent(data.UserId) + '&datee=' + encodeURIComponent(data.Datee);
    window.location.href = url;
}


function popModal(id, plannedId) {
    debugger
    currentPlannedId = "plannedHr_" + plannedId;
    crrPlandHr = document.getElementById(currentPlannedId).innerHTML,
    $('#datId').val(id);
    $('#allocate_Shift').modal('show');
}

var currentPlannedId = "";
var crrPlandHr = "";
var strtTime = "";
var endTime = "";

$(document).ready(function () {
    $('.clickable-cell').click(function (event) {
        debugger
        event.preventDefault(); 
        var loc = $(this).closest('tr').find('.showValue:nth-child(1)').text();
        var name = $(this).closest('tr').find('.showValue:nth-child(2)').text();
        strtTime = $(this).closest('tr').find('.showValue:nth-child(3)').text();
        endTime = $(this).closest('tr').find('.showValue:nth-child(4)').text();
        var unpaidTime = $(this).closest('tr').find('.showValue:nth-child(5)').text();
        // Set the values in the input fields
        $('#start_TimeId').val(strtTime);
        $('#end_TimeId').val(endTime);
        $('#unpaid_TimeId').val(unpaidTime);

        var rangez = strtTime + "-" + endTime;
        var badge = '<td class="text-center">{TRANGE} <span class="badge bg-success">{LOC}</span></td>';
        tzt = badge.replace("{LOC}", loc)
        tzt = tzt.replace("{TRANGE}", rangez)
    });
});

function mapShiftDetails(id) {
    $('#shfId').val(id);
}

function updateRota() {
    debugger;
    var data = {};
    data.Date = $("#datId").val();
    data.UserId = $("#uId").val();
    data.Year = $("#yearId").val();
    data.ShiftId = $("#shfId").val();
    if (data.Date != "" && data.UserId != "" && data.Year != "" && data.ShiftId != "") {
        let time11 = subtractTime(strtTime, endTime);
        let newPlannedhr = addHour(crrPlandHr, time11);
        document.getElementById(data.Date).innerHTML = tzt;
        document.getElementById(currentPlannedId).innerHTML = newPlannedhr;
        $.ajax({
            type: 'POST',
            url: '/Admin/UpdateRotaData',
            data: {
                rotaData: JSON.stringify(data),
            },
        });
    } else {
        errorAlert("Network Fail");
    }
    $('#allocate_Shift').modal('hide');   
}

function addHour(timeString1, timeString2) {
    let [hours1, minutes1] = timeString1.split(":").map(Number);
    let [hours2, minutes2] = timeString2.split(":").map(Number);

    let totalHours = hours1 + hours2;
    let totalMinutes = minutes1 + minutes2;
    if (totalMinutes >= 60) {
        totalHours += Math.floor(totalMinutes / 60);
        totalMinutes %= 60;
    }
    let sumTimeString = `${totalHours}:${totalMinutes.toString().padStart(2, "0")}`;

    return sumTimeString;  
}

function subtractTime(timeString1, timeString2) {

    let date1 = new Date();
    let date2 = new Date();

    let [hours1, minutes1] = timeString1.split(":").map(Number);
    let [hours2, minutes2] = timeString2.split(":").map(Number);

    date1.setHours(hours1);
    date1.setMinutes(minutes1);

    date2.setHours(hours2);
    date2.setMinutes(minutes2);
    let timeDifference = date2 - date1;

    let totalMinutes = Math.floor(timeDifference / 60000); 
    let hours = Math.floor(totalMinutes / 60);
    let minutes = totalMinutes % 60;
    let differenceString = `${hours}:${minutes.toString().padStart(2, "0")}`;
    return differenceString;  
}