
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using SaBooBo.MediaService.Models;
using SaBooBo.MediaService.Repositories;

namespace SaBooBo.MediaService.Services;

/// <summary>
/// Interface for the Media Service
/// </summary>
public interface IMediaService
{
    /// <summary>
    /// Uploads a file to Blob Storage
    /// </summary>
    /// <param name="file">The file to upload</param>
    /// <returns></returns>
    Task<Media> UploadFileAsync(IFormFile file);

    Task<Azure.Response<bool>> DeleteFileAsync(string fileUrl);

    Task DeleteAsync(Guid id);

    Task<List<Media>> ListAsync();

}

/// <summary>
/// Implementation of the Media Service
/// </summary>
public class MediaService : IMediaService
{
    private readonly AzureStorageAccountSettings _azureStorageAccountSettings;
    private readonly IMediaRepository _mediaRepository;

    public MediaService(
        IConfiguration configuration,
        IMediaRepository mediaRepository
    )
    {
        _mediaRepository = mediaRepository;

        var azStorageAccountParsed = configuration.GetSection("Azure:StorageAccount").Get<AzureStorageAccountSettings>();

        if (azStorageAccountParsed == null)
        {
            throw new ArgumentNullException(nameof(azStorageAccountParsed));
        }

        _azureStorageAccountSettings = azStorageAccountParsed;
    }

    /// <inheritdoc />
    public async Task<Media> UploadFileAsync(IFormFile file)
    {
        // Create a BlobServiceClient object which will be used to create a container client
        BlobServiceClient blobServiceClient = new BlobServiceClient(_azureStorageAccountSettings.ConnectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_azureStorageAccountSettings.ContainerName);

        // Create the container if it does not exist
        await containerClient.CreateIfNotExistsAsync();

        BlobClient blobClient = containerClient.GetBlobClient(file.FileName);

        Azure.Response<BlobContentInfo> blobContentInfoUpload;

        // Upload the file to Blob Storage
        using (var stream = file.OpenReadStream())
        {
            blobContentInfoUpload = await blobClient.UploadAsync(stream, overwrite: true);
        }

        // Set access tier to hot
        await blobClient.SetAccessTierAsync(AccessTier.Hot);

        // Get the URL of the file in Blob Storage after upload is complete
        string fileUrl = blobClient.Uri.AbsoluteUri;

        Media media = new(
            userId: Guid.NewGuid(),
            name: file.FileName,
            url: fileUrl,
            size: file.Length,
            contentType: file.ContentType,
            accessTier: AccessTier.Hot.ToString()
        );

        // Save the file metadata to the database
        await _mediaRepository.AddMediaAsync(media);

        // Save changes to the database
        await _mediaRepository.UnitOfWork.SaveChangesAsync();

        // Return the file metadata
        return media;
    }

    public async Task<Azure.Response<bool>> DeleteFileAsync(string fileUrl)
    {
        // 1. Delete the file from Blob Storage

        // Create a BlobServiceClient object which will be used to create a container client
        BlobServiceClient blobServiceClient = new BlobServiceClient(_azureStorageAccountSettings.ConnectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_azureStorageAccountSettings.ContainerName);

        // Create the container if it does not exist
        await containerClient.CreateIfNotExistsAsync();

        // Get blob name from the file URL
        string blobName = new Uri(fileUrl).Segments.Last();

        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        var resultDelete = await blobClient.DeleteIfExistsAsync();


        // 2. Delete the file metadata from the database
        var media = await _mediaRepository.GetByUrlAsync(fileUrl);

        if (media != null)
        {
            _mediaRepository.DeleteMedia(media);

            await _mediaRepository.UnitOfWork.SaveChangesAsync();
        }

        return resultDelete;
    }

    public async Task<List<Media>> ListAsync()
    {
        return await _mediaRepository.ListAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _mediaRepository.DeleteAsync(id);

        await _mediaRepository.UnitOfWork.SaveChangesAsync();
    }
}