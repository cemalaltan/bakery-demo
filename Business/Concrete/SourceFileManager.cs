using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
namespace Business.Concrete
{
    public class SourceFileManager : ISourceFileService
    {


        ISourceFileDal _sourceFileDal;
    

        public SourceFileManager(ISourceFileDal sourceFileDal)
        {
            _sourceFileDal = sourceFileDal;
        }


        public List<SourceFile> GetAll()
        {
            return _sourceFileDal.GetAll();
        }

        public SourceFile GetById(int id)
        {
            return _sourceFileDal.Get(x => x.Id == id);
        }

        public List<SourceFile> GetByDate(DateTime date)
        {
            return _sourceFileDal.GetAll(s => s.CreatedAt.Date == date.Date);
        }

        public List<SourceFile> GetByUserIdAndDate(int userId, DateTime date)
        {
            return _sourceFileDal.GetAll(s => s.UserId == userId && s.CreatedAt.Date == date.Date);
        }

        public void Add(SourceFile sourceFile)
        {
            _sourceFileDal.Add(sourceFile); 
        }

        public void DeleteById(int id)
        {
            _sourceFileDal.DeleteById(id);
        }
        
       
    }
}
