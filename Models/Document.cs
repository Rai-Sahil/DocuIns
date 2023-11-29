namespace DocuIns.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Path { get; set; }
        public string? Owner { get; set; }
        public string? Status { get; set; }
        public string? Tag { get; set; }
        public string? CreatedDate { get; set; }
        public string? ModifiedDate { get; set; }
    }
}


