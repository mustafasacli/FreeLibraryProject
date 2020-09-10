using FreeLibrary.Core.Parameter;
using System.Collections.Generic;
using System.Data;

namespace FreeLibrary.Core.Parameter
{
    /// <summary>
    /// Defines key-value Pairs with ParameterDirection.
    /// </summary>
    public sealed class Hashmap
    {
        //private List<string> _keys;
        //private Hashtable keyValColl;
        private List<FreeParameter> _paramList;

        /// <summary>
        /// Create new instance of Hashmap object.
        /// </summary>
        public Hashmap()
        {
            //_keys = new List<string>();
            //keyValColl = new Hashtable();
            _paramList = new List<FreeParameter>();
        }

        /// <summary>
        /// Gets Parameter List.
        /// </summary>
        public List<FreeParameter> Parameters { get { return _paramList; } }

        //public List<string> Keys { get { return _keys; } }

        public List<FreeDbParameter> GetParameters()
        {
            List<FreeDbParameter> prms = null;

            List<FreeParameter> frmPrmLst = Parameters ?? new List<FreeParameter>();

            if (frmPrmLst.Count > 0)
            {
                prms = new List<FreeDbParameter>();

                foreach (FreeParameter item in frmPrmLst)
                {
                    prms.Add(new FreeDbParameter
                    {
                        ParameterName = item.Name,
                        Direction = item.Direction,
                        Value = item.Value
                    });
                }
            }

            return prms;
        }

        /// <summary>
        /// Adds or Updates Value for given key.
        /// </summary>
        /// <param name="key">key for parametername.</param>
        /// <param name="value">value for given key</param>
        public void Set(string key, object value)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                int indx = _paramList.IndexOf(new FreeParameter { Name = key });
                FreeParameter prmtr;
                if (indx != -1)
                {
                    prmtr = _paramList[indx];
                    prmtr.Value = value;
                }
                else
                {
                    prmtr = new FreeParameter(key, value);
                    prmtr.Direction = ParameterDirection.Input;
                    _paramList.Add(prmtr);
                }
            }
        }

        /// <summary>
        /// Adds or Updates Value for given key with ParameterDirection.
        /// </summary>
        /// <param name="key">key for parametername.</param>
        /// <param name="value">value for given key</param>
        /// <param name="direction">ParameterDirection for given key.</param>
        public void Set(string key, object value, ParameterDirection direction)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                int indx = _paramList.IndexOf(new FreeParameter { Name = key });
                FreeParameter prmtr;
                if (indx != -1)
                {
                    prmtr = _paramList[indx];
                    prmtr.Value = value;
                    prmtr.Direction = direction;
                }
                else
                {
                    prmtr = new FreeParameter(key, value);
                    prmtr.Direction = direction;
                    _paramList.Add(prmtr);
                }
            }
        }

        /// <summary>
        /// Sets Direction Of Parameter with given key.
        /// </summary>
        /// <param name="key">key for parametername.</param>
        /// <param name="direction">ParameterDirection for given key.</param>
        public void SetDirection(string key, ParameterDirection direction)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                int indx = _paramList.IndexOf(new FreeParameter { Name = key });
                FreeParameter prmtr;
                if (indx != -1)
                {
                    prmtr = _paramList[indx];
                    prmtr.Direction = direction;
                    _paramList[indx] = prmtr;
                }
            }
        }
        /// <summary>
        /// Gets Value of Parameter with given key.
        /// </summary>
        /// <param name="key">key for parametername.</param>
        /// <returns>Return an object.</returns>
        public object Get(string key)
        {
            object obj = null;

            if (string.IsNullOrWhiteSpace(key))
            {
                return obj;
            }

            int indx = _paramList.IndexOf(new FreeParameter { Name = key });

            if (indx != -1)
            {
                FreeParameter prmtr;
                prmtr = _paramList[indx];
                obj = prmtr.Value;
            }

            return obj;
        }

        /// <summary>
        /// Remove Parameter from list with given key.
        /// </summary>
        /// <param name="key">key for parametername.</param>
        public void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }

            int indx = _paramList.IndexOf(new FreeParameter { Name = key });

            if (indx != -1)
            {
                _paramList.RemoveAt(indx);
            }
        }

        /// <summary>
        /// if Parameter List is empty or null return true, else return false.
        /// </summary>
        /// <returns>Return abool object.</returns>
        public bool IsEmpty()
        {
            bool result = true;

            if (_paramList != null)
            {
                result = _paramList.Count < 1;
            }

            return result;
        }
    }
}