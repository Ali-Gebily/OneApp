using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using OneApp.Common.Core.Exceptions;
using OneApp.Common.Core.Managers.Encryption;
using OneApp.Common.Core.Managers.Logs;
using OneApp.Common.Core.Managers.Settings;

namespace OneApp.Common.Core.Managers.Emails
{
    public class EmailLiveSender: IEmailSender
    {
        const string _SMPTHostKey = "SMPTHost";
        const string _SMPTPortKey = "SMPTPort";
        const string _SenderEmailKey = "EmailFrom";
        const string _EmailEncodedPasswordKey = "EmailFromEncodedPassword";


        static string _SMPTHosValue;
        static int _SMPTPortValue;
        static string _EmailFrom;
        static string _EmailFromDecodedPasswordValue;
        static EmailLiveSender()
        {
            LoadSettings();
        }
        static void LoadSettings()
        {

            ISettingsManager settingsManager = SettingsManager.Instance;
            _SMPTHosValue = settingsManager.GetValue(_SMPTHostKey);
            _SMPTPortValue = int.Parse(settingsManager.GetValue(_SMPTPortKey));
            _EmailFrom = settingsManager.GetValue(_SenderEmailKey);
            _EmailFromDecodedPasswordValue = TextEncryptionManager.Instance.Decrypt(settingsManager.GetValue(_EmailEncodedPasswordKey));
        }
        public void Send(string to, string subject, string body, bool isBodyHtml)
        {
            try
            {
                using (var msg = new MailMessage(_EmailFrom, to))
                {
                    msg.Subject = subject;
                    msg.Body = body;
                    msg.IsBodyHtml = isBodyHtml;

                    SmtpClient smtpClient = new SmtpClient(_SMPTHosValue, _SMPTPortValue);

                    smtpClient.EnableSsl = true;
                    smtpClient.Timeout = 120000;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Credentials = new System.Net.NetworkCredential(_EmailFrom, _EmailFromDecodedPasswordValue);
                    smtpClient.Send(msg);
                }
            }
            catch (SmtpException ex)
            {
                if (ex.HResult == -2146233088)
                {
                    LogsManager.Error(ex);
                    throw new BusinessException("Operation timed out, please try again");
                }
                throw;
            }
        }

    }
}
