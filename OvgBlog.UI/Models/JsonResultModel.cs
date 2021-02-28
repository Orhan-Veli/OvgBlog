namespace OvgBlog.UI.Models
{
    public class JsonResultModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public JsonResultModel(bool success)
        {
            Success = success;
        }
        public JsonResultModel(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
