﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class EleProteccion
    {
        [Key]
        public int Epro_Id { get; set; }
        public string Epro_Nom { get; set; }
        public DateTime Epro_Registro { get; set; }
        // Permite A EprotEmepresa acceder a la Data
        public ICollection<EprotEmpresa> EprotEmpresas { get; set; }
    }
}