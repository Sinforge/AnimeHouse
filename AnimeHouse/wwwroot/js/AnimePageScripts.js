$(document).ready(function() {
        $('#send_comment').click(function (e) {
            e.preventDefault();
            console.log($("#send_comment_form").serialize());
            $.ajax({
                url: "/Anime/LeaveComment",
                type: "POST",
                data: $("#send_comment_form").serialize(),
                success: function (response) {
                    console.log(response);
                    $("#list_of_comments").html(response);

                }
            });
        });
    $('#delete_user_comment').click(function(e) {
        e.preventDefault();

    console.log($("#delete_comment_form").serialize());
    console.log(userName, animeId, text);
    $.ajax({
        url: "/Admin/DeleteComment",
    type: "POST",
    data: $("#delete_comment_form").serialize(),
    success: function(response) {
        console.log(response);
    $("#list_of_comments").html(response);
									  
                                 }
                            });
                        });
    $('#add-delete-favorite').click(function(e) {

        console.log("Button is clicked");
    console.log($("#add-delete-favorite-form").serialize());
    if ($('#add-delete-favorite').text() == "Delete from favorite") {
        $.ajax({
            url: "/Anime/DeleteFromFavorite",
            type: "POST",
            data: $("#add-delete-favorite-form").serialize(),
            success: function (response) {
                console.log(response);
                $("#add-delete-favorite").text("Add to favorite");

            }
        });
							}
    else {
        $.ajax({
            url: "/Anime/AddToFavorite",
            type: "POST",
            data: $("#add-delete-favorite-form").serialize(),
            success: function (response) {
                console.log(response);
                $("#add-delete-favorite").text("Delete from favorite");

            }
        });
							}
						
						});
                 
						 
});


