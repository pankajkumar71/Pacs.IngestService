using Microsoft.Extensions.Hosting;

using Microsoft.Extensions.Logging;

using Pacs.Core.Models;

namespace Pacs.IngestService
{
	public class Worker : BackgroundService
	
	{
		private readonly ILogger<Worker> _logger;
		
		public Worker(ILogger<Worker>logger)
		{
			_logger = logger;
		
		}
	
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("PACS Ingest Service started.");


			var study = new Study
			{
				StudyInstanceUid = Guid.NewGuid().ToString(),
				PatientId = "P001",
				StudyDate = DateTime.Now,
				Modality = "CT",
				AccessionNumber = "A1234"
			};

			_logger.LogInformation("Ingest Study: {studyInstanceUid}", study.StudyInstanceUid);
	
			await Task.Delay(10000, stoppingToken);
		}
	}
}