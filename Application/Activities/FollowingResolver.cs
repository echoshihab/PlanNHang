using System.Linq;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class FollowingResolver : IValueResolver<UserActivity, AttendeeDto, bool>
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserAccessor _userAccessor;
        public FollowingResolver(ApplicationDbContext db, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _db = db;
        }

        public bool Resolve(UserActivity source, AttendeeDto destination, bool destMember, ResolutionContext context)
        {
            var currentUser = _db.Users.SingleOrDefaultAsync(x =>
            x.UserName == _userAccessor.GetCurrentUserName()).Result;

            if (currentUser.Followings.Any(x => x.TargetId == source.AppUserId))
                return true;

            return false;
        }
    }
}