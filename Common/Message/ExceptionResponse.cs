﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain;

namespace Common.Exception
{
    public class ExceptionResponse
    {
        public DateTime TimeStamp { get; }
        public string Message { get; }
        public Error ErrorCode { get; }
        public Dictionary<string,object> Errors { get; }

        public ExceptionResponse(string message, Error errorCode)
        {
            Message = message;
            ErrorCode = errorCode;
            TimeStamp = DateTime.Now;
            Errors = new Dictionary<string, object>();
        }

        public ExceptionResponse(string message, Error errorCode, Dictionary<string,object> errors )
        {
            Message = message;
            ErrorCode = errorCode;
            TimeStamp = DateTime.Now;
            Errors = errors;
        }
    }
}
