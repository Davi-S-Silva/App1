using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using App1.Model;
using Newtonsoft.Json;

namespace App1.Service
{
    class ApiLaravel
    {
        const String URL = "http://localhost/projetos/eletronet/public/api/user";

        private HttpClient GetUser()
        {
            HttpClient user = new HttpClient();

            user.DefaultRequestHeaders.Add("Accept", "Application/json");
            user.DefaultRequestHeaders.Add("Connection", "close");
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            HttpClient client = GetUser();
            //HttpResponseMessage response = await client.GetAsync(dados);
            var response = await client.GetAsync(URL);
            if (response.IsSuccessStatusCode) //codigo 200
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<User>>(content);
            }
            return new List<User>();
        }

        public async Task<User> GetHighScore()
        {
            String dados = URL;
            //Uri uri = new Uri(dados);
            HttpClient client = GetUser();
            HttpResponseMessage response = await client.GetAsync(dados);
            //var response = await client.GetAsync(dados);
            if (response.IsSuccessStatusCode) //codigo 200
            {
                string content = await response.Content.ReadAsStringAsync();
                var games = JsonConvert.DeserializeObject<List<User>>(content);
                return games[0];
            }
            return new User();
        }

        public async Task CreateHighScore(User game)
        {
            String dados = URL;
            string json = JsonConvert.SerializeObject(game);
            HttpClient client = GetUser();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(dados, content);
        }

        public async Task UpDateHighScore(User game)
        {
            String dados = URL + "/" + game.id;
            string json = JsonConvert.SerializeObject(game);
            HttpClient client = GetUser();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(dados, content);
        }

        public async Task DeleteHighScore(int Id)
        {
            String dados = URL + "/" + Id.ToString();
            HttpClient client = GetUser();
            HttpResponseMessage response = await client.DeleteAsync(dados);
        }
    }
}
