using Newtonsoft.Json;
using static FarmEase.Domain.Helper.Constants;

namespace FarmEase.Domain.DTO
{
    /// <summary>
    /// Generic API Response Object
    /// </summary>
    [Serializable]
    public class ApiResponse
    {
        public ApiResponse() { }

        public ApiResponse(object data)
        {
            Data = data;
            Success = true;
            Error = null;
        }

        public ApiResponse(ApiError error)
        {
            Error = error;
            Success = false;
            Data = null;
        }

        public ApiResponse(object data, bool success, ApiError error)
        {
            Data = data;
            Success = success;
            Error = error;
        }

        /// <summary>
        /// Specifies Response Success or Failure
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// Generic Data Object
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }

        /// <summary>
        /// Custom Error if response is not success
        /// </summary>
        [JsonProperty(PropertyName = "error")]
        public ApiError Error { get; set; }
    }

    /// <summary>
    /// API Response Object With Type
    /// </summary>
    [Serializable]
    public class ApiResponse<T>
    {
        public ApiResponse() { }

        public ApiResponse(T data)
        {
            Data = data;
            Success = true;
            Error = null;
        }

        public ApiResponse(ApiError error)
        {
            Error = error;
            Success = false;
            Data = default(T);
        }

        public ApiResponse(T data, bool success, ApiError error)
        {
            Data = data;
            Success = success;
            Error = error;
        }

        /// <summary>
        /// Specifies Response Success or Failure
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// Generic Data Object
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }

        /// <summary>
        /// Custom Error if response is not success
        /// </summary>
        [JsonProperty(PropertyName = "error")]
        public ApiError Error { get; set; }
    }

    /// <summary>
    /// Custom Error if response is not success
    /// </summary>
    [Serializable]
    public class ApiError
    {
        public ApiError() { }
        public ApiError(string errorMessage, ErrorCode errorCode)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Custom Error Message
        /// </summary>
        [JsonProperty(PropertyName = "errorMessage")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Custom Error Code
        /// </summary>
        [JsonProperty(PropertyName = "errorCode")]
        public ErrorCode ErrorCode { get; set; }
    }
}
