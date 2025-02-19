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
        [TestMethod]
        public async Task TestPostProductionReturn()
        {
            await HttpPost("https://mtnav.guzelenerji.com.tr/api/MESApi/PostProductionReturn", FileContent("PostProductionReturn.json"));
        }

        [TestMethod]
        public async Task TestPostProductionWarehousing()
        {
            await HttpPost("https://mtnav.guzelenerji.com.tr/api/MESApi/PostProductionWarehousing", FileContent("PostProductionWarehousing.json"));
        }

        [TestMethod]
        public async Task TestGetWorkOrderInformation()
        {
            await HttpGet("https://mtnav.guzelenerji.com.tr/api/MESApi/GetWorkOrderInformation", FileContent("Parameter.json"));
        }

        [TestMethod]
        public async Task TestGetWorkOrderBOMInformation()
        {
            await HttpGet("https://mtnav.guzelenerji.com.tr/api/MESApi/GetWorkOrderBOMInformation", FileContent("Parameter.json"));
        }

        [TestMethod]
        public async Task TestGetMaterialPickingList()
        {
            await HttpGet("https://mtnav.guzelenerji.com.tr/api/MESApi/GetMaterialPickingList", FileContent("Parameter.json"));
        }

        [TestMethod]
        public async Task GetMaterialPickingDetail()
        {
            await HttpGet("https://mtnav.guzelenerji.com.tr/api/MESApi/GetMaterialPickingDetail", FileContent("Parameter.json"));
        }

        private static async Task<string> HttpPost(string url, string jsonContent)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("MES-Header", FileContent("Header.json"));
                return await (await httpClient.PostAsync(url, new StringContent(jsonContent, Encoding.UTF8, "application/json"))).Content.ReadAsStringAsync();
            }
        }

        private static async Task<string> HttpGet(string url, string inputJson)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("MES-Header", FileContent("Header.json"));
                return await (await httpClient.GetAsync($"{url}?input={Uri.EscapeDataString(inputJson)}")).Content.ReadAsStringAsync();
            }
        }

        private static string FileContent(string fileName)
        {
            return File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestJson", fileName));
        }
    }
}
