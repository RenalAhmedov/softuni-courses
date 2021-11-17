using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Stealer
{
    public class Spy
    {  
        public string RevealPrivateMethods(string className)                                                        //
        {                                                                                                               //
            StringBuilder sb = new StringBuilder();                                                                     //
            Type typeClass = Type.GetType(className);                                                                   //
            MethodInfo[] allMethods = typeClass.GetMethods(BindingFlags.Static | BindingFlags.NonPublic |               //
                                                        BindingFlags.Instance | BindingFlags.Public);                   //
            foreach (var method in allMethods.Where(m => m.Name.StartsWith("get")))                                     //
            {                                                                                                           //
                sb.AppendLine($"{method.Name} will return {method.ReturnType}");                                        // 4. Collector
            }                                                                                                           //
                                                                                                                        //
            foreach (var method in allMethods.Where(m => m.Name.StartsWith("set")))                                     //
            {                                                                                                           //
                sb.AppendLine($"{method.Name} will set field of {method.GetParameters().First().ParameterType}");       //
            }                                                                                                           //
                                                                                                                        //
            return sb.ToString().TrimEnd();                                                                             //
        }
        
    }
}
