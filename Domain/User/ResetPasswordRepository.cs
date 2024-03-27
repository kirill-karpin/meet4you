using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Abstraction;

namespace User
{
    public class ResetPasswordRepository : CrudRepository<ResetPassword>, IResetPasswordRepository
    {
        public ResetPasswordRepository(DbContext context) : base(context)
        {
        }
    }
}