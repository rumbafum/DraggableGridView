using System.Collections.Generic;
using System.Linq;

namespace DraggableGridView.Data
{
    public class Product
    {
        public Product(int categoryID, int productID, string productName, int order)
        {
            CategoryID = categoryID;
            ProductID = productID;
            ProductName = productName;
            Order = order;
        }

        public int CategoryID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Order { get; set; }
    }

    public class Products
    {
        static List<Product> _products = new List<Product>();

        public Products()
        {
            SetProducts(null);
        }

        public static List<Product> ProductDataSource
        {
            get { return _products; }
        }

        public static void SetProducts(object products)
        {
            if (products == null)
            {
                if (_products.Count == 0)
                {
                    _products.Add(new Product(1, 1, "produto 1_1", 1));
                    _products.Add(new Product(1, 2, "produto 1_2", 2));
                    _products.Add(new Product(2, 3, "produto 2_3", 1));
                    _products.Add(new Product(2, 4, "produto 2_4", 2));
                    _products.Add(new Product(3, 5, "produto 3_5", 1));
                    _products.Add(new Product(3, 6, "produto 3_6", 2));
                    _products.Add(new Product(3, 7, "produto 3_7", 3));
                }
            }
            else
            {
                var tempProducts = products as List<Product>;
                if (tempProducts != null)
                {
                    if (tempProducts.Count > 0)
                        _products.Clear();
                    _products.AddRange(tempProducts);
                }
            }
        }

        public List<Product> GetProducts(int categoryId)
        {
            return _products.FindAll(p => p.CategoryID == categoryId).OrderBy(p => p.Order).ToList();
        }

        public static void UpdateProductCategory(int productId, int categoryId, int prevOrder, int newCategoryId)
        {
            Product theProduct = _products.Find(p => p.CategoryID == categoryId && p.ProductID == productId);

            theProduct.CategoryID = newCategoryId;
            theProduct.Order = prevOrder+1;

            foreach (Product p in _products.FindAll(p => p.CategoryID == newCategoryId).OrderBy(p => p.Order).ToList())
            {
                if (prevOrder == 0 && p.ProductID != productId)
                    p.Order += 1;
                else
                {
                    if (p.ProductID != productId && p.Order >= prevOrder+1)
                        p.Order += 1;
                }
            }
            int i = 1;
            foreach (Product p in _products.FindAll(p => p.CategoryID == newCategoryId).OrderBy(p => p.Order).ToList())
            {
                p.Order = i;
                i++;
            }
            i = 1;
            foreach (Product p in _products.FindAll(p => p.CategoryID == categoryId).OrderBy(p => p.Order).ToList())
            {
                p.Order = i;
                i++;
            }
        }
    }
}