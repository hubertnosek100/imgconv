using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace API.Infrastructure
{
    public static class ReflectiveEnumerator
    {
        public static IEnumerable<T> GetEnumerableOfType<T>(IServiceProvider provider) where T : class
        {
            List<T> objects = new List<T>();
            foreach (Type type in
                Assembly.GetAssembly(typeof(T)).GetTypes()
                    .Where(myType =>
                        (myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))) |
                        (myType.IsClass && !myType.IsAbstract && typeof(T).IsAssignableFrom(myType))))
            {
                ParameterInfo[] infos = type.GetConstructors().FirstOrDefault()?.GetParameters() ??
                                        new ParameterInfo[] { };

                objects.Add((T) Activator.CreateInstance(type,
                    infos.Select(x => x.ParameterType).Select(provider.GetService).ToArray()));
            }

            return objects;
        }
    }
}