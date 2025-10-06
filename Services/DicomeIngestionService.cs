using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Pacs.IngestService.Services
{
	public class DicomIngestionService: BackgroundService
	{
		private readonly ILogger<DicomIngestionService> _logger;
		private readonly string _watchFolder = @"C:\PACS\Ingest\Incoming";


		public DicomIngestionService(ILogger<DicomIngestionService> logger)
		{
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("DICOM Ingestion Service started at: {time}", DateTimeOffset.Now);


			using var watcher = new FileSystemWatcher(_watchFolder, "*.dcm")
			{
				EnableRaisingEvents = true,
				IncludeSubdirectories = false
			};

			watcher.Created += async (sender, e) =>
			{
				_logger.LogInformation("New DICOM file detected: {file}", e.FullPath);
				await ProcessDicomFileAsync(e.FullPath);
			};


			while (!stoppingToken.IsCancellationRequested)
			{
				await Task.Delay(1000, stoppingToken);
			}

			_logger.LogInformation("DICOM Ingestion Service stopping at: {time}", DateTimeOffset.Now);


		}

		private Task ProcessDicomFileAsync(string filePath)
		{
			_logger.LogInformation("Processing file {file}", filePath);
			return Task.CompletedTask;
		}
	}
}
	
	

