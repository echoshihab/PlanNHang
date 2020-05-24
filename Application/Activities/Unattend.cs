using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Unattend
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ApplicationDbContext _db;
            private readonly IUserAccessor _userAccessor;
            public Handler(ApplicationDbContext db, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _db = db;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                //handler logic
                var activity = await _db.Activities.FindAsync(request.Id);

                if (activity == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Activity = "Could not find activity" });

                var user = await _db.Users.SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetCurrentUserName());

                var attendance = await _db.UserActivities.SingleOrDefaultAsync(x => x.ActivityId == activity.Id && x.AppUserId
                == user.Id);

                if (attendance == null)
                    return Unit.Value;

                if (attendance.IsHost)
                    throw new RestException(HttpStatusCode.BadRequest, new { Attendance = "You cannot remove yourself as host" });

                _db.UserActivities.Remove(attendance);

                var success = await _db.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }

    }
}