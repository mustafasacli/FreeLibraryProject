using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreeLibrary.Entity.Validation
{
    public interface IEntityValidationResult
    {

        IList<ValidationResult> Errors { get; }

        bool HasError { get; }

        Exception FailError { get; }

    }
}
