using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace Auth.Tests;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<ILogger<AuthService>> _loggerMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _configurationMock = new Mock<IConfiguration>();
        _loggerMock = new Mock<ILogger<AuthService>>();

        // Setup JWT configuration
        _configurationMock.Setup(c => c["Jwt:Key"]).Returns("asdfghjklqwertyuzxcvbnmasdfghjkl");
        _configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("https://localhost:5001");
        _configurationMock.Setup(c => c["Jwt:Audience"]).Returns("https://localhost:5001");

        _authService = new AuthService(_userRepositoryMock.Object, _configurationMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task LoginAsync_EmptyEmailOrPassword_ReturnsFailure()
    {
        // Arrange
        var request = new LoginRequest("", "password");

        // Act
        var result = await _authService.LoginAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Email and password are required.", result.Error);
        _userRepositoryMock.Verify(r => r.GetByEmailAsync(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task LoginAsync_UserNotFound_ReturnsFailure()
    {
        // Arrange
        var request = new LoginRequest("testuser@example.com", "password");
        _userRepositoryMock.Setup(r => r.GetByEmailAsync(request.Email)).ReturnsAsync((User)null);

        // Act
        var result = await _authService.LoginAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid email or password.", result.Error);
        _userRepositoryMock.Verify(r => r.GetByEmailAsync(request.Email), Times.Once);
    }

    [Fact]
    public async Task LoginAsync_IncorrectPassword_ReturnsFailure()
    {
        // Arrange
        var request = new LoginRequest("testuser@example.com", "wrongpassword");
        var user = new User("testuser@example.com", "correctpassword");
        _userRepositoryMock.Setup(r => r.GetByEmailAsync(request.Email)).ReturnsAsync(user);

        // Act
        var result = await _authService.LoginAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid email or password.", result.Error);
        _userRepositoryMock.Verify(r => r.GetByEmailAsync(request.Email), Times.Once);
    }

    [Fact]
    public async Task LoginAsync_ValidCredentials_ReturnsSuccess()
    {
        // Arrange
        var request = new LoginRequest("user1@gmail.com", "123");
        var user = new User("user1@gmail.com", "123");
        _userRepositoryMock.Setup(r => r.GetByEmailAsync(request.Email)).ReturnsAsync(user);

        // Act
        var result = await _authService.LoginAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data.Token);
        Assert.Equal(user.Id, result.Data.UserId);
       
    }

    [Fact]
    public async Task LoginAsync_RepositoryThrowsException_ReturnsFailure()
    {
        // Arrange
        var request = new LoginRequest("testuser@example.com", "password");
        _userRepositoryMock.Setup(r => r.GetByEmailAsync(request.Email))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _authService.LoginAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("An error occurred during login.", result.Error);
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Error during login for testuser@example.com")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once());
    }
}