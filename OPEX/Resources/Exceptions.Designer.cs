﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToolBX.OPEX.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Exceptions {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Exceptions() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ToolBX.OPEX.Resources.Exceptions", typeof(Exceptions).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can&apos;t get randoms from collection : numberOfElements must be a positive number but its value is {0}.
        /// </summary>
        internal static string CannotGetManyRandomsBecauseNumberNegative {
            get {
                return ResourceManager.GetString("CannotGetManyRandomsBecauseNumberNegative", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} method does not support fixed-size collections such as arrays.
        /// </summary>
        internal static string CannotUseMethodBecauseIsFixedSize {
            get {
                return ResourceManager.GetString("CannotUseMethodBecauseIsFixedSize", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot use Uniform : the collection is empty.
        /// </summary>
        internal static string CannotUseUniformOnEmptyCollection {
            get {
                return ResourceManager.GetString("CannotUseUniformOnEmptyCollection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Item &apos;{0}&apos; could not be removed from collection.
        /// </summary>
        internal static string ItemCouldNotBeRemoved {
            get {
                return ResourceManager.GetString("ItemCouldNotBeRemoved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A null value could not be removed from collection.
        /// </summary>
        internal static string NullCouldNotBeRemoved {
            get {
                return ResourceManager.GetString("NullCouldNotBeRemoved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Item to remove could not be found with predicate.
        /// </summary>
        internal static string PredicateItemCouldNotBeRemoved {
            get {
                return ResourceManager.GetString("PredicateItemCouldNotBeRemoved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot use Uniform : there are duplicate entries in the collection.
        /// </summary>
        internal static string UniformFoundNonDuplicates {
            get {
                return ResourceManager.GetString("UniformFoundNonDuplicates", resourceCulture);
            }
        }
    }
}
