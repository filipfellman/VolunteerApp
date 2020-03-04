using System;
using System.Collections.Generic;
using System.Text;

namespace Volunteer
{
    public class AuthInfo
    {
        public bool IsAuthorized { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Scope { get; set; }
        public string IdToken { get; set; }
    }
}
