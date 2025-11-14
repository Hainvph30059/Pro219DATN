namespace Pro219.API.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }
        public int? UpdateBy { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool isParent { get; set; }
        public List<CategoryDTO>? SubCategory { get; set; }
    }
}

