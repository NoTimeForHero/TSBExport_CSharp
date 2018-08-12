using System.Configuration;

namespace TSBExport_CSharp
{
    // TODO: Refactor this to manual (de/se)rialization with color parametered constructor support (perfomance improvment and more clean code)
    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    public sealed class ConfigSettings : ApplicationSettingsBase
    {
        [UserScopedSetting]
        [SettingsSerializeAs(SettingsSerializeAs.Xml)]
        public GridSettings GridSettings
        {
            get { return (GridSettings)this[nameof(GridSettings)]; }
            set { this[nameof(GridSettings)] = value; }
        }
    }
}