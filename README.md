# Bots v4 + Azure Functions

## Bot understanding

### Bots 101
```
USER <--> BOT
```

### How bots work
```
USER -- Azure Bot Service -- BOT
```

### How bots work+
```
USER -- ABS -- Adapter -- BOT
```

### How bots work+++
```
USER -- ABS -- Adapter(TurnContext) +- Middlewares -- BOT
                                    +-- Storage
```
### How storage work (Spin-off)
```
Activity -- Adapter -- TurnContext(Activity) -- Middleware -- BOT
               |
            Storage
```
### How storage work+ (Spin-off)
```
Activity -- Adapter -- TurnContext(Activity) -- Middleware -- BOT
              |
    statePropertyAccessors -- BotState
                              /      \
                        UserState  ConversationState
                              \      /
                              Storage (CosmosDB)
```

# V1 - with static functions
```c#
/// MessageTrigger.cs
public static class MessageTrigger
{
    [FunctionName("messages")]
    public static Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log,
        ExecutionContext context)
    {
        ICredentialProvider credentialProvider = GetCredentialProvider(context);
        IBotFrameworkFunctionsAdapter adapter = new BotFrameworkFunctionsAdapter(credentialProvider, logger: log);
        IBot bot = new StaticBot();

        return adapter.ProcessAsync(req, bot);
    }

    private static ConfigurationCredentialProvider GetCredentialProvider(ExecutionContext context)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(context.FunctionAppDirectory)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        return new ConfigurationCredentialProvider(config);
    }
}
```

# V2 - with dependency injection

```C#
/// MessageTrigger.cs
public static class MessageTrigger
{
    [FunctionName("messages")]
    public static Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        [Inject] IBotFrameworkFunctionsAdapter adapter,
        [Inject] IBot bot)
    {
        return adapter.ProcessAsync(req, bot);
    }
}

/// StartUp.cs
[assembly: WebJobsStartup(typeof(FunctionBot.StartUp))]
namespace FunctionBot
{
    public class StartUp : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddDependencyInjection(SimpleWire);
        }

        private void SimpleWire(IServiceCollection services)
        {
            services.AddSingleton<ICredentialProvider>(new SimpleCredentialProvider("<YOUR-APPID>", "<YOUR-PASSWORD>"));
            services.AddSingleton<IBotFrameworkFunctionsAdapter, SimpleAdapter>();
            services.AddSingleton<IBot, SimpleBot>();
        }
    }
}
```
# V3 - NEXT
>NOTE: This version uses the latest version of Azure Functions with native support for DI.
```C#
/// MessageTrigger.cs
public class MessageTrigger
{
    private readonly IBotFrameworkFunctionsAdapter adapter;
    private readonly IBot bot;

    public MessageTrigger(IBotFrameworkFunctionsAdapter adapter, IBot bot)
    {
        this.adapter = adapter;
        this.bot = bot;
    }

    [FunctionName("messages")]
    public Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
    {
        return adapter.ProcessAsync(req, bot);
    }
}

/// StartUp.cs
[assembly: WebJobsStartup(typeof(DIFunctionBot.StartUp))]
namespace DIFunctionBot
{
    public class StartUp : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddSingleton<ICredentialProvider, ConfigurationCredentialProvider>();
            builder.Services.AddSingleton<IBotFrameworkFunctionsAdapter, SimpleAdapter>();
            builder.Services.AddSingleton<IBot, SimpleBot>();
        }
    }
}
```

