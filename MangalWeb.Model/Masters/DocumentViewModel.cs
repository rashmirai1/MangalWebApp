﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangalWeb.Model.Masters
{
    public class DocumentViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Document Name is Required")]
        [StringLength(50)]
        //[Remote("doesSourceNameExist", "SourceofApplication", ErrorMessage = "Source Name Already Exists.")]
        public string DocumentName { get; set; }

        [Required(ErrorMessage = "Please Select Document Type")]
        public int DocumentType { get; set; }

        public bool ExpiryDateApplicable { get; set; }
  
        [Required(ErrorMessage = "Please Select Status")]
        public short DocumentStatus { get; set; }

        public string DocumentTypeStr { get; set; }
        [Required(ErrorMessage = "Please Select Expiry Date Applicable")]
        public string ExpiryApplicableStr { get; set; }
        public string DocumentStatusStr { get; set; }

        public string operation { get; set; }

    }
}