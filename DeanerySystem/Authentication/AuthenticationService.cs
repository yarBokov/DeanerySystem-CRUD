using DeanerySystem.Models.Authentication;
using DeanerySystem.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text.Json;

namespace DeanerySystem.Authentication
{
    public class AuthenticationService
    {
        private readonly AccountService _accountService;
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private const string AdminStorageKey = "deanery_admin";

        public AuthenticationService(AccountService accountService, ProtectedLocalStorage protectedLocalStorage)
        {
            _accountService = accountService;
            _protectedLocalStorage = protectedLocalStorage;
        }

        public async Task<LoggedInAdmin?> LoginUserAsync(LoginModel model)
        {
            var loggedInAdmin = await _accountService.LoginAsync(model);
            if (loggedInAdmin is not null)
            {
                await SaveUserToBrowserStorageAsync(loggedInAdmin.Value);
            }
            return loggedInAdmin;
        }

        private JsonSerializerOptions jsonSerializerOpts = new JsonSerializerOptions
        {

        };

        public async Task SaveUserToBrowserStorageAsync(LoggedInAdmin user)
        {
            await _protectedLocalStorage.SetAsync(AdminStorageKey, JsonSerializer.Serialize(user, jsonSerializerOpts));
        }

        public async Task<LoggedInAdmin?> GetUserFromBrowserStorageAsync()
        {
            try
            {
                var result = await _protectedLocalStorage.GetAsync<string>(AdminStorageKey);
                if (result.Success && !string.IsNullOrWhiteSpace(result.Value))
                {
                    var loggedInAdmin = JsonSerializer.Deserialize<LoggedInAdmin>(result.Value, jsonSerializerOpts);
                    return loggedInAdmin;
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task RemoveFromBrowserStorageAsync()
        {
            await _protectedLocalStorage.DeleteAsync(AdminStorageKey);
        }
    }
}
