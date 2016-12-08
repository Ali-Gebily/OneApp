using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneApp.Common.Core.Injectors;
using OneApp.Common.Core.Managers.Emails;
using OneApp.Common.Core.Managers.Settings;
using OneApp.Common.Core.Models;

namespace OneApp.Common.Core.Injectors
{
    class CoreInjector : IOneAppNinjectResolver
    {

        public void RegisterServices(IOneAppKernel kernel)
        {
            var emailchannel = OneAppConfigurationKeys.Emailchannel;
            switch (emailchannel)
            {
                case Emailchannel.Mock:
                    kernel.BindConcerteToAbstact<IEmailSender, EmailMockSender>();
                    break;
                default:
                    kernel.BindConcerteToAbstact<IEmailSender, EmailLiveSender>();
                    break;
            }
        }

    }
}