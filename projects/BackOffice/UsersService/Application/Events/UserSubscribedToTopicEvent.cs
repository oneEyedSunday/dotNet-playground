namespace UsersService.Application.Events
{
    public class UserSubscribedToTopicEvent
    {
        public int UserId { get; set; }

        public int[] TopicIds { get; set; }
    }
}