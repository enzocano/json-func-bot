using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using System.Threading;
using System.Threading.Tasks;

namespace JsonBot.Sample.Integration
{
    public interface IBotFrameworkFunctionsAdapter
    {
        Task<IActionResult> ProcessAsync(HttpRequest req, IBot bot, CancellationToken cancellationToken = default(CancellationToken));
    }
}
