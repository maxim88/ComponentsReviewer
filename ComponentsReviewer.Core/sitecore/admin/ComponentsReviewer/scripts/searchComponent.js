
define(["jquery", "helper", "hbs!search"], function ($, helper, searchComponent) {
   
    var searchHelper = {
        getSearchOtions() {
            var options = [];
            $.each($(".search-option"), function (i, optionEl) {
                if ($(this).prop("checked")) {
                    var option = $(optionEl).attr("data-type");
                    options.push(option);
                } 
            });
        },

        getElements() { 
            return {
                searchComponent: $(this).parents(".search-component"),
                resultsEl: component.find(".search-results"),
                preview : component.find(".search-preview"),
                autocomplete : component.find(".search-preview")
            }
        },
         
        doSearch(text) {
            var searchOptions = searchHelper.getSearchOtions();
            var searchRes = [];
            $.each(renderings, function (i, rendering) {
                
                if (helper.isExist("name", searchOptions) && rendering.Name.lastIndexOf(text, 0) == 0) {
                    searchRes.push(rendering);
                }
                else if (helper.isExist("comment", searchOptions) && rendering.Comments.lastIndexOf(text, 0) == 0) {
                    searchRes.push(rendering);
                }
                else if (helper.isExist("reference", searchOptions) && rendering.Link.lastIndexOf(text, 0) == 0) {
                    searchRes.push(rendering);
                }
            }); 
        } 
    }
       
    $(".search-component").html(searchComponent());

    return searchHelper;
});
