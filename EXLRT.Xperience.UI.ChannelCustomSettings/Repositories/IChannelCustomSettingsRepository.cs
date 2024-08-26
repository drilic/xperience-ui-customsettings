using EXLRT.Xperience.UI.ChannelCustomSettings.Admin.Objects;

namespace EXLRT.Xperience.UI.ChannelCustomSettings.Repositories
{
    public interface IChannelCustomSettingsRepository
    {
        Task<string> GetStringValueAsync(string key, string defaultValue = "", string channelName = null);

        Task<bool> GetBoolValueAsync(string key, bool defaultValue = false, string channelName = null);

        Task<int> GetIntegerValueAsync(string key, int defaultValue = 0, string channelName = null);

        Task<ChannelCustomSettingsInfo> InsertOrUpdatedSettingsKey(string key, string? value, int channelId);

        Task<ChannelCustomSettingsInfo> GetSettingsKey(string key, int channelId);
    }
}
