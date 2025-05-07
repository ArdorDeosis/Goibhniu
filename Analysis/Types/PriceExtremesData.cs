using Goibhniu.Model;

namespace Goibhniu.Analysis;

public readonly record struct PriceExtremesData(ProductArticlePair Cheapest, ProductArticlePair MostExpensive)
{
  public static implicit operator PriceExtremesData(ValueTuple<ProductArticlePair, ProductArticlePair> data) => 
    new(data.Item1, data.Item2);
}