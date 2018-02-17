

define(["jquery", "bootstrap", "hbs!../html/tableRow"], function ($, b, row) {
    'use strict';


    alert("dddddddd")
    var data = { Name: "test", Path: "/das/" }

    $(".table.components").html(row(data));





    $.ajax({
        url: "/api/sitecore/ComponentsReviewer/GetAllRenderings"
    }).done(function (data) {
        var url = "";
        $.each(data,
            function (i, rendering) {
                console.log(rendering);

                $.each(rendering.Links, function (j, link) {
                    url = link.Url;
                    $("#siteloader").html('<object data="' + link.Url + '">');
                });

            });
        // window.location.href = url;
    });

});


