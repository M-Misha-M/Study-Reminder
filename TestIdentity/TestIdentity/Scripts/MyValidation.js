
$.validator.setDefaults({
    submitHandler: function (form) {
        console.log("Submitted!");
        form.submit();

    }
});
$.validator.addMethod("myCustomRule", function (value, element) {
    return /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(value);
}, "Enter valid email adress");

$.validator.addMethod("dateFormat",
    function (value, element) {
        return value.match(/(^(((0[1-9]|1[0-9]|2[0-8])[-](0[1-9]|1[012]))|((29|30|31)[-](0[13578]|1[02]))|((29|30)[-](0[4,6,9]|11)))[-](19|[2-9][0-9])\d\d$)|(^29[-]02[-](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)/);
    },
    "Please enter a date in the format dd-mm-yyyy.");

$(document).ready(function () {
    $("#registerForm").validate({
        rules: {
            Name: "required",
            LastName: "required",
            Password: {
                required: true,
                minlength: 5
            },
            ConfirmPassword: {
                required: true,
                minlength: 5,
                equalTo: "#password"
            },
            Email: {
                myCustomRule: true
            },

            BirthDate: {
                dateFormat: true
            }

        },
        messages: {
            Name: "Please enter your name",
            LastName: "Please enter your lastname",
            Password: {
                required: "Please provide a password",
                minlength: "Your password must be at least 5 characters long"
            },
            ConfirmPassword: {
                required: "Please provide a password",
                minlength: "Your password must be at least 5 characters long",
                equalTo: "Please enter the same password as above"
            },


        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            // Add the `help-block` class to the error element
            error.addClass("help-block");

            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.parent("label"));
            } else {
                error.insertAfter(element);
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).parents(".form-group").addClass("has-error").removeClass("has-success");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents(".form-group").addClass("has-success").removeClass("has-error");
        }
    });

    $('#registerForm input').on('keyup blur', function () {
        if ($('#registerForm').valid()) {
            $('#submit-btn').prop('disabled', false);
        } else {
            $('#submit-btn').prop('disabled', 'disabled');
        }
    });

});