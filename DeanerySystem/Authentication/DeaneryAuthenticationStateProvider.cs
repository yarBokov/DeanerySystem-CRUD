namespace DeanerySystem.Authentication
{
    public class DeaneryAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
    { 
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task LogoutAsync()
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
