using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ExpressionTreeSample.Engine;

namespace ExpressionTreeSample.ConsoleApp
{

    internal class Program
    {
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                })
                .ConfigureServices((context, services) =>
                {
                    //add your service registrations
                    services.AddSingleton<IInputDataRepository, InputDataRepository>();
                });

            return hostBuilder;
        }

        private static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var repository = host.Services.GetService<IInputDataRepository>();
            var data = repository.GetData();


            Console.WriteLine("#########################################");
            Console.WriteLine("First 10 input data objects::");
            data.Take(10).ToList().ForEach(x => Console.WriteLine(x.ToString()));
            Console.WriteLine("#########################################");
            Console.WriteLine("Provide operations on data, for example (A+B):");
            var inputOperation = Console.ReadLine();
            Console.WriteLine($"Starting executing provided operations ({inputOperation})");

            var result = data.Select(x => ExpressionTreeHelper.CallAnyMathOperation<InputData>(inputOperation)(x)).ToList();

            Console.WriteLine("#########################################");
            Console.WriteLine("Operation succeeded. First 10 results:");
            Console.WriteLine("#########################################");
            result.Take(10).ToList().ForEach(x => Console.WriteLine(x.ToString()));
            Console.WriteLine("#########################################");
            Console.WriteLine(ExpressionTreeHelper.SerializeAnyMathOperation<InputData>(inputOperation));
        }
    }
}