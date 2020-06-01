using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class List
    {
        public class ActivitiesEnvelope
        {
            public List<ActivityDto> Activities { get; set; }
            public int ActivityCount { get; set; }
        }

        public class Query : IRequest<ActivitiesEnvelope>
        {
            //will be used for paging
            public Query(int? limit, int? offset)
            {
                Limit = limit;
                Offset = offset;

            }
            public int? Limit { get; set; }
            public int? Offset { get; set; }
        }

        public class Handler : IRequestHandler<Query, ActivitiesEnvelope>
        {
            private readonly ApplicationDbContext _db;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }
            public async Task<ActivitiesEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _db.Activities.AsQueryable();

                var activities = await queryable
                .Skip(request.Offset ?? 0)
                .Take(request.Limit ?? 3).ToListAsync();

                //eager loading
                // .Include(x => x.UserActivities)
                // .ThenInclude(x => x.AppUser)
                // .ToListAsync();

                return new ActivitiesEnvelope
                {
                    Activities = _mapper.Map<List<Activity>, List<ActivityDto>>(activities),
                    ActivityCount = queryable.Count()
                };


            }
        }
    }
}
