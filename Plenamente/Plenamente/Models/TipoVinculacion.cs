using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class TipoVinculacion
    {
        [Key]
        public int Tvin_Id { get; set; }
        public string Tvin_Nom { get; set; }
        public DateTime Tvin_Registro { get; set; }
        //Nombre de la colección definido en el prural de TipoVinculacion <TipoVinculaciones>
        public ICollection<TipoVinculacion> TipoVinculaciones { get; set; }
    }
}