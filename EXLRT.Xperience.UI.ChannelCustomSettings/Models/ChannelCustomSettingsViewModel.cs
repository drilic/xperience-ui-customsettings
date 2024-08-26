using EXLRT.Xperience.UI.ChannelCustomSettings.Configuration.Attributes;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using Kentico.Xperience.Admin.Websites.FormAnnotations;

namespace EXLRT.Xperience.UI.ChannelCustomSettings.Models
{
    [FormCategory(Label = "Pages", Order = 100, Collapsible = true, IsCollapsed = false)]
    [FormCategory(Label = "Security", Order = 200, Collapsible = true, IsCollapsed = true)]
    [FormCategory(Label = "Sitemap", Order = 300, Collapsible = true, IsCollapsed = true)]
    public class ChannelCustomSettingsViewModel
    {
        // pages
        [UrlSelectorComponent(Label = "Not Found URL", Order = 101)]
        [XperienceSettingsData("Pages.NotFoundURL")]
        public string NotFoundURL { get; set; }


        // security
        [NumberInputComponent(Label = "Basic Auth Status", Order = 201, ExplanationText = "Basic Authentication, to prevent accessing (0 : Ignore, 1 : All Non-Admin, 2 : Hangfire Only)")]
        [MinimumIntegerValueValidationRule(0)]
        [MaximumIntegerValueValidationRule(2)]
        [XperienceSettingsData("Security.BasicAuthStatus", 1)]
        public int BasicAuthStatus { get; set; }


        // sitemap
        [CheckBoxComponent(Label = "Multilingual Enable", Order = 301)]
        [XperienceSettingsData("Sitemap.MultilingualEnable", false)]
        public bool MultilingualEnable { get; set; }

        [NumberInputComponent(Label = "Page Size", Order = 302)]
        [XperienceSettingsData("Sitemap.PageSize", 50000)]
        public int PageSize { get; set; }
    }
}