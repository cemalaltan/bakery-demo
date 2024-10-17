using Entities.Concrete;
using iText.StyledXmlParser.Jsoup.Select;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface ISourceFileService
    {

        
        List<SourceFile> GetAll();
        
        SourceFile GetById(int id);
        List<SourceFile> GetByDate(DateTime date);
        List<SourceFile> GetByUserIdAndDate(int userId, DateTime date);
        void Add(SourceFile sourceFile);
        void DeleteById(int id);
       
    }
}
