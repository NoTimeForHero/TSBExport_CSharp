using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSBExport_CSharp.Other;

namespace TSBExport_CSharp.GUI.Controls
{
    [SuppressMessage("ReSharper", "ArrangeAccessorOwnerBody")] // Visual Studio has a strange bug with lambda expression getter
    public partial class ControlRangeValues : UserControl
    {
        private static readonly string DateTime_Delimiter = "  -  ";
        private static readonly string DateTime_Mask = "00/00/0000" + DateTime_Delimiter + "00:00:00";
        public static readonly string DateTime_Format = "dd.MM.yyyy" + DateTime_Delimiter + "HH:mm:ss";

        public enum EnumValueType
        {
            String,
            Integer,
            Float,
            Date
        }

        [Category("ControlDateInput")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] // Attributes to change property in Desinger IMMEDIATELY
        //[ListBindable(true), Editor(typeof(ComboBox), typeof(UITypeEditor))]
        public EnumValueType ValueType
        {
            get
            {
                return _valueType;
            }
            set
            {
                _valueType = value;
                mtbValue1.Text = "";
                mtbValue2.Text = "";
                OnRenderUpdate();
            }
        }

        [Category("ControlDateInput")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool EnabledRandomValue
        {
            get { return _enabledRandomValue; }
            set
            {
                _enabledRandomValue = value;
                OnRenderUpdate();
            }
        }

        [Category("ControlDateInput")]
        [DefaultValue(typeof(Color), "Red")]
        public Color ErrorColor
        {
            get { return _errorColor; }
            set
            {
                _errorColor = value;
                Invalidate();
            }
        }

        [Category("ControlDateInput")]
        [DefaultValue(typeof(Color), "Black")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color DefaultColor
        {
            get { return _defaultColor; }
            set
            {
                _defaultColor = value;
                OnRenderUpdate();
            }
        }

        [Category("ControlDateInput")]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool PlaySoundOnValidationError { get; set; }

        public Object Text1
        {
            get { return returnCorrectValue(mtbValue1.Text, false); }
            set { mtbValue1.Text = value?.ToString() ?? ""; }
        }

        public Object Text2
        {
            get { return returnCorrectValue(mtbValue2.Text, false); }
            set {  mtbValue2.Text = value?.ToString() ?? ""; }
        }

        protected EnumValueType _valueType = EnumValueType.String;
        protected bool _enabledRandomValue = false;
        private Color _errorColor = Color.Red;
        private Color _defaultColor = Color.Black;

        public ControlRangeValues()
        {
            InitializeComponent();
            cbType.DataSource = Enum.GetValues(typeof(EnumValueType));
            cbType.SelectedIndexChanged += (e,v) => ValueType = (EnumValueType) cbType.SelectedItem;
            cbType.CausesValidation = false;
            chkRandom.CheckedChanged += (e, v) => EnabledRandomValue = chkRandom.Checked;
            OnRenderUpdate();
        }

        public void OnRenderUpdate()
        {
            this.SuspendLayout();
            chkRandom.Visible = _valueType != EnumValueType.String;
            chkRandom.Checked = _enabledRandomValue;
            cbType.SelectedItem = _valueType;

            var span = !_enabledRandomValue || _valueType == EnumValueType.String ? 2 : 1;
            tableLayoutPanel1.SetColumnSpan(mtbValue1, span);

            mtbValue2.Visible = _enabledRandomValue && _valueType != EnumValueType.String;

            labelInfo.ForeColor = _defaultColor;
            mtbValue1.BorderColor = _defaultColor;
            mtbValue2.BorderColor = _defaultColor;
            mtbValue1.ForeColor = _defaultColor;
            mtbValue2.ForeColor = _defaultColor;
            chkRandom.ForeColor = _defaultColor;

            switch (_valueType)
            {
                case EnumValueType.Date:
                    mtbValue1.Mask = DateTime_Mask;
                    mtbValue2.Mask = DateTime_Mask;

                    labelInfo.Text = DateTime_Format;
                    break;
                case EnumValueType.String:
                    mtbValue1.Mask = "";
                    mtbValue2.Mask = "";

                    labelInfo.Text = "{row} - for current row, {n} - new line";
                    break;
                case EnumValueType.Float:
                case EnumValueType.Integer:
                    mtbValue1.Mask = "";
                    mtbValue2.Mask = "";
                    labelInfo.Text = "";
                    break;
                default:
                    throw new UnknownTypeException("Unknown type: " + _valueType);
            }

            // Redraw
            this.ResumeLayout();
            Invalidate();
        }

        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        private void MaskedTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (!(sender is MaskedTextBoxWithBorder mtb)) return;
            if (!EnabledRandomValue && ReferenceEquals(mtb, mtbValue2)) return;

            try
            {
                // This method throws exception on failed 
                returnCorrectValue(mtb.Text, true);
                OnRenderUpdate();
            }
            catch (FormatException)
            {
                e.Cancel = true;
                if (PlaySoundOnValidationError) SystemSounds.Exclamation.Play();

                labelInfo.ForeColor = _errorColor;
                mtb.BorderColor = _errorColor;
                labelInfo.Text = $"Invalid {_valueType} format!";
            }
            catch (OverflowException)
            {
                e.Cancel = true;
                if (PlaySoundOnValidationError) SystemSounds.Exclamation.Play();

                labelInfo.ForeColor = _errorColor;
                mtb.BorderColor = _errorColor;
                labelInfo.Text = $"Overflow for type {_valueType}!";
            }
        }

        private Object returnCorrectValue(String text, bool allowThrow)
        {
            try
            {
                switch (_valueType)
                {
                    case EnumValueType.Float:
                        return Double.Parse(text);
                    case EnumValueType.Integer:
                        return Int32.Parse(text);
                    case EnumValueType.Date:
                        return DateTime.ParseExact(text, DateTime_Format, CultureInfo.InvariantCulture);
                    case EnumValueType.String:
                        return text;
                    default:
                        throw new UnknownTypeException("Unknown type: " + _valueType);
                }
            }
            catch (Exception ex) when (!allowThrow)
            {
                if (ex is FormatException) return null;
                if (ex is OverflowException) return null;

                throw;
            }
        }

        private void MaskedTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!(sender is MaskedTextBoxWithBorder txtBox)) return;

            if (e.KeyCode == Keys.Tab && _valueType == EnumValueType.Date)
            {
                int posOfPart2 = txtBox.Mask.IndexOf(DateTime_Delimiter, StringComparison.Ordinal);
                if (txtBox.SelectionStart <= posOfPart2)
                {
                    txtBox.SelectionStart = posOfPart2 + DateTime_Delimiter.Length;
                    e.IsInputKey = true;
                }
            }
        }
    }
}
