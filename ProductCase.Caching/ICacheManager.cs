﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCase.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        void Set(string key, object data, int cacheTime);
        bool IsSet(string key);
        void Remove(string key);
    }
}
