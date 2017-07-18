﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public class DebugMailService : IMailService
    {
        public void sendMail(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending mail to:"+to+" from:"+from+" subject:"+subject);

        }
    }
}