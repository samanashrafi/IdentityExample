using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityExample.DomainClasses
{
    public class RequestData
    {
        public int current { get; set; }
        public int rowCount { get; set; }
        public string searchPhrase { get; set; }
        public IEnumerable<SortData> sortItems { get; set; }
        //SortData sort = new SortData();
        //Dictionary<string, string> sortDictionary = new Dictionary<string, string>();
    }  

    public class SortData
    {
        public string Field { get; set; } // FIeld Name
        public string Type { get; set; } // ASC or DESC
    }

    public class ResponseData<T> where T : class
    {
        public int current { get; set; } // current page
        public int rowCount { get; set; } // rows per page
        public T rows { get; set; } // items
        public int total { get; set; } // total rows for whole query
    }
}