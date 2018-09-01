using System.Collections.Generic;
using Cbn.Infrastructure.Common.Foundation;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.TestTools.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cbn.Infrastructure.TestTools.Tests
{
    [TestClass]
    public class MapperTest
    {
        [TestMethod]
        public void MapTest001()
        {
            var mapper = new Mapper();
            var e1 = new MapTest01 { MyProperty01 = "01", MyProperty02 = 2, MyProperty03 = new List<string> { "03", "04" } };
            var a1 = mapper.Map<MapTest02>(e1);
            a1.Is(e1);
            var e2 = new List<MapTest01>
            {
                new MapTest01 { MyProperty01 = "01", MyProperty02 = 2, MyProperty03 = new List<string> { "03", "04" } },
                new MapTest01 { MyProperty01 = "01", MyProperty02 = 2, MyProperty03 = new List<string> { "03", "04" } },
            };
            var a2 = mapper.Map<MapTest02>(e2);
            a2.Is(e2);
            var e3 = new MapTest03 { MyProperty01 = "01" };
            var a3 = mapper.Map<MapTest03>(e1);
            a3.Is(e3);
            Assert.ThrowsException<AssertException>(() => a3.Is(mapper.Map<MapTest04>(a3)));
        }

        [TestMethod]
        public void MapTest002()
        {
            var mapRegister = new MapRegister();
            mapRegister.Register<MapTest03, MapTest04>((s, d) =>
            {
                d.MyProperty01 = int.Parse(s.MyProperty01) + 1;
            });
            mapRegister.Register<MapTest04, MapTest03>((s, d) =>
            {
                d.MyProperty01 = (s.MyProperty01 - 1).ToString();
            });
            this.MapTestInner(mapRegister);
        }

        [TestMethod]
        public void MapTest003()
        {
            var mapRegister = new MapRegister();
            mapRegister.RegisterDefinition(new MapDefinition01());
            this.MapTestInner(mapRegister);
        }

        [TestMethod]
        public void MapTest004()
        {
            var mapRegister = new MapRegister();
            mapRegister.RegisterDefinition(new MapDefinition01());
            var mapper = new Mapper(mapRegister);

            var expected1 = new MapTest05 { MyProperty01 = "1" };
            var expected2 = new MapTest04 { MyProperty01 = 2 };
            var actual2 = mapper.Map<MapTest04>(expected1);
            var actual1 = mapper.Map<MapTest05>(actual2);
            actual1.Is(expected1);
            actual2.Is(expected2);
        }

        private void MapTestInner(MapRegister mapRegister)
        {
            var mapper = new Mapper(mapRegister);

            var expected1 = new MapTest03 { MyProperty01 = "1" };
            var expected2 = new MapTest04 { MyProperty01 = 2 };
            var actual2 = mapper.Map<MapTest04>(expected1);
            var actual1 = mapper.Map<MapTest03>(actual2);
            actual1.Is(expected1);
            actual2.Is(expected2);
        }

        class MapTest01
        {
            public string MyProperty01 { get; set; }
            public int MyProperty02 { get; set; }
            public List<string> MyProperty03 { get; set; }
        }
        class MapTest02
        {
            public string MyProperty01 { get; set; }
            public int? MyProperty02 { get; set; }
            public IEnumerable<string> MyProperty03 { get; set; }
        }
        class MapTest03
        {
            public string MyProperty01 { get; set; }
        }
        class MapTest04
        {
            public int MyProperty01 { get; set; }
        }
        class MapTest05 : MapTest03 { }
        class MapDefinition01 : IMapDefinition<MapTest03, MapTest04>
        {
            public void Map(MapTest03 source, MapTest04 destination)
            {
                destination.MyProperty01 = int.Parse(source.MyProperty01) + 1;
            }

            public void MapReverse(MapTest04 source, MapTest03 destination)
            {
                destination.MyProperty01 = (source.MyProperty01 - 1).ToString();
            }
        }
    }
}