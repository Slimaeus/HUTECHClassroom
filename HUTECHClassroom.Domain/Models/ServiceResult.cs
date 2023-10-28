namespace HUTECHClassroom.Domain.Models;

public sealed class ServiceResult<TData>
{
    public bool IsSuccess { get; set; } = true;
    public TData? Data { get; set; } = default;
    public ICollection<string> Errors { get; set; } = new HashSet<string>();

    public static ServiceResult<TData> Success(TData data)
        => new() { Data = data };
    public static ServiceResult<TData> Error(string error)
        => new() { IsSuccess = false, Errors = new List<string> { error } };
    public static ServiceResult<TData> Error(ICollection<string> error)
        => new() { IsSuccess = false, Errors = error };
}
