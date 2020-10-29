using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePlus.Blazor.Data.Https
{
    public class HttpResponseResult<T>
    {
        public T Result { get; set; }

        public string HttpResponseMessages { get; set; }
    }
}
