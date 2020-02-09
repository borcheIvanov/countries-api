using System.Net;
using Microsoft.AspNetCore.Http;

namespace Countries.Api.DTModels
{
    public class ErrorResponseModel
    {
        public int? StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}