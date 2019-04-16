function RegisterSubmitOnClick() {
    $('.register-submit').on('click',
        function () {
            var formElements = document.forms["ajax-register-form"].elements;
            var userRegisterData = {
                UserName: formElements.UserName.value,
                Email: formElements.Email.value,
                Password: formElements.Password.value,
                PasswordConfirmation: formElements.PasswordConfirmation.value,
                FirstName: formElements.FirstName.value,
                LastName: formElements.LastName.value
            };
            $.ajax({
                type: "POST",
                url: "/Account/Register",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(userRegisterData),
                success: function(data) {
                    if (data.includes('class="login-section"')) {
                        $('.signin-section').remove();
                        $('body').append(data);
                        LogInSubmitOnClick();
                    } else {
                        $('.signin-section').remove();
                        $('body').append(data);
                        RegisterSubmitOnClick();
                    }
                },
                timeout: 5000
            });
        });
}
function LogInSubmitOnClick() {
    $('.login-submit-btn').on('click',
        function () {
            var formElements = document.forms["ajax-login-form"].elements;
            var userLogInData = {
                Login: formElements.Login.value,
                Password: formElements.Password.value
            }
            $.ajax({
                type: "POST",
                url: "/Account/LogIn",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(userLogInData),
                success: function(data) {
                    $.ajax({
                        type: "GET",
                        url: "/Account/GetUserDDLProfile",
                        success: function(data) {
                            $('.user-login-register-links').remove();
                            $('.aa-header-right').append(data);
                            $('.login-section').remove();
                            LogOutOnClick();
                        }
                    });
                },
                error: function(data) {
                    console.log("LogIn-post-fail");
                    $('.login-section').remove();
                    $('body').append(data.responseText);

                    LogInSubmitOnClick();
                },
                timeout: 5000
            });
        });
}
function LogInOnClick() {
    $('.aa-login').on('click',
        function () {
            $.ajax({
                type: "GET",
                url: "/Account/LogIn",
                success: function (data) {
                    $('.signin-section').remove();
                    $('body').append(data);

                    LogInSubmitOnClick();
                }
            });
        });
}
function RegisterOnClick() {
    $('.aa-register').on('click',
        function () {
            $.ajax({
                type: "GET",
                url: "/Account/Register",
                success: function (data) {
                    $('.signin-section').remove();
                    $('body').append(data);

                    RegisterSubmitOnClick();
                }
            });
        });
}

$(function () {
    $.ajaxSetup({
        error: function (x, status, error) {
            window.location.href = "/Error/Error500/true";
        }
    });
    RegisterOnClick();
    LogInOnClick();
});

$(document).mouseup(function (e) {
    var container = $(".aa-signin-form");

    if (!container.is(e.target)
        && container.has(e.target).length === 0) {
        $(".signin-section").remove();
        $(".login-section").remove();
    }
});



