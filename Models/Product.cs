namespace RackApi.Models;

/// <summary>
/// Represents a clothing product with properties such as name, vendor, price, and the URL at which it can be found.
/// </summary>
public class Product
{
    /// <summary>
    /// Unique identifier for the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The product's name.
    /// </summary>
    required public String Name { get; set; }

    /// <summary>
    /// URL at which the product can be found and purchased online.
    /// </summary>
    required public String Url { get; set; }

    /// <summary>
    /// Name of the brand or company selling the product.
    /// </summary>
    required public String Vendor { get; set; }

    /// <summary>
    /// The product's current price in US dollars.
    /// </summary>
    required public double Price { get; set; }

    /// <summary>
    /// The product's price before any discounts or sales, in US dollars.
    /// </summary>
    public double BeforeSalePrice { get; set; }
    
    /// <summary>
    /// Checks if the product is currently on sale by comparing its price to its before-sale price.
    /// </summary>
    /// <returns>
    /// <c>True</c> if the product does have a before-sale price which its current price is less than
    /// </returns>
    public Boolean IsOnSale() => Price < BeforeSalePrice;
}