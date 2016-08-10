using System;
using System.Collections.Generic;
using LightInject;

namespace opcode.ioc.ligthinject
{
    public static class IocRegistrator
    {
        public static ServiceContainer Container = new ServiceContainer();

        public static void Register(string[] dllList)
        {
            //var path = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            //Container.EnableAnnotatedPropertyInjection();

            foreach (var dll in dllList)
            {
                RegisterDll(dll);
            }
        }

        public static void Register(Type serviceType, Type implType, string serviceName)
        {
            Container.Register(serviceType, implType, serviceName);
        }

        private static void RegisterDll(string dll)
        {
            try
            {
                Container.RegisterAssembly(dll); 
            }
            catch (Exception ex)
            {
                throw new Exception($"opcode.ioc.lightinject.IocRegistrator [{dll}]: {ex.Message}");
            }
        }

        public static T GetObject<T>(string key)
        {
            return Container.GetInstance<T>(key);
        }

        public static T GetObject<T>()
        {
            return Container.GetInstance<T>();
        }

        public static List<string> RegisteredServices()
        {
            var r = new List<string>();
            foreach (var rs in Container.AvailableServices)
            {
                r.Add($"Instance: {rs.ServiceType.FullName} \r\nImplementation: {rs.ImplementingType.FullName} \r\nKey: {rs.ServiceName}");
            }

            return r;
        }
    }
}
