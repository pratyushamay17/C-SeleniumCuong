﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Driver
{
    public static class FactoryBuilder
    {

        private static readonly IDictionary<string, Type> factories = Assembly.GetExecutingAssembly()
                                                                              .GetTypes()
                                                                              .Where(type => type.GetInterface(typeof(IDriverFactory).ToString()) != null)
                                                                              .ToDictionary(type => type.Name.ToLower(), type => type);

        public static IDriverFactory GetFactory(string browser)
        {
            foreach (var factory in factories)
            {
                if (factory.Key.Contains(browser))
                {
                    return Activator.CreateInstance(factory.Value) as IDriverFactory;
                }
            }
            throw new Exception("Driver factory " + browser + " not supported");
        }

        public static IDriverFactory GetFactory(string browser, string gridUrl)
        {
            return new RemoteDriverFactory(browser, gridUrl);
        }
    }
}
