using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Bot.Builder.Integration.Functions
{
    public interface IBotFrameworkFunctionsAdapter
    {
        Task<IActionResult> ProcessAsync(HttpRequest req, IBot bot, CancellationToken cancellationToken = default);
    }
}
