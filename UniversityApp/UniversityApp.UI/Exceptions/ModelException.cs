﻿using System.Net;
using UniversityApp.UI.Models;

namespace UniversityApp.UI.Exceptions
{
    public class ModelException:HttpException
    {
        public ErrorResponse Error { get; set; }

        public ModelException(HttpStatusCode status, ErrorResponse error):base(status)
        {
            this.Error = error;
        }

        public ModelException(HttpStatusCode status, ErrorResponse error,string message) : base(status, message)
        {
            this.Error = error;
        }
    }
}
