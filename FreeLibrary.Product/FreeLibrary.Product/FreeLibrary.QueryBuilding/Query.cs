using System;
using FreeLibrary.Core;
using FreeLibrary.Core.Parameter;

namespace FreeLibrary.QueryBuilding
{
    /// <summary>
    /// Description of Query.
    /// </summary>
    public class Query : IQuery
    {
        private Hashmap h_prms = null;
        private string text_ = string.Empty;

        public Query()
        {
            h_prms = new Hashmap();
        }

        public Hashmap Parameters
        {
            get { return h_prms; }
            set { h_prms = value; }
        }

        public string Text
        {
            get { return text_; }
            set { text_ = value; }
        }
        
    }
}