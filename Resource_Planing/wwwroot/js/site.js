$(window).on('load', function () {
    $('#loader').delay(100).fadeOut('slow');
    $('#loader-wrapper').delay(500).fadeOut('slow');
});
function AccountsRegistraion() {
    debugger
    var name = document.getElementById("name").value;
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var cpassword = document.getElementById("password_confirm").value;
    var policy = document.querySelector("#checkbox3");
    var terms = document.querySelector("#checkbox2");
    var PVPCheck = document.querySelector("#checkbox3Validation");
    var termsCheck = document.querySelector("#checkbox2Validation");

    debugger
    if (name != "" && email != "" && password != "" && cpassword != "" && password == cpassword && policy.checked == true && terms.checked == true) {
        debugger;
        MainRegistrationScript(RefID);
    }
    else {

        if (name == "") {
            document.getElementById("nameValidation").style.display = "block";
        } else {
            document.getElementById("nameValidation").style.display = "none";
        }
        if (email == "") {
            document.getElementById("emailValidation").style.display = "block";
        } else {
            document.getElementById("emailValidation").style.display = "none";
        }
        if (password == "") {
            document.getElementById("passwordValidation").style.display = "block";
        } else {
            document.getElementById("passwordValidation").style.display = "none";
            if (password != cpassword) {
                document.getElementById("confirm_passwordValidation").style.display = "block";
            } else {
                document.getElementById("confirm_passwordValidation").style.display = "none";
            }
        }
        if (policy.checked == false) {
            PVPCheck.style.display = "block";
        } else {
            PVPCheck.style.display = "none";
        }
        if (terms.checked == false) {
            termsCheck.style.display = "block";
        } else {
            termsCheck.style.display = "none";
        }
    }

}
function Registeration() {
    debugger;
    var data = {};
    const capturedImage = profilePix; // Use the global variable directly
    data.Email = $('#email').val();
    data.Address = $('#address').val();
    data.Phone = $('#phoneNumber').val();
    data.FirstName = $('#firstName').val();
    data.LastName = $('#lastName').val();
    data.Password = $('#password').val();
    data.Religion = $('#religion').val();
    data.GenderId = $('#genderId').val();
    data.DisplayOnRota = $('#displayOnRota').is(":checked");
    data.CheckBox = $('#termsCondition').is(":checked");
    data.ConfirmPassword = $('#confirmpassword').val();
    data.FaceImageData = profilePix;
    if (data.Email != "" && data.Phone != "" && data.FirstName != "" && data.LastName != "" && data.Password != "" && data.Gender != "" && capturedImage != "" && data.FaceImageData != "") {
        if (data.Password == data.ConfirmPassword) {
            var companyDetails = JSON.stringify(data);
            $.ajax({
                type: 'Post',
                url: '/Account/Registeration', // we are calling json method
                dataType: 'json',
                data: {
                    userRegistrationData: companyDetails,
                },
                success: function (result) {
                    debugger
                    if (!result.isError) {
                        const userId = result.staffId;
                        saveImageOnServer(capturedImage, userId);
                        var url = '/Account/Login';
                        successAlertWithRedirect(result.msg, url);
                    } else {
                        errorAlert(result.msg);
                    }
                },
                error: function (ex) {
                    errorAlert("Network failure, please try again");
                }
            });
        } else {
            errorAlert("Password and comfirmpassword not match");
        }
    } else {
        errorAlert("Fill the form correctly");
    }
}

