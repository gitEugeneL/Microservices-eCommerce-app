namespace ProductApi.Exceptions;

public sealed class UnauthorizedException(string message) : Exception(message);
