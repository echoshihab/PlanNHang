using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Security
{
    public class IsHostRequirement : IAuthorizationRequirement
    {

    }

    public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _db;
        public IsHostRequirementHandler(IHttpContextAccessor httpContextAccessor, ApplicationDbContext db)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
        {
            var currentUserName = _httpContextAccessor.HttpContext.User?.Claims?
            .SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var activityId = Guid.Parse(_httpContextAccessor.HttpContext.Request.RouteValues.SingleOrDefault(x => x.Key == "id")
            .Value.ToString());

            var activity = _db.Activities.FindAsync(activityId).Result;

            var host = activity.UserActivities.FirstOrDefault(x => x.IsHost);

            if (host?.AppUser?.UserName == currentUserName)
                context.Succeed(requirement); //we will get 403 forbidden if context doesn't succeed

            return Task.CompletedTask;

        }
    }
}