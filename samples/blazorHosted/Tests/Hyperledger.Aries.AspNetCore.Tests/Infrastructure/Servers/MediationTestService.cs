﻿namespace Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure
{
  using MediatR;
  using Microsoft.Extensions.DependencyInjection;
  using System;
  using System.Threading.Tasks;

  public class MediationTestService
  {
    private readonly IServiceScopeFactory ServiceScopeFactory;

    public MediationTestService(IServiceProvider aServiceProvider)
    {
      ServiceScopeFactory = aServiceProvider.GetService<IServiceScopeFactory>();
    }

    internal Task Send(IRequest aRequest)
    {
      return ExecuteInScope
      (
        aServiceProvider =>
        {
          IMediator mediator = aServiceProvider.GetService<IMediator>();

          return mediator.Send(aRequest);
        }
      );
    }

    internal Task<TResponse> Send<TResponse>(IRequest<TResponse> aRequest)
    {
      return ExecuteInScope
      (
        aServiceProvider =>
        {
          IMediator mediator = aServiceProvider.GetService<IMediator>();

          return mediator.Send(aRequest);
        }
      );
    }

    internal async Task<T> ExecuteInScope<T>(Func<IServiceProvider, Task<T>> aAction)
    {
      using IServiceScope serviceScope = ServiceScopeFactory.CreateScope();
      return await aAction(serviceScope.ServiceProvider);
    }
  }

  public partial class TestApplication
  {
    internal Task Send(IRequest aRequest) => MediationTestService.Send(aRequest);
    internal Task<TResponse> Send<TResponse>(IRequest<TResponse> aRequest) =>
      MediationTestService.Send(aRequest);
  }
}
