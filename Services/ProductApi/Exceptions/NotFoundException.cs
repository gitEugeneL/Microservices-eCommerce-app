namespace ProductApi.Exceptions;

public sealed class NotFoundException(string name, object key) 
    : Exception($"Entity: {name} ({key}) not found");
