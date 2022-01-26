$(document).ready(function () {
    $(".btnAvailableCount").on("click", function () {
        debugger;
        $.ajax({
            url: "/api/HotelRooms",
            type: 'GET',
            success: function (response) {
                debugger;
            },
            error: function (response) {
                debugger;

            }
        });
    });
});
