﻿using System.Net;
using UniversityApp.UI.Models;

namespace UniversityApp.UI.Exceptions
{
    public class HttpException:Exception
    {
        public HttpStatusCode Status { get;set; }

        public HttpException(HttpStatusCode status)
        {
            this.Status = status;
        }

        public HttpException(HttpStatusCode status, string message) : base(message)
        {
            this.Status = status;
        }

      
    }
}
