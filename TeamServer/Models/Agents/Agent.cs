using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TeamServer.Models.Agents
{
    public class Agent
    {

        public AgentMetaData Metadata { get; }

        public DateTime LastSeen { get; private set; }

        public Agent(AgentMetaData metadata)
        {
            Metadata = metadata;
        }

        public void CheckIn()
        {
            LastSeen = DateTime.UtcNow;
        }

    }
}
