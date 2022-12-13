using Microsoft.Extensions.Configuration;

namespace NSE.Core.Utils;

public static class ConfigurationExtesions
{
    public static string GetMessageQueueConnection(this IConfiguration configuration, string name)
    {
        var messageQueueConnection = configuration?.GetSection("MessageQueueConnection")?[name];
        return messageQueueConnection.ToString();
    }
}