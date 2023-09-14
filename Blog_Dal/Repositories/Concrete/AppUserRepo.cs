using Blog_Dal.Context;
using Blog_Dal.Repositories.Interfaces.Concrete;
using Blog_model.Models.Concrete;
using Blog_model.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Dal.Repositories.Concrete
{
    public class AppUserRepo : IAppUserRepo
    {

        private readonly ProjectContext _context;
        private readonly UserManager<AppUser> _manager;
        private readonly DbSet<AppUser> _table;

        public AppUserRepo(ProjectContext context, UserManager<AppUser> manager)
        {
            _context = context;
            _manager = manager;
            _table = _context.Set<AppUser>();
        }


        // asenkron yapı task,await async unutma !! 

        public async Task Create(AppUser appUser)
        {
            await _manager.CreateAsync(appUser, appUser.Password);   // kayıtlı kullanıcı için şifre aldık
            await _manager.AddToRoleAsync(appUser, "Member");   // kayıtlı olmayan kuallnıcı için mamber rolunü verdik

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
    }
}
