using CKLLib;

namespace CKLLibTests
{
    [TestClass]
    public class DataClassesTests
    {
        [TestMethod]
        public void TestPairEquality1()
        {
            Pair p1 = new Pair("g1", "p1");
            Pair p2 = new Pair("g1", "p1");
            
            bool res = p1.Equals(p2);
            bool exp = true;
            
            Assert.AreEqual(exp, res);
        }

        [TestMethod]
        public void TestTimeIntervalEquality1() 
        {
            TimeInterval t1 = new TimeInterval(new DateTime(2024, 11, 12), new DateTime(2025, 11, 12));
            TimeInterval t2 = new TimeInterval(new DateTime(2024, 11, 12), new DateTime(2025, 11, 12));

            bool res = t1.Equals(t2);
            bool exp = true;

            Assert.AreEqual(exp, res);
        }
    }
}