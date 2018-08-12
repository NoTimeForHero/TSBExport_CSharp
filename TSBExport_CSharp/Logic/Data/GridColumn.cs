using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace TSBExport_CSharp
{
    [DataContract]
    public class GridColumn
    {
        protected static List<Type> allowedTypes = new List<Type> { typeof(String), typeof(DateTime), typeof(Double), typeof(Int32) };

        public String format;

        public object Value1 { get; set; } // TODO: FIX PRIVATE SETTER
        public object Value2 { get; set; } // TODO: FIX PRIVATE SETTER

        public Type type => Value1.GetType(); // Don't know how to fix it without a lot of logic (which also break serialization lul)
        public bool Range => Value2 != null;

        // ReSharper disable once UnusedMember.Local (FOR XML SERIALIZATION)
        private GridColumn() {}

        public GridColumn(object value1, object value2 = null, string format = null)
        {
            if (value1 == null)
                throw new NoNullAllowedException("Required argument can't be null!");

            if (!allowedTypes.Contains(value1.GetType()))
                throw new ArgumentException($"Type \"{value1.GetType().FullName}\" is not allowed for {nameof(GridColumn)}!");

            this.format = format;
            this.Value1 = value1;


            if (value2 == null) return;

            if (!allowedTypes.Contains(value2.GetType()))
                throw new ArgumentException($"Type \"{value2.GetType().FullName}\" is not allowed for {nameof(GridColumn)}!");

            if (value1.GetType() != value2.GetType())
                throw new ArgumentException($"Type are not equals: {value1.GetType().FullName} and {value2.GetType().FullName}");

            this.Value2 = value2;
        }
    }
}