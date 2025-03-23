using WebShop.Core.Models.User;

namespace WebShop.Core.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileViewModel> GetUserProfileBuUserId(string userId, CancellationToken cancellationToken);
    }
}
