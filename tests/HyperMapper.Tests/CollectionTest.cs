using HyperMapper.Resolvers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HyperMapper.Tests
{
    public class CollectionTest
    {
        public static IEnumerable<object[]> collectionTestData = new object[][]
        {
            new object[]{ new List<int>{ 1,10, 100 } , new List<int>{ 1,10, 100 } },
            new object[]{ new LinkedList<int>(new[]{ 1, 10, 100 }) , new List<int>{ 1,10, 100 } },
            new object[]{ new int[] { 1,10, 100 } ,    new List<int>{ 1,10, 100 } },

                //new object[]{ new int[]{ 1,10, 100 } , null },
                
                //new object[]{ new LinkedList<int>(new[] { 1, 10, 100 }) , null },
                //new object[]{ new Queue<int>(new[] { 1, 10, 100 }) , null },
                //new object[]{ new HashSet<int>(new[] { 1, 10, 100 }), null },
                //new object[]{ new ReadOnlyCollection<int>(new[] { 1, 10, 100 }), null },
                //new object[]{ new ObservableCollection<int>(new[] { 1, 10, 100 }), null },
                //new object[]{ new ReadOnlyObservableCollection<int>(new ObservableCollection<int>(new[] { 1, 10, 100 })), null },
        };


        [Theory]
        [MemberData(nameof(collectionTestData))]
        public void CollectionCheck<T, U>(T from, U to)
        {
            BuiltinResolver.Instance.GetMapper<T, U>().Map(from, BuiltinResolver.Instance).Is(to);
        }
    }
}
