using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace TSBExport_CSharp
{
    public partial class FormGenerate : Form
    {
        private int y;
        public GridSettings ResultSettings = null;

        public FormGenerate(GridSettings gridSettings)
        {
            InitializeComponent();
            panelFields.Controls.Remove(panelExample);

            CRV_TS_Converter.Load(gridSettings, this);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAddField_Click(object sender, EventArgs e)
        {
            createDataPanel();
        }

        private void createDataPanel(bool EnabledRandomValue = true,
            ControlRangeValues.EnumValueType ValueType = ControlRangeValues.EnumValueType.Integer,
            string Text1 = "", string Text2 = "")
        {
            Panel panel = new Panel
            {
                BackColor = Color.Transparent,
                Size = new Size(683, 97),
                Location = new Point(0, y - panelFields.VerticalScroll.Value)
            };
            y += 97;

            ControlRangeValues dateInput = new ControlRangeValues
            {
                BackColor = Color.Transparent,
                Location = new Point(9, 18),
                Name = "ControlRangeValue",
                Size = new Size(582, 75),
                ErrorColor = Decorator.errorColor,
                PlaySoundOnValidationError = true,
                EnabledRandomValue = EnabledRandomValue,
                ValueType = ValueType,
                Text1 = Text1,
                Text2 = Text2,
            };

            Button btnDelete = new Button
            {
                BackColor = buttonExit.BackColor,
                ForeColor = protoFieldDelete.ForeColor,
                Location = new Point(599, 22),
                Name = "protoFieldDelete",
                Size = new Size(71, 35),
                TabIndex = 4,
                Text = "Delete",
                UseVisualStyleBackColor = false,
                CausesValidation = false
            };
            btnDelete.Click += (e2, v) => RemoveField(panel);

            panel.Controls.Add(dateInput);
            panel.Controls.Add(btnDelete);
            panelFields.Controls.Add(panel);
        }

        private void RemoveField(Control panel)
        {
            panelFields.Controls.Remove(panel);
            y = 0 - panelFields.VerticalScroll.Value;
            foreach (Control item in panelFields.Controls)
            {
                item.Location = new Point(item.Location.X, y);
                y += item.Size.Height;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Validation failed!\nPlease check all fields with red border!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ResultSettings = CRV_TS_Converter.Save(this);
            Close();
        }

        private void TextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!(sender is MaskedTextBoxWithBorder tb)) return;
            if (tb.Text == "" || !Int64.TryParse(tb.Text, out long unused))
            {
                tb.BorderColor = Decorator.errorColor;
                SystemSounds.Exclamation.Play();
                e.Cancel = true;
                return;
            }

            tb.BorderColor = ForeColor;
        }


        private static class CRV_TS_Converter
        {
            public static void Load(GridSettings gridSettings, FormGenerate form)
            {
                form.tbRecords.Text = gridSettings.records.ToString();
                form.tbSeed.Text = gridSettings.rndSeed.ToString();
                foreach (GridColumn col in gridSettings.columns)
                {
                    ControlRangeValues.EnumValueType? type = null;

                    if (col.type == typeof(String)) type = ControlRangeValues.EnumValueType.String;
                    if (col.type == typeof(Double)) type = ControlRangeValues.EnumValueType.Float;
                    if (col.type == typeof(Int32)) type = ControlRangeValues.EnumValueType.Integer;

                    if (col.type == typeof(DateTime))
                    {
                        type = ControlRangeValues.EnumValueType.Date;
                        DateTime dt1 = (DateTime) col.Value1;
                        DateTime dt2 = (DateTime) col.Value2;

                        form.createDataPanel(col.Range, type.Value,
                            dt1.ToString(ControlRangeValues.DateTime_Format,CultureInfo.InvariantCulture),
                            dt2.ToString(ControlRangeValues.DateTime_Format, CultureInfo.InvariantCulture));
                        continue;
                    }

                    if (!type.HasValue) throw new UnknownTypeException($"Unknown type: {col.type} for enum {nameof(ControlRangeValues.EnumValueType)}!");

                    form.createDataPanel(col.Range, type.Value, col.Value1.ToString(), col.Value2?.ToString());
                }
            }

            public static GridSettings Save(FormGenerate form)
            {
                var settings = new GridSettings
                {
                    rndSeed = Int32.Parse(form.tbSeed.Text),
                    records = Int32.Parse(form.tbRecords.Text)
                };

                foreach (Panel panel in form.panelFields.Controls)
                {
                    var control = panel.Controls.OfType<ControlRangeValues>().First();
                    if (control == null) throw new NoNullAllowedException($"Missing ${nameof(ControlRangeValues)} in panel inside {nameof(form.panelFields)}");
                    settings.columns.Add(new GridColumn(control.Text1, control.EnabledRandomValue ? control.Text2 : null));
                }

                return settings;
            }
        }
    }
}
