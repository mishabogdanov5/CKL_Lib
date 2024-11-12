using CKLLib;
using CKLLib.Operations;

namespace CKLTests
{
    [TestClass]
    public class CKLMathTest
    {
        [TestInitialize]
        public void AccessData() 
        {
            
        }
        
        [TestMethod]
        public void TimeTransformTest1 ()
        {
            CKL source = new CKL(
                    "Port Shedule",
                    new DateTime(2024, 11, 12, 2, 20, 0),
                    new DateTime(2024, 11, 12, 2, 23, 30),
                    new HashSet<string>[] { 
                        new HashSet<string>() { "gruz1", "gruz2", "gruz3"} ,
                        new HashSet<string>() { "port1", "port2"}
                    },
                    new HashSet<RelationItem>() 
                    {
                        new RelationItem(
                            ["gruz1", "port1"],
                            [new DateTime(2024, 11, 12, 2, 20, 30)],
                            [new DateTime(2024, 11, 12, 2, 22, 0)]
                            ),
                        new RelationItem(
                            ["gruz2", "port1"],
                            [new DateTime(2024, 11, 12, 2, 21, 0)],
                            [new DateTime(2024, 11, 12, 2, 23, 0)]
                            ),
                        new RelationItem(
                            ["gruz2", "port2"],
                            [new DateTime(2024, 11, 12, 2, 22, 30)],
                            [new DateTime(2024, 11, 12, 2, 23, 30)]
                            ),
                        new RelationItem(
                            ["gruz3", "port2"],
                            [new DateTime(2024, 11, 12, 2, 21, 30)],
                            [new DateTime(2024, 11, 12, 2, 23, 0)]
                            )
                    }

                );

            CKL res = CKLMath.TimeTransform(source, new DateTime(2024, 11, 12, 2, 22, 30), new DateTime(2024, 11, 12, 2, 25, 0));
            
            CKL exp = new CKL
                (
                    "Port Shedule",
                    new DateTime(2024, 11, 12, 2, 22, 30),
                    new DateTime(2024, 11, 12, 2, 25, 0),
                    new HashSet<string>[] {
                        new HashSet<string>() { "gruz1", "gruz2", "gruz3"} ,
                        new HashSet<string>() { "port1", "port2"}
                    },
                    new HashSet<RelationItem>()
                    {
                        new RelationItem(
                            ["gruz2", "port1"],
                            [new DateTime(2024, 11, 12, 2, 22, 30)],
                            [new DateTime(2024, 11, 12, 2, 23, 0)]
                            ),
                        new RelationItem(
                            ["gruz2", "port2"],
                            [new DateTime(2024, 11, 12, 2, 22, 30)],
                            [new DateTime(2024, 11, 12, 2, 23, 30)]
                            ),
                        new RelationItem(
                            ["gruz3", "port2"],
                            [new DateTime(2024, 11, 12, 2, 22, 30)],
                            [new DateTime(2024, 11, 12, 2, 23, 0)]
                            )
                    }
                );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "No Exception!")]
        public void TimeTransformTest2()
        {
            CKL source = new CKL(
                "Port Shedule",
                new DateTime(2024, 11, 12, 2, 20, 0),
                new DateTime(2024, 11, 12, 2, 23, 30),
                new HashSet<string>[] {
                        new HashSet<string>() { "gruz1", "gruz2", "gruz3"} ,
                        new HashSet<string>() { "port1", "port2"}
                },
                new HashSet<RelationItem>()
                {
                        new RelationItem(
                            ["gruz1", "port1"],
                            [new DateTime(2024, 11, 12, 2, 20, 30)],
                            [new DateTime(2024, 11, 12, 2, 22, 0)]
                            ),
                        new RelationItem(
                            ["gruz2", "port1"],
                            [new DateTime(2024, 11, 12, 2, 21, 0)],
                            [new DateTime(2024, 11, 12, 2, 23, 0)]
                            ),
                        new RelationItem(
                            ["gruz2", "port2"],
                            [new DateTime(2024, 11, 12, 2, 22, 30)],
                            [new DateTime(2024, 11, 12, 2, 23, 30)]
                            ),
                        new RelationItem(
                            ["gruz3", "port2"],
                            [new DateTime(2024, 11, 12, 2, 21, 30)],
                            [new DateTime(2024, 11, 12, 2, 23, 0)]
                            )
                }

            );
            CKL res = CKLMath.TimeTransform(source, new DateTime(2024, 11, 12, 2, 20, 30), 
                new DateTime(2024, 11, 12, 2, 20, 10));
        }
    }
}