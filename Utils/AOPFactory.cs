using Back.Db;
using Castle.DynamicProxy;

namespace Back.Utils
{
    public static class AOPFactory
    {
        private static readonly ProxyGenerator _proxyGenerator = new ProxyGenerator();

        public static T CreateServiceWithParam<T>(Type type, MySqlContext db) where T : class
        {
            return (T)_proxyGenerator.CreateClassProxy(type, new object[] { db }, new AOPLogger());
        }

        public static T CreateService<T>(Type type) where T : class
        {
            return (T)_proxyGenerator.CreateClassProxy(type, new AOPLogger());
        }
    }
}
