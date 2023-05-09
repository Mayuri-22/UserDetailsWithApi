using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Web;

namespace UserDetailsWithApi.Models
{
    public class UserRepository : IUserRepository
    {
        public string getUserlist(string apiBaseAddress, string token)
        {
            string responseContent = "";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(apiBaseAddress).Result;
                if (response.IsSuccessStatusCode)
                {
                    responseContent = response.Content.ReadAsStringAsync().Result;
                }
            }
            return responseContent;
        }

        public bool addUser(string user, string apiBaseAddress, string token)
        {

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(user, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(apiBaseAddress, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = response.Content.ReadAsStringAsync().Result;
                    return true;
                }

            }
            return false;

        }

        public bool updateUser(int id, string user, string apiBaseAddress, string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            string url = apiBaseAddress + id;

            StringContent data = new StringContent(user, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url, data).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {

                return true;
            }
            return false;

        }

        public bool RemoveUser(int id, string apiBaseAddress, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = client.DeleteAsync($"{apiBaseAddress}/{id}").Result;
            if (response.StatusCode == HttpStatusCode.NoContent)
                return true;
            return false;


        }
    }
}