namespace Application.Shared
{
    public interface TEnvelope<T>
    {
        string Key { get; set; }
        T Value { get; set; }
        string Topic { get; }
    }
}
