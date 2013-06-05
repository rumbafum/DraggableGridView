

(function ($) {

    $.fn.sortGrid = function (options) {

        var settings = $.extend({
            isMasterDetail: false,
            masterGridClientInstanceName: 'MasterGrid',
            masterDisableSelection: true,
            detailDisableSelection: true,
            detailDropClass: ".droppable",
            onMasterSortUpdate: function (ui) { },
            onDetailSortUpdate: function (ui) { }
        }, options);

        if (!$(this).is(".dxgvControl")) return this;

        if (settings.isMasterDetail) {
            $(this).find(".dxgvDetailRow").each(function () {
                $(this).find(".dxgvDataRow").addClass("draggable");
            });
        }

        $(this).find(".dxgvDataRow").not(".draggable").addClass("masterDraggable");

        if (settings.isMasterDetail) {

            //            $(this).find(".dxgvDetailRow").each(function () {
            //                $(this).find(".dxgvControl:lt(1)").wrap("<div class=\"droppable\" />");
            //            });

            $(this).find(settings.detailDropClass).each(function () {
                $(this).find("tbody:lt(1) tr:lt(1) table:lt(1) tbody:lt(1)").addClass("detailDroppable");
            });

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

                        settings.onDetailSortUpdate.call(this, ui);

                        /*var clone = (ui.item).clone();

                        var productId = $(clone).find(".theProductID").text();

                        var categoryId = $(clone).find(".theCategoryID").text();
                        //only to be easier to get parent key
                        var newCategoryId = $(this).parents(".droppable").find("#HiddenCategoryID").val();

                        var order = "0";
                        var previousRow = $(ui.item).prev(".draggable");
                        if (previousRow.length > 0)
                        order = $(previousRow).find(".theOrder").text();

                        var grid = ASPxClientControl.GetControlCollection().GetByName("DetailGrid_" + newCategoryId);
                        grid.PerformCallback(categoryId + "_" + productId + "_" + order);*/
                    }
                }
            });

        }

        $(this).find("tbody:lt(1) tr:lt(1) table:lt(1) tbody:lt(1)").sortable({
            helper: 'clone',
            revert: true,
            opacity: 0.8,
            items: "> .masterDraggable",
            start: function (e, ui) {
                $(ui.helper).addClass("ui-draggable-helper");
            },
            update: function (ev, ui) {

                settings.onMasterSortUpdate.call(this, ui);

                //                var clone = (ui.item).clone();

                //                var categoryId = $(clone).find(".theCategoryID:lt(1)").text();

                //                var order = "0";
                //                var previousRow = $(ui.item).prevAll(".masterDraggable:lt(1)");
                //                if (previousRow.length > 0)
                //                    order = $(previousRow).find(".theOrder:lt(1)").text();
                //                var grid = ASPxClientControl.GetControlCollection().GetByName(settings.masterGridClientInstanceName);
                //                grid.PerformCallback(categoryId + "_" + order);
            }
        });

        if (settings.detailDisableSelection) {
            $('.detailDroppable').disableSelection();
        }

        if (settings.masterDisableSelection) {
            $(this).find("tbody:lt(1) tr:lt(1) table:lt(1) tbody:lt(1)").disableSelection();
        }


        return this;

    };

} (jQuery));