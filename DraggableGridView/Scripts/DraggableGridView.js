
function InitializejQuery() {

    $('.droppable').each(function () {

        $(this).find("tbody:lt(1) tr:lt(1) table:lt(1) tbody:lt(1)").addClass("detailDroppable");

    });

    /*$('.masterDraggable:lt(1)').each(function () {
        var row = $(this).prev();
        $(row).wrap("<tbody />");
    });

    $('.masterDraggable').each(function () {
        var rows = $(this).prev().nextAll().slice(0, 2);
        $(rows).wrapAll("<tbody class=\"masterDraggableDiv\" />");
    });*/

    $('.detailDroppable').sortable({
        helper: 'clone',
        revert: true,
        opacity: 0.8,
        items: "> .draggable",
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

    $("#MasterGrid tbody:lt(1) tr:lt(1) table:lt(1) tbody:lt(1)").sortable({
        helper: 'clone',
        revert: true,
        opacity: 0.8,
        items: "> .masterDraggable",
        start: function (e, ui) {
            $(ui.helper).addClass("ui-draggable-helper");
        },
        update: function (ev, ui) {
            var clone = (ui.item).clone();

            var categoryId = $(clone).find(".theCategoryID:lt(1)").text();

            var order = "0";
            var previousRow = $(ui.item).prevAll(".masterDraggable:lt(1)");
            if (previousRow.length > 0)
                order = $(previousRow).find(".theOrder:lt(1)").text();
            MasterGrid.PerformCallback(categoryId + "_" + order);
        }
    });
    $('.detailDroppable').disableSelection();
    $("#MasterGrid tbody:lt(1) tr:lt(1) table:lt(1) tbody:lt(1)").disableSelection();

}

function InitializeNestedjQuery() {

    $("#MasterGrid").nestedSortable({
        //protectRoot : "true",
        items: "dxgvDataRow",
        branchClass: "dxgvDetailRow",
        listType: "table"//,
        //maxLevels: "1",
    });

}