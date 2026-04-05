using System.Text.Json.Serialization;

namespace WeatherDisplay.Api.Updater.Models
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(DeleteFileStep), "delete-file")]
    [JsonDerivedType(typeof(DownloadFileStep), "download-file")]
    [JsonDerivedType(typeof(DownloadFtpFileStep), "download-ftp-file")]
    [JsonDerivedType(typeof(DownloadHttpFileStep), "download-http-file")]
    [JsonDerivedType(typeof(ExtractZipStep), "extract-zip")]
    [JsonDerivedType(typeof(ProcessStartExecutorStep), "process-start")]
    public interface IExecutorStep
    {
    }
}
