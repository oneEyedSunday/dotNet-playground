using System.ComponentModel.DataAnnotations;

namespace UsersService.Domain.Requests
{
    public class TopicRegistration
    {
        // Fake authentication
        [Required(ErrorMessage = "Authentication is required")]
        public int userId { get; set; }

        [Required(ErrorMessage = "Topic Id is required")]
        [MinLength(1, ErrorMessage = "At least one topicId must be specified")]
        public int[] topicId { get; set; }
    }
}