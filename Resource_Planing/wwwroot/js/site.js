﻿function AccountsRegistraion() {
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
    if (data.Email != "" && data.Phone != "" && data.FirstName != "" && data.LastName != "" && data.Password != "" && data.Gender != "" && data.Password != "")
    {
        if (data.Password == data.ConfirmPassword) {
            var companyDetails = JSON.stringify(data);
            $.ajax({
                type: 'Post',
                url: '/Account/Registeration', // we are calling json method
                dataType: 'json',
                data:
                {
                    userRegistrationData: companyDetails,
                },
                success: function (result) {
                    if (!result.isError) {
                        var url = '/Account/Login';
                        successAlertWithRedirect(result.msg, url);
                    }
                    else {
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
    } 
    else {
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
            }
        } else {
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
                    var url = '/Admin/Shift';
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
    } else {
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
            }
        } else {
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


