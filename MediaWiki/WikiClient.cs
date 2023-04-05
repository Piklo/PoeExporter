using Serilog;
using System.Text.Json;

namespace MediaWiki;

/// <summary>
/// Class used to call wiki api.
/// </summary>
public sealed class WikiClient : IDisposable
{
    private readonly ILogger? logger;
    private readonly HttpClient httpClient;
    private bool disposedValue;

    /// <summary>
    /// Gets or sets api end point used by the <see cref="WikiClient"/>.
    /// </summary>
    public string ApiEndPoint { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WikiClient"/> class.
    /// </summary>
    /// <param name="apiEndPoint">api end point.</param>
    /// <param name="logger">logger.</param>
    /// <param name="httpMessageHandler">overrides <see cref="HttpMessageHandler"/> used by the <see cref="WikiClient"/>.</param>
    /// <param name="disposeHandler">sets whether message handler should be disposed.</param>
    public WikiClient(string apiEndPoint, ILogger? logger = null, HttpMessageHandler? httpMessageHandler = null, bool disposeHandler = true)
    {
        ApiEndPoint = apiEndPoint;
        this.logger = logger;

        if (httpMessageHandler is not null)
        {
            httpClient = new HttpClient(httpMessageHandler, disposeHandler);
        }
        else
        {
            httpClient = new HttpClient();
        }
    }

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                httpClient.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~WikiClient()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    /// <inheritdoc/>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Gets page content.
    /// </summary>
    /// <param name="pageTitle">page title.</param>
    /// <returns>Task.</returns>
    public async Task GetPageContent(string pageTitle)
    {
        var url = $"{ApiEndPoint}?action=query&prop=revisions&titles={pageTitle}&rvslots=*&rvprop=content&format=json";
        logger?.Verbose($"""getting page content for page ="{url}".""");

        var test = await httpClient.GetAsync(url);
        var content = test.Content;
        var contentString = await content.ReadAsStringAsync();
        using var res = JsonDocument.Parse(contentString);
        logger?.Verbose(contentString);

        var garbage = JsonDocument.Parse(contentString).RootElement
                                  .GetProperty("query")
                                  .GetProperty("pages")
                                  .EnumerateObject()
                                  .ToArray()[0].Value
                                  .GetProperty("revisions")[0]
                                  .GetProperty("slots")
                                  .GetProperty("main")
                                  .GetProperty("*")
                                  .GetString();

        if (garbage is not null)
        {
            logger?.Verbose("{varName} =\n{result}", nameof(garbage), garbage);
        }
        else
        {
            logger?.Verbose("{varName} is null", nameof(garbage));
        }
    }
}
