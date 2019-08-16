﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace QuestionCollection.CentralHub
{
    public class SignalrHub : Hub
    {
        public async Task BroadcastMessage()
        {
            await Clients.All.SendAsync("Connected");
        }
        public async Task AddQuestion()
        {
            await Clients.All.SendAsync("UpdateQuestionList");
        }
    }
}
