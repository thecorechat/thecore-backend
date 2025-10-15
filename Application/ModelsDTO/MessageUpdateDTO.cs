namespace Application.ModelsDTO
{
    public class MessageUpdateDTO
    {
        public string Message { get; set; } = null!;
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
    }
}
