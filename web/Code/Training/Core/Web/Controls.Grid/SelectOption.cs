using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//----------------------------------------------------   
//  Copyright (c) 2009-2010 Epic Systems Corporation   
//  
//  Revision History:   
//   *BCM 01/09 - Created  
//----------------------------------------------------   
namespace Epic.Training.Core.Controls.Grid.Web
{
    public class SelectOption:ClientKeyValuePair
    {
        public string Value { get; set; }
        public string DisplayText { get; set; }



        public override string ToKeyValuePair()
        {
            return "'" + Value + "':'" + DisplayText + "'";
        }
    }
}
