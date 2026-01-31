using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using LeadAdmin.Utilities.Constants;

namespace LeadAdmin.Utilities.Helpers
{
    public static class FileHelper
    {
        public static void SaveFile(this Byte[] memoryStreamArray, string fileName, short organizationId, long? locationId = 0, BlobHttpHeaders blobHttpHeaders = null)
        {
            var container = GetCloudBlobContainer(organizationId);

            var parsedFileName = string.Format(@"{0}/{1}", locationId ?? 0, fileName.ToValidFileName());

            var updatedStream = new MemoryStream(memoryStreamArray);
            updatedStream.Seek(0, SeekOrigin.Begin);

            container.UploadBlob(parsedFileName, updatedStream);

            if (blobHttpHeaders!= null)
            {
                BlobClient blobClient = container.GetBlobClient(parsedFileName);
                blobClient.SetHttpHeaders(blobHttpHeaders);
            }
        }
        public static BlobContainerClient GetCloudBlobContainer(int institutionId)
        {
            var storageAccountConnectionString = ConfigSettings.Instance.AzureBlobSettings.LogsConnectionString;
            var storageAccount = new BlobServiceClient(storageAccountConnectionString);

            var containerName = $"c{institutionId}";
            return storageAccount.GetBlobContainerClient(containerName);
        }
        public static BlobContainerClient CreateContainerIfNotExists(int institutionId, int inputControlTypeId = 11)
        {
            var storageAccountConnectionString = ConfigSettings.Instance.AzureBlobSettings.LogsConnectionString;
            var storageAccount = new BlobServiceClient(storageAccountConnectionString);
            var containerName = $"c{institutionId}";

            try
            {
                var myClient = storageAccount.CreateBlobContainer(containerName, Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            }
            catch
            {

            }
            return storageAccount.GetBlobContainerClient(containerName);

        }
        public static void DeleteFile(string fileName, int institutionId, long? locationId = 0)
        {
            var container = GetCloudBlobContainer(institutionId);

            var parsedFileName = string.Format(@"{0}/{1}", locationId ?? 0, fileName.ToValidFileName());
            container.DeleteBlob(parsedFileName);
        }
        
        public static string ToValidFileName(this string fileName)
        {
            fileName = fileName.ToLower().Replace(" ", "_").Replace("(", "_").Replace(")", "_").Replace("&", "_").Replace("*", "_").Replace("-", "_");
            return string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
        }

        public static async Task UploadFileToBlobAsync(MemoryStream memoryStream, string fileName, int institutionId)
        {
            try
            {
                var container = FileHelper.GetCloudBlobContainer(institutionId);

                var parsedFileName = string.Format(@"{1}", fileName.ToValidFileName());

                //var cblob = container.GetBlockBlobReference(parsedFileName);

                var updatedStream = new MemoryStream(memoryStream.ToArray());
                updatedStream.Seek(0, SeekOrigin.Begin);

                await container.DeleteBlobIfExistsAsync(parsedFileName);
                await container.UploadBlobAsync(parsedFileName, updatedStream);
                //await cblob.UploadFromStreamAsync(updatedStream);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
