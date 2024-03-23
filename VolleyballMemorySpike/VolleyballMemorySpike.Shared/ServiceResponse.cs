namespace BlazorEcommerce.Shared;

public class ServiceResponse<T>
{
    public T? Value { get; set; }

    public bool IsSuccess { get; set; } = true;

    public string Message { get; set; } = string.Empty;
}
