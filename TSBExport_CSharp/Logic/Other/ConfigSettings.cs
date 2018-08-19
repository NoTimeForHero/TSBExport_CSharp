using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using TSBExport_CSharp.Grid;

namespace TSBExport_CSharp.Other
{
    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    public sealed class ConfigSettings : ApplicationSettingsBase
    {
        [UserScopedSetting]
        public GridSettings GridSettings
        {
            get => (GridSettings)this[nameof(GridSettings)];
            set => this[nameof(GridSettings)] = value;
        }

        [UserScopedSetting]
        public Point? WindowLocation
        {
            get => (Point?)this[nameof(WindowLocation)];
            set => this[nameof(WindowLocation)] = value;
        }

        [UserScopedSetting]
        public Size? WindowSize
        {
            get => (Size?)this[nameof(WindowSize)];
            set => this[nameof(WindowSize)] = value;
        }

        [UserScopedSetting]
        public List<int> ColumnsWidth
        {
            get => (List<int>)this[nameof(ColumnsWidth)];
            set => this[nameof(ColumnsWidth)] = value;
        }

        [UserScopedSetting]
        public string CurrentApperance
        {
            get => (string)this[nameof(CurrentApperance)];
            set => this[nameof(CurrentApperance)] = value;
        }

        [UserScopedSetting]
        public bool ForceThrowExcelExceptions
        {
            get => (bool)this[nameof(ForceThrowExcelExceptions)];
            set => this[nameof(ForceThrowExcelExceptions)] = value;
        }
    }
}