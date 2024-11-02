﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Common.Models
{
    public class ApiResponse<T>
    {
        public ApiResponseStatus Status { get; set; }
        public T Data { get; set; }
    }

    public class ApiResponseStatus
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

}
