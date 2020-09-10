using System;
using System.Data;

namespace FreeLibrary.Core.Parameter
{
    public struct FreeParameter
    {
        private string _name;
        private object _value;
        private ParameterDirection _direction;

        public FreeParameter(string name, object value)
            : this(name, value, ParameterDirection.Input)
        {
        }

        public FreeParameter(string name, object value, ParameterDirection direction)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Parameter name can not be null, empty or white space string.");

            this._name = name;
            this._value = value;
            this._direction = direction;
        }

        public string Name
        {
            get { return _name ?? string.Empty; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Parameter name can not be null, empty or white space string.");

                _name = value;
            }
        }

        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                _value = _value ?? DBNull.Value;
            }
        }

        public ParameterDirection Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public override bool Equals(object obj)
        {
            bool result = false;

            if (obj is FreeParameter)
            {
                FreeParameter prm = (FreeParameter)obj;

                result = this.Name.Equals(prm.Name);
            }

            return result;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}