$(document).ready(function () {
    $("#dateForm").submit(function (e) {
        var studyDate = $("#StudyDate").val();
        if (studyDate == "") {
            e.preventDefault();
            $("#formAlert").removeClass("hide");
            $("#formAlert").slideDown(400);
        }

        $(function () {
            $("[data-hide]").on("click", function () {
                $(this).closest("." + $(this).attr("data-hide")).hide();
            });
        });
    });
    $("#success-alert").fadeTo(4000, 500).slideUp(500, function () {
        $("#success-alert").slideUp(500);
    });
});