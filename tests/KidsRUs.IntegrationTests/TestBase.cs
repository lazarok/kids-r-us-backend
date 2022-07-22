using System.Net.Http.Headers;
using KidsRUs.Application.Handlers.Users.Commands.SignIn;
using KidsRUs.Application.Helper;
using KidsRUs.Application.Models.Dtos;
using KidsRUs.Domain.Common;
using KidsRUs.Persistence.Context;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace KidsRUs.IntegrationTests;

public class TestBase
{
    protected ApiWebApplicationFactory Application;

    protected TestBase()
    {
        Application = new ApiWebApplicationFactory();
        EnsureDatabase();
    }

    private void EnsureDatabase()
    {
        using var scope = Application.Services.CreateScope();
        
        var context = scope.ServiceProvider.GetService<KidsRUsContext>();
        var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
        
        context?.Database.EnsureDeleted(); 
        context?.Database.EnsureCreated();

        if (unitOfWork != null) Seed.SeedDataAsync(unitOfWork);
    }
    
    /// <summary>
    /// Shortcut to authenticate a user for testing
    /// </summary>
    private async Task<TokenDto> GetToken(string email, string password)
    {
        using var scope = Application.Services.CreateScope();

        var result = await SendAsync(new SignInCommand()
        {
            Email = email,
            Password = password
        });

        return result.Data;
    }
    
    public async Task<(HttpClient client, int userId)> CreateTestUser(string email, string password, string fullName,  RoleType roleType)
    {
        using var scope = Application.Services.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        PasswordHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
        
        var user = new User
        {
            RoleId = (int) roleType,
            Email = email,
            FullName = fullName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };
        
        unitOfWork.User.Add(user);

        await unitOfWork.SaveAsync();

        var token = await GetToken(email, password);

        var client = Application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

        return (client, user.Id);
    }
    
    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = Application.Services.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }
    
    /// <summary>
    /// Create an HttpClient including a valid JWT with admin user
    /// </summary>
    public Task<(HttpClient Client, int UserId)> GetClientAsAdmin() =>
        CreateTestUser("user@admin.com", "Pass.W0rd", "Super Admin", RoleType.Admin);
}