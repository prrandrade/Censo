namespace Censo.Domain.Interfaces.API
{
    using System.Threading.Tasks;

    public interface IHub
    {
        Task SendMessage(object message);
    }
}
