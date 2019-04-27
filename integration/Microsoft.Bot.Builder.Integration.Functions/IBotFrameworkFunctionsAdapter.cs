namespace Microsoft.Bot.Builder.Integration.AspNet.Functions
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IBotFrameworkFunctionsAdapter
    {
        Task<IActionResult> ProcessAsync(HttpRequest req, IBot bot, CancellationToken cancellationToken = default);
    }
}
