using Blog_model.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Dal.Repositories.Interfaces.Concrete
{
    public interface IAppUserRepo
    {

        Task Create(AppUser appUser);

        Task Update(AppUser appUser);

        Task Delete(AppUser appUser);

    }
}
