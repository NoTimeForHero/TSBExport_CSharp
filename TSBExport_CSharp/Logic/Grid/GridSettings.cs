using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TSBExport_CSharp.Grid
{
    [DataContract]
    public class GridSettings
    {
        public int rndSeed = 0;
        public int records = 0;

        public List<GridColumn> columns = new List<GridColumn>();

        public DataTable getActualData()
        {
            DataTable data = new DataTable();

            for (int i = 0; i < columns.Count; i++)
            {
                data.Columns.Add(new DataColumn());
            }

            DataRow header = data.NewRow();
            for (int i = 0; i < data.Columns.Count; i++)
                header[i] = i == 0 ? "#" : $"Header_{i}";
            data.Rows.Add(header);

            // Array of random because columns should be independent of each other
            Random[] seedDataGrid = new Random[data.Columns.Count];
            for (int i = 0; i < data.Columns.Count; i++) seedDataGrid[i] = new Random(rndSeed);

            // GENERATE DATA
            for (int i = 0; i < records; i++)
            {
                DataRow row = data.NewRow();
                data.Rows.Add(fillRow(row, seedDataGrid, i + 1));
            }

            DataRow footer = data.NewRow();
            for (int i = 0; i < data.Columns.Count; i++)
                footer[i] = i == 0 ? "#" : $"Footer_{i}";
            data.Rows.Add(footer);

            return data;
        }

        private DataRow fillRow(DataRow row, Random[] seedDataGrid, int index)
        {
            for (int j = 0; j < columns.Count; j++)
            {
                var setting = columns[j];

                if (setting.type == typeof(String))
                {
                    row[j] = ((string)setting.Value1).Replace("{row}", index.ToString());
                    continue;
                }

                if (setting.type == typeof(Double))
                {
                    row[j] = setting.Range
                        ? seedDataGrid[j].NextDouble() * ((double)setting.Value2 - (double)setting.Value1) + (double)setting.Value1
                        : (double)setting.Value1;
                    continue;
                }

                if (setting.type == typeof(Int32))
                {

                    row[j] = setting.Range
                        ? seedDataGrid[j].Next((int)setting.Value1, (int)setting.Value2)
                        : (int)setting.Value1;
                    continue;
                }

                if (setting.type == typeof(DateTime))
                {
                    if (!setting.Range)
                    {
                        row[j] = (DateTime)setting.Value1;
                        continue;
                    }

                    long ticks1 = ((DateTime)setting.Value1).Ticks;
                    long ticks2 = ((DateTime)setting.Value2).Ticks;

                    row[j] = new DateTime(seedDataGrid[j].Next(ticks1, ticks2));
                    continue;
                }

                throw new UnknownTypeException($"Unknown type: {setting.type}");
            }

            return row;
        }
    }
}