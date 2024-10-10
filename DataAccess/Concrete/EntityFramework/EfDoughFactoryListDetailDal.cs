using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfDoughFactoryListDetailDal : EfEntityRepositoryBase<DoughFactoryListDetail, BakeryAppContext>, IDoughFactoryListDetailDal
    {
        private readonly BakeryAppContext _context;
        public EfDoughFactoryListDetailDal(BakeryAppContext context) : base(context)
        {
            _context = context;
        }

        public bool IsExist(int id, int listId)
        {
          
                return (_context.DoughFactoryListDetails?.Any(p => p.DoughFactoryProductId == id && p.DoughFactoryListId == listId)).GetValueOrDefault();
           
        }
    }
}
