using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class ItemEvidenciaUrl
    {
        public int Id { get; set; }
        public int ItemEvidencia_Id { get; set; }
        public string Url { get; set; }
        public virtual ItemEvidencia ItemEvidencia { get; set; }
    }
}