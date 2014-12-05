﻿using brewday.auth.endpoint.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace brewday.auth.endpoint
{
    public class SqliteAuthRepository : IDisposable, IAuthRepository
    {
        readonly UserManager<User> _userMgr;

        public SqliteAuthRepository(UserManager<User> userMgr)
        {
            _userMgr = userMgr;
        }

        public async Task<IdentityResult> RegisterUser(User user, string password)
        {
            var result = await _userMgr.CreateAsync(user, password);
            await _userMgr.SendEmailAsync(user.Id, "Success", "Hey welcome to BLAH");
            return result;
        }

        public async Task<User> FindUser(string userName, string password)
        {
            var user = await _userMgr.FindAsync(userName, password);
            return user;
        }

        public async Task<IdentityResult> ChangePassword(string id, string oldPassword, string newPassword)
        {
            var result = await _userMgr.ChangePasswordAsync(id, oldPassword, newPassword);
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // get rid of managed resources
                _userMgr.Dispose();
            }
            // get rid of unmanaged resources
        }

        // only if you use unmanaged resources directly in AuthRepository
        //AuthRepository()
        //{
        //    Dispose(false);
        //}


    }
}