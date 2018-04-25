﻿namespace ITOps.ViewModelComposition
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static void AddViewModelComposition(this IServiceCollection services,
            string assemblySearchPattern = "*ViewModelComposition*.dll")
        {
            var fileNames = Directory.GetFiles(AppContext.BaseDirectory, assemblySearchPattern);

            var types = new List<Type>();
            foreach (var fileName in fileNames)
            {
                var temp = AssemblyLoader.Load(fileName)
                    .GetTypes()
                    .Where(t =>
                    {
                        var typeInfo = t.GetTypeInfo();
                        return !typeInfo.IsInterface
                               && !typeInfo.IsAbstract
                               && typeof(IInterceptRoutes).IsAssignableFrom(t);
                    });

                types.AddRange(temp);
            }

            foreach (var type in types) services.AddSingleton(typeof(IInterceptRoutes), type);
        }
    }
}