﻿using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace QuickAccounting.Data.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedLocalStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                //await Task.Delay(5000);
                var userSessionStorageResult = await _sessionStorage.GetAsync<UserSession>("UserSession");
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userSession == null)
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }
                else
                {
                    var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.Email),
                    new Claim(ClaimTypes.Role, userSession.RoleName),
                    new Claim(ClaimTypes.NameIdentifier, userSession.UserId.ToString())
				}, "CustomAuth"));
                    return await Task.FromResult(new AuthenticationState(claimsPrincipal));
                }
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                await _sessionStorage.SetAsync("UserSession", userSession);
				claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
				{
					new Claim(ClaimTypes.Name, userSession.Email),
					new Claim(ClaimTypes.Role, userSession.RoleName),
                    new Claim(ClaimTypes.NameIdentifier, userSession.UserId.ToString()),
					new Claim("tenantId", userSession.UserId.ToString())
				}, "CustomAuth"));
			}
            else
            {
                await _sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
