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
		public CreatePdfController(ICreatePdfService createPdfService)
		{
			_createPdfService = createPdfService;
		}

		[HttpGet("CreatePdf")]
		public async Task<ActionResult> CreatePdf(DateTime date)
		{

			try
			{

				byte[] pdfContent = await _createPdfService.CreatePdfAsync(date);
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
		public async Task<ActionResult> CreatePdfForHamurhane(DateTime date)
		{

			try
			{

				byte[] pdfContent = await _createPdfService.CreatePdfForHamurhaneAsync(date);
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
		public async Task<ActionResult> CreatePdfForMarketService(DateTime date)
		{

			try
			{

				byte[] pdfContent = await _createPdfService.CreatePdfForMarketServiceAsync(date);
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
	}
}
