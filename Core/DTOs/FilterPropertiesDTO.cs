namespace Core.DTOs
{
    public class FilterPropertiesDTO
    {
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public int? YearMin { get; set; }
        public int? YearMax { get; set; }
        public int? IdOwner { get; set; }
    }
}
