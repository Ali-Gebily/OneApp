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

    public class StylesController : BaseApiController
    {
        IStylesRepository _repo;
        public StylesController(IStylesRepository repository)
        {
            _repo = repository;
        }
        /// <summary>
        /// baseUrl is the base path for images url formatting
        /// </summary>
        /// <param name="base_url"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<BaseHttpActionResult> GetFormattedStyles([FromUri]GetFormattedRuleVM model)
        {
            if ((model.Scope != RuleEntityScope.Global && model.Scope != RuleEntityScope.User) &&
                string.IsNullOrEmpty(model.entity_id))//is not a global neither a user scope
            {
                return ErrorHttpActionResult.GenerateBadRequest("You have to provide entityId if the scope is not global neither user");
            }
            if (model.Scope == RuleEntityScope.User)
            {
                if (User.Identity.IsAuthenticated)
                {
                    model.entity_id = UserId;
                }
                else
                {
                    return new ErrorHttpActionResult(HttpStatusCode.Unauthorized, new ErrorResponse("You have to login to access user styles"));
                }
            }

            var styles = await _repo.GetStyles(model.Scope, model.entity_id);
            StringBuilder sb = new StringBuilder();
            foreach (var item in styles)
            {
                sb.Append(item.Format(model.base_url) + "\n");
            }
            return new SuccessHttpActionResult(sb.ToString());
        }


        [HttpGet]
        [Authorize]
        public async Task<BaseHttpActionResult> GetRulesSummary(RuleEntityScope scope)
        {
            var stylesSummary = await _repo.GetRulesSummary(scope);
            return new SuccessHttpActionResult(stylesSummary);
        }

        [HttpGet]
        [Authorize]
        public async Task<BaseHttpActionResult> GetRuleDetails([FromUri] GetRuleVM model)
        {
            if (string.IsNullOrEmpty(model.entity_id))
            {
                //this line will cause an issue if the client asked for not global neither user scope rule, and he did not pass entityId
                model.entity_id = UserId;

            }
            return new SuccessHttpActionResult(await _repo.GetRule(model.id,model.entity_id));
        }
        [HttpPost]
        [Authorize]
        public async Task<BaseHttpActionResult> UpdateRuleStyle()
        {
            var ruleJson = HttpContext.Current.Request.Form["rule"];
            var baseUrl = HttpContext.Current.Request.Form["base_url"];
            var entityId = HttpContext.Current.Request.Form["entity_id"];
            if(string.IsNullOrEmpty(entityId))
            {
                entityId = UserId;
            }
            if (string.IsNullOrEmpty(ruleJson))
            {
                return ErrorHttpActionResult.GenerateBadRequest("rule form data is required");
            }
            RuleDTO rule = JsonConvert.DeserializeObject<RuleDTO>(ruleJson);

            // Get the uploaded files from the Files collection , add data to the fileinfo 
            IList<FileDataDTO> fileDtos = new List<FileDataDTO>();
            foreach (var cssProperty in HttpContext.Current.Request.Files.AllKeys)
            {

                var httpPostedFile = HttpContext.Current.Request.Files[cssProperty];

                int length = httpPostedFile.ContentLength;
                string contentType = httpPostedFile.ContentType;
                string name = Path.GetFileName(httpPostedFile.FileName);

                var fileData = new FileDataDTO(cssProperty);
                fileData.Data = new byte[length];
                fileData.Name = name;
                fileData.Length = length;
                fileData.ContentType = contentType;
                httpPostedFile.InputStream.Read(fileData.Data, 0, length);
                fileDtos.Add(fileData);
            }
        
            var updateRule = await _repo.UpdateRuleStyle(rule,fileDtos, entityId);

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