using MediatR;

namespace DanceStudio.Reviews.Application.Common.Interfaces
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}