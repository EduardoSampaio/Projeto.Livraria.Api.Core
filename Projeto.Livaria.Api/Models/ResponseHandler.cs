using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;

namespace Projeto.Livaria.Api.Models
{
    /// <summary>
    /// Default Response WebApi
    /// </summary>
    public class ResponseHandler
    {
        private ResponseHandler(dynamic data, string apiVersion, DateTime timestamp, HttpStatusCode statusCode)
        {
            Data = data;
            ApiVersion = apiVersion;
            Timestamp = timestamp;
            StatusCode = statusCode;
        }

        private ResponseHandler(string apiVersion, DateTime timestamp, HttpStatusCode statusCode, Dictionary<string, string[]> errors)
        {
            ApiVersion = apiVersion;
            Timestamp = timestamp;
            StatusCode = statusCode;
            Errors = errors;
        }

        private ResponseHandler(string apiVersion, string messages, DateTime timestamp, HttpStatusCode statusCode)
        {
            ApiVersion = apiVersion;
            Messages = messages;
            Timestamp = timestamp;
            StatusCode = statusCode;
        }

        public dynamic Data { get; private set; }
        public string ApiVersion { get; private set; }
        public string Messages { get; private set; }
        public DateTime Timestamp { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public Dictionary<string,string[]> Errors { get; private set; }

        /// <summary>
        /// Build Response
        /// </summary>
        /// <param name="data"></param>
        /// <param name="apiVersion"></param>
        /// <param name="timestamp"></param>
        /// <param name="statusCode"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static ResponseHandler BuildResponse(dynamic data, string apiVersion, DateTime timestamp, HttpStatusCode statusCode, HttpResponse response)
        {
            response.StatusCode = (int)statusCode;
            return new ResponseHandler(data, apiVersion, timestamp, statusCode);
        }

        /// <summary>
        /// Build Response
        /// </summary>
        /// <param name="apiVersion"></param>
        /// <param name="timestamp"></param>
        /// <param name="statusCode"></param>
        /// <param name="errors"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static ResponseHandler BuildResponse(string apiVersion, DateTime timestamp, HttpStatusCode statusCode, Dictionary<string, string[]> errors, HttpResponse response)
        {
            response.StatusCode = (int)statusCode;
            return new ResponseHandler(apiVersion, timestamp, statusCode, errors);
        }

        /// <summary>
        /// Build Response 
        /// </summary>
        /// <param name="apiVersion"></param>
        /// <param name="messages"></param>
        /// <param name="timestamp"></param>
        /// <param name="statusCode"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static ResponseHandler BuildResponse(string apiVersion, string messages, DateTime timestamp, HttpStatusCode statusCode, HttpResponse response)
        {
            response.StatusCode = (int)statusCode;
            return new ResponseHandler(apiVersion, messages, timestamp, statusCode);
        }
    }
}
