using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Protected;
using PatientService.Application.DTOs;
using PatientService.Application.Services;
using PatientService.Domain.Entities;
using PatientService.Infrastructure.Repositories;
using System.Net.Http.Json;
using System.Net;
using System.Security.Claims;

namespace Patient.UnitTests;

public class CarePatientServiceTests
{
    private readonly Mock<IPatientRepository> _patientRepositoryMock;
    private readonly HttpClient _httpClient;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private readonly PatientServices _patientService;

    public CarePatientServiceTests()
    {
        _patientRepositoryMock = new Mock<IPatientRepository>();
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        // Setup HttpClient with a mocked HttpMessageHandler
        var handlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://localhost:7001")
        };

        _patientService = new PatientServices(_patientRepositoryMock.Object, _httpClient, _httpContextAccessorMock.Object);
    }

    [Fact]
    public async Task GetAllPatientsAsync_ReturnsEmptyList_WhenNoPatientsExist()
    {
        // Arrange
        _patientRepositoryMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<PatientInfo>());

        // Act
        var result = await _patientService.GetAllPatientsAsync();

        // Assert
        Assert.Empty(result);
        _patientRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllPatientsAsync_ReturnsPatientDtos_WhenPatientsExist()
    {
        // Arrange
        var patients = new List<PatientInfo>
    {
        new PatientInfo("John", "Doe", new DateTime(1980, 1, 1))
        {
            MedicalRecords = { new MedicalRecord("Checkup", DateTime.UtcNow) }
        }
    };
        _patientRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(patients);

        // Act
        var result = await _patientService.GetAllPatientsAsync();

        // Assert
        Assert.Single(result);
        var dto = result.First();
        Assert.Equal("John", dto.Name);
        Assert.Equal("Doe", dto.Surname);
        Assert.Equal(new DateTime(1980, 1, 1), dto.Birthdate);
        Assert.Single(dto.MedicalRecords);
        Assert.Equal("Checkup", dto.MedicalRecords.First().DoctorRemarks);
        _patientRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetPatientByIdAsync_ReturnsPatientDto_WhenPatientExists()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var patient = new PatientInfo("Jane", "Smith", new DateTime(1990, 2, 2))
        {
            MedicalRecords = { new MedicalRecord("Healthy", DateTime.UtcNow) }
        };
        _patientRepositoryMock.Setup(r => r.GetByIdAsync(patientId)).ReturnsAsync(patient);

        // Act
        var result = await _patientService.GetPatientByIdAsync(patientId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Jane", result.Name);
        Assert.Equal("Smith", result.Surname);
        Assert.Equal(new DateTime(1990, 2, 2), result.Birthdate);
        Assert.Single(result.MedicalRecords);
        Assert.Equal("Healthy", result.MedicalRecords.First().DoctorRemarks);
        _patientRepositoryMock.Verify(r => r.GetByIdAsync(patientId), Times.Once);
    }

    [Fact]
    public async Task GetPatientByIdAsync_ThrowsException_WhenPatientNotFound()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        _patientRepositoryMock.Setup(r => r.GetByIdAsync(patientId)).ReturnsAsync((PatientInfo)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => _patientService.GetPatientByIdAsync(patientId));
        Assert.Equal("Patient not found", ex.Message);
        _patientRepositoryMock.Verify(r => r.GetByIdAsync(patientId), Times.Once);
    }

    [Fact]
    public async Task CreatePatientAsync_CreatesPatient_WithDefaultMedicalRecord()
    {
        // Arrange
        var dto = new CreatePatientDto
        {
            Name = "Alice",
            Surname = "Johnson",
            Birthdate = new DateTime(1975, 3, 3)
        };
        PatientInfo capturedPatient = null;
        _patientRepositoryMock.Setup(r => r.AddAsync(It.IsAny<PatientInfo>()))
            .Callback<PatientInfo>(p => capturedPatient = p)
            .Returns(Task.CompletedTask);

        // Act
        await _patientService.CreatePatientAsync(dto);

        // Assert
        Assert.NotNull(capturedPatient);
        Assert.Equal("Alice", capturedPatient.Name);
        Assert.Equal("Johnson", capturedPatient.Surname);
        Assert.Equal(new DateTime(1975, 3, 3), capturedPatient.Birthdate);
        Assert.Single(capturedPatient.MedicalRecords);
        Assert.Equal("Initial checkup: Healthy", capturedPatient.MedicalRecords.First().DoctorRemarks);
        _patientRepositoryMock.Verify(r => r.AddAsync(It.IsAny<PatientInfo>()), Times.Once);
    }

    [Fact]
    public async Task DeletePatientAsync_DeletesPatient_WhenPatientExists()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var patient = new PatientInfo("Bob", "Williams", new DateTime(1985, 4, 4));
        _patientRepositoryMock.Setup(r => r.GetByIdAsync(patientId)).ReturnsAsync(patient);

        // Act
        await _patientService.DeletePatientAsync(patientId);

        // Assert
        _patientRepositoryMock.Verify(r => r.GetByIdAsync(patientId), Times.Once);
        _patientRepositoryMock.Verify(r => r.DeleteAsync(patient), Times.Once);
    }

    [Fact]
    public async Task DeletePatientAsync_ThrowsException_WhenPatientNotFound()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        _patientRepositoryMock.Setup(r => r.GetByIdAsync(patientId)).ReturnsAsync((PatientInfo)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => _patientService.DeletePatientAsync(patientId));
        Assert.Equal("Patient not found", ex.Message);
        _patientRepositoryMock.Verify(r => r.GetByIdAsync(patientId), Times.Once);
        _patientRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<PatientInfo>()), Times.Never);
    }

  
    [Fact]
    public async Task GetAIPredictionAsync_ThrowsKeyNotFoundException_WhenPatientNotFound()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        _patientRepositoryMock.Setup(r => r.GetByIdAsync(patientId)).ReturnsAsync((PatientInfo)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() => _patientService.GetAIPredictionAsync(patientId));
        Assert.Equal("Patient not found.", ex.Message);
        _patientRepositoryMock.Verify(r => r.GetByIdAsync(patientId), Times.Once);
    }

    [Fact]
    public async Task GetAIPredictionAsync_ThrowsUnauthorizedAccessException_WhenTokenMissing()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var patient = new PatientInfo("Charlie", "Brown", new DateTime(1970, 5, 5));
        _patientRepositoryMock.Setup(r => r.GetByIdAsync(patientId)).ReturnsAsync(patient);

        var httpContext = new Mock<HttpContext>();
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        httpContext.Setup(c => c.User).Returns(user);
        httpContext.Setup(c => c.Request.Headers).Returns(new HeaderDictionary());
        _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContext.Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _patientService.GetAIPredictionAsync(patientId));
        Assert.Equal("JWT token is missing.", ex.Message);
        _patientRepositoryMock.Verify(r => r.GetByIdAsync(patientId), Times.Once);
    }
}
