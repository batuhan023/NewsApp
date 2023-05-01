using NewsApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Services
{
    public class ApiService
    {
        public async Task<Root> GetNews(string categoryName)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("https://gnews.io/api/v4/top-headlines?category=general&apikey=9ab42f1a9602992ef1eaec5e0aa4b72e&lang=en&category" + categoryName.ToLower());
            return JsonConvert.DeserializeObject<Root>(response);
        }
    }
}
