# Global Error Handler

Creating a **.NET 𝗴𝗹𝗼𝗯𝗮𝗹 𝗲𝗿𝗿𝗼𝗿 𝗵𝗮𝗻𝗱𝗹𝗲r** to catch unhandled exceptions that occur anywhere in an application.

Before .NET 8 you had to implement custom middleware to do this, but starting with ASP.NET Core 8 there’s a new [IExceptionHandler](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.diagnostics.iexceptionhandler?view=aspnetcore-8.0) interface that makes this much easier.

The idea behind is to map exceptions to the correct HTTP status code and log the appropriate exception details.


## Problem details
Let's apply the standard error responses [RFC 7807](https://datatracker.ietf.org/doc/html/rfc7807) that defines a common format for HTTP APIs to communicate errors. By using that standard, API consumers will get a much more useful response.

Technically speaking:
- `AddProblemDetails()` **registers** the Problem Details middleware. This middleware is responsible for handling exceptions and generating standardized problem details responses.
- `UseStatusCodePages()` integrates middleware into the request pipeline that generates Problem Details responses **for common HTTP status codes**. This middleware intercepts responses with certain status codes (like 404, 500) and returns a structured error response with details about the error.
- `UseExceptionHandler()` adds middleware that catches **unhandled exceptions** during request processing. When an unhandled exception occurs, this middleware generates a Problem Details response, detailing the error in a structured format without exposing internal server details.


## Global exception handler
Instead of adding a try/catch block to each endpoint, which would result in a lot of duplicated code, we can create a global exception handler. This handler will:
- Catch all unhandled exceptions and return a problem details response.
- Map each exception to the appropriate problem details response.
- Log the exception details to our logging provider.

The new **IExceptionHandler** interface available with [.NET 8](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-8.0), implementing this global exception handler is straightforward.

Here is an example of the returned result by using the GlobalExceptionHandler:

```json
//500 internal server error
{
    "type": "https://tools.ietf.org/html/rfc9110#section-15.6.1",
    "title": "Internal Error Server: We are on it!",
    "status": 500,
    "detail": "The database connection is closed!"
}

//400 bad request
{
    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
    "title": "Bad request",
    "status": 400,
    "detail": "The id must be greater than 0! (Parameter 'id')"
}
```