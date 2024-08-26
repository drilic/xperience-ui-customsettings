using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using EXLRT.Xperience.UI.ChannelCustomSettings.Admin.Objects;
using System.Data;
using System.Runtime.Serialization;

[assembly: RegisterObjectType(typeof(ChannelCustomSettingsInfo), ChannelCustomSettingsInfo.OBJECT_TYPE)]

namespace EXLRT.Xperience.UI.ChannelCustomSettings.Admin.Objects
{
    /// <summary>
    /// Data container class for <see cref="ChannelCustomSettingsInfo"/>.
    /// </summary>
    [Serializable]
    public partial class ChannelCustomSettingsInfo : AbstractInfo<ChannelCustomSettingsInfo, IChannelCustomSettingsInfoProvider>, IInfoWithId
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "boilerplate.channelcustomsettings";


        /// <summary>
        /// Type information.
        /// </summary>
#warning "You will need to configure the type info."
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(ChannelCustomSettingsInfoProvider), OBJECT_TYPE, "Boilerplate.ChannelCustomSettings", "ChannelCustomSettingsID", null, null, null, null, null, null, null)
        {
            TouchCacheDependencies = true,
            DependsOn = new List<ObjectDependency>()
            {
                new ObjectDependency("ChannelCustomSettingsChannelID", "cms.channel", ObjectDependencyEnum.Required),
            },
        };


        /// <summary>
        /// Channel custom settings ID.
        /// </summary>
        [DatabaseField]
        public virtual int ChannelCustomSettingsID
        {
            get => ValidationHelper.GetInteger(GetValue(nameof(ChannelCustomSettingsID)), 0);
            set => SetValue(nameof(ChannelCustomSettingsID), value);
        }


        /// <summary>
        /// Channel custom settings key.
        /// </summary>
        [DatabaseField]
        public virtual string ChannelCustomSettingsKey
        {
            get => ValidationHelper.GetString(GetValue(nameof(ChannelCustomSettingsKey)), String.Empty);
            set => SetValue(nameof(ChannelCustomSettingsKey), value);
        }


        /// <summary>
        /// Channel custom settings value.
        /// </summary>
        [DatabaseField]
        public virtual string ChannelCustomSettingsValue
        {
            get => ValidationHelper.GetString(GetValue(nameof(ChannelCustomSettingsValue)), String.Empty);
            set => SetValue(nameof(ChannelCustomSettingsValue), value, String.Empty);
        }


        /// <summary>
        /// Channel custom settings channel ID.
        /// </summary>
        [DatabaseField]
        public virtual int ChannelCustomSettingsChannelID
        {
            get => ValidationHelper.GetInteger(GetValue(nameof(ChannelCustomSettingsChannelID)), 0);
            set => SetValue(nameof(ChannelCustomSettingsChannelID), value);
        }


        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            Provider.Delete(this);
        }


        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            Provider.Set(this);
        }


        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected ChannelCustomSettingsInfo(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="ChannelCustomSettingsInfo"/> class.
        /// </summary>
        public ChannelCustomSettingsInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="ChannelCustomSettingsInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public ChannelCustomSettingsInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}