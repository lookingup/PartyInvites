using System;
using System.Reflection;
//----------------------------------------------------   
//  Copyright (c) 2009-2010 Epic Systems Corporation   
//  
//  Revision History:   
//   *BCM 01/09 - Created  
//----------------------------------------------------   

namespace Epic.Training.Core.Controls.Grid.Web
{
    /// <summary>
    /// Represents one property of a JavaScript object.  A property is defined as a 'Key':Value pair.  The
    /// Value may be any valid JavaScript type:  String, Number, Boolean or Object/Array.  Classes inheriting
    /// form this class must implement the ToKeyValuePair method to return a string representation to be sent to
    /// the client.
    /// </summary>
    public abstract class ClientKeyValuePair
    {
        /// <summary>
        /// Return the 'Key':Value pair as a string
        /// </summary>
        /// <returns>String representation of 'Key':Value for this pair</returns>
        public abstract string ToKeyValuePair();

        /// <summary>
        /// Get the Type-specific 'Key':Value pair of a given class inheriting from ClientKeyValuePair.  Along
        /// the chain of inheritance, it is possible that a derived class implements several different versions of
        /// ToKeyValuePair, if the "new" keyword was used in the derived classes.  This method accesses the specific
        /// version defined within the class T.
        /// </summary>
        /// <typeparam name="T">The class from which to access ToKeyValuePair</typeparam>
        /// <param name="pair">The ClientKeyValuePair from which to extract the string representation</param>
        /// <returns>String representation of 'Key':Value for the given pair</returns>
        public static string GetTypeSpecificKeyValuePair<T>(T pair) where T : ClientKeyValuePair
        {
            // Get the T specific "ToKeyValuePair" implementation, if one exists.  If one does not
            // exist specific to T, then null is returned
            MethodInfo toKeyValuePair = typeof(T).GetMethod("ToKeyValuePair");
            string keyValuePair = String.Empty;
            if (toKeyValuePair != null)
            {   // Invoke class-specific ToKeyValuePair() implemented using new
                keyValuePair = toKeyValuePair.Invoke(pair, null).ToString();
            }
            else
            {   // Invoke latest ToKeyValuePair() implemented using override
                keyValuePair = pair.ToKeyValuePair();
            }
            return keyValuePair;
        }
    }
}
