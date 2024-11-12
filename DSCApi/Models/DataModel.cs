using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCApi.Models
{
    public class DataModel<T>
    {
        public IEnumerable<T> Data { get; set; }
        public DataState State { get; set; }
        public string Message { get; set; }
    }

    public enum DataState
    {
        error = 0,
        ok = 1
    }
}