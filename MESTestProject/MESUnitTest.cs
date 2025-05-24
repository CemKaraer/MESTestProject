using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MESTestProject
{
    [TestClass]
    public class MESUnitTest
    {
        private readonly string baseUri = "https://mtnav.guzelenerji.com.tr/api/MES";
        // private readonly string baseUri = "http://localhost/dwi_clone/api/MES";

        #region POST
        [TestMethod]
        public async Task TestPostMaterialConsumption()
        {
            var result = await HttpPost(baseUri + "/PostMaterialConsumption", FileContent("PostMaterialConsumption.json"));
        }

        [TestMethod]
        public async Task TestPostProductionWarehousing()
        {
            var result = await HttpPost(baseUri + "/PostProductionWarehousing", FileContent("PostProductionWarehousing.json"));
        }

        [TestMethod]
        public async Task TestPostRework()
        {
            var result = await HttpPost(baseUri + "/PostRework", FileContent("PostRework.json"));
        }
        #endregion

        #region GET
        [TestMethod]
        public async Task TestGetWorkOrderInformation()
        {
            var result = await HttpGet(baseUri + "/GetWorkOrderInformation", "GetWorkOrderInformationParameter.json");
        }

        [TestMethod]
        public async Task TestGetWorkOrderBOMInformation()
        {
            var result = await HttpGet(baseUri + "/GetWorkOrderBOMInformation", "GetWorkOrderBOMInformationParameter.json");
        }

        [TestMethod]
        public async Task TestGetMaterialPickingList()
        {
            var result = await HttpGet(baseUri + "/GetMaterialPickingList", "GetMaterialPickingListParameter.json");
        }

        [TestMethod]
        public async Task TestGetMaterialPickingDetail()
        {
            var result = await HttpGet(baseUri + "/GetMaterialPickingDetail", "GetMaterialPickingDetailParameter.json");
        }
        #endregion

        private static async Task<string> HttpPost(string url, string jsonContent)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("MES-Header", FileContent("Header.json"));
                    return await (await httpClient.PostAsync(url, new StringContent(jsonContent, Encoding.UTF8, "application/json"))).Content.ReadAsStringAsync();
                }
            }
            catch (Exception exc)
            {
                throw;
            }
        }

        private static async Task<string> HttpGet(string url, string parameterFile)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("MES-Header", FileContent("Header.json"));
                    return await (await httpClient.GetAsync($"{url}?input={Uri.EscapeDataString(FileContent(parameterFile))}")).Content.ReadAsStringAsync();
                }
            }
            catch (Exception exc)
            {
                throw;
            }
        }

        private static string FileContent(string fileName)
        {
            return File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestJson", fileName));
        }
    }
}
