using System;
using System.ComponentModel;

namespace KiraSoft.CrossCutting.Operation.Exceptions.Enum
{
    [Flags]
    public enum FailureCode
    {
        //// System failure codes
        [Description("Set here the description")]
        UnknownError = 0,

        [Description("Set here the description")]
        UnauthorizedAccess = 1,

        [Description("Set here the description")]
        NotFound = 2,

        [Description("Set here the description")]
        AlreadyExist = 3,

        [Description("Set here the description")]
        InvalidTransaction = 4,

        [Description("Set here the description")]
        StatusDoesNotExist = 5,

        [Description("Error at create user")]
        ErrorAtCreateUser = 6,

        [Description("Model State Error")]
        ModelStateError = 7,

        [Description("Bad Request Server Error")]
        BadRequest = 400
    }
}
