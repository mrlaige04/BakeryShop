using Ardalis.Result;
using MediatR;

namespace BakeryShop.Application.Common.Abstractions;

/// <summary>
/// Base interface of all commands in system
/// Such commands that return no data
/// </summary>
public interface ICommand : IRequest<Result>;

/// <summary>
/// Base interface of commands that return data
/// </summary>
/// <typeparam name="TResponse">Type of return data</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>;

/// <summary>
/// Base interface of Command Handler that handles commands that return no data
/// </summary>
/// <typeparam name="TRequest">Command type</typeparam>
public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest, Result> where TRequest : ICommand;

/// <summary>
/// Base interface of Command Handler that handles commands that return data
/// </summary>
/// <typeparam name="TRequest">Command type</typeparam>
/// <typeparam name="TResponse">Return data type</typeparam>
public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>> 
    where TRequest : ICommand<TResponse>;