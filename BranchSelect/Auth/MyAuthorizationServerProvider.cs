using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BranchSelect.Auth
{
    public class MyAuthorizationServerProvider:OAuthAuthorizationServerProvider
    {
        public override async Task
            ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        //ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        //{
        //    context.Validated();
        //}

        public override async Task
            GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //context.OwinContext.Response.Headers.Add("Acces-Control-Allow-Origin", new[] { "*" });
            using (var service=new UserService())
            {
                var user = service.ValidateUser(context.UserName, context.Password);

                if (user!=null)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name,user.UserId));
                    identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("Invalid_grant", "Kullanıcı adı ve şifresi yanlış");
                }
            }


           
        }
    }
}
