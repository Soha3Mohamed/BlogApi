using System.Security.Cryptography.Xml;
using UserPostApi.Models.Entities;

namespace UserPostApi.Helpers
{
    public class ServiceResult<T>
    {
        // go to services and controllers to see how i used this class
        public T? Data { get; set; } //to hold DTOs is there is one (it can be null)

        public string? ErrorMessage { get; set; } //to hold error message and if null, no error occurred

        public bool Success => ErrorMessage == null; //this is computed property, i am telling if ErrorMessage is null, then Success = true and false is not null

        public static ServiceResult<T> Ok(T data) //passing data and filling it
        {
            return new ServiceResult<T> { Data = data };
        }

        public static ServiceResult<T> Fail(string errorMessage)//in case of failure, pass why this happened
        {
            return new ServiceResult<T> { ErrorMessage = errorMessage };
        }
    }
}
