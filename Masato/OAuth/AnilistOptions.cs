using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Masato.OAuth
{
    public class AnilistOptions : OAuthOptions
    {
        public AnilistOptions()
        {
            CallbackPath = new PathString("/signin-anilist");
            AuthorizationEndpoint = AnilistDefaults.AuthorizationEndpoint;
            TokenEndpoint = AnilistDefaults.TokenEndpoint;
            UserInformationEndpoint = AnilistDefaults.UserInformationEndpoint;

            ClaimActions.MapCustomJson(ClaimTypes.NameIdentifier, ClaimValueTypes.UInteger64, GetValue("id"));
            ClaimActions.MapCustomJson(ClaimTypes.Name, ClaimValueTypes.String, GetValue("name"));
            // Not sure if there is a ClaimTypes object for these, so use a urn:anilist on it.
            ClaimActions.MapCustomJson("urn:anilist:avatar", ClaimValueTypes.String, GetValue("avatar.large"));
            ClaimActions.MapCustomJson("urn:anilist:titlePreference", ClaimValueTypes.String, GetValue("options.titleLanguage"));
            ClaimActions.MapCustomJson("urn:anilist:adultContent", ClaimValueTypes.Boolean, GetValue("options.displayAdultContent"));
        }

        // Too lazy to do this for all of em... :D
        private Func<JObject, string> GetValue(string key)
            => j => (j.SelectToken($"$.data.Viewer.{key}") as JValue)?.Value.ToString();
    }
}
