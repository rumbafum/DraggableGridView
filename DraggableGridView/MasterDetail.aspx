<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterDetail.aspx.cs" Inherits="DraggableGridView.MasterDetail" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxGlobalEvents" Assembly="DevExpress.Web.v12.1, Version=12.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dxwgv" Namespace="DevExpress.Web.ASPxGridView" Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="DX.ashx?jsfolder=~/Scripts" type="text/javascript"></script>
    <link href="DX.ashx?cssfolder=~/css" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxwgv:ASPxGridView runat="server" ID="MasterGrid" OnDataBound="MasterGrid_DataBound" KeyFieldName="CategoryID" OnCustomCallback="MasterGrid_CustomCallback" ClientInstanceName="MasterGrid" 
            DataSourceID="masterDataSource">
                <ClientSideEvents EndCallback="function(s,e){ if (s.cpIsCustomCallback) {s.cpIsCustomCallback = false;MasterGrid.Refresh();} }"></ClientSideEvents>
                <Columns>
                    <dxwgv:GridViewDataColumn CellStyle-CssClass="theCategoryID" FieldName="CategoryID" VisibleIndex="0" />
                    <dxwgv:GridViewDataColumn FieldName="CategoryName" VisibleIndex="1" />
                    <dxwgv:GridViewDataColumn CellStyle-CssClass="theOrder hide" FieldName="Order" VisibleIndex="2" HeaderStyle-CssClass="hide" />
                </Columns>
                <Templates>
                    <DetailRow>
                        <div class="droppable">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="DetailGrid" ID="DetailGrid" KeyFieldName="ProductID" 
                                DataSourceID="detailDataSource" OnBeforePerformDataSelect="DetailGrid_BeforePerformDataSelect" OnInit="DetailGrid_Init"
                                OnCustomCallback="DetailGrid_CustomCallback" OnAfterPerformCallback="DetailGrid_AfterPerformCallback">
                                <ClientSideEvents EndCallback="function(s,e){ if (s.cpIsCustomCallback) {s.cpIsCustomCallback = false;MasterGrid.Refresh();} }"
                                    ></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewDataColumn CellStyle-CssClass="theCategoryID" FieldName="CategoryID" VisibleIndex="0" />
                                    <dxwgv:GridViewDataColumn CellStyle-CssClass="theProductID" FieldName="ProductID" VisibleIndex="1" />
                                    <dxwgv:GridViewDataColumn FieldName="ProductName" VisibleIndex="2" />
                                    <dxwgv:GridViewDataColumn CellStyle-CssClass="theOrder hide" FieldName="Order" VisibleIndex="3" HeaderStyle-CssClass="hide" />
                                </Columns>
                                <Styles>
                                    <RowHotTrack BackColor="Yellow"></RowHotTrack>
                                    <Cell Paddings-Padding="10" Border-BorderColor="Black" Border-BorderWidth="1" Border-BorderStyle="Dotted"></Cell>
                                </Styles>
                                <SettingsBehavior EnableRowHotTrack="true" />
                            </dxwgv:ASPxGridView>
                            <input type="hidden" id="HiddenCategoryID" value="<%# Eval("CategoryID") %>" />
                        </div>
                    </DetailRow>
                </Templates>
                <SettingsDetail ShowDetailRow="true" />
            </dxwgv:ASPxGridView>
        </div>
        <asp:ObjectDataSource ID="masterDataSource" runat="server" SelectMethod="GetCategories" TypeName="DraggableGridView.Data.Categories"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="detailDataSource" runat="server" SelectMethod="GetProducts" TypeName="DraggableGridView.Data.Products">
            <SelectParameters>
                <asp:SessionParameter Name="CategoryId" SessionField="CategoryID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <dx:ASPxGlobalEvents ID="ge" runat="server">
            <ClientSideEvents ControlsInitialized="InitializejQuery" EndCallback="InitializejQuery" />
        </dx:ASPxGlobalEvents>
    </form>
</body>
</html>
