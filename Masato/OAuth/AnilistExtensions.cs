using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Masato.OAuth
{
    public static class AnilistExtensions
    {
        public static AuthenticationBuilder AddAnilist(this AuthenticationBuilder builder)
            => builder.AddAnilist(AnilistDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddAnilist(this AuthenticationBuilder builder, Action<AnilistOptions> configureOptions)
            => builder.AddAnilist(AnilistDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddAnilist(this AuthenticationBuilder builder, string authenticationName, Action<AnilistOptions> configureOptions)
            => builder.AddAnilist(authenticationName, AnilistDefaults.DisplayName, configureOptions);

        public static AuthenticationBuilder AddAnilist(this AuthenticationBuilder builder, string authenticationName, string displayName, Action<AnilistOptions> configureOptions)
            => builder.AddOAuth<AnilistOptions, AnilistHandler>(authenticationName, displayName, configureOptions);
    }
}
