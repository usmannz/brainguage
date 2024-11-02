using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using FRCSPreparationPortal.Common.Entities;

namespace FRCSPreparationPortal.Common
{

    /// <summary>
    /// This class contains different extension methods
    /// </summary>
    public static class ExtensionMethods
    {
        #region STRING
        public static string EncodeForUrl(this string str) => str.ToLower().Replace(" ", "-");

        /// <summary>
        /// Replace invalid characters from string that break XML
        /// </summary>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static string ToXmlSafe(this string pValue)
        {
            //return pValue.Replace("&lt:", "<").Replace("&gt:", ">")
            return pValue.Replace("<", "&lt:").Replace(">", "&gt:");
        }
        /// <summary>
        /// Replace temporary characters from string with actual characters
        /// </summary>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static string FromXmlSafe(this string pValue)
        {
            return pValue.Replace("&lt:", "<").Replace("&gt:", ">");
        }
        #endregion

        #region GENERIC LISTS
        /// <summary>
        /// Generate cloned copy of list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToClone"></param>
        /// <returns></returns>
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
        /// <summary>
        /// Deep copy of list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToClone"></param>
        /// <returns></returns>
        // public static List<T> DeepClone<T>(this List<T> listToClone)
        // {
        //     using (var ms = new MemoryStream())
        //     {
        //         var formatter = new BinaryFormatter();
        //         formatter.Serialize(ms, listToClone);
        //         ms.Position = 0;

        //         return (List<T>)formatter.Deserialize(ms);
        //     }
        // }
        /// <summary>
        /// Deep Clone an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        // public static T DeepClone<T>(this T obj)
        // {
        //     using (var ms = new MemoryStream())
        //     {
        //         var formatter = new BinaryFormatter();
        //         formatter.Serialize(ms, obj);
        //         ms.Position = 0;

        //         return (T)formatter.Deserialize(ms);
        //     }
        // }
        #endregion

        #region PaginationMethods
        /// <summary>
        /// Pagination Functions
        /// </summary>
        /// <param name="PaginationMethods"></param>
        /// <returns></returns>
        public static bool HasPrevious(this Pager paginations)
        {
            return (paginations.PageIndex > 1);
        }

        public static bool HasNext(this Pager paginations, int totalCount)
        {
            return (paginations.PageIndex < (int)GetTotalPages(paginations, totalCount));
        }

        public static double GetTotalPages(this Pager paginations, int totalCount)
        {
            return Math.Ceiling(totalCount / (double)paginations.PageSize);
        }
      
        public static bool EqualsIgnoreCase(this string val, string valToCompare)
        {
            return val.Equals(valToCompare, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool ContainsIgnoreCase(this string val, string valToCompare)
        {
            return val.Contains(valToCompare, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion
    }
}