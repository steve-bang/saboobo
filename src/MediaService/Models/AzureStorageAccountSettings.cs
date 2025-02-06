
namespace SaBooBo.MediaService.Models
{
    /// <summary>
    /// This class is used to store the Azure Storage Account settings.
    /// </summary>
    public class AzureStorageAccountSettings
    {
        /// <summary>
        /// The Azure Storage Account name.
        /// </summary>
        public string? AccountName { get; set; }

        /// <summary>
        /// The Azure Storage Account key.
        /// </summary>
        public string? AccountKey { get; set; }

        /// <summary>
        /// The Azure Storage Account container name.   
        /// </summary>
        public string? ContainerName { get; set; }

        /// <summary>
        /// The Azure Storage Account connection string.
        /// </summary>
        public string? ConnectionString { get; set; }
    }
}