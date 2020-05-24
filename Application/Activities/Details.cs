using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<ActivityDto>
        {

            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ActivityDto>
        {
            private readonly ApplicationDbContext _db;
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<ActivityDto> Handle(Query request, CancellationToken cancellationToken)
            {

                var activity = await _db.Activities.FindAsync(request.Id);

                //eager loading
                // .Include(x => x.UserActivities)
                // .ThenInclude(x => x.AppUser)
                // .SingleOrDefaultAsync(x => x.Id == request.Id);

                if (activity == null) throw new RestException(HttpStatusCode.NotFound, new { activity = "Not Found" });

                var activityToReturn = _mapper.Map<Activity, ActivityDto>(activity);

                return activityToReturn;
            }
        }
    }
}