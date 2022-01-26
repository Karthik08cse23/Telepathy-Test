$(document).ready(function () {
    $(".btnAvailableCount").on("click", function () {
        debugger;
        $.ajax({
            url: "/api/HotelRooms/GetRooms",
            type: 'GET',
            success: function (data) {
                $(".availableCount").find('p').text(data.count);
                alert(data.count);
            },
            error: function (response) {
                debugger;

            }
        });
    });

    $(".bookRoom").on("click", function () {
        debugger;
        var roomCount = $(".roomCount").val();
        $.ajax({
            url: "/api/HotelRooms/BookRoom/" + roomCount,
            type: 'PUT',
            success: function (data) {
                jQuery.each(data.bookedRooms, function (i, val) {
                    $(".bookedRoomList").append("<li> " + val + "</li>");
                });
                alert(data.bookedRooms);
                $(".btnAvailableCount").trigger("click");
            },
            error: function (response) {
                debugger;

            }
        });
    });

    $(".btnCheckout").on("click", function () {
        debugger;
        var occupiedRoom = $(".occupiedRoom").val();
        $.ajax({
            url: "/api/HotelRooms/CheckOut/" + occupiedRoom,
            type: 'PUT',
            success: function () {
                debugger;
                $(".CheckedOutRoom").text(occupiedRoom);
                $(".occupiedRoom").find("option[value="+occupiedRoom +"]").remove();
            },
            error: function (response) {
                debugger;

            }
        });
    });
});


