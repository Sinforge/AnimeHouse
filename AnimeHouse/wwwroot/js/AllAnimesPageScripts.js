$(document).ready(function() {
    $('#search').click(function(e) {
        e.preventDefault();
        console.log($("#search_anime").serialize());
        $.ajax({
            url: "/Anime/SearchAnime",
            type: "GET",
            data: $("#search_anime").serialize(),
            success: function(response) {
                console.log(response);
                $("#anime_list").html(response);

            }
        });
    });
});