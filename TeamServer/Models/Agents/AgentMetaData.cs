﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TeamServer.Models.Agents
{
    public class AgentMetaData
    {
        public string Id { get; set; }
        
        public string Hostname { get; set; }

        public string Username { get; set; }

        public string ProcessName { get; set; }

        public int ProcessId { get; set; }

        public string Integrity {  get; set; }

        public string Architecture { get; set; }
    }
}
