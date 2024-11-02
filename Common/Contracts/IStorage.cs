using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Common.Contracts
{
   public interface IStorage
    {
        /// <summary>
        /// save image on disk
        /// </summary>
        /// <returns></returns>
        bool Save(string path, byte[] bytes);

        /// <summary>
        /// Delete image
        /// </summary>
        /// <returns></returns>
        bool Delete(string path);

        /// <summary>
        /// Read all file contents
        /// </summary>
        /// <returns></returns>
        string Read(string path);
    }
}
