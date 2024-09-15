using Ardalis.Result;
using MediatR;

namespace BakeryShop.Application.Common.Abstractions;

/// <summary>
/// Base interface of all application queries
/// </summary>
/// <typeparam name="TResponse">Return data type</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;

/// <summary>
/// Base interface of query handlers
/// </summary>
/// <typeparam name="TRequest">Query type</typeparam>
/// <typeparam name="TResponse">Return data type</typeparam>
public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
    where TRequest : IQuery<TResponse>;