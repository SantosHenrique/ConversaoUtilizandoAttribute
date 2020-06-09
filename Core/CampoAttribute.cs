using System;

namespace Core
{
    /// <summary>
    /// Attribute para decorar atributos de classes que posssuem duas propriedades com tipos diferentes, porém que representam a mesma coisa 
    /// </summary>
    public class CampoAttribute : Attribute
    {
        //Nome da outra propriedade que receberá o valor convertido
        public string Convertido { get; set; }
    }
}
