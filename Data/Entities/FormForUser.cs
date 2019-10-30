using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpSubbieWebAPI.Data.Entities
{
    public class FormForUser
    {        
        public int Id { get; set; }
        public string FormType { get; set; }
        public int FormVersion { get; set; }
        public string FormDescription { get; set; }
        public string FormQuestionHeader { get; set; }
        public string FormTickHeader { get; set; }
        public bool Complete { get; set; }
        public DateTime? DateComplete { get; set; }
    }
}
