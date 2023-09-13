
$(document).ready(function () {
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

function LoadRota() {
    debugger
    var sDate = $('#start_DateId').val();
    var eDate = $('#end_DateId').val();
    var url = '/Rota/Index?startDate=' + encodeURIComponent(sDate) + '&endDate=' + encodeURIComponent(eDate);
    window.location.href = url;
}

function popModal(date, uid, year) {
    //currentPlannedId = "plannedHr_" + plannedId;
    //crrPlandHr = document.getElementById(currentPlannedId).innerHTML,
    $('#datE').val(date);
    $('#uId').val(uid);
    $('#yeaR').val(year);
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
    let data = id;
    $.ajax({
        type: 'GET',
        url: '/Admin/GetShiftByID',
        data: { rotaShiftId: data },
        dataType: 'json',
        success: function (data) {
            if (!data.isError) {
                debugger
                var tRang = data.startTime + " - " + data.endTime;
                $('#s_TimeId').val(data.startTime);
                $('#e_TimeId').val(data.endTime);
                $('#u_TimeId').val(data.unpaidTime);
                $('#loc_Abb').val(data.locations.abbreviatedName);
                $('#t_Range').val(tRang);
                $('#f_Amt').val(data.fixedAmount);
                $('#h_Pay').val(data.hourlyPay);
                tzt = tRang + '<span class="badge bg-success">' + data.locations.abbreviatedName + '</span>';
            }
        }
    });
}

function updateRota() {
    debugger;
    var data = {};
    data.Date = $("#datE").val();
    data.UserId = $("#uId").val();
    data.Year = $("#yeaR").val();
    data.StartTime = $('#s_TimeId').val();
    data.EndTime = $('#e_TimeId').val();
    data.UnpaidTime = $('#u_TimeId').val();
    data.Location = $('#loc_Abb').val();
    data.TRange = $('#t_Range').val();
    var fAmt = $('#f_Amt').val();
    if (fAmt == "") {
        data.FixedAmount = 0;
    } else {
        data.FixedAmount = parseFloat(fAmt);
    }
    var hPay = $('#h_Pay').val();
    if (hPay == "") {
        data.HourlyPay = 0;
    } else {
        data.HourlyPay = parseFloat(hPay);
    }
    if (data.Date != "" && data.UserId != "" && data.Year != "") {
        //let time11 = subtractTime(strtTime, endTime);
        //let newPlannedhr = addHour(crrPlandHr, time11);
        document.getElementById(data.Date + "_" + data.UserId).innerHTML = tzt;
        //document.getElementById(currentPlannedId).innerHTML = newPlannedhr;
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


function errorCheck(x, y) {
    debugger
    var differenceString = subtractTime(x, y)
    if (differenceString.includes("-")){
        return false;
    }
    return true;
}

function GetRotaByDateRange() {
    debugger
    var sDate = $('#start_DateId').val();
    var eDate = $('#end_DateId').val();
    var locId = $('#locId').val();
    if (sDate != "" && eDate != "" && locId != "0") {
        $.ajax({
            type: 'GET',
            url: '/Rota/GetRotaDataByDateRange',
            data: { startDate: sDate, endDate: eDate, locId: locId },
            dataType: 'json',
            success: function (data) {
                if (!data.isError) {
                    debugger
                    $("#rotaTBLContainer").empty();
                    $("#rotaTBLContainer").append(data.rotaTableContainer);
                }
            }
        });
    } else {
        errorAlert("Fill the form correctly")
    }
}