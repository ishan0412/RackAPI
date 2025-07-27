using RackApi.Models;

namespace RackApi.test.TestHelpers;

public class FakeProductBuilder
{
    private readonly Product _product;

    public FakeProductBuilder()
    {
        _product = new Product
        {
            Id = default,
            Name = string.Empty,
            Url = string.Empty,
            Vendor = string.Empty,
            Price = default,
            BeforeSalePrice = default,
        };
    }

    public FakeProductBuilder WithId(int id)
    {
        _product.Id = id;
        return this;
    }

    public FakeProductBuilder WithName(string name)
    {
        _product.Name = name;
        return this;
    }

    public FakeProductBuilder WithUrl(string url)
    {
        _product.Url = url;
        return this;
    }

    public FakeProductBuilder WithVendor(string vendor)
    {
        _product.Vendor = vendor;
        return this;
    }

    public FakeProductBuilder WithPrice(double price)
    {
        _product.Price = price;
        return this;
    }

    public FakeProductBuilder WithBeforeSalePrice(double beforeSalePrice)
    {
        _product.BeforeSalePrice = beforeSalePrice;
        return this;
    }

    public Product Build() => _product;
}
