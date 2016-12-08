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
using OneApp.Modules.Styles.Models.CSSAttributes;
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
        public async Task<BaseHttpActionResult> GetFormattedAppStyle(string base_url)
        {
            var styles = await _repo.GetAllStyles();
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
            var styles = await _repo.GetAllStyles();
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
            return new SuccessHttpActionResult(await _repo.GetRule(id));
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
                CSSProperty p = (CSSProperty)int.Parse(cssProperty);
                var fileAttribute = rule.Style.GetProperty(p);

                if (fileAttribute != null)
                {
                    var httpPostedFile = HttpContext.Current.Request.Files[cssProperty];

                    int length = httpPostedFile.ContentLength;
                    string contentType = httpPostedFile.ContentType;
                    string name = Path.GetFileName(httpPostedFile.FileName);

                    if (!string.IsNullOrEmpty(fileAttribute.Value))
                    {
                        await _repo.RemoveFileData(fileAttribute.Value);//remove old file 
                    }

                    var fileData = new FileDataDTO();
                    fileData.Data = new byte[length];
                    fileData.Name = name;
                    fileData.Length = length;
                    fileData.ContentType = contentType;
                    httpPostedFile.InputStream.Read(fileData.Data, 0, length);

                    fileAttribute.Value = await _repo.InsertFileData(fileData);
                }
                else
                {
                    throw new Exception("No atttibute defined for property=" + p);
                }
            }
            string oldValue = null;
            var oldRule = await _repo.GetRule(rule.Id);
            var newFileProperties = rule.Style.GetFileProperties();
            var oldFileProperties = oldRule.Style.GetFileProperties();

            foreach (var newItem in newFileProperties)
            {
                if (newItem != null)
                {
                    oldValue = oldFileProperties.FirstOrDefault(attr => attr.CSSProperty == newItem.CSSProperty).Value;
                    if (!string.IsNullOrEmpty(oldValue) && newItem.Value != oldValue)//old value is cleared
                    {
                        await _repo.RemoveFileData(oldValue);
                    }
                }
            }
            var updateRule = await _repo.UpdateRuleStyle(rule);

            return new SuccessHttpActionResult(updateRule.Format(baseUrl));
        }

        [HttpGet]
        public async Task<BaseHttpActionResult> GetCSSImage(string id)
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