using Agent.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Agent
{
     class Program
    {
        private static AgentMetadata _metadata;

        static void Main(string[] args)
        {
            GenerateMetadata();
        }

        static void GenerateMetadata()
        {
            var process = Process.GetCurrentProcess();

            var username = Environment.UserName;
            var integrity = "Medium";

            if (username.Equals("SYSTEM"))
                integrity = "SYSTEM";

            using (var identity = WindowsIdentity.GetCurrent())
            {
                if (identity.User != identity.Owner)
                {
                    integrity = "High";
                }
            }

            _metadata = new AgentMetadata
            {
                Id = Guid.NewGuid().ToString(),
                Hostname = Environment.MachineName,
                Username = Environment.UserName,
                ProcessName = process.ProcessName,
                ProcessId = process.Id,
                Integrity = integrity,
                Architecture = Environment.Is64BitOperatingSystem ? "x64" : "x86"

            };
        }
    }
}
