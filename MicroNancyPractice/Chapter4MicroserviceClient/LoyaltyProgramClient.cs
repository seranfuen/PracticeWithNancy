using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Chapter4MicroserviceClient
{
    public class LoyaltyProgramClient
    {
        private const string Address = "http://localhost:51423";
        private const string MediaTypeInRequests = "application/json";

        public async Task<LoyaltyProgramUser> QueryUser(int userId)
        {
            using (var client = InitializeClient())
            {
                var response = await client.GetAsync($"/users/{userId}");
                var readAsStringAsync = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoyaltyProgramUser>(readAsStringAsync);
            }
        }

        private static HttpClient InitializeClient()
        {
            var client = new HttpClient {BaseAddress = new Uri(Address)};
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<LoyaltyProgramUser> RegisterUser(LoyaltyProgramUser newUser)
        {
            using (var client = InitializeClient())
            {
                client.BaseAddress = new Uri(Address);

                var response = await client.PostAsync("/users/",
                    GetStringContentFromObject(newUser));

                return JsonConvert.DeserializeObject<LoyaltyProgramUser>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<LoyaltyProgramUser> UpdateUser(LoyaltyProgramUser user)
        {
            using (var client = InitializeClient())
            {
                client.BaseAddress = new Uri(Address);
                var response = await client.PutAsync($"/users/{user.Id}",
                    new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, MediaTypeInRequests));

                return JsonConvert.DeserializeObject<LoyaltyProgramUser>(await response.Content.ReadAsStringAsync());
            }
        }

        private static StringContent GetStringContentFromObject(LoyaltyProgramUser newUser)
        {
            return new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, MediaTypeInRequests);
        }
    }
}