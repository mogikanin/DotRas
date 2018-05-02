//--------------------------------------------------------------------------
// <copyright file="ThrowHelper.cs" company="Jeff Winn">
//      Copyright (c) Jeff Winn. All rights reserved.
//
//      The use and distribution terms for this software is covered by the
//      GNU Library General Public License (LGPL) v2.1 which can be found
//      in the License.rtf at the root of this distribution.
//      By using this software in any fashion, you are agreeing to be bound by
//      the terms of this license.
//
//      You must not remove this notice, or any other, from this software.
// </copyright>
//--------------------------------------------------------------------------

namespace DotRas.Internal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;

    /// <summary>
    /// Provides methods used to throw exceptions within the assembly.
    /// </summary>
    internal static class ThrowHelper
    {
        /// <summary>
        /// Throws a new <see cref="System.ArgumentOutOfRangeException"/> exception.
        /// </summary>
        /// <param name="argumentName">The argument name that caused the exception.</param>
        /// <param name="actualValue">The value of the argument.</param>
        /// <param name="resource">An <see cref="System.String"/> to include with the exception message.</param>
        public static void ThrowArgumentOutOfRangeException(string argumentName, object actualValue, string resource)
        {
            throw new ArgumentOutOfRangeException(argumentName, actualValue, ThrowHelper.FormatResourceString(resource, argumentName));
        }

        /// <summary>
        /// Throws a new <see cref="System.IO.FileNotFoundException"/> exception.
        /// </summary>
        /// <param name="fileName">The filename that was not found.</param>
        /// <param name="message">A message that describes the error.</param>
        public static void ThrowFileNotFoundException(string fileName, string message)
        {
            throw new FileNotFoundException(message, ThrowHelper.FormatResourceString(message, fileName));
        }

        /// <summary>
        /// Throws a new <see cref="System.ArgumentNullException"/> exception.
        /// </summary>
        /// <param name="argumentName">The argument name that caused the exception.</param>
        public static void ThrowArgumentNullException(string argumentName)
        {
            throw new ArgumentNullException(argumentName);
        }

        /// <summary>
        /// Throws a new <see cref="System.ArgumentException"/> exception.
        /// </summary>
        /// <param name="argumentName">The argument name that caused the exception.</param>
        /// <param name="resource">An <see cref="System.String"/> to include with the exception message.</param>
        public static void ThrowArgumentException(string argumentName, string resource)
        {
            object[] args = { argumentName };

            ThrowHelper.ThrowArgumentException(argumentName, resource, args);
        }

        /// <summary>
        /// Throws a new <see cref="System.ArgumentException"/> exception.
        /// </summary>
        /// <param name="argumentName">The argument name that caused the exception.</param>
        /// <param name="resource">An <see cref="System.String"/> to include with the exception message.</param>
        /// <param name="args">A <see cref="System.Object"/> array containing zero or more items to format.</param>
        public static void ThrowArgumentException(string argumentName, string resource, params object[] args)
        {
            throw new ArgumentException(ThrowHelper.FormatResourceString(resource, args), argumentName);
        }

        /// <summary>
        /// Throws a new <see cref="DotRas.InvalidHandleException"/> exception.
        /// </summary>
        /// <param name="handle">The <see cref="System.IntPtr"/> that caused the exception.</param>
        /// <param name="resource">An <see cref="System.String"/> to include with the exception message.</param>
        public static void ThrowInvalidHandleException(RasHandle handle, string resource)
        {
            throw new InvalidHandleException(handle, resource);
        }

        /// <summary>
        /// Throws a new <see cref="DotRas.InvalidHandleException"/> exception.
        /// </summary>
        /// <param name="handle">The <see cref="System.IntPtr"/> that caused the exception.</param>
        /// <param name="argumentName">The argument name that caused the exception.</param>
        /// <param name="resource">An <see cref="System.String"/> to include with the exception message.</param>
        public static void ThrowInvalidHandleException(RasHandle handle, string argumentName, string resource)
        {
            throw new InvalidHandleException(handle, ThrowHelper.FormatResourceString(resource, new object[] { argumentName }));
        }

        /// <summary>
        /// Throws a new <see cref="System.InvalidOperationException"/> exception.
        /// </summary>
        /// <param name="resource">An <see cref="System.String"/> to include with the exception message.</param>
        public static void ThrowInvalidOperationException(string resource)
        {
            throw new InvalidOperationException(resource);
        }

        /// <summary>
        /// Throws a new <see cref="System.InvalidOperationException"/> exception.
        /// </summary>
        /// <param name="resource">An <see cref="System.String"/> to include with the exception message.</param>
        /// <param name="args">An <see cref="System.Object"/> array containing zero or more items to format.</param>
        public static void ThrowInvalidOperationException(string resource, params object[] args)
        {
            throw new InvalidOperationException(ThrowHelper.FormatResourceString(resource, args));
        }

        /// <summary>
        /// Throws a new <see cref="System.NotSupportedException"/> exception.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public static void ThrowNotSupportedException(string message)
        {
            throw new NotSupportedException(message);
        }

        /// <summary>
        /// Throws a new <see cref="System.NotSupportedException"/> exception.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="args">An <see cref="System.Object"/> array containing zero or more items to format.</param>
        public static void ThrowNotSupportedException(string message, params object[] args)
        {
            throw new NotSupportedException(ThrowHelper.FormatResourceString(message, args));
        }

        /// <summary>
        /// Throws a new <see cref="System.UnauthorizedAccessException"/> exception.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public static void ThrowUnauthorizedAccessException(string message)
        {
            throw new UnauthorizedAccessException(message);
        }

        /// <summary>
        /// Throws a new <see cref="DotRas.RasException"/> exception.
        /// </summary>
        /// <param name="errorCode">The error code that caused the exception.</param>
        public static void ThrowRasException(int errorCode)
        {
            throw new RasException(errorCode);
        }

        /// <summary>
        /// Throws a new <see cref="System.ComponentModel.Win32Exception"/> exception containing the last Win32 error that occurred.
        /// </summary>
        public static void ThrowWin32Exception()
        {
            throw new Win32Exception();
        }

        /// <summary>
        /// Throws a new <see cref="System.ComponentModel.Win32Exception"/> exception containing the last Win32 error that occurred.
        /// </summary>
        /// <param name="errorCode">The error code that caused the exception.</param>
        public static void ThrowWin32Exception(int errorCode)
        {
            throw new Win32Exception(errorCode);
        }

        /// <summary>
        /// Replaces the format item of the <see cref="System.String"/> resource specified with the equivalent in the <paramref name="args"/> array specified.
        /// </summary>
        /// <param name="resource">An <see cref="System.String"/> to format.</param>
        /// <param name="args">An <see cref="System.Object"/> array containing zero or more items to format.</param>
        /// <returns>The formatted resource string.</returns>
        private static string FormatResourceString(string resource, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, resource, args);
        }
    }
}