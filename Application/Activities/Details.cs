using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Activity>
        {

            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Activity>
        {
            private readonly ApplicationDbContext _db;
            public Handler(ApplicationDbContext db)
            {
                _db = db;
            }

            public async Task<Activity> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await _db.Activities.FindAsync(request.Id);

                if (activity == null) throw new RestException(HttpStatusCode.NotFound, new { activity = "Not Found" });

                return activity;
            }
        }
    }
}