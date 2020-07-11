namespace Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure
{
  using Fixie;
  using Microsoft.Extensions.DependencyInjection;
  using Newtonsoft.Json;
  using System;
  using System.Reflection;
  using System.Text.Json;
  using System.Threading.Tasks;

  [NotTest]
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class NotTest : Attribute { }

  [NotTest]
  public class TestingConvention : Discovery, Execution, IAsyncDisposable, IDisposable
  {
    private readonly IServiceScopeFactory ServiceScopeFactory;
    private AliceApplication AliceApplication;
    private bool Disposed;
    private FaberApplication FaberApplication;
    private readonly ServiceProvider ServiceProvider;
    //private FaberWebApplicationFactory FaberWebApplicationFactory;
    //private AliceWebApplicationFactory AliceWebApplicationFactory;

    public TestingConvention()
    {
      var testServices = new ServiceCollection();
      ConfigureTestServices(testServices);
      ServiceProvider = testServices.BuildServiceProvider();
      ServiceScopeFactory = ServiceProvider.GetService<IServiceScopeFactory>();

      Classes.Where(aType => aType.IsPublic && !aType.Has<NotTest>())
        .Where(aType => aType.Name.Contains("AliceServer"));
      Methods.Where(aMethodInfo => aMethodInfo.Name != nameof(Setup));
    }

    public void Dispose()
    {
      Dispose(aIsDisposing: true);
      GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
      Console.WriteLine("==== TestingConvention.DisposeAsync ====");
      await ServiceProvider.DisposeAsync().ConfigureAwait(false);
      await FaberApplication.DisposeAsync().ConfigureAwait(false);
      await AliceApplication.DisposeAsync().ConfigureAwait(false);
      Dispose(aIsDisposing: false);
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
      //FaberServer = new FaberServer();
      //AliceServer = new AliceServer();
      //FaberWebApplicationFactory = new FaberWebApplicationFactory();
      //AliceWebApplicationFactory = new AliceWebApplicationFactory();

      //aServiceCollection.AddSingleton<WebApplicationFactory<Startup>>(FaberWebApplicationFactory);
      //aServiceCollection.AddSingleton(FaberWebApplicationFactory);
      //aServiceCollection.AddSingleton(AliceWebApplicationFactory);

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
        //FaberWebApplicationFactory?.Dispose();
        //AliceWebApplicationFactory?.Dispose();
        DisposeAsync().GetAwaiter().GetResult();
        ServiceScopeFactory?.Dispose();
      }
      Disposed = true;
    }
  }
}
