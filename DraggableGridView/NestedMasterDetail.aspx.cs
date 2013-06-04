using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DraggableGridView.Data;

namespace DraggableGridView
{
    public partial class NestedMasterDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DetailGrid_BeforePerformDataSelect(object sender, EventArgs e)
        {
            Session["CategoryID"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        }

        protected void DetailGrid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
                e.Row.CssClass = "draggable";
        }

        protected void DetailGrid_Init(object sender, EventArgs e)
        {
            ASPxGridView detailGridView = (ASPxGridView)sender;
            GridViewDetailRowTemplateContainer templateContainer = (GridViewDetailRowTemplateContainer)detailGridView.NamingContainer;
            detailGridView.ClientInstanceName = string.Format("DetailGrid_{0}", templateContainer.KeyValue);
        }

        protected void DetailGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var detailGridView = (ASPxGridView)sender;
            var templateContainer = (GridViewDetailRowTemplateContainer)detailGridView.NamingContainer;
            var splited = e.Parameters.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            Products.UpdateProductCategory(Convert.ToInt32(splited[1]), Convert.ToInt32(splited[0]), Convert.ToInt32(splited[2]), Convert.ToInt32(templateContainer.KeyValue));
            detailGridView.DataBind();
        }

        protected void MasterGrid_DataBound(object sender, EventArgs e)
        {
            MasterGrid.DetailRows.ExpandAllRows();
        }

        protected void DetailGrid_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            ASPxGridView gv = (ASPxGridView)sender;
            gv.JSProperties.Add("cpIsCustomCallback", null);
            gv.JSProperties["cpIsCustomCallback"] = e.CallbackName == "CUSTOMCALLBACK";
        }

        protected void DetailGrid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
        }
    }
}