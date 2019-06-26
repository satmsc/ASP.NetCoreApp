using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Utilities
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        private readonly string allowdomain;

        public ValidEmailDomainAttribute(string allowdomain)
        {
            this.allowdomain = allowdomain;
        }

        public override bool IsValid(object value)
        {
            string[] strings= value.ToString().Split('@');

            return strings[1].ToUpper() == allowdomain.ToUpper();
        }
    }
}
