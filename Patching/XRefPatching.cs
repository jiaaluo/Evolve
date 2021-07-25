using Evolve.ConsoleUtils;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnhollowerRuntimeLib.XrefScans;

namespace Evolve.Patch
{
    class XRefPatching
    {
        private static MethodInfo Method;

        public static MethodInfo Find()
        {
            return Method = typeof(Extensions).GetMethods().Single(delegate (MethodInfo Method)
            {
                if (Method.ReturnType == typeof(void) && Method.GetParameters().Length == 2)
                {
                    return XrefScanner.XrefScan(Method).Any(XrefInstance =>
                    {
                        if (XrefInstance.Type == XrefType.Global)
                        {
                            Il2CppSystem.Object @object = XrefInstance.ReadAsObject();
                            if (@object != null)
                            {
                                if (@object.ToString().Contains(""))
                                {
                                    EvoConsole.Log($"{Method.Name} : {@object.ToString()}");
                                    XRefPatching.Method = Method;
                                    return true;
                                }
                            }
                        }
                        return false;
                    });
                }
                return false;
            });
        }
    }
}
