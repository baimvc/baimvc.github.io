using IdentityModel.Client;
using System;
using System.Net.Http;

namespace Client
{
    class Program
    {

        static async System.Threading.Tasks.Task Main(string[] args)
        {

            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://127.0.0.1:6000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }
            //// request token 不带用户名密码的
            //var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            //{
            //    Address = disco.TokenEndpoint,

            //    ClientId = "NewsClient",
            //    ClientSecret = "byf1025",
            //    Scope = "ApiNews",


            //});
            // request token 带用户名密码的
            // request token 不带用户名密码的
            var tokenResponse2 = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "NewsClient2",
                ClientSecret = "byf1025",
                Scope = "ApiNews",
                UserName = "byf",
                Password = "123"

            });

            if (tokenResponse2.IsError)
            {
                Console.WriteLine(tokenResponse2.Error);
                return;
            }
            var accessToken = tokenResponse2.AccessToken;
            if (string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine("accessToken为空！");
                return;
            }
            await GetPwdApiAsync(accessToken);
            //Console.WriteLine(tokenResponse2.Json);
        }
        public static async System.Threading.Tasks.Task GetPwdApiAsync(string accessToken)
        {
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(accessToken);

            var reponst = await apiClient.GetAsync("http://127.0.0.1:6001/api/Banner/GetAllBanners");
            if (!reponst.IsSuccessStatusCode)
            {
                Console.WriteLine(reponst.StatusCode.ToString());
            }
            else
            {
                var content = await reponst.Content.ReadAsStringAsync();
                Console.WriteLine("成功返回：" + content);
            }

        }

    }
}
