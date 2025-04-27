using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Post.Common.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }

        public ApiResponse(bool success, string message, T data, Dictionary<string, string[]> errors = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors ?? new Dictionary<string, string[]>();
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
            => new ApiResponse<T>(true, message, data);

        public static ApiResponse<T> ErrorResponse(string message, Dictionary<string, string[]> errors = null)
            => new ApiResponse<T>(false, message, default, errors);

        public static ApiResponse<object> ErrorResponse(string message, object errors)
        {
            Dictionary<string, string[]> errorDict = new();

            // Try casting to Dictionary directly if possible
            if (errors is Dictionary<string, string[]> dict)
            {
                errorDict = dict;
            }
            else
            {
                // Fallback: put the raw object into the dictionary under a generic key
                errorDict["General"] = new[] { errors?.ToString() ?? "Unknown error" };
            }

            return new ApiResponse<object>(false, message, null, errorDict);
        }


    }
}
