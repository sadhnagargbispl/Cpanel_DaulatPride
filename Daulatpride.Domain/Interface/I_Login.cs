using Daulatpride.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daulatpride.Domain.Interface
{
    public interface I_Login
    {

        Task<Login> GetLogin(Login req);
        Task<User> ValidateUser(Login model);

    }
}
