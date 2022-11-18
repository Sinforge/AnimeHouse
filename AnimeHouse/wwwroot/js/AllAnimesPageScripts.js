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
                item.style.transform = "scale(1.1)";
            } else {
                item.style.filter = "blur(2px)";
            }
        });
    });
    $('.anime-container').mouseout(function () {
        
        $('.anime-container').each(function (index, item) {
            item.style.filter = "none";
            item.style.transform = "scale(1)";
        });
    });

var categories = document.querySelectorAll(".categories input");
var arrSelectedCategories = [];
var methods = document.querySelectorAll(".sort-method input");
var selectedMethod = null;
for (let i = 0; i < methods.length; i++) {
    methods[i].addEventListener("change",
        () => {

            if (methods[i].checked) {
                for (let j = 0; j < methods.length; j++) {
                    if (methods[i].name != methods[j].name) {
                        methods[j].checked = false;
                    }

                }
                selectedMethod = methods[i].name;
            } else {
                selectedMethod = null;
            }
            console.log(methods[i].name);
            console.log(selectedMethod);
            $.ajax({
                url: "/Anime/FilterAnimes",
                type: "post",
                traditional: true,
                data: { categories: arrSelectedCategories, sortMethod: selectedMethod },
                success: function (response) {
                    console.log(response);
                    $("#anime_list").html(response);

                }
            });
            
        });
}
for (let i = 0; i < categories.length; i++)
{
    categories[i].addEventListener("change", () => {
        console.log(categories[i]);
        if (categories[i].checked) {
            arrSelectedCategories.push(categories[i].name);
        } else {
            arrSelectedCategories = arrSelectedCategories.filter(val => val !== categories[i].name);
        }
        console.log(JSON.stringify({ categories: arrSelectedCategories }));
        console.log(arrSelectedCategories);
         $.ajax({
             url: "/Anime/FilterAnimes",
             type: "post",
             traditional: true,
             data: {categories: arrSelectedCategories, sortMethod:  selectedMethod},
             success: function (response) {
                 console.log(response);
                 $("#anime_list").html(response);

             }
         });
        
    });
}


});