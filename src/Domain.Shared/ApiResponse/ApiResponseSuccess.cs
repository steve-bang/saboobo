
using System.Net;
using Microsoft.AspNetCore.Http;

namespace SaBooBo.Domain.Shared.ApiResponse;

public class ApiResponseSuccess<T> : BaseApiResponse
{
    public T? Data { get; protected set; }

    public ApiResponseSuccess(int httpStatus, T? data)
    {
        Success = true;
        HttpStatus = httpStatus;
        Data = data;
    }

    public ApiResponseSuccess(HttpStatusCode httpStatus, T? data) : this((int)httpStatus, data)
    {
    }

    /// <summary>
    /// Returns a response with a status code of 200 (OK) and the data provided
    /// </summary>
    /// <param name="data">The data to be returned</param>
    /// <returns></returns>
    public static ApiResponseSuccess<T> BuildSuccess(T data)
    {
        return new ApiResponseSuccess<T>(HttpStatusCode.OK, data);
    }


    /// <summary>
    /// Returns a response with a status code of Created (201) and the data provided
    /// </summary>
    /// <param name="data">The data to be returned</param>
    /// <returns></returns>
    public static ApiResponseSuccess<T> BuildCreated(T data)
    {
        return new ApiResponseSuccess<T>(HttpStatusCode.Created, data);
    }


    /// <summary>
    /// Returns a response with a status code of Created (201) and the data provided
    /// </summary>
    /// <param name="data">The data to be returned</param>
    /// <returns></returns>
    public static IResult BuildCreated(string path, T data)
    {
        return Results.Created(path, new ApiResponseSuccess<T>(HttpStatusCode.Created, data));
    }

    /// <summary>
    /// Returns a response with a status code of OK (200) and the data provided
    /// </summary>
    /// <param name="data">The data to be returned</param>
    /// <returns></returns>
    public static IResult BuildSuccessResult(T data)
    {
        return Results.Ok(new ApiResponseSuccess<T>(HttpStatusCode.OK, data));
    }



    /// <summary>
    /// This method returns a response with a status code of 204 (No Content) and no data
    /// </summary>
    /// <returns></returns>
    public static IResult BuildNoContent()
    {
        return Results.NoContent();
    }
}