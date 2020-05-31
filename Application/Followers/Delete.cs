using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Followers
{
    public class Delete
    {
        public class Command : IRequest
        {
            public string UserName { get; set; }

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
                var observer = await _db.Users
                .SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetCurrentUserName());

                var target = await _db.Users
                .SingleOrDefaultAsync(x => x.UserName == request.UserName);

                if (target == null)
                    throw new RestException(HttpStatusCode.NotFound, new { User = "Not Found" });

                var following = await _db.Followings
                .SingleOrDefaultAsync(x => x.ObserverId == observer.Id && x.TargetId == target.Id);

                if (following == null)
                    throw new RestException(HttpStatusCode.BadRequest
                    , new { User = "You are not following this user" });

                if (following != null)
                {

                    _db.Followings.Remove(following);

                }

                //handler logic
                var success = await _db.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
