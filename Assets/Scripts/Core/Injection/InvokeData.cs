using System.Reflection;

namespace Core.Injection
{
    public class InvokeData
    {    
        public MethodInfo MethodInfo { get; }
        public object Target { get; }
        
        public InvokeData(MethodInfo methodInfo, object target)
        {
            MethodInfo = methodInfo;
            Target = target;
        }
    }
}