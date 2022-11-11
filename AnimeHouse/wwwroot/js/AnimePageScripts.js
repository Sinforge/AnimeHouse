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
        let heart = document.getElementById("heart");
        var isLiked;
        if (document.getElementById("isLiked").innerHTML === "0") {
            isLiked = false;
        } else {
            isLiked = true;
        }
        var counter = document.getElementById("counter");
    heart.addEventListener("click", () => {
        if (!isLiked) {
                document.getElementById("isLiked").innerHTML = "1";
                isLiked = true;
                heart.style.color = "red";
                counter.innerHTML = Number(counter.innerHTML) + 1;
                $.ajax({
                    url: "/Anime/AddToFavorite",
                    type: "POST",
                    data: $("#add-delete-favorite-form").serialize(),

                });
               
            } else {
                document.getElementById("isLiked").innerHTML = "0";
                isLiked = false;
                heart.style.color = "black";
                counter.innerHTML = Number(counter.innerHTML) - 1;
                $.ajax({
                    url: "/Anime/DeleteFromFavorite",
                    type: "POST",
                    data: $("#add-delete-favorite-form").serialize(),
                });

            }
    });
                 
						 
});


