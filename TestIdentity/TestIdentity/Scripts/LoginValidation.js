$.validator.setDefaults({
    submitHandler: function (form) {
        console.log("Submitted!");
        form.submit();

    }
});
$.validator.addMethod("myCustomRule", function (value, element) {
    return /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(value);
}, "Enter valid email adress");



$(document).ready(function () {
    $("#login-form").validate({
        rules: {

            Password: {
                required: true,
                minlength: 5
            },

            Email: {
                myCustomRule: true
            },



        },
        messages: {

            Password: {
                required: "Please provide a password",
                minlength: "Your password must be at least 5 characters long"
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



});