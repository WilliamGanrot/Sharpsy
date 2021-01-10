using Microsoft.AspNetCore.Components.Authorization;
using Sharpsy.Library.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Sharpsy.App.Services
{
    public class State
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly UserManager<ApplicationUser> _userManager;
        public State(AuthenticationStateProvider AuthenticationStateProvider, UserManager<ApplicationUser> UserManager)
        {
            _authenticationStateProvider = AuthenticationStateProvider;
            _userManager = UserManager;

        }

        public async Task<ApplicationUser> GetAuthenticatedUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return await _userManager.GetUserAsync(authState.User);
        }

        public async Task<ApplicationUser> GetUserByClaim(ClaimsPrincipal p)
        {
            return await _userManager.GetUserAsync(p);
        }
    }
}
