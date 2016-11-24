using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Common.Core.Managers.Emails
{
  public  class EmailMockSender : IEmailSender
    {
        public void Send(string to, string subject, string body, bool isBodyHtml)
        {
            Logs.LogsManager.Debug("Sending email to " + to);
            Logs.LogsManager.Debug("Subject " + subject);
            Logs.LogsManager.Debug("Body " + body); 



        }
    }
}
