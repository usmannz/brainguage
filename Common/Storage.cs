using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using FRCSPreparationPortal.Common.Contracts;
using static System.Net.Mime.MediaTypeNames;

namespace FRCSPreparationPortal.Common
{
     public static class Storage
    {
        public static IHostingEnvironment Environment { get; set; }

        public static IStorage Provider
        {
            get
            {
                return new FileStorage(Environment);
            }
        }
    }

}
