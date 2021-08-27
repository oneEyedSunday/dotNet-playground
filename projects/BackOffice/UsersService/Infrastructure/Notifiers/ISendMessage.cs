using System.Threading.Tasks;

namespace UsersService.Infrastructure.Notifiers
{
    public interface ISendMessage
    {
       Task SendMessage<T>(T message, string endPoint);
    }
}