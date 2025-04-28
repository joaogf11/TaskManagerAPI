namespace TaskManager.Domain.DTOs
{
    public class ServiceResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
