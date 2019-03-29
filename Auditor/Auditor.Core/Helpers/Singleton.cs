using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auditor.Core.Helpers
{
    public abstract class Singleton<T> where T : new()
    {
        protected static readonly T _instance = new T();
        protected static readonly object _lock = new object();

        static Singleton() { }
        protected Singleton() { }

        public static T Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}