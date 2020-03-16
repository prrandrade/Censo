namespace Censo.API.Hubs
{
    using System.Threading.Tasks;
    using Domain.Interfaces.API;
    using Microsoft.AspNetCore.SignalR;

    public class DashboardHub : Hub, IHub
    {
        private readonly IHubContext<DashboardHub> _context;

        public DashboardHub(IHubContext<DashboardHub> context)
        {
            _context = context;
        }

        [HubMethodName("DashboardUpdate")]
        public async Task SendMessage(object message)
        {
            if (_context.Clients != null)
                await _context.Clients.All.SendAsync("UpdateDashboard", message);
        }
    }
}
