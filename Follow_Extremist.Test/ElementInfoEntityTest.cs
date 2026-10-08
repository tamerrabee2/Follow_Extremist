using Follow_Extremist.Core;
using Follow_Extremist.Data;
using Follow_Extremist.Data.SqlServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow_Extremist.Test
{
    [TestClass]
   public class ElementInfoEntityTest
    {
        IDataHelper<ElementInfo> dataHeElper;
        public ElementInfoEntityTest()
        {
            SqlCon.SqlConnection = @"Server=.;Database=FollowExtremistDatabase;Trusted_Connection=True;TrustServerCertificate=True;";
            dataHeElper = new ElementInfoEntity();
        }

        [TestMethod]
        public void AddTest()
        {
            // arrange (set)
            ElementInfo elementInfo = new ElementInfo
            { 
                ElementName = "mn",
                BirthDate = DateTime.Now,
                DateFollowStart = DateTime.Now,
                FollowDaysCount = 25,
                DateFollowNow = DateTime.Now
            };
            // act and expt (get)
            int act = dataHeElper.Add(elementInfo);
            int expt = 1;
            // assert (test)
            Assert.AreEqual(expt, act);
        }

        [TestMethod]
        public void EditTest()
        {
            // arrange (set)
            ElementInfo elementInfo = new ElementInfo
            {
                Id = 1,
                ElementName = "mn",
                BirthDate = DateTime.Now,
                DateFollowStart = DateTime.Now,
                FollowDaysCount = 25,
                DateFollowNow = DateTime.Now
            };
            // act and expt (get)
            int act = dataHeElper.Edit(elementInfo);
            int expt = 1;
            // assert (test)
            Assert.AreEqual(expt, act);
        }

        
    }
}
