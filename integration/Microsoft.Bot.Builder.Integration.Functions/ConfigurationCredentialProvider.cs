using Microsoft.Azure.WebJobs;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Bot.Builder.Integration.Functions
{
    /// <summary>
    /// Credential provider which uses <see cref="Microsoft.Extensions.Configuration.IConfiguration"/> to lookup appId and password.
    /// </summary>
    /// <remarks>
    /// This will populate the <see cref="SimpleCredentialProvider.AppId"/> from an configuration entry with the key of <see cref="MicrosoftAppCredentials.MicrosoftAppIdKey"/>
    /// and the <see cref="SimpleCredentialProvider.Password"/> from a configuration entry with the key of <see cref="MicrosoftAppCredentials.MicrosoftAppPasswordKey"/>.
    ///
    /// NOTE: if the keys are not present, a <c>null</c> value will be used.
    /// </remarks>
    public sealed class ConfigurationCredentialProvider : SimpleCredentialProvider
    {
        public ConfigurationCredentialProvider(IConfiguration configuration)
        {
            this.AppId = configuration.GetSection(MicrosoftAppCredentials.MicrosoftAppIdKey)?.Value;
            this.Password = configuration.GetSection(MicrosoftAppCredentials.MicrosoftAppPasswordKey)?.Value;
        }

        public static ConfigurationCredentialProvider FromExecutionContext(ExecutionContext context)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            return new ConfigurationCredentialProvider(config);
        }
    }
}
