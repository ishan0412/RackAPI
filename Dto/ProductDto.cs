namespace RackApi.Dto;

public class ProductDto
{
    public required string Name { get; set; }
    public required string Url { get; set; }
    public required string Vendor { get; set; }
    public required double Price { get; set; }
    public double BeforeSalePrice { get; set; }
}
