using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadStatus
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length > 0 && args.Length < 2)
            {

                var obj = new ApiHost();

                var response = obj.GetStatusByRoadName(args[0]).Result;

                response.Data = response.Data.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });

                if (response.StatusCode == 200)
                {
                    DisplaySucccessResponseData(response.Data);
                }
                else
                {
                    DisplayFailureResponseData(response.StatusCode, response.Data);
                }

            }
            else
            {

                Console.WriteLine("Invalid arguments");
            }
            Console.Read();
          
        }

        private static void DisplayFailureResponseData(int StatusCode,string data)
        {

            JObject jsonData = JObject.Parse(data);

            if (StatusCode == 402)
            {
                string errMsg = (string)jsonData["error"];

                Console.WriteLine("Error:", errMsg);
            }
            else
            {
                string displayName = (string)jsonData["relativeUri"];
                var le = displayName.Length - displayName.LastIndexOf('/') - 1;


                displayName = displayName.Substring(displayName.LastIndexOf('/') + 1, le);
                Console.WriteLine("{0} is not a vaild road", displayName);
            }
            
           
        }

        private static void DisplaySucccessResponseData(string data)
        {
            JObject jsonData = JObject.Parse(data);
            string displayName = (string)jsonData["displayName"];
            string status = (string)jsonData["statusSeverity"];
            string RoadStatusDes = (string)jsonData["statusSeverityDescription"];

            Console.WriteLine("The status of the {0} is a follows",displayName);
            Console.WriteLine("Road Status is {0}\nRoad Status Description is {1}",status,RoadStatusDes);
        }
    }
}
