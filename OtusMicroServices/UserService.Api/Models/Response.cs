namespace UserService.Api.Models
{
    public class Response
    {
        public bool Ok { get; } = true;
        public List<ResponseError> Errors { get; set; }

        public Response()
        {
            
        }

        public Response(bool isSuccess, List<ResponseError> errors = null)
        {
            Ok = isSuccess;
            Errors = errors;
        }
    }

    public class ResponseError
    {
        public string Code { get; set; }
        public string Key { get; set; }
        public string Message { get; set; }
    }

    public sealed class Response<T> : Response
    {
        public T Data { get; set; }

        public Response(T data)
        {
            Data = data;
        }

        public Response()
        {
        }
    }
}