namespace Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure
{
  using Fixie;
  using Microsoft.Extensions.DependencyInjection;
  using Newtonsoft.Json;
  using System;
  using System.Reflection;
  using System.Text.Json;

  [NotTest]
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class NotTest : Attribute { }

  [NotTest]
  public sealed class TestingConvention : Discovery, Execution, IDisposable
  {
    private readonly ServiceProvider ServiceProvider;
    private readonly IServiceScopeFactory ServiceScopeFactory;
    private AliceApplication AliceApplication;
    private bool Disposed;
    private FaberApplication FaberApplication;

    public TestingConvention()
    {
      var testServices = new ServiceCollection();
      ConfigureTestServices(testServices);
      ServiceProvider = testServices.BuildServiceProvider();
      ServiceScopeFactory = ServiceProvider.GetService<IServiceScopeFactory>();

      Classes.Where(aType => aType.IsPublic && !aType.Has<NotTest>());
        //.Where(aType => aType.Name.Contains("AliceServer"));
      Methods.Where(aMethodInfo => aMethodInfo.Name != nameof(Setup));
    }

    public void Dispose()
    {
      Dispose(aIsDisposing: true);
      GC.SuppressFinalize(this);
    }

    public void Execute(TestClass aTestClass)
    {
      aTestClass.RunCases
      (
        aCase =>
        {
          using IServiceScope serviceScope = ServiceScopeFactory.CreateScope();
          object instance = serviceScope.ServiceProvider.GetService(aTestClass.Type);
          Setup(instance);

          aCase.Execute(instance);
          instance.Dispose();
        }
      );
    }

    private static void Setup(object aInstance)
    {
      MethodInfo methodInfo = aInstance.GetType().GetMethod(nameof(Setup));
      methodInfo?.Execute(aInstance);
    }

    private void ConfigureTestServices(ServiceCollection aServiceCollection)
    {
      FaberApplication FaberServerFactory(IServiceProvider aServiceProvider)
      {
        FaberApplication = new FaberApplication();
        return FaberApplication;
      }

      AliceApplication AliceServerFactory(IServiceProvider aServiceProvider)
      {
        AliceApplication = new AliceApplication();
        return AliceApplication;
      }

      aServiceCollection.AddSingleton<FaberApplication>(FaberServerFactory);
      aServiceCollection.AddSingleton<AliceApplication>(AliceServerFactory);
      aServiceCollection.AddSingleton(new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
      aServiceCollection.AddSingleton(new JsonSerializerSettings());
      aServiceCollection.Scan
      (
        aTypeSourceSelector => aTypeSourceSelector
          .FromAssemblyOf<TestingConvention>()
          .AddClasses(action: (aClasses) => aClasses.Where(aType => aType.IsPublic && !aType.Has<NotTest>()))
          .AsSelf()
          .WithScopedLifetime()
      );
    }

    private void Dispose(bool aIsDisposing)
    {
      if (Disposed) return;

      if (aIsDisposing)
      {
        Console.WriteLine("==== TestingConvention.Dispose ====");
        FaberApplication?.DisposeAsync().GetAwaiter().GetResult();
        AliceApplication?.DisposeAsync().GetAwaiter().GetResult();
        ServiceScopeFactory?.Dispose();
      }
      Disposed = true;
    }
  }
}
