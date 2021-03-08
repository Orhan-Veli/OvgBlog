namespace OvgBlog.UI.Models
{
    public class JsonResultModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public T Data { get; set; }
        public JsonResultModel(bool success)
        {
            Success = success;
        }
        public JsonResultModel(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public JsonResultModel(bool success, T data, string message)
        {
            Data = data;
            Success = success;
            Message = message;
        }
    }
}
