using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using Newtonsoft.Json;
using OneApp.Common.Core.Utilities;

namespace OneApp.Common.WebServices.Models
{
    public class ErrorResponse : BaseResponse
    {

        [JsonProperty("errors")]
        public IList<ErrorDetail> Errors { get; set; }

        public ErrorResponse(string errorMessage) : this(new List<string>() { errorMessage })
        {

        }

        public ErrorResponse([Required]List<string> errors)
        {

            this.Errors = errors.Select(er => new ErrorDetail(er)).ToList();
        }



        public ErrorResponse(ModelStateDictionary modelState)
        {
            this.Errors = new List<ErrorDetail>(); 
            foreach (var value in modelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    if (error.Exception != null)
                    {
                        throw error.Exception;
                    }  
                    this.Errors.Add(new ErrorDetail(error.ErrorMessage));
                }
            }
        }
    }
}