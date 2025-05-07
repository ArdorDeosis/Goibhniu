using Goibhniu.Analysis;
using Goibhniu.Infrastructure;
using Goibhniu.Model;
using Goibhniu.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Goibhniu.Application;

[ApiController]
[Route("api/analysis")]
[ServiceFilter<HttpResponseExceptionFilter>]
public class ProductAnalysisController : ControllerBase
{
  private readonly IProductDataService productDataService;
  private readonly IProductAnalysisService productAnalysisService;
  private readonly IRepresentationProvider representationProvider;

  public ProductAnalysisController(
    IProductDataService productDataService,
    IProductAnalysisService productAnalysisService,
    IRepresentationProvider representationProvider)
  {
    this.productDataService = productDataService;
    this.productAnalysisService = productAnalysisService;
    this.representationProvider = representationProvider;
  }

  [HttpGet("price-extremes")]
  public async Task<IActionResult> GetPriceExtremes([FromQuery] string url, CancellationToken cancellationToken)
  {
    ValidateUrl(url);
    var products = await FetchProducts(url, cancellationToken);
    var result = productAnalysisService.GetPriceExtremes(products)
      .ValueOr500("analysis failed");
    return RepresentationOf(result);
  }

  [HttpGet("exact-price")]
  public async Task<IActionResult> GetExactPrice([FromQuery] string url, [FromQuery] decimal price,
    CancellationToken cancellationToken)
  {
    ValidateUrl(url);
    ValidatePrice(price);
    var products = await FetchProducts(url, cancellationToken);
    var result = productAnalysisService.GetProductsWithPrice(products, price)
      .ValueOr500("analysis failed");
    return RepresentationOf(result);
  }

  [HttpGet("most-bottles")]
  public async Task<IActionResult> GetMostBottles([FromQuery] string url, CancellationToken cancellationToken)
  {
    ValidateUrl(url);
    var products = await FetchProducts(url, cancellationToken);
    var result = productAnalysisService.GetProductWithMostBottles(products)
      .ValueOr500("analysis failed");
    return RepresentationOf(result);
  }

  /// <summary>
  /// Validates the provided URL to ensure it is not null, empty, or consists only of white-space characters.
  /// </summary>
  /// <param name="url">The URL to validate.</param>
  /// <exception cref="HttpResponseException">Thrown when the URL is invalid or missing.</exception>
  // ---
  // NOTE: This could have more checks, e.g. regex checks for valid URLs, or checks for preventing code injection
  private void ValidateUrl(string url)
  {
    if (string.IsNullOrWhiteSpace(url))
      throw new HttpResponseException(BadRequest("No URL provided"));
  }

  /// <summary>
  /// Validates the provided price to ensure it is non-negative.
  /// </summary>
  /// <param name="price">The price to validate.</param>
  /// <exception cref="HttpResponseException">Thrown when the price is negative.</exception>
  private void ValidatePrice(decimal price)
  {
    if (price < 0)
      throw new HttpResponseException(BadRequest("price must be positive"));
  }

  /// <summary>
  /// Fetches a collection of products from the provided URL.
  /// </summary>
  /// <param name="url">The URL from which to fetch the product data.</param>
  /// <param name="cancellationToken">Token to observe while waiting for the task to complete.</param>
  private async Task<IReadOnlyCollection<Product>> FetchProducts(string url, CancellationToken cancellationToken) =>
    await productDataService.FetchProductData(url, cancellationToken).ValueOr500("Error fetching products");

  /// <summary>
  /// Converts an object to a client-facing representation.
  /// </summary>
  private IActionResult RepresentationOf(object? raw) =>
    representationProvider.MakeRepresentation(raw) is {} output
      ? Ok(output)
      : Problem("Data has no representation");
}

file static class Extensions
{
  /// <summary>
  /// Extracts the value from a successful <see cref="Result{T}"/>, or throws a <see cref="HttpResponseException"/> with
  /// error code 500 and the specified error message if the result is unsuccessful.
  /// </summary>
  /// <typeparam name="T">The type of the value contained in the result.</typeparam>
  /// <param name="result">The result object containing success or error state.</param>
  /// <param name="errorMessage">The error message to include in the exception if the result is unsuccessful.</param>
  /// <returns>The value of type <typeparamref name="T"/> if the result is successful.</returns>
  /// <exception cref="HttpResponseException">
  /// Thrown when the result is unsuccessful, with the specified error message.
  /// </exception>
  public static T ValueOr500<T>(this Result<T> result, string errorMessage) where T : notnull =>
    result is { IsSuccess: true, Data: { } value }
      ? value
      : throw new HttpResponseException(errorMessage);

  /// <summary>
  /// An overload of <see cref="ValueOr500{T}(Result{T},string)"/> that handles async results.
  /// </summary>
  public static async Task<T> ValueOr500<T>(this Task<Result<T>> result, string errorMessage) where T : notnull =>
    (await result).ValueOr500(errorMessage);
}