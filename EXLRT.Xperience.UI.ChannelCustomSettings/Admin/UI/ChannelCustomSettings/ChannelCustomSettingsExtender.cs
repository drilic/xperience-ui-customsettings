using AngleSharp.Text;
using CMS.Base;
using CMS.ContentEngine;
using CMS.Core;
using CMS.DataEngine;
using EXLRT.Xperience.UI.ChannelCustomSettings.Admin;
using EXLRT.Xperience.UI.ChannelCustomSettings.Configuration.Attributes;
using EXLRT.Xperience.UI.ChannelCustomSettings.Models;
using EXLRT.Xperience.UI.ChannelCustomSettings.Repositories;
using EXLRT.Xperience.UI.ChannelCustomSettings.Utility;
using Kentico.Xperience.Admin.Base;
using Kentico.Xperience.Admin.Base.Forms;
using System.Reflection;

[assembly: UIPage(parentType: typeof(Kentico.Xperience.Admin.Base.UIPages.ChannelEditSection),
    slug: "channel-custom-settings",
    uiPageType: typeof(ChannelCustomSettingsExtender),
    name: "Custom settings",
    templateName: TemplateNames.EDIT,
    order: UIPageOrder.NoOrder)]
namespace EXLRT.Xperience.UI.ChannelCustomSettings.Admin
{
    public class ChannelCustomSettingsExtender : ModelEditPage<ChannelCustomSettingsViewModel>
    {
        private readonly IChannelCustomSettingsRepository channelCustomSettings;
        private readonly IInfoProvider<ChannelInfo> channelInfoProvider;

        private ChannelCustomSettingsViewModel _model;

        public ChannelCustomSettingsExtender(Kentico.Xperience.Admin.Base.Forms.Internal.IFormItemCollectionProvider formItemCollectionProvider,
                                                                         IFormDataBinder formDataBinder,
                                                                         IChannelCustomSettingsRepository customChannelSettings,
                                                                         IInfoProvider<ChannelInfo> channelInfoProvider) : base(formItemCollectionProvider, formDataBinder)
        {
            this.channelCustomSettings = customChannelSettings;
            this.channelInfoProvider = channelInfoProvider;
        }

        protected override ChannelCustomSettingsViewModel Model
        {
            get
            {
                this._model ??= new ChannelCustomSettingsViewModel();

                return this._model;
            }
        }

        [PageParameter(typeof(IntPageModelBinder))]
        public int ObjectId { get; set; }

        public override Task ConfigurePage()
        {
            this.PageConfiguration.SubmitConfiguration.Label = "Save";

            var channel = this.channelInfoProvider.Get(this.ObjectId);
            if (channel?.ChannelType != ChannelType.Website)
            {
                this.PageConfiguration.SubmitConfiguration.Visible = false;
                this.PageConfiguration.Disabled = true;
                this.PageConfiguration.Headline = "Channel custom settings is not supported.";

                this.PageConfiguration.Callouts = new List<CalloutConfiguration>()
                {
                    new()
                    {
                        Headline = "NOT SUPPORTED!",
                        Content = "<p>Channel custom settings is supported only for <strong>Website</strong> channels!</p>",
                        ContentAsHtml = true,
                        Type = CalloutType.FriendlyWarning
                    }
                };
            }
            else
            {
                this.PopulateData();
            }

            return base.ConfigurePage();
        }

        protected override async Task<ICommandResponse> ProcessFormData(ChannelCustomSettingsViewModel model, ICollection<IFormItem> formItems)
        {
            if (model == null)
            {
                return this.GetErrorResponse("Something went wrong. Please try later!");
            }

            try
            {
                await SaveDataAsync();
            }
            catch (Exception ex)
            {
                this.EventLogService.LogException($"{nameof(ChannelCustomSettingsExtender)} -> UNEXPECTED ERROR", $"{nameof(ChannelCustomSettingsExtender)}", ex);

                return this.GetErrorResponse($"Unable to save data. Check event logs for more details!");
            }

            return this.GetSuccessResponse("Settings saved successfully!");
        }

        private async Task SaveDataAsync()
        {
            var settingsProperties = CustomChannelSettingsReflectionHelper.GetAllPropertiesWithAttribute(this.Model.GetType(), typeof(XperienceSettingsDataAttribute));
            if (settingsProperties != null)
            {
                foreach (var prop in settingsProperties)
                {
                    var settingsKey = prop.GetCustomAttribute<XperienceSettingsDataAttribute>();
                    if (settingsKey == null)
                    {
                        continue;
                    }

                    await this.channelCustomSettings.InsertOrUpdatedSettingsKey(settingsKey.Name, CustomChannelSettingsReflectionHelper.GetPropertyValue(this.Model, prop.Name)?.ToString(), this.ObjectId);
                }
            }
        }

        private void PopulateData()
        {
            var settingsProperties = CustomChannelSettingsReflectionHelper.GetAllPropertiesWithAttribute(this.Model.GetType(), typeof(XperienceSettingsDataAttribute));
            if (settingsProperties != null)
            {
                foreach (var prop in settingsProperties)
                {
                    var settingsKey = prop.GetCustomAttribute<XperienceSettingsDataAttribute>();
                    if (settingsKey == null)
                    {
                        continue;
                    }

                    var settings = this.channelCustomSettings.GetSettingsKey(settingsKey.Name, this.ObjectId).Result;
                    if (settings != null)
                    {
                        object val = null;
                        switch (Type.GetTypeCode(prop.PropertyType))
                        {
                            case TypeCode.Boolean:
                                {
                                    val = settings.ChannelCustomSettingsValue.ToBoolean(settingsKey.DefaultValue.ToBoolean(false));
                                    break;
                                }
                            case TypeCode.Int32:
                                {
                                    val = settings.ChannelCustomSettingsValue.ToInteger(settingsKey.DefaultValue.ToInteger(int.MinValue));
                                    break;
                                }
                            default:
                                //string
                                val = settings.ChannelCustomSettingsValue;
                                break;
                        }

                        CustomChannelSettingsReflectionHelper.SetPropertyValue(this.Model, prop.Name, val);
                    }
                    else
                    {
                        CustomChannelSettingsReflectionHelper.SetPropertyValue(this.Model, prop.Name, settingsKey.DefaultValue);
                    }
                }
            }
        }

        private ICommandResponse<FormSubmissionResult> GetErrorResponse(params string[] messages)
        {
            var response = this.ResponseFrom(new FormSubmissionResult(FormSubmissionStatus.Error));

            foreach (var message in messages)
            {
                response.AddErrorMessage(message);
            }

            return response;
        }

        private ICommandResponse<FormSubmissionResult> GetSuccessResponse(string message)
        {
            var response = this.ResponseFrom(new FormSubmissionResult(FormSubmissionStatus.ValidationSuccess));

            response.AddSuccessMessage(message);

            return response;
        }
    }
}