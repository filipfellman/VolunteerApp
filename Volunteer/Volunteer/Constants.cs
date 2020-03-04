using System;
using System.Collections.Generic;
using System.Text;

namespace Volunteer
{
    public class Constants
    {
        public const string AuthStateKey = "authState";
        public const string AuthServiceDiscoveryKey = "authServiceDiscovery";

        public const string ClientId = "0oa2v1c4vfCxWxBWO4x6";
        public const string RedirectUri = "com.okta.dev-212447:/callback";
        public const string OrgUrl = "https://dev-212447.okta.com";
        public const string AuthorizationServerId = "default";

        public static readonly string DiscoveryEndpoint =
            $"{OrgUrl}/oauth2/{AuthorizationServerId}/.well-known/openid-configuration";


        public static readonly string[] Scopes = new string[] {
        "openid", "profile", "email", "offline_access" };
    }
}
