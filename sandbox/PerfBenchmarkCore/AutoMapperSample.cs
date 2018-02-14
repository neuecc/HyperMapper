using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Benchmark.Flattening
{
    [MessagePackObject]
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

        [Key(0)]
        public string Name { get; set; }

        [Key(1)]
        public int Int32 { get; set; }

        [Key(2)]
        public long Int64 { set; get; }

        [Key(3)]
        public int? NullInt { get; set; }

        [Key(4)]
        public float Floatn { get; set; }

        [Key(5)]
        public double Doublen { get; set; }

        [Key(6)]
        public DateTime DateTime { get; set; }

        [Key(7)]
        public InnerFoo Foo1 { get; set; }

        [Key(8)]
        public List<InnerFoo> Foos { get; set; }

        [Key(9)]
        public InnerFoo[] FooArr { get; set; }

        [Key(10)]
        public int[] IntArr { get; set; }

        [Key(11)]
        public int[] Ints { get; set; }
    }

    [MessagePackObject]
    public class InnerFoo
    {
        [Key(0)]
        public string Name { get; set; }
        [Key(1)]
        public int Int32 { get; set; }
        [Key(2)]
        public long Int64 { set; get; }
        [Key(3)]
        public int? NullInt { get; set; }
    }

    [MessagePackObject]
    public class InnerFooDest
    {
        [Key(0)]
        public string Name { get; set; }
        [Key(1)]
        public int Int32 { get; set; }
        [Key(2)]
        public long Int64 { set; get; }
        [Key(3)]
        public int? NullInt { get; set; }
    }

    [MessagePackObject]
    public class FooDest
    {
        [Key(0)]
        public string Name { get; set; }

        [Key(1)]
        public int Int32 { get; set; }

        [Key(2)]
        public long Int64 { set; get; }

        [Key(3)]
        public int? NullInt { get; set; }

        [Key(4)]
        public float Floatn { get; set; }

        [Key(5)]
        public double Doublen { get; set; }

        [Key(6)]
        public DateTime DateTime { get; set; }

        [Key(7)]
        public InnerFooDest Foo1 { get; set; }

        [Key(8)]
        public List<InnerFooDest> Foos { get; set; }

        [Key(9)]
        public InnerFooDest[] FooArr { get; set; }

        [Key(10)]
        public int[] IntArr { get; set; }

        [Key(11)]
        public int[] Ints { get; set; }
    }
}