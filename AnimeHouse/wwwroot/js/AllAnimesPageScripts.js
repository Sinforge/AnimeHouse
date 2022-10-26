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
    let containers = document.getElementsByClassName("anime-container");
    
    $('.anime-container').mouseover(function () {
        let conts = document.getElementsByClassName("anime-container");
        let currentElem = this;
        $('.anime-container').each(function (index, item) {
            if (item.getAttribute('id') === currentElem.getAttribute('id')) {
                item.style.filter = "brightness(120%)";
            } else {
                item.style.filter = "blur(5px)";
            }
        });
    });
    $('.anime-container').mouseout(function () {
        
        $('.anime-container').each(function (index, item) {
            item.style.filter = "none";
        });
    });
    

    
});