namespace Application.ModelsDTO
{
    public class MessageUpdateDTO
    {
#warning Finish Message Update DTO
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string Message { get; set; } = null!;
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
    }
}
