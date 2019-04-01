using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickstartIdentityServer
{
    public class ApiConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("ApiNews", "My API News Blog")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "NewsClient1",//客户端名称
                    ClientName = "My API News Blog",//描述
                    AllowedGrantTypes = GrantTypes.ClientCredentials,//指定允许的授权类型
                    //（AuthorizationCode，Implicit，Hybrid，ResourceOwner，ClientCredentials的合法组合）。
                    AllowAccessTokensViaBrowser = true,//是否通过浏览器为此客户端传输访问令牌
                    RedirectUris =
                    {
                        "http://localhost:6001/swagger/oauth2-redirect.html"
                    },
                    ClientSecrets =
                    {
                        new Secret("byf1025".Sha256())
                    },
                    AllowedScopes = { "ApiNews" }
                },

                // resource owner password grant client
                new Client
                {
                    ClientId = "NewsClient2",
                    ClientName = "My API News Blog",//描述
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenLifetime = 20,//20秒过期
                    AllowAccessTokensViaBrowser = true,//是否通过浏览器为此客户端传输访问令牌
                    RedirectUris =
                    {
                        "http://localhost:6001/swagger/oauth2-redirect.html"
                    },
                    ClientSecrets =
                    {
                        new Secret("byf1025".Sha256())
                    },
                    AllowedScopes = { "ApiNews" }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "byf",
                    Password = "123"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "admin",
                    Password = "123"
                }
            };
        }
    }
}
