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

        [TestMethod]
        public async Task TestPostProductionReturn()
        {
            var result = await HttpPost(baseUri + "/PostProductionReturn", FileContent("PostProductionReturn.json"));
        }

        [TestMethod]
        public async Task TestPostProductionWarehousing()
        {
            var result = await HttpPost(baseUri + "/PostProductionWarehousing", FileContent("PostProductionWarehousing.json"));
        }

        [TestMethod]
        public async Task TestGetWorkOrderInformation()
        {
            var result = await HttpGet(baseUri + "/GetWorkOrderInformation");
        }

        [TestMethod]
        public async Task TestGetWorkOrderBOMInformation()
        {
            var result = await HttpGet(baseUri + "/GetWorkOrderBOMInformation");
        }

        [TestMethod]
        public async Task TestGetMaterialPickingList()
        {
            var result = await HttpGet(baseUri + "/GetMaterialPickingList");
        }

        [TestMethod]
        public async Task GetMaterialPickingDetail()
        {
            var result = await HttpGet(baseUri + "/GetMaterialPickingDetail");
        }

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

        private static async Task<string> HttpGet(string url)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("MES-Header", FileContent("Header.json"));
                    return await (await httpClient.GetAsync($"{url}?input={Uri.EscapeDataString(FileContent("Parameter.json"))}")).Content.ReadAsStringAsync();
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
