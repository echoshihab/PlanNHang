using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class ProfileReader : IProfileReader
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserAccessor _userAcceessor;
        public ProfileReader(ApplicationDbContext db, IUserAccessor userAcceessor)
        {
            _userAcceessor = userAcceessor;
            _db = db;

        }
        public async Task<Profile> ReadProfile(string username)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.UserName == username);

            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, new { User = "Not Found" });

            var currentUser = await _db.Users.SingleOrDefaultAsync(x => x.UserName == _userAcceessor.GetCurrentUserName());

            var profile = new Profile
            {
                DisplayName = user.DisplayName,
                UserName = user.UserName,
                Image = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                Photos = user.Photos,
                Bio = user.Bio,
                FollowersCount = user.Followers.Count(),
                FollowingCount = user.Followings.Count()
            };

            if (currentUser.Followings.Any(x => x.TargetId == user.Id))
                profile.IsFollowed = true;


            return profile;
        }
    }
}