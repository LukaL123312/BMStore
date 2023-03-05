using BMStore.Api.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Xunit;

namespace BMStore.Api.Tests.MiddlewareTests.ExceptionMiddlewareTests;

public class ExceptionMiddlewareTests
{
    private readonly DefaultHttpContext httpContext;
    private readonly Mock<ILogger<ExceptionMiddleware>> iloggerMock;
    private ExceptionMiddleware? _sut;
    private RequestDelegate? next;

    public ExceptionMiddlewareTests()
    {
        httpContext = new();
        iloggerMock = new();
    }

    [Fact]
    public async void HandleExceptionAsync_SetsStatusCodeAndErrorMessageCorrectly_WhenDefaultException()
    {
        // Arrange
        var exception = new Exception();
        next = (httpContext) => {
            return Task.FromException(exception);
        };
        var contentType = "application/json";
        _sut = new ExceptionMiddleware(next, iloggerMock.Object);

        // Act
        await _sut.InvokeAsync(httpContext);

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)httpContext.Response.StatusCode);
        Assert.Equal(contentType, httpContext.Response.ContentType);
    }
}

