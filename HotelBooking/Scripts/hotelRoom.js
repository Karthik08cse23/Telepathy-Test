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

    $(".vacantRepairedRooms").on("change", function () {
        debugger;
        
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
        debugger;
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
                }
                else if (selectedStatus == "4") {
                    alert(selectedRoom + " Under repair.")
                    $(".statusChange").find("option[value='1']").remove();
                    $(".statusChange").find("option[value='4']").remove();
                    $(".statusChange").find("option[value='3']").remove();
                    $(".vacantRepairedRooms").find("option[value=" + selectedRoom + "]").attr("data-html", "Repair");
                    $(".vacantRepairedRooms").val(0);
                }
                else {
                    alert(selectedRoom + " Repair done. Moved to Vacant.");
                    $(".statusChange").find("option[value='1']").remove();
                    $(".statusChange").find("option[value='4']").remove();
                    $(".statusChange").find("option[value='3']").remove();
                    $(".vacantRepairedRooms").find("option[value=" + selectedRoom + "]").attr("data-html", "Vaccant");
                    $(".vacantRepairedRooms").val(0);
                }
            },
            error: function (response) {
                debugger;

            }
        });
    });
});


