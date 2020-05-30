using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Comments;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;
        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        //unlike api controller, the name is important here becuuse 'SendComment' will be invoked

        public async Task SendComment(Create.Command command)
        {
            //this token value is available due to config in start up -see opt events - new jwt bearer
            //events
            var userName = Context.User?.Claims?.FirstOrDefault(x => x.Type ==
            ClaimTypes.NameIdentifier)?.Value;


            command.UserName = userName;

            var comment = await _mediator.Send(command);

            await Clients.All.SendAsync("ReceiveComment", comment);
        }

    }
}