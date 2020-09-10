using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreeLibrary.Entity.Validation
{
    public class EntityValidationResult : IEntityValidationResult
    {
        public EntityValidationResult(IList<ValidationResult> errors = null, Exception ex = null)
        {
            this.Errors = errors ?? new List<ValidationResult>();
            if (ex != null)
            {
                this.FailError = ex;
            }
        }

        public IList<ValidationResult> Errors { get; private set; }

        public bool HasError
        {
            get
            {
                return Errors?.Count > 0;
            }
        }


        public Exception FailError { get; private set; }
    }
}