using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Common
{
    [Serializable]
    public enum BuildMode
    {
        None = 0,
        Dev = 1,
        Production = 2,
        Staging = 3
    }

    public enum Roles
    {
        Admin = 1,
        User = 2
    }

       public enum UserStatus
    {
        All = 0,
        Active = 1,
        Locked = 2,
        Pending = 3
    }

}
