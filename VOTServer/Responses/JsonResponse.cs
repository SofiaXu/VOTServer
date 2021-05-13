using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VOTServer.Responses
{
    public class JsonResponse
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }
    }

    public class JsonResponse<T> : JsonResponse
    {
        public T Content { get; set; }
    }
}
