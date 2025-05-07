Named after [Goibhniu](https://en.wikipedia.org/wiki/Goibniu) (pronounced *GOV-new*), Irish god of smithing, brewing and hospitality.

# State

This project has room for improvement. Here's a list of things one could add:

- Logging
- Some better HTTP error codes (although most cases are covered)
- Telemetry (I have OpenTelemetry on my bucket list, maybe this is the chance to do it)
- Integration Tests
- automated test runs via github actions

# Notes

I have placed notes for you, dear reader, throughout the code starting with `// NOTE:` like so:

```csharp
// NOTE: this is a note
```

### Naming Conventions
- Leading underscores for private member variables: I don't like them. They look janky and interrupt my reading flow. With modern IDEs there is no reason to mark private fields as such.
- Async suffix on async methods: Similar to the point above. We live in a world where a lot of code is async. *If* we ever have the choice between sync and async methods, the async should be the default. But then why should I mark the default? With modern IDEs, I don't need to add this suffix in the name to identify async methods.

### Thoughts I had during Development
- for the data fetch service, I'm debating whether it should return result types or throw in case of an error. Throwing exceptions is cleaner to read in success cases, but try/catch blocks are a hassle to read flow-wise and catching exceptions is rather expensive, so I lean towards the result type solution.
- I was thinking about using a command pattern for the product analysis, but since there is no shared procedures between them, I decided to put the analysis questions into one service. Should this change in the future, a command pattern might be the right choice.
- Was thinking about how specific the Regex pattern should be to match the string information, but decided to keep them as they are for now. good opportunity, though, to increase accepted strings in the future
- Thinking hard about the `GetProductsWithMostBottles` method implementation. The cleanest and easiest to read would be a short LINQ query that creates tuples of product and article for all articles. But that could have an unnecessary allocation overhead. I could manually iterate at the cost of readability. it would be more declarative than imperative. Now I'll try to implement it with an `Aggregate` call.
  - the whole situation would be a bit simpler if I could ignore articles with identical bottle count. In a "real world" scenario I would clarify what is meant exactly before implementing.
    - after another read of the specification, I found that it states *Which **one** product [...]*, so, no list I guess
- I am wondering whether the decision to separate data provision and analysis is the best approach. In cases where there is a lot of data in the JSON source, but I am only looking for very few entries, an approach that does not serialize all data directly could be beneficial. The data stream could be parsed into an `IAsyncEnumerable` and instances could be thrown out directly, before being written to the result.
- I'm wondering at question two, whether it should return articles or products. The question sounds like it expects a Product (beer), that has an article with the exact price. Then I should order by price per unit. Problem is, if the result is a list of Products, these products have potentially multiple articles and thus multiple values to sort by. I could sort by the article's value that has the queried price.
  - not quite happy with this solution, the filter and ordering is coupled tightly together.