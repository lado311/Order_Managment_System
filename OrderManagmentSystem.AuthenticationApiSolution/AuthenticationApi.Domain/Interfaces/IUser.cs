using AuthApi.Domain.Entities;
using OrderManagment.SharedLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApi.Domain.Interfaces
{
    public interface IUser : IGenericRepository<User>
    {
        Task<string> LoginUser(User user);
    }
}
