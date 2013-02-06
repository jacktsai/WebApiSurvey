using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WebApi.ComponentModel.DataAnnotations
{
    class MyRequiredAttribute : RequiredAttribute
    {
        public override object TypeId
        {
            get
            {
                return base.TypeId;
            }
        }

        public override bool IsDefaultAttribute()
        {
            return base.IsDefaultAttribute();
        }

        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }

        public override bool Match(object obj)
        {
            return base.Match(obj);
        }
    }
}
