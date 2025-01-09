namespace Web.Models
{
    public class PropertySaveModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public int IdOwner { get; set; }
    }

    public class PropertyUpdatePriceModel
    {
       public decimal Price { get; set; }
    }
}
