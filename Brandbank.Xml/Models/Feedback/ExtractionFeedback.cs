﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.6.81.0.
// 
namespace Brandbank.Xml.Models.Feedback { 
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.brandbank.com/schemas/rpf/2005/11")]
    [System.Xml.Serialization.XmlRootAttribute("RetailerProcessFeedback", Namespace="http://www.brandbank.com/schemas/rpf/2005/11", IsNullable=false)]
    public partial class ReportType {
        
        /// <remarks/>
        public MessageType Message;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Item")]
        public ItemType[] Item;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.brandbank.com/schemas/rpf/2005/11")]
    public partial class MessageType {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime DateTime;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DateTimeSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Domain;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.brandbank.com/schemas/rpf/2005/11")]
    public partial class ItemType {
        
        /// <remarks/>
        public string RetailerID;
        
        /// <remarks/>
        public string MessageGUID;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer")]
        public string BrandbankID;
        
        /// <remarks/>
        public string Description;
        
        /// <remarks/>
        public string Comment;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public StatusType Status;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool StatusSpecified;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]

    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.brandbank.com/schemas/rpf/2005/11")]
    public enum StatusType {
        
        /// <remarks/>
        Matched,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Not Sold")]
        NotSold,
        
        /// <remarks/>
        Manual,
        
        /// <remarks/>
        Failed,
        
        /// <remarks/>
        Deleted,
    }
}

