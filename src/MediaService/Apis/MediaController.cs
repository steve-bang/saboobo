
using Microsoft.AspNetCore.Mvc;
using SaBooBo.Domain.Shared.ApiResponse;
using SaBooBo.Domain.Shared.ExceptionHandler;
using SaBooBo.MediaService.Errors;
using SaBooBo.MediaService.Models;
using SaBooBo.MediaService.Services;

namespace SaBooBo.MediaService.Apis;

[ApiController]
[Route("api/v1/medias")]
public class MediaController : ControllerBase
{
    private readonly IMediaService _mediaService;

    public MediaController(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadFileAsync([FromForm] IFormFile file)
    {
        if (file == null)
        {
            throw new BadRequestException(
                FileErrors.FileRequired,
                "The file is required to upload the file.",
                "The file is required to upload the file. Please provide the file."
            );
        }

        var fileUrl = await _mediaService.UploadFileAsync(file);

        return Ok(ApiResponseSuccess<Media>.BuildSuccess(fileUrl));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFileAsync([FromBody] MediaDeleteRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FileUrl))
        {
            throw new BadRequestException(
                FileErrors.FileUrlRequired,
                "The file URL is required to delete the file.",
                "The file URL is required to delete the file. Please provide the file URL."
            );
        }

        var result = await _mediaService.DeleteFileAsync(request.FileUrl);

        return Ok(ApiResponseSuccess<Azure.Response<bool>>.BuildSuccess(result));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _mediaService.DeleteAsync(id);

        return Ok(ApiResponseSuccess<bool>.BuildNoContent());
    }

    [HttpGet]
    public async Task<IActionResult> ListAsync()
    {
        var result = await _mediaService.ListAsync();

        return Ok(ApiResponseSuccess<List<Media>>.BuildSuccess(result));
    }
}