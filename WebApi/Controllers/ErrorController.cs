﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class ErrorController : ApiController
    {
        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
	    public HttpResponseMessage NotFound()
	    {
	        var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
	        responseMessage.ReasonPhrase = "The requested resource is not found.";
	        return responseMessage;
	    }
    }
}
