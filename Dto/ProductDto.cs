namespace RackApi.Dto;

public class ProductDto
{
    required public string Name { get; set; }
    required public string Url { get; set; }
    required public string Vendor { get; set; }
    required public double Price { get; set; }
    public double BeforeSalePrice { get; set; }
}