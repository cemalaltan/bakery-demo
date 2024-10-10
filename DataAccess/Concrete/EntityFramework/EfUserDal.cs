using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, BakeryAppContext>, IUserDal
    {
        private readonly BakeryAppContext _context;
        public EfUserDal(BakeryAppContext context) : base(context)
        {
            _context = context;
        }



        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            var result = await (from operationClaim in _context.OperationClaims
                                join userOperationClaim in _context.UserOperationClaims
                                    on operationClaim.Id equals userOperationClaim.OperationClaimId
                                where userOperationClaim.UserId == user.Id
                                select new OperationClaim
                                {
                                    Id = operationClaim.Id,
                                    Name = operationClaim.Name
                                })
                                .ToListAsync();

            return result;
        }
    }
}
