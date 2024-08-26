using CMS.ContentEngine;
using CMS.DataEngine;
using CMS.Websites.Routing;
using EXLRT.Xperience.UI.ChannelCustomSettings.Admin.Objects;

namespace EXLRT.Xperience.UI.ChannelCustomSettings.Repositories.Implementation
{
    public class ChannelCustomSettingsRepository : IChannelCustomSettingsRepository
    {
        private readonly IInfoProvider<ChannelInfo> channelInfoProvider;
        private readonly IInfoProvider<ChannelCustomSettingsInfo> customSettingsInfoProvider;
        private readonly IWebsiteChannelContext websiteChannelContext;

        public ChannelCustomSettingsRepository(IInfoProvider<ChannelInfo> channelInfoProvider, IInfoProvider<ChannelCustomSettingsInfo> customSettingsInfoProvider, IWebsiteChannelContext websiteChannelContext)
        {
            this.channelInfoProvider = channelInfoProvider;
            this.customSettingsInfoProvider = customSettingsInfoProvider;
            this.websiteChannelContext = websiteChannelContext;
        }

        public async Task<bool> GetBoolValueAsync(string key, bool defaultValue = false, string channelName = null)
        {
            var value = await GetSettingsItemAsync(key, channelName);
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (value.Equals("0") || value.Equals("1"))
                {
                    return Convert.ToBoolean(Convert.ToInt16(value));
                }

                if (bool.TryParse(value, out var result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        public async Task<int> GetIntegerValueAsync(string key, int defaultValue = 0, string channelName = null)
        {
            var value = await GetSettingsItemAsync(key, channelName);
            if (int.TryParse(value, out var result))
            {
                return result;
            }

            return defaultValue;
        }

        public async Task<string> GetStringValueAsync(string key, string defaultValue = "", string channelName = null)
        {
            var value = await GetSettingsItemAsync(key, channelName);

            return value ?? defaultValue;
        }

        public async Task<ChannelCustomSettingsInfo> InsertOrUpdatedSettingsKey(string key, string value, int channelId)
        {
            var settings = await this.GetSettingsKey(key, channelId);
            if (settings == null)
            {
                settings = new ChannelCustomSettingsInfo()
                {
                    ChannelCustomSettingsKey = key,
                    ChannelCustomSettingsChannelID = channelId,
                    ChannelCustomSettingsValue = value
                };
            }

            settings.ChannelCustomSettingsValue = value;
            customSettingsInfoProvider.Set(settings);

            return settings;
        }

        public async Task<ChannelCustomSettingsInfo> GetSettingsKey(string key, int channelId)
        {
            return (await this.GetSettingsItemsAsync()).FirstOrDefault(channel => channel.ChannelCustomSettingsKey.Equals(key, StringComparison.InvariantCultureIgnoreCase)
                                                            && channel.ChannelCustomSettingsChannelID == channelId);
        }

        private async Task<string?> GetSettingsItemAsync(string key, string channelName)
        {
            var channelId = this.websiteChannelContext.WebsiteChannelID;
            if (!string.IsNullOrEmpty(channelName))
            {
                var channel = channelInfoProvider.Get().Where(site => site.ChannelType == ChannelType.Website)
                                                       .FirstOrDefault(chan => chan.ChannelName.Equals(channelName, StringComparison.InvariantCultureIgnoreCase));

                channelId = channel?.ChannelID ?? -1;
            }

            return (await GetSettingsItemsAsync())
                .Where(item => item.ChannelCustomSettingsKey.Equals(key, StringComparison.OrdinalIgnoreCase) && (item.ChannelCustomSettingsChannelID == channelId))
                .ToList()?
                .FirstOrDefault()?.ChannelCustomSettingsValue;
        }

        private async Task<IEnumerable<ChannelCustomSettingsInfo>> GetSettingsItemsAsync()
        {
            return await this.customSettingsInfoProvider.Get().GetEnumerableTypedResultAsync();
        }
    }
}