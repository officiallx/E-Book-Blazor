namespace ebook.Models.SubCategory
{
    public class SubCategoryModel
    {
        public int subcategory_id { get; set; }
        public string subcategory_name { get; set; }
        public string description { get; set; }
        public int category_id { get; set; }
        public string category_name { get; set; }
    }
}
