using System;

namespace AcademicInfo.Models
{
    public class Response
    {
        public Response() { }
        public Response(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        public Response(bool success, string message, List<string> errors)
        {
            Success = success;
            Message = message;
            Errors = errors;
        }

        public bool? Success { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public object? ResponseValue { get; set; }
    }
}
