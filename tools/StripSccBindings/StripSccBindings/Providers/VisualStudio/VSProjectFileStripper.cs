//--------------------------------------------------------------------------
// <copyright file="VSProjectFileStripper.cs" company="Jeff Winn">
//      Copyright (c) Jeff Winn. All rights reserved.
//
//      The use and distribution terms for this software is covered by the
//      GNU Library General Public License (LGPL) v2.1 which can be found
//      in the License.rtf at the root of this distribution.
//      By using this software in any fashion, you are agreeing to be bound by
//      terms of this license.
//
//      You must not remove this notice, or any other, from this software.
// </copyright>
//--------------------------------------------------------------------------

namespace StripSccBindings.Providers.VisualStudio
{
    using System;
    using System.IO;
    using System.Xml;
    
    /// <summary>
    /// Provides a stripper used to remove source control bindings from a Visual Studio project (.*proj) file.
    /// </summary>
    internal sealed class VSProjectFileStripper : VisualStudioStripperBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StripSccBindings.Providers.VisualStudio.VSProjectFileStripper"/> class.
        /// </summary>
        public VSProjectFileStripper()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Strips the bindings from the file.
        /// </summary>
        protected override void InternalStripBindings()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.Context.File.FullName);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("ms", "http://schemas.microsoft.com/developer/msbuild/2003");

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/ms:Project/ms:PropertyGroup/ms:SccProvider", nsmgr);
            if (nodes != null && nodes.Count > 0)
            {
                // Iterate through all of the nodes that were found to have an SccProvider and remove them and their related nodes.
                foreach (XmlNode node in nodes)
                {
                    XmlNode parent = node.ParentNode;

                    parent.RemoveChild(parent["SccProjectName"]);
                    parent.RemoveChild(parent["SccLocalPath"]);
                    parent.RemoveChild(parent["SccAuxPath"]);
                    parent.RemoveChild(parent["SccProvider"]);
                }

                doc.Save(this.Context.File.FullName);
            }
        }

        /// <summary>
        /// Raises the <see cref="AfterStripBindings"/> event.
        /// </summary>
        /// <param name="e">An <see cref="System.EventArgs"/> containing event data.</param>
        protected override void OnAfterStripBindings(EventArgs e)
        {
            string vsssccFileName = this.Context.File.FullName + ".vspscc";

            if (File.Exists(vsssccFileName))
            {
                File.Delete(vsssccFileName);
            }

            base.OnAfterStripBindings(e);
        }

        #endregion
    }
}