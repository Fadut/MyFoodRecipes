using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace RecipesWebApp.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        //private ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

        //public override Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    return Task.FromResult(new AuthenticationState(_anonymous));
        //}

        //public void MarkUserAsAuthenticated(string username)
        //{
        //    var identity = new ClaimsIdentity(new[]
        //    {
        //    new Claim(ClaimTypes.Name, username),
        //}, "apiauth_type");

        //    var user = new ClaimsPrincipal(identity);

        //    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        //}

        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_currentUser));
        }

        public void MarkUserAsAuthenticated(string username)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
            }, "apiauth_type");
          
            _currentUser = new ClaimsPrincipal(identity);
           
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }

        // Needed? Or remove.
        //public void MarkUserAsLoggedOut()
        //{
        //    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        //}
    }

}
