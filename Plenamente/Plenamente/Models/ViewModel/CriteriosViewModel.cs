using System;
using System.Collections.Generic;

namespace Plenamente.Models.ViewModel
{
    public class CriteriosViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Porcentaje { get; set; }
        public DateTime Registro { get; set; }
        public List<EstandaresViewModel> Estandares { get; set; }
    }
    public class EstandaresViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Porcentaje { get; set; }
        public DateTime Registro { get; set; }
        public List<ElementoViewModel> Elementos { get; set; }
    }
    public class ElementoViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Verificar { get; set; }
        public float Porcentaje { get; set; }
        public DateTime Periodo { get; set; }
        public string Observaciones { get; set; }
        public DateTime Registro { get; set; }
        public string Video { get; set; }
        public string Recurso { get; set; }
        public string Reursob { get; set; }
        public List<Cumplimiento> Cumplimientos { get; set; }
    }
}