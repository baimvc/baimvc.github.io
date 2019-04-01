using Blog.Demo.Application.Authorization.Secret.Dto;
using BlogDemo.Infrastructure.Imp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Demo.Application.Authorization.Secret
{
    public class SecretAppService
    {
        
        #region API Implements

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="account">账户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public async Task<SecretDto>  GetCurrentUserAsync(string account, string password)
        {
            var user = await new SysUserInfoRepository().GetUserForLoginAsync(account,password);
            SecretDto secretDto = new SecretDto();
            if (user != null)
            {
                secretDto.Account = user.Account;
                secretDto.Password = user.Password;
                //secretDto.Token = user.;
               
            }


            return secretDto;
        }

        #endregion
    }
}
