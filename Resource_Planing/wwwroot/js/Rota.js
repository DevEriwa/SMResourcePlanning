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

