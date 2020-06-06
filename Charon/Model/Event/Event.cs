using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Charon.Model
{
    public class Event
    {
        public string TableName { get; set; }
        public string Name { get; set; }
        public int Stats { get; set; }
        public Dictionary<string,object>[] Users { get; set; }


        public Event(string TableName, string Name, int Stats, Dictionary<string,object>[] Users)
        {
            this.TableName = TableName;
            this.Name = Name;
            this.Stats = Stats;
            this.Users = Users;
        }
    }
}
