
function InitializejQuery() {
    $('.droppable tbody').sortable({
        helper: 'clone',
        revert: true,
        opacity: 0.8,
        start: function (e, ui) {
            $(ui.helper).addClass("ui-draggable-helper");
        },
        connectWith: '.droppable tbody',
        update: function (ev, ui) {
            if (this === ui.item.parent()[0]) {

                var clone = (ui.item).clone();

                var productId = $(clone).find(".theProductID").text();

                var categoryId = $(clone).find(".theCategoryID").text();
                //only to be easier to get parent key
                var newCategoryId = $(this).parents(".droppable").find("#HiddenCategoryID").val();

                var order = "0";
                var previousRow = $(ui.item).prev(".draggable");
                if (previousRow.length > 0)
                    order = $(previousRow).find(".theOrder").text();

                var grid = ASPxClientControl.GetControlCollection().GetByName("DetailGrid_" + newCategoryId);
                grid.PerformCallback(categoryId + "_" + productId + "_" + order);
            }
        }
    });
}