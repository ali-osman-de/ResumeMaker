using MediatR;

namespace ResumeMaker.Application.Common;

public interface IServiceResultWrapper
{
    public interface IRequestByServiceResult<T> : IRequest<ServiceResult<T>>;
    public interface IRequestByServiceResult : IRequest<ServiceResult>;
}
