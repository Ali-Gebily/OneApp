using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using OneApp.Common.Core.Managers;
using OneApp.Common.Core.Managers.Encryption;
using OneApp.Common.WebServices.Models;

namespace OneApp.Common.WebServices.Controllers
{
    public class TextEncryptionController : BaseApiController
    {
        public TextEncryptionController()
        {

        }

        [HttpPost, HttpGet]
        public BaseHttpActionResult Encrypt(string clearText)
        {
            return new SuccessHttpActionResult(TextEncryptionManager.Instance.Encrypt(clearText));
        }
        [HttpPost, HttpGet]
        public BaseHttpActionResult Encrypt(string clearText, string encryptionKey)
        {
            return new SuccessHttpActionResult(TextEncryptionManager.Instance.Encrypt(clearText, encryptionKey));
        }
        [HttpPost, HttpGet]
        public BaseHttpActionResult Decrypt(string cipherText, string encryptionKey)
        {
            return new SuccessHttpActionResult(TextEncryptionManager.Instance.Decrypt(cipherText, encryptionKey));
        }
    }
}