function Registerationsd() {
    debugger;
    var data = {};
    data.Email = $('#email').val();
    data.Address = $('#address').val();
    data.Phone = $('#phoneNumber').val();
    data.FirstName = $('#firstName').val();
    data.LastName = $('#lastName').val();
    data.Password = $('#password').val();
    data.Religion = $('#religion').val();
    data.GenderId = $('#genderId').val();
    data.DisplayOnRota = $('#displayOnRota').is(":checked");
    data.CheckBox = $('#termsCondition').is(":checked");
    data.ConfirmPassword = $('#confirmpassword').val();

    // Update the assignment of profilePix using the actual captured image data
    data.ProfilePix = captureUserImage();

    if (data.Email != "" && data.Phone != "" && data.FirstName != "" && data.LastName != "" && data.Password != "" && data.Gender != "" && data.ProfilePix != "") {
        if (data.Password == data.ConfirmPassword) {
            var companyDetails = JSON.stringify(data);
            $.ajax({
                type: 'POST',
                url: '/Account/Register',
                data: registrationData,
                success: function (registrationResult) {
                    if (!registrationResult.isError) {
                        // Registration successful, obtain staffId
                        const staffId = registrationResult.staffId;

                        // Save the captured image on the server
                        saveImageOnServer(capturedImage, staffId);
                    } else {
                        errorAlert(result.msg);
                    }
                },
                error: function (ex) {
                    errorAlert("Network failure, please try again");
                }
            });
        } else {
            errorAlert("Password and confirm password do not match");
        }
    } else {
        errorAlert("Fill the form correctly");
    }
}
function CreateAdminAccount() {
    debugger
    var data = {};
    data.Name = $('#names').val();
    data.Email = $('#emails').val();
    data.Password = $('#passwords').val();
    data.ConfirmPassword = $('#confirmpasswords').val();
    debugger
    if (data.Name != "" && data.Email != "" && data.Password != "" && data.Password == data.ConfirmPassword) {
        let userViewModel = JSON.stringify(data);
        debugger;
        if (userViewModel != "") {
            $.ajax({
                type: 'Post',
                dataType: 'json',
                url: '/Account/AdminRegisteration',
                data:
                {
                    adminRegistrationData: userViewModel,
                },
                success: function (result) {
                    debugger;
                    if (!result.isError) {
                        var url = '/Account/Login';
                        successAlertWithRedirect(result.msg, url)
                    }
                    else {
                        errorAlert(result.msg)
                    }
                },
                error: function (ex) {
                    errorAlert("Error occured try again");
                }
            });
        }
        else {
            errorAlert("Incorrect Details");
        }

    }
    else {
        if (data.Name == "") {
            document.getElementById("nameValidation").style.display = "block";
        }
        else {
            document.getElementById("nameValidation").style.display = "none";
        }
        if (data.Email == "") {
            document.getElementById("emailValidation").style.display = "block";
        } else {
            document.getElementById("emailValidation").style.display = "none";
        }
        if (data.Password == "") {
            document.getElementById("passwordValidation").style.display = "block";
        }
        else {
            document.getElementById("passwordValidation").style.display = "none";
            if (data.Password != data.ConfirmPassword) {
                document.getElementById("confirm_passwordValidation").style.display = "block";
            }
            else {
                document.getElementById("confirm_passwordValidation").style.display = "none";
            }
        }
    }

}
// LOGIN POST ACTION
function login() {
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);
    var data = {};
    data.Email = $('#email').val();
    data.Password = $('#password').val();
    let details = JSON.stringify(data);
    $.ajax({
        type: 'Post',
        url: '/Account/Login',
        dataType: 'json',
        data:
        {
            loginData: details 
        },
        success: function (result) {
            if (!result.isError) {
                var n = 1;
                localStorage.clear();
                localStorage.setItem("on_load_counter", n);
                location.href = result.dashboard;

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
}

let timeDiff = "";
function addShift() {
    debugger;
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);
    var data = {};
    data.Name = $('#shiftName').val();
    data.AbbreviatedName = $('#abbreviationlocationName').val();
    data.LocationId = $('#locationId').val();
    data.FixedAmount = $('#fixedAmountId').val();
    data.HourlyPay = $('#hourlyPay').val();
    data.UnpaidTime = $('#unpaid_TimeId').val();
    data.EndTime = $('#end_TimeId').val();
    data.StartTime = $('#start_TimeId').val();
    data.IsFixed = $('#fixedId').is(":checked");
    timeDiff = errorCheck(data.StartTime, data.EndTime);
    if (data.Name != "" && data.AbbreviatedName != "" && data.LocationId != "0" && data.StartTime != "" && data.EndTime != "" && timeDiff) {
        if (data.IsFixed) {
            if (data.FixedAmount == "") {
                shiftValidtion('');
                return;
            } else {
                $('#hourlyPay').val("");
            }
        } else {
            if (data.HourlyPay == "") {
                shiftValidtion('');
                return;
            }
            $('#fixedAmountId').val("");
            data.FixedAmount = "";
        }
        shiftValidtion('');
        let shiftDetails = JSON.stringify(data);
        $.ajax({
            type: 'Post',
            url: '/Admin/AddShift',
            dataType: 'json',
            data:
            {
                shiftDetails: shiftDetails,
            },
            success: function (result) {
                if (!result.isError) {
                    debugger;
                    var url = window.location.pathname.toLowerCase(); // Ensure lowercase comparison
                    if (url.includes("admin/shift")) { // Check if the URL contains "admin/shift"
                        successAlertWithRedirect(result.msg, url);

                        // Reload the page only when the URL contains "admin/shift"
                        window.location.reload();
                    } else {
                        var shiftTbl = $('#shiftTblBody');
                        var newRow = "<tr onclick=\"mapShiftDetails('" + result.data.id + "')\">" +
                            "<td class=\"showValue py-0\"> <a href=\"#\">" + result.data.locations.abbreviatedName + "</a></td>" +
                            "<td class=\"showValue py-0\"> <a href=\"#\">" + result.data.name + "</a></td>" +
                            "<td class=\"showValue py-0\"> <a href=\"#\">" + result.data.startTime + "</a></td>" +
                            "<td class=\"showValue py-0\"> <a href=\"#\">" + result.data.endTime + "</a></td>" +
                            "<td class=\"showValue py-0\"> <a href=\"#\">" + result.data.unpaidTime + "</a></td></tr>";
                        shiftTbl.append(newRow);
                        $('#add_Shift').modal('hide');
                    }
                    $('#submit_btn').html(defaultBtnValue);
                    $('#submit_btn').attr("disabled", false);
                } else {
                    // Handle error case
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
        shiftValidtion('');
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
    }
}

function shiftValidtion(i) {
    if ($('#shiftName' + i).val() == "") {
        $('#shiftName' + i).css('border', 'solid 2px red');
    } else {
        $('#shiftName' + i).css('border', 'solid 1px #ccc');
    }
    if ($('#abbreviationlocationName' + i).val() == "") {
        $('#abbreviationlocationName' + i).css('border', 'solid 2px red');
    } else {
        $('#abbreviationlocationName' + i).css('border', 'solid 1px #ccc');
    }
    if ($('#locationId' + i).val() == "0") {
        $('#locationId' + i).css('border', 'solid 2px red');
    } else {
        $('#locationId' + i).css('border', 'solid 1px #ccc');
    }
    if ($('#end_TimeId' + i).val() == "") {
        $('#end_TimeId' + i).css('border', 'solid 2px red');
    } else {
        $('#end_TimeId' + i).css('border', 'solid 1px #ccc');
    }
    if ($('#start_TimeId' + i).val() == "") {
        $('#start_TimeId' + i).css('border', 'solid 2px red');
    } else {
        $('#start_TimeId' + i).css('border', 'solid 1px #ccc');
    }
    if (($('#fixedId' + i).is(":checked") && $('#fixedAmountId' + i).val() == "")) {
        $('#fixedAmountId' + i).css('border', 'solid 2px red');
        $('#hourlyPay' + i).css('border', 'solid 1px #ccc');
    } else {
        if ($('#hourlyPay' + i).val() == "") {
            $('#hourlyPay' + i).css('border', 'solid 2px red');
        } else {
            $('#hourlyPay' + i).css('border', 'solid 1px #ccc');
        }
        $('#fixedAmountId' + i).css('border', 'solid 1px #ccc');
    }
    if ($('#start_TimeId' + i).val() == "") {
        $('#start_TimeId' + i).css('border', 'solid 2px red');
    } else {
        $('#start_TimeId' + i).css('border', 'solid 1px #ccc');
    }
    if (!timeDiff) {
        errorAlert("Starttime cannot be greater than end time");
    }
}

function EditShift() {
    debugger;
    var defaultBtnValue = $('#submit_Btn').html();
    $('#submit_Btn').html("Please wait...");
    $('#submit_Btn').attr("disabled", true);
    var data = {};
    data.Id = $('#editshiftId').val();
    data.Name = $('#shiftName_edit').val();
    data.AbbreviatedName = $('#abbreviatedName_edit').val();
    data.LocationId = $('#locationId_edit').val();
    data.FixedAmount = $('#fixedAmountId_edit').val();
    data.HourlyPay = $('#hourlyPay_edit').val();
    data.UnpaidTime = $('#unpaid_TimeId_edit').val();
    data.EndTime = $('#end_TimeId_edit').val();
    data.StartTime = $('#start_TimeId_edit').val();
    data.IsFixed = $('#fixedId_edit').is(":checked");
    timeDiff = errorCheck(data.StartTime, data.EndTime);
    if (data.Id != 0 && data.Name != "" && data.AbbreviatedName != "" && data.LocationId != "0" && data.StartTime != "" && data.EndTime != "" && timeDiff) {
        if (data.IsFixed) {
            if (data.FixedAmount == "") {
                shiftValidtion('_edit');
                return;
            } else {
                $('#hourlyPay_edit').val("");
            }
        } else {
            if (data.HourlyPay == "") {
                shiftValidtion('_edit');
                return;
            }
            $('#fixedAmountId_edit').val("");
            data.FixedAmount = "";
        }
        shiftValidtion('_edit');
        let shiftViewModel = JSON.stringify(data);
        if (shiftViewModel != "") {
            $.ajax({
                type: 'Post',
                dataType: 'json',
                url: '/Admin/EditShift',
                data:
                {
                    shiftDetails: shiftViewModel,
                },
                success: function (result) {
                    if (!result.isError) {
                        var url = '/Admin/Shift';
                        successAlertWithRedirect(result.msg, url)
                        $('#submit_Btn').html(defaultBtnValue);
                    }
                    else {
                        $('#submit_Btn').html(defaultBtnValue);
                        $('#submit_Btn').attr("disabled", false);
                        errorAlert(result.msg)
                    }
                },
                error: function (ex) {
                    $('#submit_Btn').html(defaultBtnValue);
                    $('#submit_Btn').attr("disabled", false);
                    errorAlert("Network failure, please try again");
                }
            });
        }
    }
    else {

        shiftValidtion('_edit');
        $('#submit_Btn').html(defaultBtnValue);
        $('#submit_Btn').attr("disabled", false);
        errorAlert("Please fill in the correct Details");
    }
}

function deleteShift() {
    debugger
    var id = $('#deleteShiftId').val();
    $.ajax({
        type: 'Post',
        dataType: 'Json',
        url: '/Admin/DeleteShift',
        data: {
            id: id
        },
        success: function (result) {
            if (!result.isError) {
                var url = '/Admin/Shift'
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

function GetShiftByID(Id) {
    debugger;
    let data = Id;
    $.ajax({
        type: 'GET',
        url: '/Admin/GetShiftByID',
        data: { rotaShiftId: data },
        dataType: 'json',
        success: function (data) {
            if (!data.isError) {
                $("#editshiftId").val(data.id);
                $("#deleteShiftId").val(data.id);
                $("#shiftName_edit").val(data.name);
                $("#abbreviatedName_edit").val(data.abbreviatedName);
                $("#locationId_edit").val(data.locationId);
                $('#fixedAmountId_edit').val(data.fixedAmount);
                $('#unpaid_TimeId_edit').val(data.unpaidTime);
                $('#end_TimeId_edit').val(data.endTime);
                $('#start_TimeId_edit').val(data.startTime);
                if (data.isFixed) {
                    $('#fixedId_edit').prop('checked', true);
                } else {
                    $('#fixedId_edit').prop('checked', false);
                }
                $('#fixedId_edit').val(data.isFixed);
                $("#select2-EditedlocationId-container").text(data.location.abbreviatedName);
            }
        }
    });
}

// CHANGE PASSWORD POST ACTION
function ChangePasswordPost() {
    debugger;
    var data = {};
    data.OldPassword = $("#OldPasswrd").val();
    data.NewPassword = $("#NewPasswrd").val();
    data.ConfirmPassword = $("#Cpasswrd").val();
    if (data.OldPassword != "" && data.NewPassword != "" && data.NewPassword == data.ConfirmPassword) {
        let changePasswordDetails = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Accounts/ChangePasswordPost',
            data:
            {
                userPasswordDetails: changePasswordDetails
            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    successAlertWithRedirect(result.msg, result.url)
                }
                else {
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                errorAlert(ex);
            }
        });
    }
    else {
        if (data.OldPassword == "") {
            document.getElementById("oldPassVDT").style.display = "block";
        } else {
            document.getElementById("oldPassVDT").style.display = "none";
        }
        if (data.NewPassword == "") {
            document.getElementById("newPassVDT").style.display = "block";
        } else {
            document.getElementById("newPassVDT").style.display = "none";
            if (data.Password != data.ConfirmPassword) {
                document.getElementById("cnewPassVDT").style.display = "block";
            } else {
                document.getElementById("cnewPassVDT").style.display = "none";
            }
        }
    }

}

// FORGOT PASSWORD POST ACTION
function GetPasswordRestLink() {
    debugger;
    var email = $("#email").val();
    if (email != "") {
        let emailAccount = email;
        $.ajax({
            type: 'POST',
            url: '/Accounts/GetPasswordResetLinkWithEmail',
            dataType: 'json',
            data:
            {
                email: emailAccount,
            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    var url = '/Accounts/Login';
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    errorAlert(result.msg);
                }
            },
            Error: function (ex) {
                errorAlert(ex);
            }
        });
    } else {
        if (email == "") {
            document.querySelector("#emailVDT").style.display = "block"
        } else {
            document.querySelector("#emailVDT").style.display = "none"
        }
    }

}

function ResetPassword(token) {
    debugger;
    var data = {};
    data.Password = $("#password").val();
    data.Token = token;
    var confirmPassword = $("#cpassword").val();
    if (data.Password != "" && data.Password == confirmPassword && data.Token != "") {
        let passdata = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            url: '/Accounts/ResetUserPassword',
            dataType: 'json',
            data:
            {
                retetData: passdata,
            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    var url = '/Accounts/Login';
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    errorAlert(result.msg);
                }
            },
            Error: function (ex) {
                errorAlert(ex);
            }
        });
    } else {
        if (data.Password == "") {
            document.querySelector("#passwordVDT").style.display = "block"
        } else {
            document.querySelector("#passwordVDT").style.display = "none"
        }
        if (confirmPassword != data.Password) {
            document.querySelector("#cpasswordVDT").style.display = "block"
        } else {
            document.querySelector("#cpasswordVDT").style.display = "none"
        }
        if (data.Token == "") {
            errorAlert("Link Expired");
        }
    }

}


function addLocation() {
    debugger
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);
    var data = {};
    data.Name = $('#locationName').val();
    data.AbbreviatedName = $('#abbreviationlocationName').val();
    data.ListOfUserId = $('#userIds').val();
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

function GetlocationToBeEdited(id) {
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
                if (result.userIds != null) {
                    var userArray = JSON.parse(result.userIds)
                    if (userArray.length > 0) {
                        $('#editUserIds').val(userArray);
                        $('#editUserIds').trigger('change');
                    }
                } else {
                    $('#editUserIds').val(null).trigger('change');
                }
                $('#edit_location').modal('show');
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
    data.ListOfUserId = $('#editUserIds').val();
    if (data.Name != "" && data.ListOfUserId != "" && data.AbbreviatedName != "") {
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
    if (data.Name != "" && data.DateCreated != "") {
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
function createShiftLocation() {
    debugger;
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);
    var data = {};
    data.PostalCode = $('#postalCode').val();
    data.Address = $('#adrress').val();
    data.Name = $('#sl_Name').val();
    data.ShiftId = $('#shiftId').val();
    data.StateId = $('#stateId').val();

    if (data.PostalCode != "" && data.Address != "" && data.Name != "" && data.ShiftId != "0" && data.StateId != "0") {
        let ShiftViewModel = JSON.stringify(data);
        $.ajax({
            type: 'Post',
            dataType: 'json',
            url: '/ClockIn/CreateShiftLocation',
            data:
            {
                shiftDetails: ShiftViewModel,
            },
            success: function (result) {
                if (!result.isError) {
                    var url = '/ClockIn/Index';
                    successAlertWithRedirect(result.msg, url)
                    $('#submit_btn').html(defaultBtnValue);
                }
                else {
                    $('#submit_btn').html(defaultBtnValue);
                    $('#submit_btn').attr("disabled", false);
                    errorAlert(result.msg)
                }
            },
            error: function (ex) {
                $('#submit_btn').html(defaultBtnValue);
                $('#submit_btn').attr("disabled", false);
                errorAlert("Network failure, please try again");
            }
        });

    }
    else {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("please fill the form correctly");
    }

}

function EditShiftLocation() {
    debugger
    var defaultBtnValue = $('#submit_Btn').html();
    $('#submit_Btn').html("Please wait...");
    $('#submit_Btn').attr("disabled", true);
    var data = {};
    data.PostalCode = $('#edit_postalCode').val();
    data.Address = $('#edit_adrress').val();
    data.Name = $('#edit_sl_Name').val();
    data.ShiftId = $('#edit_shiftId').val();
    data.StateId = $('#edit_stateId').val();
    if (data.PostalCode != "" && data.Address != "" && data.Name != "" && data.ShiftId != "0" && data.StateId != "0") {
        let ShiftViewModel = JSON.stringify(data);
        $.ajax({
            type: 'Post',
            dataType: 'json',
            url: '/ClockIn/EditShiftLocation',
            data:
            {
                shiftDetails: ShiftViewModel,
            },
            success: function (result) {
                if (!result.isError) {
                    newSuccessAlert(result.msg, result.url)
                    $('#submit_Btn').html(defaultBtnValue);
                }
                else {
                    $('#submit_Btn').html(defaultBtnValue);
                    $('#submit_Btn').attr("disabled", false);
                    errorAlert(result.msg)
                }
            },
            error: function (ex) {
                $('#submit_Btn').html(defaultBtnValue);
                $('#submit_Btn').attr("disabled", false);
                errorAlert("Network failure, please try again");
            }
        });
    }
    else {
        $('#submit_Btn').html(defaultBtnValue);
        $('#submit_Btn').attr("disabled", false);
        errorAlert("please fill the form correctly");
    }
}

function DeleteShiftLocation() {
    var data = {};
    data.Id = $('#delete_Id').val();
    if (data.Id != null) {
        let ShiftViewModel = JSON.stringify(data);
        if (ShiftViewModel != "") {
            $.ajax({
                type: 'Post',
                dataType: 'json',
                url: '/ClockIn/DeleteShiftLocation',
                data:
                {
                    shiftDetails: ShiftViewModel,
                },
                success: function (result) {
                    if (!result.isError) {
                        var url = '/ClockIn/Index';
                        successAlertWithRedirect(result.msg, url)
                    }
                    else {
                        errorAlert(result.msg)
                    }
                },
                error: function (ex) {
                    errorAlert("Network failure, please try again");
                }
            });
        }
        else {
            errorAlert("Please fill the form correctly");
        }
    }
}

function GetShiftLocationById(id) {
    debugger
    $.ajax({
        type: 'GET',
        url: '/ClockIn/GetShiftLocationById', 
        dataType: 'json',
        data:
        {
            shiftLocationID: id
        },
        success: function (data) {
            debugger
            if (!data.isError) {
                $("#editId").val(data.data.id);
                $("#deleteId").val(data.data.id);
                $("#edit_stateId").val(data.data.state.id);
                $("#edit_shiftId").val(data.data.shift.id);
                $("#edit_sl_Name").val(data.data.name);
                $("#edit_adrress").val(data.data.address);
                $("#edit_postalCode").val(data.data.postalCode);
            }
        },
        error: function (ex) {
            "please fill the form correctly" + errorAlert(ex);
        }
    });
}
function addShiftLocation(id) {
    debugger;
    $.ajax({
        type: 'GET',
        url: '/ClockIn/AddShiftLocation/' + id, 
        data: { shiftId: id },
        success: function (data) {
            $("#myModalBody").html(data);
            $("#myModal").modal(); 
        },
        error: function (ex) {
            "No Data Found" + errorAlert(ex);
        }
    });
}
function GetLocations() {
    if (navigator.geolocation) {
        debugger
        navigator.geolocation.getCurrentPosition(function (showPosition) {
            var latitude = showPosition.coords.latitude;
            var longitude = showPosition.coords.longitude;
            $("#lat").val(latitude).prop("readonly", true);
            $("#long").val(longitude).prop("readonly", true);
        });
    }
}

function collectInputData() {
    var latitude = document.getElementById("lat").value;
    var longitude = document.getElementById("long").value;
    var acceptedRadius = document.getElementById("Autocomp").value;
    var locationId = "@Model.Id";
    var data = {
        Latitude: latitude,
        Longitude: longitude,
        AcceptedRadius: acceptedRadius,
        LocationId: locationId
    };
    return data;
}

function saveLocation() {
    var data = collectInputData();
    $.ajax({
        type: "POST",
        url: "/ClockIn/AddLocationShift", 
        data: data,
        success: function (response) {
            alert("Location updated successfully!");
        },
        error: function (error) {
            alert("An error occurred while updating the location.");
        }
    });
}

function locationToBeEdited(locationId) {
    debugger
    document.getElementById("editLocationId").value = locationId;
    $('#edit_Shift_location').modal('show');
}


function updateLocation() {
    debugger
    var locationId = document.getElementById("editLocationId").value;
    var latitude = document.getElementById("lat").value;
    var longitude = document.getElementById("long").value;
    var acceptedRadius = document.getElementById("Autocomp").value;

    var data = {
        locationId: locationId,
        latitude: latitude,
        longitude: longitude,
        acceptedRadius: acceptedRadius
    };

    $.ajax({
        url: "/Admin/UpdateLocation", 
        method: 'POST',
        data: data,
       success: function (data) {
            if (data.isError === false) { 
                $('#updateModal').modal('hide');
                successAlertWithRedirect(data.msg, data.url);
            } else {
                errorAlert(data.msg);
            }
       },
        error: function (ex) {
            errorAlert("Network failure, please try again");
        }
    });
}

// Function to send the image to the server
function saveImageOnServer(imageDataUrl, userId) {
    $.ajax({
        type: 'POST',
        url: '/Account/SaveImage',
        data: {
            imageData: imageDataUrl,
            userId: userId
        },
        success: function (result) {
            if (result.isError) {
                console.error('Error saving image on the server:', result.error);
            } else {
                console.log('Image saved on the server:', result.imagePath);
            }
        },
        error: function (ex) {
            console.error('Error saving image on the server:', ex);
        }
    });
}


var profilePix = "";


function captureUserImage() {
    const video = document.getElementById('camera');
    const capturedImage = document.getElementById('captured_image');
    let stream;

    // Get user's media (camera) when the page loads
    navigator.mediaDevices.getUserMedia({ video: true })
        .then(function (mediaStream) {
            stream = mediaStream;
            video.srcObject = mediaStream;
        })
        .catch(function (error) {
            console.error('Error accessing the camera:', error);
        });

    // Capture an image from the camera
    document.getElementById('capture-button').addEventListener('click', function () {
        debugger
        const canvas = document.createElement('canvas');
        canvas.width = video.videoWidth;
        canvas.height = video.videoHeight;
        const context = canvas.getContext('2d');
        context.drawImage(video, 0, 0, canvas.width, canvas.height);

        // Convert the captured image to a data URL
        const imageDataUrl = canvas.toDataURL('image/png');

        // Display the captured image
        capturedImage.src = imageDataUrl;
        capturedImage.style.display = 'block';
        profilePix = imageDataUrl;
    });

    // When the user closes the page, stop the camera stream
    window.addEventListener('beforeunload', function () {
        if (stream) {
            stream.getTracks().forEach(track => track.stop());
        }
    });


    function getProfilePicture(userId) {
        $.ajax({
            type: 'GET',
            url: '/Account/GetProfilePicture', 
            data: { userId: userId },
            success: function (result) {
                if (!result.isError) {
                    const profilePictureUrl = result.imageDataUrl;
                    $('#profilePicture').attr('src', profilePictureUrl);
                } else {
                    console.error('Error retrieving profile picture:', result.msg);
                }
            },
            error: function (ex) {
                console.error('Error retrieving profile picture:', ex);
            }
        });
    }

}