using Charon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Charon.Services
{
    public class EventsService
    {
        public Dictionary<string,string> Events { get; set; }
        public HttpClient Client { get; set; }

        public Event SelectedEvent { get; set; }

        public EventsService(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public async Task<Dictionary<string,string>> GetAllEvents()
        {
            
            var events = await Client.GetFromJsonAsync<Dictionary<string, string>>("/api/events");
            if (!events.ContainsKey("message"))
            {
                Events = events;
            }
            Console.WriteLine(Events);
            return Events;
        }


        public async Task<Event> GetEvent(string tableName)
        {

            var Users = Client.GetFromJsonAsync<Dictionary<string, object>[]>($"/api/users?table={tableName}");
            var Stats = Client.GetFromJsonAsync<Dictionary<string, int>>($"/api/stats?table={tableName}");
            await Task.WhenAll(Users, Stats);


            SelectedEvent = new Event(tableName,Events[tableName],Stats.Result[Events[tableName]],Users.Result);

            return SelectedEvent;
        }

    }
}
