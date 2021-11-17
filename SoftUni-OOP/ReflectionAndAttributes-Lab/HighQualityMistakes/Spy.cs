using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Stealer
{
    public class Spy
    {
        public string AnalyzeAccessModifiers(string className)                                                                     
        {                                                                                                                          
            StringBuilder sb = new StringBuilder();                                                                                
            Type typeClass = Type.GetType(className);                                                                              
            FieldInfo[] fields = typeClass.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);        
            
            foreach (var fieldInfo in fields)                                                                                      
            {                                                                                                                      
                sb.AppendLine($"{fieldInfo.Name} must be private!");                                                               
            }                                                                                                                      
                                                                                                                                   
            MethodInfo[] nonPublics = typeClass.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);                        
            MethodInfo[] publics = typeClass.GetMethods(BindingFlags.Public | BindingFlags.Instance);                              
            foreach (var methodInfo in nonPublics.Where(m => m.Name.StartsWith("get")))                                            
            {                                                                                                                      
                sb.AppendLine($"{methodInfo.Name} have to be public!");                                                            
            }                                                                                                                      
                                                                                                                                   
            foreach (var methodInfo in publics.Where(m => m.Name.StartsWith("set")))                                               
            {                                                                                                                     
                sb.AppendLine($"{methodInfo.Name} have to be private!");                                                           
            }                                                                                                                      
                                                                                                                                   
            return sb.ToString().TrimEnd();                                                                                        
        }
    }
}
