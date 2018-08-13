using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TSBExport_CSharp
{
    [DataContract]
    public class GridColumn
    {
        protected static List<Type> allowedTypes = new List<Type> { typeof(String), typeof(DateTime), typeof(Double), typeof(Int32) };

        public String format;

        private object _value1;
        private object _value2;

        public object Value1 {
            get => _value1;
            set
            {
                if (value == null)
                    throw new NoNullAllowedException("Required argument can't be null!");

                if (_value1 != null)
                    throw new FieldAccessException($"{nameof(Value1)} already initialized!");

                if (!allowedTypes.Contains(value.GetType()))
                    throw new ArgumentException($"Type \"{value.GetType().FullName}\" is not allowed for {nameof(GridColumn)}!");

                _value1 = value;
                type = value.GetType();
            }
        }

        public object Value2
        {
            get => _value2;
            set
            {
                if (value == null)
                    throw new NoNullAllowedException("Required argument can't be null!");

                if (_value2 != null)
                    throw new FieldAccessException($"{nameof(Value2)} already initialized!");

                if (!allowedTypes.Contains(value.GetType()))
                    throw new ArgumentException($"Type \"{value.GetType().FullName}\" is not allowed for {nameof(GridColumn)}!");

                if (_value1.GetType() != value.GetType())
                    throw new ArgumentException($"Type are not equals: {value.GetType().FullName} and {value.GetType().FullName}");

                _value2 = value;
                Range = true;
            }
        }

        [XmlIgnore]
        public Type type;
        public bool Range;

        // ReSharper disable once UnusedMember.Local (FOR XML SERIALIZATION)
        public GridColumn() {}

        public GridColumn(object value1, object value2 = null, string format = null)
        {
            Value1 = value1;
            if (value2 != null) Value2 = value2;
            this.format = format;
        }
    }
}