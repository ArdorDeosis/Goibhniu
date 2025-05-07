namespace Goibhniu.Infrastructure;

internal interface IDeserializationStrategy
{
  public ProductDataDeserializer Resolve(string url);
}