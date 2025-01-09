namespace Web.Models
{
    public class PropertyImageModel
    {
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public string Url { get; set; }
        public bool Enabled { get; set; }
        public PropertyModel Property { get; set; }
    }
}
