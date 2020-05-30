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

namespace Application.Comments
{
    public class Create
    {
        public class Command : IRequest<CommentDto>
        {
            //what are we receiving?
            public string Body { get; set; }
            public Guid ActivityId { get; set; }
            //we are not using http request so can't use httpcontext user acessor here
            //signalR uses websockets
            public string UserName { get; set; }
        }

        public class Handler : IRequestHandler<Command, CommentDto>
        {
            private readonly ApplicationDbContext _db;
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext db, IMapper mapper)
            {
                this._mapper = mapper;
                _db = db;
            }

            public async Task<CommentDto> Handle(Command request, CancellationToken cancellationToken)
            {
                //get the necessary items to save to database comment entity and activity entity
                var activity = await _db.Activities.FindAsync(request.ActivityId);

                if (activity == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Activity = "Not Found" });

                var user = await _db.Users.SingleOrDefaultAsync(x => x.UserName == request.UserName);

                //create new comment 
                var comment = new Comment
                {
                    Author = user,
                    Activity = activity,
                    Body = request.Body,
                    CreatedAt = DateTime.Now
                };

                //add comment to activity
                activity.Comments.Add(comment);

                //save both comment + activity
                var success = await _db.SaveChangesAsync() > 0;


                //return comment DTO - map from comment
                if (success) return _mapper.Map<CommentDto>(comment);


                throw new Exception("Problem saving changes");
            }
        }
    }
}