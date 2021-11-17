using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Stealer
{
    public class Spy
    {
        public string RevealPrivateMethods(string className)                                                                                    //
        {                                                                                                                                       //
            Type typeClass = Type.GetType(className);                                                                                           //
            MethodInfo[] methods = typeClass.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);                   //
            StringBuilder sb = new StringBuilder();                                                                                             //
            sb.AppendLine($"All Private Methods Of Class: {typeClass.FullName}");                                                               //
            sb.AppendLine($"Base Class: {typeClass.BaseType.Name}");                                                                            //
            foreach (var method in methods)                                                                                                     // 3. Mission Private Impossible
            {                                                                                                                                   //
                sb.AppendLine(method.Name);                                                                                                     //
            }                                                                                                                                   //
                                                                                                                                                //
            return sb.ToString().TrimEnd();                                                                                                     //
        }
     
    }
}
