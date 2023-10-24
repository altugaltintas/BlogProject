using Blog_Dal.Context;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Blog_model.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Dal.Repositories.Concrete
{
    public class AppUserRepo : IAppUserRepo
    {

        private readonly ProjectContext _context;
        private readonly UserManager<AppUser> _manager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly DbSet<AppUser> _table;

        public AppUserRepo(ProjectContext context, UserManager<AppUser> manager,IPasswordHasher<AppUser> passwordHasher)
        {
            _context = context;
            _manager = manager;
            _passwordHasher = passwordHasher;
            _table = _context.Set<AppUser>();
        }

        public async Task Active(AppUser appUser)
        {
            appUser.Statu = Statu.Active;
            await _context.SaveChangesAsync();
        }


        // asenkron yapı task,await async unutma !! 

        public async Task Create(AppUser appUser)
        {
            await _manager.CreateAsync(appUser, appUser.Password);   // kayıtlı kullanıcı için şifre aldık            
            await _manager.AddToRoleAsync(appUser,"Member");   // kayıtlı olmayan kuallnıcı için mamber rolunü verdik
            var oldPasswordHistory = new OldPasswordHistory
            {
                UserId = appUser.Id,
                PasswordHash = appUser.PasswordHash, // veya şifreleme işlemini burada uygulayın
                CreatedDate = appUser.CreatedDate
            };
            _context.oldPasswordHistories.Add(oldPasswordHistory);

            _context.SaveChanges();
        }

        public async Task Delete(AppUser appUser)
        {

            // usermanager delete yapılabilrmi kontrol et

            appUser.Statu = Statu.Passive;
            await _context.SaveChangesAsync();
        }

        public async Task Update(AppUser appUser)
        {

            // usermanager update işlemlerine bak 

            appUser.Statu = Statu.Modified;
            _table.Update(appUser);
            await _context.SaveChangesAsync();
        }






        public bool IsEmailUnique(string email)
        {
                        return !_context.Users.Any(u => u.Email == email);
        }

        public bool IsUserlUnique(string user)
        {           
            return !_context.Users.Any(u => u.UserName == user);
        }

        public bool IsEmailUniqueHaric(string email, string currentUserEmail)
        {
            return !_context.Users.Any(a => a.Email == email && a.Email != currentUserEmail);
        }

        public bool IsUserlUniqueHaric(string user, string currentUSer)
        {
            return !_context.Users.Any(a => a.UserName == user && a.UserName != currentUSer);
        }

        public async Task<bool> IsPasswordHistoryViolatedAsync(string userId, string newPassword, int passwordHistoryLimit)
        {
            var user = await _manager.FindByIdAsync(userId);

            if (user != null)
            {
                var passwordHistories = _context.oldPasswordHistories
                    .Where(p => p.UserId == userId)
                    .OrderByDescending(p => p.CreatedDate)
                    .Take(passwordHistoryLimit)
                    .ToList();

                foreach (var passwordHistory in passwordHistories)
                {
                    var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, passwordHistory.PasswordHash, newPassword);

                    if (passwordVerificationResult == PasswordVerificationResult.Success)
                    {
                        // Yeni şifre önceki şifrelerden birini içeriyor
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
