using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using OneApp.Common.Core.Exceptions;
using OneApp.Common.WebServices.Controllers;
using OneApp.Common.WebServices.Models;
using OneApp.Modules.Styles.Models;
using OneApp.Modules.Styles.Repositories;

namespace OneApp.Modules.Styles.Controllers
{

    public class AppStyleController : BaseApiController
    {
        IStylesRepository _repo;
        public AppStyleController(IStylesRepository repository)
        {
            _repo = repository;
        }
        /// <summary>
        /// baseUrl is the base path for images url formatting
        /// </summary>
        /// <param name="base_url"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<BaseHttpActionResult> GetFormattedAppStyle(string base_url)
        {
            var styles = await _repo.GetStyles(UserId);
            StringBuilder sb = new StringBuilder();
            foreach (var item in styles)
            {
                sb.Append(item.Format(base_url) + "\n");
            }
            return new SuccessHttpActionResult(sb.ToString());
        }

        [HttpGet]
        [Authorize]
        public async Task<BaseHttpActionResult> GetRulesSummary()
        {
            var styles = await _repo.GetStyles(UserId);
            var stylesSummary = new List<RuleSummaryDTO>();
            foreach (var item in styles)
            {
                stylesSummary.Add(item.CopyRuleSummaryDTO());
            }
            return new SuccessHttpActionResult(stylesSummary);
        }

        [HttpGet]
        [Authorize]
        public async Task<BaseHttpActionResult> GetRule(int id)
        {
            return new SuccessHttpActionResult(await _repo.GetRule(id, UserId));
        }
        [HttpPost]
        [Authorize]
        public async Task<BaseHttpActionResult> UpdateRuleStyle()
        {
            var ruleJson = HttpContext.Current.Request.Form["rule"];
            var baseUrl = HttpContext.Current.Request.Form["base_url"];
            if (string.IsNullOrEmpty(ruleJson))
            {
                return ErrorHttpActionResult.GenerateBadRequest("rule form data is required");
            }
            RuleDTO rule = JsonConvert.DeserializeObject<RuleDTO>(ruleJson);

            // Get the uploaded files from the Files collection , add data to the fileinfo 
            foreach (var cssProperty in HttpContext.Current.Request.Files.AllKeys)
            {

                var httpPostedFile = HttpContext.Current.Request.Files[cssProperty];

                int length = httpPostedFile.ContentLength;
                string contentType = httpPostedFile.ContentType;
                string name = Path.GetFileName(httpPostedFile.FileName);

                var fileData = new FileDataDTO();
                fileData.Data = new byte[length];
                fileData.Name = name;
                fileData.Length = length;
                fileData.ContentType = contentType;
                httpPostedFile.InputStream.Read(fileData.Data, 0, length);

                rule.Style.SetFilePropertyWithId(cssProperty, await _repo.InsertFileData(fileData));
            }

            //remove old files 
            var oldRule = await _repo.GetRule(rule.Id, UserId);
            var clientFilePropertiesValues = rule.Style.GetFilePropertiesValues();
            var oldFilePropertiesValues = oldRule.Style.GetFilePropertiesValues();

            foreach (var oldValue in oldFilePropertiesValues)
            {

                if (!clientFilePropertiesValues.Contains(oldValue))//the file is delete or replaced with another one, then delete old one
                {
                    await _repo.RemoveFileData(oldValue);
                }

            }
            var updateRule = await _repo.UpdateRuleStyle(rule,UserId);

            return new SuccessHttpActionResult(updateRule.Format(baseUrl));
        }

        [HttpGet]
        public async Task<BaseHttpActionResult> GetCSSImage(int id)
        {
            var file = await _repo.GetFileData(id);

            if (file == null)
            {
                //we should load image that represents error, and pass it in response
                return new MediaTypeHttpActionResult(HttpStatusCode.OK, new MediaTypeResponse(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, "image/png"));
            }
            else
            {
                return new MediaTypeHttpActionResult(new MediaTypeResponse(file.Data, file.ContentType));
            }

        }
    }
}