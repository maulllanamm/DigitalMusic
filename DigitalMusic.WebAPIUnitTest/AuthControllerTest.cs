using DigitalMusic.Application.Features.AuthFeatures.LoginFeatures;
using DigitalMusic.Application.Features.AuthFeatures.RegisterFeatures;
using DigitalMusic.Application.Helper;
using DigitalMusic.Application.Helper.Interface;
using DigitalMusic.Domain.Entities;
using DigitalMusic.WebAPI.Controllers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Moq;

namespace DigitalMusic.WebAPIUnitTest
{
    public class AuthControllerTest
    {
        private readonly AuthController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IAccessTokenHelper> _accessTokenHelperMock;
        private readonly Mock<IRefreshTokenHelper> _refreshTokenHelperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public AuthControllerTest()
        {
            // Setup mocks
            _mediatorMock = new Mock<IMediator>();
            _accessTokenHelperMock = new Mock<IAccessTokenHelper>();
            _refreshTokenHelperMock = new Mock<IRefreshTokenHelper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Initialize controller with mocks
            _controller = new AuthController(
                _mediatorMock.Object,
                _accessTokenHelperMock.Object,
                _refreshTokenHelperMock.Object,
                _httpContextAccessorMock.Object
            );
        }


        [Fact]
        public async Task Register_SuccessfulRegister_ReturnsOk()
        {
            // Arrange
            var registerRequest = new RegisterRequest(
                Username: "testusername",
                Password: "testpassword",
                Email: "testemail@gmail.com",
                Fullname: "test user",
                PhoneNumber: "1234567890",
                Address: "test address"
            );

            var cancellationToken = CancellationToken.None;
            var expectedResult = new RegisterResponse // Mock expected response
            {
                Username = registerRequest.Username,
                Email = registerRequest.Email,
                FullName = registerRequest.Fullname,
                PhoneNumber = registerRequest.PhoneNumber,
                Address = registerRequest.Address
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<RegisterRequest>(), cancellationToken))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.Register(registerRequest, cancellationToken);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var registerResponse = okResult.Value as RegisterResponse;
            registerResponse.Should().NotBeNull();
            registerResponse.Username.Should().Be(expectedResult.Username);
            registerResponse.Email.Should().Be(expectedResult.Email);
            registerResponse.FullName.Should().Be(expectedResult.FullName);
            registerResponse.PhoneNumber.Should().Be(expectedResult.PhoneNumber);
            registerResponse.Address.Should().Be(expectedResult.Address);
        }

        [Fact]
        public async Task Login_SuccessfulLogin_ReturnsOk()
        {
            // Arrange
            var request = new LoginRequest(
                Username: "testusername",
                Password: "testpassword"
            );

            var cancellationToken = CancellationToken.None;
            var expectedResult = new LoginResponse // Mock expected response
            {
                Id = new Guid(),
                Username = request.Username,
                Email = "testEmail@gmail.com",
                FullName = "testFullName",
                PhoneNumber = "0812345678",
                Address = "testAddress",
                Role = new Role
                {
                    id = 1,
                    name = "testRole"
                }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<LoginRequest>(), cancellationToken))
                .ReturnsAsync(expectedResult);

            _accessTokenHelperMock
                .Setup(m => m.GenerateAccessToken(expectedResult.Id.ToString(), expectedResult.Email,request.Username, expectedResult.Role.name))
                .Returns("fake_access_token");

            _refreshTokenHelperMock
                .Setup(m => m.GenerateRefreshToken(expectedResult.Id.ToString(), expectedResult.Email,request.Username, expectedResult.Role.name))
                .Returns("fake_refresh_token");

            _refreshTokenHelperMock
                .Setup(m => m.SetRefreshToken(request.Username, expectedResult.Role.name));

            // Act
            var result = await _controller.Login(request, cancellationToken);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var loginResponse = okResult.Value as string;
            loginResponse.Should().NotBeNull();
            loginResponse.Should().Be("fake_access_token");
        }
    }
}