using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicCodeDumper
{
    public class Foo
    {
        public static Foo New() => new Foo
        {
            Name = "foo",
            Int32 = 12,
            Int64 = 123123,
            NullInt = 16,
            DateTime = DateTime.Now,
            Doublen = 2312112,
            Foo1 = new InnerFoo { Name = "foo one" },
            Foos = new List<InnerFoo>
                {
                    new InnerFoo {Name = "j1", Int64 = 123, NullInt = 321},
                    new InnerFoo {Name = "j2", Int32 = 12345, NullInt = 54321},
                    new InnerFoo {Name = "j3", Int32 = 12345, NullInt = 54321},
                },
            FooArr = new[]
                {
                    new InnerFoo {Name = "a1"},
                    new InnerFoo {Name = "a2"},
                    new InnerFoo {Name = "a3"},
                },
            IntArr = new[] { 1, 2, 3, 4, 5 },
            Ints = new[] { 7, 8, 9 },
        };

        public string Name { get; set; }

        public int Int32 { get; set; }

        public long Int64 { set; get; }

        public int? NullInt { get; set; }

        public float Floatn { get; set; }

        public double Doublen { get; set; }

        public DateTime DateTime { get; set; }

        public InnerFoo Foo1 { get; set; }

        public List<InnerFoo> Foos { get; set; }

        public InnerFoo[] FooArr { get; set; }

        public int[] IntArr { get; set; }

        public int[] Ints { get; set; }
    }

    public class InnerFoo
    {
        public string Name { get; set; }
        public int Int32 { get; set; }
        public long Int64 { set; get; }
        public int? NullInt { get; set; }
    }

    public class InnerFooDest
    {
        public string Name { get; set; }
        public int Int32 { get; set; }
        public long Int64 { set; get; }
        public int? NullInt { get; set; }
    }

    public class FooDest
    {
        public string Name { get; set; }

        public int Int32 { get; set; }

        public long Int64 { set; get; }

        public int? NullInt { get; set; }

        public float Floatn { get; set; }

        public double Doublen { get; set; }

        public DateTime DateTime { get; set; }

        public InnerFooDest Foo1 { get; set; }

        public List<InnerFooDest> Foos { get; set; }

        public InnerFooDest[] FooArr { get; set; }

        public int[] IntArr { get; set; }

        public int[] Ints { get; set; }
    }
}
