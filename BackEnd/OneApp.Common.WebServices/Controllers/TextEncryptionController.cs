using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using OneApp.Common.Core.Managers;
using OneApp.Common.Core.Managers.Encryption;

namespace OneApp.Common.WebServices.Controllers
{
    public class TextEncryptionController : BaseApiController
    { 
        public TextEncryptionController()
        {
             
        } 
        
        [HttpPost, HttpGet]
        public string Encrypt(string clearText, string encryptionKey)
        {
            return TextEncryptionManager.Instance.Encrypt(clearText, encryptionKey);
        }
        [HttpPost, HttpGet]
        public string Decrypt(string cipherText, string encryptionKey)
        {
            return TextEncryptionManager.Instance.Decrypt(cipherText, encryptionKey);
        }
    }
}