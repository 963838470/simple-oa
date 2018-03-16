using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using oa_server.DAL;
using oa_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace oa_server.Providers
{
    //authorization_code
    //1	ValidateClientRedirectUri
    //2	ValidateAuthorizeRequest
    //3	AuthorizeEndpoint

    //4	ValidateClientAuthentication

    //client_credentials
    //1	ValidateClientAuthentication
    //2	GrantClientCredentials


    //password
    //1	ValidateClientAuthentication
    //2	GrantResourceOwnerCredentials


    //implicit
    //1	ValidateClientRedirectUri
    //2	ValidateAuthorizeRequest
    //3	AuthorizeEndpoint
    public class OpenAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// 验证 access_token 的请求,仅开放password请求
        /// </summary>
        public override async Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            if (context.TokenRequest.IsResourceOwnerPasswordCredentialsGrantType)
            {
                context.Validated();
            }
            else
            {
                context.Rejected();
            }
        }

        /// <summary>
        /// 验证 client 信息
        /// </summary>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            //string grant_type = context.Parameters.GetValues("grant_type").FirstOrDefault();
            //if (grant_type != "password" && grant_type != "refresh_token")
            //{
            //    if (clientId != "xishuai" || clientSecret != "123")
            //    {
            //        context.SetError("invalid_client", "client or clientSecret is not valid");
            //        return;
            //    }
            //}

            context.Validated();
        }

        /// <summary>
        /// 生成 access_token（密码模式）
        /// </summary>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            AuthorityUserDal _AuthorityUserDal = new AuthorityUserDal();
            AuthorityOuUserDal _AuthorityOuUserDal = new AuthorityOuUserDal();
            AuthorityOuDal _AuthorityOuDal = new AuthorityOuDal();

            if (string.IsNullOrEmpty(context.UserName))
            {
                context.SetError("非法的用户名", "用户名不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(context.Password))
            {
                context.SetError("非法的密码", "密码不能为空！");
                return;
            }

            AuthorityUser user = _AuthorityUserDal.GetUser(context.UserName, context.Password);
            if (user != null)
            {
                List<int> ouUser = _AuthorityOuUserDal.Get(o => o.userId == user.id).Select(o => o.ouId).ToList();
                List<AuthorityOu> ous = _AuthorityOuDal.Get(o => ouUser.Contains(o.id)).ToList();

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim("AuthorityUser", JsonHelper.JsonSerialize(user)));
                identity.AddClaim(new Claim("AuthorityOu", JsonHelper.JsonSerialize(ous)));
                context.Validated(identity);
            }
            else
            {
                context.SetError("不合法的身份验证", "账号或密码不正确！");
            }
        }

        /// <summary>
        /// 生成 access_token（client credentials 授权方式）
        /// </summary>
        public override async Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var identity = new ClaimsIdentity(
                new GenericIdentity(context.ClientId, OAuthDefaults.AuthenticationType),
                context.Scope.Select(x => new Claim("urn:oauth:scope", x)));

            context.Validated(identity);
        }

        #region 验证码模式、简化模式

        /// <summary>
        /// 验证 redirect_uri
        /// </summary>
        public override async Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            context.Validated(context.RedirectUri);
        }

        /// <summary>
        /// 验证 authorization_code 的请求
        /// </summary>
        public override async Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            if (context.AuthorizeRequest.ClientId == "xishuai" && (context.AuthorizeRequest.IsAuthorizationCodeGrantType || context.AuthorizeRequest.IsImplicitGrantType))
            {
                context.Validated();
            }
            else
            {
                context.Rejected();
            }
        }

        /// <summary>
        /// 生成 authorization_code（authorization code 授权方式）、生成 access_token （implicit 授权模式）
        /// </summary>
        public override async Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        {
            if (true)
            //if (context.AuthorizeRequest.IsImplicitGrantType)
            {
                //implicit 授权方式
                var identity = new ClaimsIdentity("Bearer");
                context.OwinContext.Authentication.SignIn(identity);
                context.RequestCompleted();
            }
            //else if (context.AuthorizeRequest.IsAuthorizationCodeGrantType)
            else if (true)
            {
                //authorization code 授权方式
                var redirectUri = context.Request.Query["redirect_uri"];
                var clientId = context.Request.Query["client_id"];
                var state = context.Request.Query["state"];
                var identity = new ClaimsIdentity(new GenericIdentity(clientId, OAuthDefaults.AuthenticationType));

                var authorizeCodeContext = new AuthenticationTokenCreateContext(
                    context.OwinContext,
                    context.Options.AuthorizationCodeFormat,
                    new AuthenticationTicket(
                        identity,
                        new AuthenticationProperties(new Dictionary<string, string>
                        {
                            {"client_id", clientId},
                            {"redirect_uri", redirectUri}
                        })
                        {
                            IssuedUtc = DateTimeOffset.UtcNow,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(context.Options.AuthorizationCodeExpireTimeSpan)
                        }));

                await context.Options.AuthorizationCodeProvider.CreateAsync(authorizeCodeContext);
                redirectUri += "?code=" + Uri.EscapeDataString(authorizeCodeContext.Token);
                if (!string.IsNullOrWhiteSpace(state))
                    redirectUri += "&state=" + HttpUtility.UrlEncode(state);
                context.Response.Redirect(redirectUri);
                context.RequestCompleted();
            }
        }

        #endregion
    }
}