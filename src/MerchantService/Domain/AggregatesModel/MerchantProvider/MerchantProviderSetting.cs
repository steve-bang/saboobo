

using Newtonsoft.Json;

namespace SaBooBo.MerchantService.Domain.AggregatesModel;

public class MerchantProviderSetting : AggregateRoot
{
    public Guid MerchantId { get; private set; }

    public MerchantProviderType ProviderType { get; private set; }

    public string Metadata { get; set; } = null!;

    public object MetadataObj => DeserializeMetadata();

    public DateTime CreatedAt { get; private set; } = DateTime.Now.ToUniversalTime();

    public MerchantProviderSetting(Guid merchantId, MerchantProviderType providerType, string metadata)
    {

        Id = CreateNewId();

        MerchantId = merchantId;
        ProviderType = providerType;
        Metadata = metadata;

        ValidateMetadata();
    }

    public static MerchantProviderSetting Create(Guid merchantId, MerchantProviderType providerType, string metadata)
    {
        return new MerchantProviderSetting(merchantId, providerType, metadata);
    }

    public void Update(MerchantProviderType providerType, string metadata)
    {
        ProviderType = providerType;
        Metadata = metadata;

        ValidateMetadata();
    }

    public void ValidateMetadata()
    {
        // Validate MetaData
        if (string.IsNullOrWhiteSpace(Metadata))
        {
            throw new ArgumentNullException(nameof(Metadata));
        }

        // Validate MetaData based on ProviderType
        switch (ProviderType)
        {
            case MerchantProviderType.Zalo:
                var zaloAppSetting = JsonConvert.DeserializeObject<ZaloAppSetting>(Metadata);
                // Validate Zalo App Setting structure
                if (zaloAppSetting == null)
                {
                    throw new InvalidDeserializeObjectMetadataException(nameof(Metadata), typeof(ZaloAppSetting));
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(ProviderType));
        }
    }

    public object DeserializeMetadata()
    {
        return ProviderType switch
        {
            MerchantProviderType.Zalo => JsonConvert.DeserializeObject<ZaloAppSetting>(Metadata),
            _ => Metadata
        };
    }


}