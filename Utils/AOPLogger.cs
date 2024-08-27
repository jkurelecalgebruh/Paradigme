using Castle.DynamicProxy;
using Metrics;

namespace Back.Utils
{ 
    public class AOPLogger : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                Console.WriteLine($"Entering method {invocation.Method.Name} with arguments {string.Join(", ", invocation.Arguments)}");
                invocation.Proceed();
                Console.WriteLine($"Exiting method {invocation.Method.Name} with return value {invocation.ReturnValue}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in method {invocation.Method.Name}: {ex.Message}");
            }
        }
    }
}
