using ProductCase.Common.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ProductCase.Common.Extensions
{
    public static class EnumExtensions
    {
        public static int GetHttpStatusCode(this ResponseStatusEnum status)
        {
            switch (status)
            {
                case ResponseStatusEnum.Success:
                    return (int)HttpStatusCode.OK;
                case ResponseStatusEnum.ValidationError:
                    return (int)HttpStatusCode.BadRequest;
                case ResponseStatusEnum.Exception:
                    return (int)HttpStatusCode.InternalServerError;
                case ResponseStatusEnum.NotFound:
                    return (int)HttpStatusCode.NotFound;
                default:
                    return (int)HttpStatusCode.NotFound;
            }
        }
    }
}
