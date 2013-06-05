
function InitializejQuery() {

    $("#MasterGrid").sortGrid({
        isMasterDetail: true,
        masterGridClientInstanceName: "MasterGrid",
        onMasterSortUpdate: function (ui) {
            var clone = (ui.item).clone();

            var categoryId = $(clone).find(".theCategoryID:lt(1)").text();

            var order = "0";
            var previousRow = $(ui.item).prevAll(".masterDraggable:lt(1)");
            if (previousRow.length > 0)
                order = $(previousRow).find(".theOrder:lt(1)").text();
            var grid = ASPxClientControl.GetControlCollection().GetByName("MasterGrid");
            grid.PerformCallback(categoryId + "_" + order);
        },
        onDetailSortUpdate: function (ui) {
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
    });

}
