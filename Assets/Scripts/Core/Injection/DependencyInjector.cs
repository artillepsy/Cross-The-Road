using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Injection
{
    public class DependencyInjector : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> singleInstances;
        private List<InvokeData> _invokers = new List<InvokeData>();
        
        private void Start()
        {
            var monoBehaviours = FindObjectsOfType<MonoBehaviour>(true);
            foreach (var monoBehaviour in monoBehaviours)
            {
                var methods = monoBehaviour.GetType()
                    .GetMethods()
                    .Where(method => Attribute.IsDefined(method, typeof(InjectAttribute)));
                foreach (var methodInfo in methods)
                {
                    _invokers.Add(new InvokeData(methodInfo, monoBehaviour));
                }
            }
            foreach (var monoBehaviour in singleInstances)
            {
                InjectValues(monoBehaviour);
            }
        }

        private void InjectValues(MonoBehaviour monoBehaviour)
        {
            // Debug.Log(monoBehaviour.GetType());
            foreach (var invokeData in _invokers)
            {
                var types = new List<Type>();
                foreach (var parameter in invokeData.MethodInfo.GetParameters())
                {
                    types.Add(parameter.ParameterType);
                }
                if (types.Contains(monoBehaviour.GetType()))
                {
                    invokeData.MethodInfo.Invoke(invokeData.Target, new object[] {monoBehaviour});
                }
            }
        }
    }
}