
using Microsoft.EntityFrameworkCore;
using SaBooBo.Domain.Shared;
using SaBooBo.MediaService.Models;

namespace SaBooBo.MediaService.Repositories
{
    public class MediaRepository(
        MediaDbContext _mediaDbContext
    ) : IMediaRepository
    {

        public IUnitOfWork UnitOfWork => _mediaDbContext;

        public async Task<Media> AddMediaAsync(Media media)
        {
            var result = await _mediaDbContext.Medias.AddAsync(media);

            return result.Entity;
        }

        public async Task<Media?> GetMediaAsync(Guid id)
        {
            return await _mediaDbContext.Medias.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Media>> GetMediaByUserIdAsync(Guid userId)
        {
            return await _mediaDbContext.Medias.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task DeleteMediaAsync(Guid id)
        {
            var media = await GetMediaAsync(id);

            if (media != null)
            {
                _mediaDbContext.Medias.Remove(media);
            }
        }

        public void DeleteMedia(Media media)
        {
            _mediaDbContext.Medias.Remove(media);
        }

        public async Task<Media?> GetByUrlAsync(string fileUrl)
        {
            return await _mediaDbContext.Medias.FirstOrDefaultAsync(x => x.Url == fileUrl);
        }

        public async Task<List<Media>> ListAsync()
        {
            return await _mediaDbContext.Medias.ToListAsync();
        }
    }
}