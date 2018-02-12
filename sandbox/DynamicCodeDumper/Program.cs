using HyperMapper;
using HyperMapper.Internal;
using HyperMapper.Resolvers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DynamicCodeDumper
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var info = MappingInfo.Create<MyClassA, MyClassB>();
                info.BeforeMap = x => Console.WriteLine(x.MyProperty);
                info.AfterMap = x => Console.WriteLine(x.MyProperty3);

                // info.AddMap(x => x.MyProperty, x => x.MyPropertyNano, x => x);
                info.AddMap(x => x.MyProperty, x => x.MyProperty3, x => x);
                info.AddUse(x => x.DT, _ => DateTime.Now);

                // info.WithConvertAction(x => x.MyProperty, x => x.MyProperty, x => new MyClassC());


                info.BuildMapper();


                // DynamicObjectResolver.Default.GetMapper<MyClassA, MyClassB>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                var a = DynamicObjectTypeBuilder.Save();
                Verify(a);
            }
        }

        static void Verify(params AssemblyBuilder[] builders)
        {
            var path = @"C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6.1 Tools\x64\PEVerify.exe";

            foreach (var targetDll in builders)
            {
                var psi = new ProcessStartInfo(path, targetDll.GetName().Name + ".dll")
                {
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                };

                var p = Process.Start(psi);
                var data = p.StandardOutput.ReadToEnd();
                Console.WriteLine(data);
            }
        }
    }

    public class MyClassA
    {
        public MyClassC MyProperty { get; set; }
    }

    public struct MyClassB
    {
        public int MyPropertyNano { get; set; }
        public MyClassC MyProperty3 { get; set; }
        public DateTime DT { get; set; }
    }

    public class MyClassC
    {
        public int Foo { get; set; }
    }
}