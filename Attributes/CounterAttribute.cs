using Metrics;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Concurrent;
using System.Xml.Linq;

namespace Back.Attributes
{
    public class CounterAttribute : Attribute, IAuthorizationFilter
    {
        private static readonly ConcurrentDictionary<string, Counter> Counters = new ConcurrentDictionary<string, Counter>();
        private readonly string _name;

        public CounterAttribute(string name)
        {
            _name = name;
            Counters.GetOrAdd(_name, _ => Metric.Counter(name, Unit.Calls));
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (Counters.ContainsKey("AllEndpoints"))
            {
                Counters.GetValueOrDefault("AllEndpoints").Increment();
            }
            else
            {
                Counters.TryAdd("AllEndpoints", Metric.Counter("AllEndpoints", Unit.Calls));
                Counters.GetValueOrDefault("AllEndpoints").Increment();
            }
            if (Counters.TryGetValue(_name, out var counter))
            {
                counter.Increment();
            }
            else
            {
                throw new Exception($"Counter for {_name} not found.");
            }
        }
    }
}
