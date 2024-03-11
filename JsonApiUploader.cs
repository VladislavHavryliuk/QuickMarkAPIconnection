using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace QuickMark
{
    public static class JsonApiUploader
    {
        static string jsonData;
        static async Task UploadJsonAsync(string[] args)
        {
            string apiUrl = "https://api.quickmark.com/data";


            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(jsonData));

                    if (response.IsSuccessStatusCode)
                    {
                        Logger.Log("Дані успішно відправлені на API.");
                    }
                    else
                    {
                        Logger.Log("Помилка при відправленні даних на API: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("Помилка: " + ex.Message);
                }
            }
        }
    }
}
