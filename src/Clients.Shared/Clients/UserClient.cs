using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using SaBooBo.Clients.Shared.User;
using SaBooBo.Domain.Shared.Configurations;

namespace SaBooBo.Clients.Shared.Clients;

public interface IUserClient
{
    Task<UserResponse> GetUserAsync(string userId);
    Task<UserResponse> CreateUserAsync(string username, string email, string phone, string password, List<string> roles, Dictionary<string, string> metadata);
    Task<UserResponse> UpdateUserAsync(string userId, string? username = null, string? email = null, string? phone = null, string? password = null, List<string>? roles = null, Dictionary<string, string>? metadata = null);
    Task<DeleteUserResponse> DeleteUserAsync(string userId);
    Task<UsersResponse> GetUsersByRoleAsync(string role, int page = 1, int pageSize = 10);
    Task<ValidateUserResponse> ValidateUserAsync(string username, string password);
}

public class UserClient : IUserClient
{
    private readonly UserGrpc.UserGrpcClient _client;
    private readonly ILogger<UserClient> _logger;

    public UserClient(ServiceConfiguration config, ILogger<UserClient> logger)
    {
        var channel = GrpcChannel.ForAddress(config.ServiceEndpoints.UserGrpc);
        _client = new UserGrpc.UserGrpcClient(channel);
        _logger = logger;
    }

    public async Task<UserResponse> GetUserAsync(string userId)
    {
        var request = new GetUserRequest { UserId = userId };
        return await _client.GetUserAsync(request);
    }

    public async Task<UserResponse> CreateUserAsync(string username, string email, string phone, string password, List<string> roles, Dictionary<string, string> metadata)
    {
        var request = new CreateUserRequest
        {
            Username = username,
            Email = email,
            Phone = phone,
            Password = password
        };
        request.Roles.AddRange(roles);
        request.Metadata.Add(metadata);
        return await _client.CreateUserAsync(request);
    }

    public async Task<UserResponse> UpdateUserAsync(string userId, string? username = null, string? email = null, string? phone = null, string? password = null, List<string>? roles = null, Dictionary<string, string>? metadata = null)
    {
        var request = new UpdateUserRequest { UserId = userId };
        if (username != null) request.Username = username;
        if (email != null) request.Email = email;
        if (phone != null) request.Phone = phone;
        if (password != null) request.Password = password;
        if (roles != null) request.Roles.AddRange(roles);
        if (metadata != null) request.Metadata.Add(metadata);
        return await _client.UpdateUserAsync(request);
    }

    public async Task<DeleteUserResponse> DeleteUserAsync(string userId)
    {
        var request = new DeleteUserRequest { UserId = userId };
        return await _client.DeleteUserAsync(request);
    }

    public async Task<UsersResponse> GetUsersByRoleAsync(string role, int page = 1, int pageSize = 10)
    {
        var request = new GetUsersByRoleRequest
        {
            Role = role,
            Page = page,
            PageSize = pageSize
        };
        return await _client.GetUsersByRoleAsync(request);
    }

    public async Task<ValidateUserResponse> ValidateUserAsync(string username, string password)
    {
        var request = new ValidateUserRequest
        {
            Username = username,
            Password = password
        };
        return await _client.ValidateUserAsync(request);
    }
} 