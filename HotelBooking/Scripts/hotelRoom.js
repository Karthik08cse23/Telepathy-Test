$(document).ready(function () {
    $(".btnAvailableCount").hide();
    $(".btnAvailableCount").on("click", function () {
        debugger;
        $.ajax({
            url: "/api/HotelRooms/GetRoomsCount",
            type: 'GET',
            success: function (count) {
                $(".availableCount").find('span').text(count);
            },
            error: function (response) {
                if (response.status == "417") {
                    alert("Error while running your input: Kindly contact Admin");
                }

            }
        });
    });

    $(".bookRoom").on("click", function () {
        debugger;
        var roomCount = $(".roomCount").val();
        $.ajax({
            url: "/api/HotelRooms/BookRoom/" + roomCount,
            type: 'PUT',
            success: function (bookedRooms) {
                $(".bookedRoomList").empty();
                jQuery.each(bookedRooms, function (i, val) {
                    $(".bookedRoomList").append("<li> " + val + "</li>");
                });
                $(".btnAvailableCount").trigger("click");
            },
            error: function (response) {
                if (response.status == "417") {
                    alert("Error while running your input: Kindly contact Admin");
                }

            }
        });
    });

    $(".btnCheckout").on("click", function () {
        var occupiedRoom = $(".occupiedRoom").val();
        $.ajax({
            url: "/api/HotelRooms/CheckOut/" + occupiedRoom,
            type: 'PUT',
            success: function () {
                $(".CheckedOutRoom").text(occupiedRoom);
                $(".occupiedRoom").find("option[value="+occupiedRoom +"]").remove();
            },
            error: function (response) {
                if (response.status == "417") {
                    alert("Error while running your input: Kindly contact Admin");
                }
            }
        });
    });

    $(".vacantRepairedRooms").on("change", function () {
        if ($(this).find(':selected').attr("data-html") == "Vaccant") {
            $(".statusChange").find("option[value='3']").remove();
            $(".statusChange").find("option[value='1']").remove();
            $(".statusChange").find("option[value='4']").remove();
            $(".statusChange").append("<option value='1'>Available</option>");
            $(".statusChange").append("<option value='4'>Repaired</option>");
        }
        else if ($(this).find(':selected').attr("data-html") == "Repair") {
            $(".statusChange").find("option[value='3']").remove();
            $(".statusChange").find("option[value='1']").remove();
            $(".statusChange").find("option[value='4']").remove();
            $(".statusChange").append("<option value='3'>Vaccant</option>");
        }
        else {
            $(".statusChange").find("option[value='1']").remove();
            $(".statusChange").find("option[value='4']").remove();
            $(".statusChange").find("option[value='3']").remove();
        }
    });

    $(".btnVacant").on("click", function () {
        var selectedRoom = $(".vacantRepairedRooms").val();
        var selectedStatus = $(".statusChange").val();
        var model = { roomNo: selectedRoom, Status: selectedStatus };
        $.ajax({
            url: "/api/HotelRooms/HouseKeeping/",
            type: 'POST',
            data:  model ,
            success: function () {
                if (selectedStatus == "1") {
                    $(".vacantRepairedRooms").find("option[value=" + selectedRoom + "]").remove();
                    $(".statusChange").find("option[value='1']").remove();
                    $(".statusChange").find("option[value='4']").remove();
                    $(".statusChange").find("option[value='3']").remove();
                    $(".submittedLabel").text("Room " + selectedRoom + " now available to book!");
                }
                else if (selectedStatus == "4") {
                    $(".statusChange").find("option[value='1']").remove();
                    $(".statusChange").find("option[value='4']").remove();
                    $(".statusChange").find("option[value='3']").remove();
                    $(".vacantRepairedRooms").find("option[value=" + selectedRoom + "]").attr("data-html", "Repair");
                    $(".vacantRepairedRooms").val(0);
                    $(".submittedLabel").text("Room " + selectedRoom + " under Repair!");
                }
                else {
                    $(".statusChange").find("option[value='1']").remove();
                    $(".statusChange").find("option[value='4']").remove();
                    $(".statusChange").find("option[value='3']").remove();
                    $(".vacantRepairedRooms").find("option[value=" + selectedRoom + "]").attr("data-html", "Vaccant");
                    $(".vacantRepairedRooms").val(0);
                    $(".submittedLabel").text("Room " + selectedRoom + " repair work done!");
                }
            },
            error: function (response) {
                if (response.status == "417") {
                    alert("Error while running your input: Kindly contact Admin");
                }

            }
        });
    });

    $(".btnAvailableCount").trigger("click");
});


