﻿using CheckClinic.Interfaces;
using CheckClinic.Model;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CheckClinic.DataParser
{
    public class TicketCollectionParser : ITicketCollectionParser
    {
        public IReadOnlyList<ITicket> Parse(string content)
        {
            var ticketCollection = new TicketCollection();
            foreach (var level1Node in JObject.Parse(content).SelectTokens("response"))
            {
                foreach(var level2Node in level1Node.Children())
                {
                    foreach(var level3Node in level2Node.Children())
                    {
                        foreach (var ticketNode in level3Node.Children())
                        {
                            var ticket = ticketNode.ToObject<Ticket>();
                            ticketCollection.Tickets.Add(ticket);
                        }
                    }
                }
            }
            var ticketList = new List<ITicket>(ticketCollection.Tickets);
            return ticketList;
        }
    }
}
