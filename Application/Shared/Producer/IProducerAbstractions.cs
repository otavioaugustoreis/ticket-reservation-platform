using Application.Shared;

namespace Application.Shared.Producer
{
    public interface IProducerAbstractions
    {
        Task ProduceAsync<T>(TEnvelope<T> envelope, CancellationToken cancellationToken);
    }
}
