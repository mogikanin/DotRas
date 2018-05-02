//--------------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="Jeff Winn">
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

using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("DotRas")]
[assembly: AssemblyDescription("DotRas")]
[assembly: AssemblyCompany("Jeff Winn")]
[assembly: AssemblyProduct("DotRas")]
[assembly: AssemblyCopyright("Copyright (c) Jeff Winn. All rights reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

#if (DEBUG)
[assembly: AssemblyConfiguration("DEBUG")]
#else
[assembly: AssemblyConfiguration("RELEASE")]
#endif

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("f3c60633-dd32-483b-94a5-fe232aa9a7b5")]
[assembly: BestFitMapping(false, ThrowOnUnmappableChar = true)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.3.*")]
[assembly: AssemblyFileVersion("1.3.0.0")]
[assembly: NeutralResourcesLanguageAttribute("en-US")]

#if (!NO_UNIT_TESTING)
[assembly: InternalsVisibleTo("DotRas.UnitTests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100f34481525f45faaf3cf26e355a51e33f32e3957d9c109d1c36b812a9ecc27d9cd753622db5cca82db349a5a5213ab653525e2b086815821c5f9613db404826cd614b0dc08ee381ae2f82cdc391acf6c5c77f258b0fdf7ed77dfa0999de763692045bd9bef9a464bbeb06e5aebc5f1daa46e0cd98ea11949314ad5830135876af")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2, PublicKey=0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7")]
#endif