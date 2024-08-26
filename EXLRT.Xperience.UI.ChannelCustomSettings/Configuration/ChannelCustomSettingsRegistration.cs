using EXLRT.Xperience.UI.ChannelCustomSettings.Repositories;
using EXLRT.Xperience.UI.ChannelCustomSettings.Repositories.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace EXLRT.Xperience.UI.ChannelCustomSettings.Configuration
{
    public static class ChannelCustomSettingsRegistration
    {
        public static void AddChannelCustomSettings(this IServiceCollection services)
        {
            services.AddScoped<IChannelCustomSettingsRepository, ChannelCustomSettingsRepository>();
        }
    }
}