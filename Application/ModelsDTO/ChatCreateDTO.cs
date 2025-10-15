
using System.ComponentModel.DataAnnotations;

namespace Application.ModelsDTO
{
    public class ChatCreateDTO
    {
        [Required(ErrorMessage = "Chat name is required")]
        [Length(1, 256, ErrorMessage = "Chat name has to be more than 1 and less than 256 characters")]
        public string Title { get; set; } = null!;
    }
}
