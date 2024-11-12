using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IdentityModel.Tokens;

using System.Security.Claims;
using System.Text;
using System.Web.Http.Cors;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace DSCApi.Controllers
{
    [RoutePrefix("api/dsc/jwt")]
    public class JwtController : ApiController
    {
        [HttpGet]
        [Route("read")]
        //EnableCors(http://localhost:60710, "*", "*")
        public Object GetToken()
        {
            //esta clave tiene que ir tambien el startup.cs
            string key = "elamortrasciendelasdimensionesdelespacioytiempo"; //Secret key which will be used later during validation    
            var issuer = "http://localhost:60710";  //normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "bilal"));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };
        }








        [Route("name1")]
        [HttpPost]
        [EnableCors("http://localhost:60710", "*", "*")]
        public String GetName1()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                }
                return "Valid";
            }
            else
            {
                return "Invalid";
            }
        }

        [Authorize]
        [HttpPost]
        [Route("name2")]
        [EnableCors("http://localhost:60710", "*", "*")]
        public Object GetName2()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
                return new
                {
                    data = name
                };

            }
            return null;
        }






    }


   

}
