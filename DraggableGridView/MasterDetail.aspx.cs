using System;
using System.Web.UI;
using DevExpress.Web.ASPxGridView;
using DraggableGridView.Data;

namespace DraggableGridView
{
    public partial class MasterDetail : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DetailGrid_BeforePerformDataSelect(object sender, EventArgs e)
        {
            Session["CategoryID"] = (sender as ASPxGridView).GetMasterRowKeyValue();
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
            if (splited.Length != 3) return;

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

        protected void MasterGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var splited = e.Parameters.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (splited.Length != 2) return;

            Categories.UpdateCategory(Convert.ToInt32(splited[0]), Convert.ToInt32(splited[1]));
            MasterGrid.DataBind();
        }
    }
}