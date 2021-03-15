using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.Models
{
    public class BaseTranslationModel
    {
        public int LanguageId { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public bool IsDefault { get; set; }
    }
}
