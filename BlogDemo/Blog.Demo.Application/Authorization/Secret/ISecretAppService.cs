using Blog.Demo.Application.Authorization.Secret.Dto;
using System.Threading.Tasks;

namespace Blog.Demo.Application.Authorization.Secret
{
    public interface ISecretAppService
    {

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="account">账户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<UserDto> GetCurrentUserAsync(string account, string password);
    }
}
