using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Nito.AsyncEx;
using OpenId.AppAuth;
using Org.Json;
using Volunteer.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(LoginProvider))]
namespace Volunteer.Droid
{
    public class LoginProvider : ILoginProvider
    {
        private readonly AuthorizationService _authService;
        private AuthState _authState;
        internal static LoginProvider Current;
        private readonly AsyncAutoResetEvent _loginResultWaitHandle
            = new AsyncAutoResetEvent(false);

        public LoginProvider()
        {
            Current = this;
            _authService = new AuthorizationService(MainActivity.Instance);
        }

        public async Task<AuthInfo> LoginAsync()
        {
            try
            {
                var serviceConfiguration = await AuthorizationServiceConfiguration.FetchFromUrlAsync(
                    Android.Net.Uri.Parse(Constants.DiscoveryEndpoint));

                MakeAuthRequest(serviceConfiguration, new AuthState());
                await _loginResultWaitHandle.WaitAsync();
            }
            catch (AuthorizationException ex)
            {
                Console.WriteLine("Failed to retrieve configuration:" + ex);
            }

            return new AuthInfo()
            {
                IsAuthorized = _authState?.IsAuthorized ?? false,
                AccessToken = _authState?.AccessToken,
                IdToken = _authState?.IdToken,
                RefreshToken = _authState?.RefreshToken,
                Scope = _authState?.Scope
            };
        }

        private void MakeAuthRequest(
            AuthorizationServiceConfiguration serviceConfig,
            AuthState authState)
        {
            var authRequest = new AuthorizationRequest.Builder(
            serviceConfig, Constants.ClientId,
            ResponseTypeValues.Code,
            Android.Net.Uri.Parse(Constants.RedirectUri))
            .SetScope(string.Join(" ", Constants.Scopes))
            .Build();

            var postAuthorizationIntent = CreatePostAuthorizationIntent(
                MainActivity.Instance, authRequest, serviceConfig.DiscoveryDoc, authState);

            _authService.PerformAuthorizationRequest(authRequest, postAuthorizationIntent);
        }


        private PendingIntent CreatePostAuthorizationIntent(
            Context context,
            AuthorizationRequest request,
            AuthorizationServiceDiscovery discoveryDoc,
            AuthState authState)
        {
            var intent = new Intent(context, typeof(MainActivity));
            intent.PutExtra(Constants.AuthStateKey, authState.JsonSerializeString());

            if (discoveryDoc != null)
            {
                intent.PutExtra(
                    Constants.AuthServiceDiscoveryKey,
                    discoveryDoc.DocJson.ToString());
            }

            return PendingIntent.GetActivity(context, request.GetHashCode(), intent, 0);
        }

        internal void NotifyOfCallback(Intent intent)
        {
            try
            {
                if (!intent.HasExtra(Constants.AuthStateKey))
                {
                    _authState = null;
                }
                else
                {
                    try
                    {
                        _authState = AuthState.JsonDeserialize(intent.GetStringExtra(Constants.AuthStateKey));
                    }
                    catch (JSONException ex)
                    {
                        Console.WriteLine("Malformed AuthState JSON saved: " + ex);
                        _authState = null;
                    }
                }
                if (_authState != null)
                {
                    AuthorizationResponse response = AuthorizationResponse.FromIntent(intent);
                    AuthorizationException authEx = AuthorizationException.FromIntent(intent);
                    _authState.Update(response, authEx);

                    if (response != null)
                    {
                        Console.WriteLine("Received AuthorizationResponse.");
                        try
                        {
                            var clientAuthentication = _authState.ClientAuthentication;
                        }
                        catch (ClientAuthenticationUnsupportedAuthenticationMethod ex)
                        {
                            _loginResultWaitHandle.Set();

                            Console.WriteLine(
                                "Token request cannot be made, client authentication for the token endpoint could not be constructed: " +
                                ex);

                            return;
                        }

                        _authService.PerformTokenRequest(response.CreateTokenExchangeRequest(), ReceivedTokenResponse);
                    }
                    else
                    {
                        Console.WriteLine("Authorization failed: " + authEx);
                    }
                }
                else
                {
                    _loginResultWaitHandle.Set();
                }
            }
            catch (Exception)
            {
                _loginResultWaitHandle.Set();
            }
        }

        private void ReceivedTokenResponse(
            TokenResponse tokenResponse,
            AuthorizationException authException)
        {
            try
            {
                _authState.Update(tokenResponse, authException);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            finally
            {
                _loginResultWaitHandle.Set();
            }
        }
    }
}