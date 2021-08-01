using System;
using System.Collections.Generic;

namespace Milvasoft.Iyzipay.Utils.Concrete
{
    public class ToStringRequestBuilder
    {
        private string _requestString;

        private ToStringRequestBuilder(string requestString)
        {
            this._requestString = requestString;
        }

        public static ToStringRequestBuilder NewInstance()
        {
            return new ToStringRequestBuilder("");
        }

        public static ToStringRequestBuilder NewInstance(string requestString)
        {
            return new ToStringRequestBuilder(requestString);
        }

        public ToStringRequestBuilder AppendSuper(string superRequestString)
        {
            if (superRequestString != null)
            {
                superRequestString = superRequestString[1..];
                superRequestString = superRequestString[0..^1];

                if (superRequestString.Length > 0)
                {
                    this._requestString = this._requestString + superRequestString + ",";
                }
            }
            return this;
        }

        public ToStringRequestBuilder Append(string key, Object value = null)
        {
            if (value != null)
            {
                if (value is IRequestStringConvertible convertible)
                {
                    AppendKeyValue(key, convertible.ToPKIRequestString());
                }
                else
                {
                    AppendKeyValue(key, value.ToString());
                }
            }
            return this;
        }

        public ToStringRequestBuilder AppendPrice(string key, string value)
        {
            if (value != null)
            {
                AppendKeyValue(key, RequestFormatter.FormatPrice(value));
            }
            return this;
        }

        public ToStringRequestBuilder AppendList<T>(string key, List<T> list = null) where T : IRequestStringConvertible
        {
            if (list != null)
            {
                string appendedValue = "";
                foreach (IRequestStringConvertible value in list)
                {
                    appendedValue = appendedValue + value.ToPKIRequestString() + ", ";
                }
                AppendKeyValueArray(key, appendedValue);
            }
            return this;
        }

        public ToStringRequestBuilder AppendList(string key, List<int> list = null)
        {
            if (list != null)
            {
                string appendedValue = "";
                foreach (int value in list)
                {
                    appendedValue = appendedValue + value + ", ";
                }
                AppendKeyValueArray(key, appendedValue);
            }
            return this;
        }

        private ToStringRequestBuilder AppendKeyValue(string key, string value)
        {
            if (value != null)
            {
                this._requestString = this._requestString + key + "=" + value + ",";
            }
            return this;
        }

        private ToStringRequestBuilder AppendKeyValueArray(string key, string value)
        {
            if (value != null)
            {
                value = value[0..^2];
                this._requestString = this._requestString + key + "=[" + value + "],";
            }
            return this;
        }

        private ToStringRequestBuilder AppendPrefix()
        {
            this._requestString = "[" + this._requestString + "]";
            return this;
        }

        private ToStringRequestBuilder RemoveTrailingComma()
        {
            if (!string.IsNullOrEmpty(this._requestString))
            {
                this._requestString = this._requestString[0..^1];
            }
            return this;
        }

        public string GetRequestString()
        {
            RemoveTrailingComma();
            AppendPrefix();
            return _requestString;
        }
    }
}
