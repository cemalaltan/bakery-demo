using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebAPI.services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourceFileController : ControllerBase
    {

        private ISourceFileService _sourceFileService;
        private FileService _fileService;
        

        public SourceFileController(ISourceFileService sourceFileService, FileService fileService)
        {
            _sourceFileService = sourceFileService; ;   
            _fileService = fileService;
        }

        

        [HttpGet("GetSourceFileByDate")]
        public ActionResult GetSourceFileByDate(DateTime date)
        {
            var result = _sourceFileService.GetByDate(date);
            return Ok(result);
        }

        [HttpGet("GetSourceFileByUserIdAndDate")]
        public ActionResult GetSourceFileByUserIdAndDate(int userId, DateTime date)
        {
            var result = _sourceFileService.GetByUserIdAndDate(userId, date);
            return Ok(result);
        }

        [HttpGet("GetFile")]
        public async Task<ActionResult> GetFile(string fileName)
        {
            var result = await _fileService.GetFile(fileName);
            return Ok(result);
        }

        [HttpPost("AddSourceFile")]
        public async Task<ActionResult> AddSourceFile( SourceFile fileEntity)
        {
            try
            {
               
                _sourceFileService.Add(fileEntity);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPost("UploadFile")]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            try
            {
                string path = await  _fileService.UploadFile(file);
              
                return Ok(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("DeleteSourceFileById")]
        public async Task<ActionResult> DeleteSourceFileById(int id)
        {
            try{
                var data = _sourceFileService.GetById(id);
                _sourceFileService.DeleteById(id);
                if(data.DataPath != null)
                     await _fileService.DeleteFile(data.DataPath);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("UpdateSourceFile")]
        public ActionResult UpdateSourceFile(AccumulatedMoney accumulatedMoneyAmount)
        {
           // _sourceFileService.u(accumulatedMoneyAmount);
            return Ok();
        }
    }
}