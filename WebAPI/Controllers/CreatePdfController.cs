using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using System.Globalization;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CreatePdfController : ControllerBase
	{

		private ICreatePdfService _createPdfService;
		private IGeneralAccountPdfService _generalAccountPdfService;
		public CreatePdfController(ICreatePdfService createPdfService, IGeneralAccountPdfService generalAccountPdfService)
		{
			_createPdfService = createPdfService;
			_generalAccountPdfService = generalAccountPdfService;
		}

		[HttpGet("CreatePdf")]
		public IActionResult CreatePdf(DateTime date)
		{

			try
			{

				byte[] pdfContent = _createPdfService.CreatePdf(date);
				string formattedDate = date.ToString("dd.MM.yyyy", new CultureInfo("tr-TR"));

				string fileName = $"Pastane_{formattedDate}.pdf";
				string contentType = "application/pdf";


				FileContentResult fileContentResult = new FileContentResult(pdfContent, contentType)
				{
					FileDownloadName = fileName
				};

				return fileContentResult;

			}
			catch (Exception e)
			{

				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("CreatePdfForHamurhane")]
		public IActionResult CreatePdfForHamurhane(DateTime date)
		{

			try
			{

				byte[] pdfContent = _createPdfService.CreatePdfForHamurhane(date);
				string formattedDate = date.ToString("dd.MM.yyyy", new CultureInfo("tr-TR"));

				string fileName = $"Hamurhane_{formattedDate}.pdf";
				string contentType = "application/pdf";


				FileContentResult fileContentResult = new FileContentResult(pdfContent, contentType)
				{
					FileDownloadName = fileName
				};

				return fileContentResult;

			}
			catch (Exception e)
			{

				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("CreatePdfForMarketService")]
		public IActionResult CreatePdfForMarketService(DateTime date)
		{

			try
			{

				byte[] pdfContent = _createPdfService.CreatePdfForMarketService(date);
				string formattedDate = date.ToString("dd.MM.yyyy", new CultureInfo("tr-TR"));

				string fileName = $"MarketServis_{formattedDate}.pdf";
				string contentType = "application/pdf";


				FileContentResult fileContentResult = new FileContentResult(pdfContent, contentType)
				{
					FileDownloadName = fileName
				};

				return fileContentResult;

			}
			catch (Exception e)
			{

				return StatusCode(500, e.Message);
			}
		}
		
		[HttpGet("CreateGeneralAccountPdf")]
		public IActionResult CreateGeneralAccountPdf(DateTime date)
		{

			try
			{

				byte[] pdfContent = _generalAccountPdfService.GetGeneralAccountPdfByDate(date);
				string formattedDate = date.ToString("dd.MM.yyyy", new CultureInfo("tr-TR"));

				string fileName = $"GenelHesap_{formattedDate}.pdf";
				string contentType = "application/pdf";


				FileContentResult fileContentResult = new FileContentResult(pdfContent, contentType)
				{
					FileDownloadName = fileName
				};

				return fileContentResult;

			}
			catch (Exception e)
			{

				return StatusCode(500, e.Message);
			}
		}
		
	}
}
