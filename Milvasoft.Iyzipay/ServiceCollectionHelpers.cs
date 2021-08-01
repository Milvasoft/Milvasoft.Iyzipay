using Microsoft.Extensions.DependencyInjection;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System;

namespace Milvasoft.Iyzipay
{
    public static class ServiceCollectionHelpers
    {
        /// <summary>
        /// Adds iyzipay services to <see cref="IServiceCollection"/>.
        /// You can define <see cref="RestHttpClient"/> and <see cref="RestHttpClientV2"/> lifetimes. 
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddIyzicoIntegration(this IServiceCollection services, Action<IOptions> options)
        {
            if (options == null)
                throw new Exception("Please provide Options.");

            var config = new Options();

            options.Invoke(config);

            services = services.AddSingleton<IOptions>(config);

            services = config.RestHttpClientLifeTime switch
            {
                ServiceLifetime.Singleton => services.AddSingleton<IRestHttpClient, RestHttpClient>(),
                ServiceLifetime.Scoped => services.AddScoped<IRestHttpClient, RestHttpClient>(),
                ServiceLifetime.Transient => services.AddTransient<IRestHttpClient, RestHttpClient>(),
                _ => services.AddScoped<IRestHttpClient, RestHttpClient>(),
            };

            services = config.RestHttpClientV2LifeTime switch
            {
                ServiceLifetime.Singleton => services.AddSingleton<IRestHttpClientV2, RestHttpClientV2>(),
                ServiceLifetime.Scoped => services.AddScoped<IRestHttpClientV2, RestHttpClientV2>(),
                ServiceLifetime.Transient => services.AddTransient<IRestHttpClientV2, RestHttpClientV2>(),
                _ => services.AddScoped<IRestHttpClientV2, RestHttpClientV2>(),
            };

            services.AddHttpClient();

            return services;
        }
    }
}
