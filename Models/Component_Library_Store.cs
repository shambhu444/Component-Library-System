//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Application_CLS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    public partial class Component_Library_Store
    {
        public int SNo { get; set; }

        [DisplayName("Component Name")]
        [Required(ErrorMessage = "* Please enter the component name")]
        public string Component_Name { get; set; }

        [DisplayName("Component Type")]
        [Required(ErrorMessage = "* Please enter the component type (Ex:Web/UI/DB/others)")]
        public string Component_Type { get; set; }

        [Required(ErrorMessage = "* Please enter the description")]
        public string Description { get; set; }

        [DisplayName("Uploader Name")]
        [Required(ErrorMessage = "* Please enter the uploader name")]
        public string Uploader_Name { get; set; }

        [DisplayName("Upload File")]
        [Required(ErrorMessage = "* Please enter the upload file")]
        public string Upload_File { get; set; }

        public string Status { get; set; }

        public HttpPostedFileBase ImageFile { get; set; } 
    }
}