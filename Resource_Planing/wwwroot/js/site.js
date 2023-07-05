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
    if (data.Name != "" && data.AbbreviatedName != "") {
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
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please fill the form Correctly");
    }


}

function EditShift() {
    debugger;
    var defaultBtnValue = $('#submit_Btn').html();
    $('#submit_Btn').html("Please wait...");
    $('#submit_Btn').attr("disabled", true);
    var data = {};
    data.Id = $('#edit_Id').val();
    data.Name = $('#edit_shiftName').val();
    data.AbbreviatedName = $('#edit_abbreivated').val();
    if (data.Id != 0 && data.Name != "" && data.AbbreviatedName != "") {

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
        $('#submit_Btn').html(defaultBtnValue);
        $('#submit_Btn').attr("disabled", false);
        errorAlert("Please fill in the correct Details");
    }
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

                $("#edit_Id").val(data.id);
                $("#edit_shiftName").val(data.name);
                $("#edit_abbreivated").val(data.abbreviatedName);
                $("#select2-EditedlocationId-container").text(data.location.abbreviatedName);
                $("#edit_locationId").val(data.locationId);
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

$(document).ready(function () {
    debugger
    var rowCount = 1; // Initial number of rows
    // Function to generate a new row
    function generateRow(schedule) {
        var row = '<tr> <td></td>';

        var rowI = '<td class="text-center" >' +
            '<h2><a href="#"><span>{LOC}</span>{TRANGE}</a>' +
            '</h2></td >';

        var rowII = '<td class="text-center" onclick="popModal({DATEID})"><span><i class="fa fa-plus-circle"></i></span></td>';
        var hhh = schedule.rotaObject;
        $.each(hhh, function (index, x) {
            var txt = "";
            if (x.shiftId != null) {
                txt = rowI.replace("{LOC}", x.shift.Locations.AbbreviatedName)
                txt = txt.replace("{TRANGE}", x.shiftId)
            } else {
                var vvv = "'" + x.date + "'";
                debugger
                txt = rowII.replace("{DATEID}", vvv)
            }
            row += txt;
        });

        row += "<td>20</td><td>19</td><td>#8000</td></tr>";
        rowCount++;
        return row;
    }
    // Event handler for the button click
    $("#add_items").click(function () {
        var tableBody = $("#myShiftTableBody");
        var data = {};
        data.UserId = $("#userId").val();
        data.WeekCount = rowCount;
        data.Datee = $("#dateId").val();
        if (data.UserId != "" && data.WeekCount > 0) {
            let passdata = JSON.stringify(data);
            $.ajax({
                type: 'GET',
                url: '/Admin/GetWeeklyUserRota',
                data: { rotaData: passdata },
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


function NavigateToRata() {
    debugger
    var data = {};
    data.UserId = $('#inRotaId').val();
    data.Datee = $('#start_DateId').val();

    var url = '/Admin/AllocateShifts?UserId=' + encodeURIComponent(data.UserId) + '&datee=' + encodeURIComponent(data.Datee);
    window.location.href = url;
}


function popModal(id) {
    debugger
    $('#allocate_Shift').modal('show');
}





