using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadStatus.Config;
using Moq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RoadStatus.Tests
{

    [TestClass]
    public class ApiHostTests
    {

        private Mock<IConfigurationManager> _config;
        private string DeveloperKey = "";
        private string applicationId = "";
        private string ApiBaseUrl = "";
        [TestMethod]
        public void ItShouldReturnResponseWith200StatusCode()
        {
            _config = new Mock<IConfigurationManager>();
           
            _config.Setup(x => x.GetAppSetting("ApiUrl"))
                .Returns(ApiBaseUrl);

            _config.Setup(x => x.GetAppSetting("DeveloperKey")
            ).Returns(DeveloperKey);

            _config.Setup(x => x.GetAppSetting("ApplicationId")
            ).Returns(applicationId);            

           
            var obj = new ApiHost(_config.Object);
           
            var response =  obj.GetStatusByRoadName("A12").Result;

            response.Data = response.Data.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });

            // JObject jObject = JObject.Parse(response.Result);

            JObject s = JObject.Parse(response.Data);
            string displayName = (string)s["displayName"];
            string status = (string)s["statusSeverity"];
            string RoadStatusDes = (string)s["statusSeverityDescription"];
            Assert.AreEqual("A12", displayName);
            Assert.AreEqual("Good", status);
            Assert.AreEqual("No Exceptional Delays", RoadStatusDes);
            Assert.AreEqual(200, response.StatusCode);
            

        }

        [TestMethod]
        public void ItShouldReturnResponseWithStatus404()
        {
            _config = new Mock<IConfigurationManager>();

            _config.Setup(x => x.GetAppSetting("ApiUrl"))
                 .Returns(ApiBaseUrl);

            _config.Setup(x => x.GetAppSetting("DeveloperKey")
            ).Returns(DeveloperKey);

            _config.Setup(x => x.GetAppSetting("ApplicationId")
            ).Returns(applicationId);

            var obj = new ApiHost(_config.Object);

            var response = obj.GetStatusByRoadName("A1112").Result;

            response.Data = response.Data.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });

            // JObject jObject = JObject.Parse(response.Result);

            JObject s = JObject.Parse(response.Data);
            string displayName = (string)s["exceptionType"];
            string status = (string)s["httpStatus"];
           
            Assert.AreEqual("EntityNotFoundException", displayName);
            Assert.AreEqual("NotFound", status);
           Assert.AreEqual(404, response.StatusCode);



        }


    }
}
