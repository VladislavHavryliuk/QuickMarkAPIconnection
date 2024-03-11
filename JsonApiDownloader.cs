using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuickMark
{
    class JsonApiDownloader
    {
        static string responseData;

        static async Task LoadJsonAsync()
        {
            try
            {
                string apiUrl = "https://api.quickmark.com/data";
                await GetApiData(apiUrl);
                Logger.Log("Отримано дані: " + responseData);
            }
            catch (Exception ex)
            {
                Logger.Log($"Помилка отримання даних: {ex.Message}");
            }
        }

        static async Task GetApiData(string apiUrl)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                responseData = await response.Content.ReadAsStringAsync();
            }
        }

        static void ProcessData(string responseData)
        {
            string[] requests = responseData.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string request in requests)
            {
                string[] parameters = request.Split('&');

                int id = 0;
                int count = 0;
                int storageId = 0;

                foreach (string parameter in parameters)
                {
                    string[] keyValue = parameter.Split('=');
                    if (keyValue.Length == 2)
                    {
                        string key = keyValue[0];
                        string value = keyValue[1];

                        switch (key)
                        {
                            case "id":
                                id = int.Parse(value);
                                break;
                            case "count":
                                count = int.Parse(value);
                                break;
                            case "storageId":
                                storageId = int.Parse(value);
                                break;
                            default:
                                break;
                        }
                    }
                }

                Actions.OrderProduct(id, count, storageId);
                Actions.RedirectProduct(id, count, storageId);
            }
        }

        
    }
}
