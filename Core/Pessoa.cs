using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    /// <summary>
    /// Utilizada para teste do algoritmo 
    /// </summary>
    public class Pessoa
    {
        public string sNome { get; set; }
        [CampoAttribute(Convertido = "dtDataNascimento")]
        public string sDataNascimento { get; set; }
        public DateTime dtDataNascimento { get; set; }
        [CampoAttribute(Convertido = "dPeso")]
        public string sPeso { get; set; }
        public Decimal dPeso { get; set; }
    }
}
