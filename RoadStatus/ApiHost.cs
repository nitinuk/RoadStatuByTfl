using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoadStatus.Config;
using RoadStatus.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace RoadStatus
{
    public class ApiHost
    {
        private IConfigurationManager _config;
        private HttpClient client = new HttpClient();

        ApiResponse resResult = new ApiResponse();
        public ApiHost() : base()
        {
            _config = new  ConfigurationManager();


        }

        public ApiHost(IConfigurationManager Config)
        {
            _config = Config;
        }

        public async Task<ApiResponse> GetStatusByRoadName(string RoadName)
        {

            string ServiceEnfPoint = _config.GetAppSetting("ApiUrl");
            string ApplicationId = _config.GetAppSetting("ApplicationId");
            string DeveloperKey = _config.GetAppSetting("DeveloperKey");

            if (!String.IsNullOrEmpty(ServiceEnfPoint)
                && !String.IsNullOrEmpty(ApplicationId)
                && !String.IsNullOrEmpty(DeveloperKey))
            {
                string endPoint = "";

                endPoint = String.Format("{0}/{1}?app_id={2}&app_key={3}",
                    ServiceEnfPoint, RoadName, ApplicationId, DeveloperKey);



                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(endPoint, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false);



                if (response.IsSuccessStatusCode)
                {
                    //Read response content result into string variable
                    string strJson = response.Content.ReadAsStringAsync().Result;


                    resResult.Data = strJson;
                    resResult.StatusCode = (int)response.StatusCode;
                }
                else
                {
                    string strJson = response.Content.ReadAsStringAsync().Result;

                    resResult.StatusCode = (int)response.StatusCode;
                    resResult.Data = strJson;

                }


                return resResult;
            }

            else
            {
                
                string jsonData = @"{  
                'error':'Invalid arugments '
                      
                    }";
                resResult.StatusCode = 402;
                resResult.Data = jsonData;
                return resResult;

            }
        }

    }

}
