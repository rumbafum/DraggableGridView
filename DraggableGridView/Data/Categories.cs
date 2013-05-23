using System;
using System.Data;

namespace DraggableGridView.Data
{
    public class Categories
    {
        public DataTable GetCategories()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("CategoryID", typeof(Int32));
            dt.Columns.Add("CategoryName", typeof(string));
            dt.Rows.Add(new object[] { 1, "Categoria 1" });
            dt.Rows.Add(new object[] { 2, "Categoria 2" });
            dt.Rows.Add(new object[] { 3, "Categoria 3" });
            return dt;
        }
    }
}