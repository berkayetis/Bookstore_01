namespace Entities.RequestFeatures
{
    public class BookParameters : RequestParameters
	{
        public uint MinPrice { get; set; } // fiyat negatif olamicagi icin uint

        public uint MaxPrice { get; set; } = 10000; // fiyat negatif olamicagi icin uint 

        public bool ValidPriceRange => MaxPrice > MinPrice;

        public String? SearchTerm { get; set; }

        public BookParameters()
        {
            OrderBy = "Title";
        }
    }
}
