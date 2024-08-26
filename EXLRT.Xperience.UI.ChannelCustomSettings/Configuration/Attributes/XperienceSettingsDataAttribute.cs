namespace EXLRT.Xperience.UI.ChannelCustomSettings.Configuration.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    sealed class XperienceSettingsDataAttribute : Attribute
    {
        public string Name { get; set; }

        public object DefaultValue { get; set; }

        public XperienceSettingsDataAttribute(string name, object defaultValue = null)
        {

            Name = name;
            DefaultValue = defaultValue;
        }
    }
}