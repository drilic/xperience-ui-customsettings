using CMS.DataEngine;

namespace EXLRT.Xperience.UI.ChannelCustomSettings.Admin.Objects
{
    /// <summary>
    /// Class providing <see cref="ChannelCustomSettingsInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(IChannelCustomSettingsInfoProvider))]
    public partial class ChannelCustomSettingsInfoProvider : AbstractInfoProvider<ChannelCustomSettingsInfo, ChannelCustomSettingsInfoProvider>, IChannelCustomSettingsInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelCustomSettingsInfoProvider"/> class.
        /// </summary>
        public ChannelCustomSettingsInfoProvider()
            : base(ChannelCustomSettingsInfo.TYPEINFO)
        {
        }
    }
}