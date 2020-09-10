namespace Net.FreeLibrary.Core
{
    public struct KeyValue
    {

        public KeyValue(string key, object value)
        {
            _key = key;
            _value = value;
        }

        private string _key;
        /// <summary>
        /// Gets, Sets Key.
        /// </summary>
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private object _value;
        /// <summary>
        /// Gets, Sets Value.
        /// </summary>
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

    }
}
