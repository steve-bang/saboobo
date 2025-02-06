
using SaBooBo.Domain.Shared;
using SaBooBo.MediaService.Models;

namespace SaBooBo.MediaService.Repositories;

public interface IMediaRepository : IRepository
{
    Task<Media> AddMediaAsync(Media media);

    Task<Media?> GetMediaAsync(Guid id);

    Task<Media?> GetByUrlAsync(string fileUrl);

    Task<List<Media>> GetMediaByUserIdAsync(Guid userId);

    Task<List<Media>> ListAsync();

    Task DeleteMediaAsync(Guid id);

    void DeleteMedia(Media media);
}