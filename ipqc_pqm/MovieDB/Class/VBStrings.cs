using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpqcDB
{
    public class VBStrings
    {
        #region　Left Method

        /// -----------------------------------------------------------------------------------
        /// <summary>
        ///     Returns a character string of the specified number of characters from the left end of the string.</summary>
        /// <param name="stTarget">
        ///     The string to be extracted.</param>
        /// <param name="iLength">
        ///     Number of characters to retrieve.</param>
        /// <returns>
        ///     A character string for the number of characters specified from the left end.
        ///     If the number of characters is exceeded, the entire string is returned.</returns>
        /// -----------------------------------------------------------------------------------
        public static string Left(string stTarget, int iLength)
        {
            if (iLength <= stTarget.Length)
            {
                return stTarget.Substring(0, iLength);
            }

            return stTarget;
        }

        #endregion


        #region　Mid Method (+1)

        /// -----------------------------------------------------------------------------------
        /// <summary>
        ///     Returns all the character strings after the specified position in the string.</summary>
        /// <param name="stTarget">
        ///     The string to be extracted.</param>
        /// <param name="iStart">
        ///     Position to start taking out.</param>
        /// <returns>
        ///     All strings after the specified position.</returns>
        /// -----------------------------------------------------------------------------------
        public static string Mid(string stTarget, int iStart)
        {
            if (iStart <= stTarget.Length)
            {
                return stTarget.Substring(iStart - 1);
            }

            return string.Empty;
        }


        /// -----------------------------------------------------------------------------------
        /// <summary>
        ///     Returns a character string of the specified number of characters from the specified position of the character string.</summary>
        /// <param name="stTarget">
        ///     The string to be extracted.</param>
        /// <param name="iStart">
        ///     Position to start taking out.</param>
        /// <param name="iLength">
        ///     Number of characters to retrieve.</param>
        /// <returns>
        ///     A character string for the specified number of characters from the specified position.
        ///     If the number of characters is exceeded, all the character strings are returned from the specified position.</returns>
        /// -----------------------------------------------------------------------------------
        public static string Mid(string stTarget, int iStart, int iLength)
        {
            if (iStart <= stTarget.Length)
            {
                if (iStart + iLength - 1 <= stTarget.Length)
                {
                    return stTarget.Substring(iStart - 1, iLength);
                }

                return stTarget.Substring(iStart - 1);
            }

            return string.Empty;
        }

        #endregion


        #region　Right Method (+1)

        /// -----------------------------------------------------------------------------------
        /// <summary>
        ///     Returns a character string of the specified number of characters from the right end of the character string.</summary>
        /// <param name="stTarget">
        ///     The string to be extracted.</param>
        /// <param name="iLength">
        ///     Number of characters to retrieve.</param>
        /// <returns>
        ///     A character string for the number of characters specified from the right end.
        ///     If the number of characters is exceeded, the entire string is returned.</returns>
        /// -----------------------------------------------------------------------------------
        public static string Right(string stTarget, int iLength)
        {
            if (iLength <= stTarget.Length)
            {
                return stTarget.Substring(stTarget.Length - iLength);
            }

            return stTarget;
        }

        #endregion
    }

}
