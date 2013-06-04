using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DraggableGridView.Data
{
    public class Category
    {
        public Category(int categoryID, string categoryName, int order)
        {
            CategoryID = categoryID;
            CategoryName = categoryName;
            Order = order;
        }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int Order { get; set; }
    }

    public class Categories
    {
        static List<Category> _categories = new List<Category>();

        public Categories()
        {
            SetCategories(null);
        }

        public static void SetCategories(object categories)
        {
            if (categories == null)
            {
                if (_categories.Count == 0)
                {
                    _categories.Add(new Category(1, "Categoria 1", 1));
                    _categories.Add(new Category(2, "Categoria 2", 2));
                    _categories.Add(new Category(3, "Categoria 3", 3));
                }
            }
            else
            {
                var tempCategories = categories as List<Category>;
                if (tempCategories != null)
                {
                    if (tempCategories.Count > 0)
                        _categories.Clear();
                    _categories.AddRange(tempCategories);
                }
            }
        }

        public List<Category> GetCategories()
        {
            return _categories.OrderBy(p => p.Order).ToList();
        }

        public static void UpdateCategory(int categoryId, int prevOrder)
        {
            Category theCategory = _categories.Find(p => p.CategoryID == categoryId);

            theCategory.Order = prevOrder + 1;

            foreach (Category c in _categories.OrderBy(c => c.Order).ToList())
            {
                if (prevOrder == 0 && c.CategoryID != categoryId)
                    c.Order += 1;
                else
                {
                    if (c.CategoryID != categoryId && c.Order >= prevOrder + 1)
                        c.Order += 1;
                }
            }
            int i = 1;
            foreach (Category c in _categories.OrderBy(c => c.Order).ToList())
            {
                c.Order = i;
                i++;
            }
        }
    }
}