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
    //var CompanyLogo = document.getElementById("companyLogo").files;
    data.Name = $('#companyName').val();
    data.Email = $('#email').val();
    data.MaritalStatus = $('#companyAddress').val();
    data.Address = $('#address').val();
    data.Phone = $('#phoneNumber').val();
    data.Mobile = $('#mobileNumber').val();
    data.FirstName = $('#firstName').val();
    data.LastName = $('#lastName').val();
    data.Password = $('#password').val();
    data.Religion = $('#religion').val();
    data.Gender = $('#gender').val(); 
   // data.Age = $('#age').val(); 
    data.CheckBox = $('#termsCondition').is(":checked");
    data.ConfirmPassword = $('#confirmpassword').val();
    if (data.Phone.length == 11 || data.Phone.length == 13) {
        if (data.Mobile.length == 11 || data.Mobile.length == 13 || data.Mobile == "") {
            var companyDetails = JSON.stringify(data);
            if (data.CheckBox != "") {
                $.ajax({
                    type: 'Post',
                    url: '/Account/Registeration', // we are calling json method
                    dataType: 'json',
                    data:
                    {
                        userRegistrationData: companyDetails,
                        //base64: base64
                    },
                    success: function (result) {
                        if (!result.isError) {
                            var url = '/Account/Login';
                            successAlertWithRedirect(result.msg, url);
                            $('#submit_btn').html(defaultBtnValue);
                        }
                        else {
                            $('#submit_btn').html(defaultBtnValue);
                            $('#submit_btn').attr("disabled", false);
                            errorAlert(result.msg);
                        }
                    },
                    //error: function (ex) {
                    //    $('#submit_btn').html(defaultBtnValue);
                    //    $('#submit_btn').attr("disabled", false);
                    //    errorAlert("Network failure, please try again");
                    //}
                });
            //} else {
            //    $('#submit_btn').html(defaultBtnValue);
            //    $('#submit_btn').attr("disabled", false);
            //    errorAlert("I have read and agree to the Terms and Conditions to Continue Thanks!.");
            }
        }
        //else {
        //    $('#submit_btn').html(defaultBtnValue);
        //    $('#submit_btn').attr("disabled", false);
        //    errorAlert("Mobile Number must be equal to 11 or 13 digit");
        //}
    }
    //else {
    //    $('#submit_btn').html(defaultBtnValue);
    //    $('#submit_btn').attr("disabled", false);
    //    errorAlert("Phone Number must be equal to 11 or 13 digit");
    //}
       
   
    
}

// APPLICATION REQUEST 
//function MainRegistrationScript(RefID) {
//    debugger;
//    var data = {};
//    data.Name = $('#name').val();
//    data.Email = $('#email').val();
//    data.Password = $('#password').val();
//    data.ConfirmPassword = $('#password_confirm').val();
//    data.RefID = RefID;

//    let userViewModel = JSON.stringify(data);
//    debugger;
//    if (userViewModel != "") {
//        $.ajax({
//            type: 'Post',
//            dataType: 'json',
//            url: '/Accounts/Registeration',
//            data:
//            {
//                userRegistrationData: userViewModel,
//            },
//            success: function (result) {
//                debugger;
//                if (!result.isError) {
//                    var url = '/Accounts/Login';
//                    successAlertWithRedirect(result.msg, url)
//                }
//                else {
//                    errorAlert(result.msg)
//                }
//            },
//            error: function (ex) {
//                errorAlert("Error occured try again");
//            }
//        });
//    }
//    else {
//        errorAlert("Incorrect Details");
//    }

//}

function CreateAdminAccount() {
    debugger
    var data = {};
    data.Name = $('#name').val();
    data.Email = $('#email').val();
    data.Password = $('#password').val();
    data.ConfirmPassword = $('#password_confirm').val();
    debugger
    if (data.Name != "" && data.Email != "" && data.Password != "" && data.Password == data.ConfirmPassword) {
        let userViewModel = JSON.stringify(data);
        debugger;
        if (userViewModel != "") {
            $.ajax({
                type: 'Post',
                dataType: 'json',
                url: '/Accounts/AdminRegisteration',
                data:
                {
                    adminRegistrationData: userViewModel,
                },
                success: function (result) {
                    debugger;
                    if (!result.isError) {
                        var url = '/Accounts/Login';
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
function loginPost() {
    debugger;
    var data = {};
    data.Email = $("#email").val();
    data.Password = $("#password").val();
    if (data.Email != "" && data.Password != "") {
        let loginViewModel = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Accounts/Login',
            data:
            {
                loginData: loginViewModel
            },
            success: function (result) {
                debugger;
                if (result.isNotVerified) {
                    errorAlert(result.msg)
                }
                else if (result.isDeactivated) {
                    errorAlert(result.msg)
                }
                else if (!result.isError) {
                    successAlertWithRedirect(result.msg, result.dashboard)
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
        if (data.Email == "") {
            document.querySelector("#emailValidation").style.display = "block"
        } else {
            document.querySelector("#emailValidation").style.display = "none"
        }
        if (data.Password == "") {
            document.querySelector("#passwordValidation").style.display = "block"
        } else {
            document.querySelector("#passwordValidation").style.display = "none"
        }
    }
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