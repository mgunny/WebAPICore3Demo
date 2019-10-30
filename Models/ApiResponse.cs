using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpSubbieWebAPI.Models
{
    public class ApiResponse
    {
        public ApiResponse(ModelStateDictionary modelState = null)
        {
            if (modelState != null)
            {
                //ModelState = new Dictionary<string, IEnumerable<string>>();
                //foreach (var pair in modelState)
                //{
                //    ModelState.Add(pair.Key, pair.Value.Errors.Select(x => x.ErrorMessage));
                //}

                ModelErrorState mes = new ModelErrorState { Fields = new List<FieldErrorMessages>() };

                foreach (var pair in modelState)
                {
                    FieldErrorMessages fem = new FieldErrorMessages() { FieldName = pair.Key, ErrMessage = new List<string>() };
                    foreach (var err in pair.Value.Errors)
                    {
                        fem.ErrMessage.Add(err.ErrorMessage);
                    }
                    mes.Fields.Add(fem);
                }

                ModelState = mes;
            }
        }

        public bool Status { get; set; } = false;
        public string ErrorMessage { get; set; }
        //public Dictionary<string, IEnumerable<string>> ModelState { get; set; }

        public ModelErrorState ModelState { get; set; }
    }


    public class ModelErrorState
    {
        public List<FieldErrorMessages> Fields { get; set; }
    }

    public class FieldErrorMessages
    {
        public string FieldName { get; set; }
        public List<string> ErrMessage { get; set; }
    }

}
