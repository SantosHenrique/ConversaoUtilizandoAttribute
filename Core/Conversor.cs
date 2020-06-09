using System;
using System.Linq;
using System.Reflection;

namespace Core
{
    /// <summary>
    /// Contém a lógica principal da aplicação
    /// </summary>
    public class Conversor
    {
        /// <summary>
        /// Percorre todas as propriedades que estejam decoradas com o CampoAttribute, de determinada classe
        /// </summary>
        /// <typeparam name="T">Tipo de classe genérico</typeparam>
        /// <param name="convertida">Classe alvo para conversão de campos</param>
        /// <returns>Mesma classe genérica de entrada, porém com campos convertidos, caso haja</returns>
        public static T ConverterPropriedades<T>(T convertida) where T : class
        {
            foreach (PropertyInfo propriedade in convertida.GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(CampoAttribute))))
            {
                if (TemValor(convertida, propriedade))
                {
                    Attribute atributos = Attribute.GetCustomAttribute(propriedade, typeof(CampoAttribute));
                    string nomeAtributoConvertido = atributos.GetType().GetProperty("Convertido").GetValue(atributos).ToString();

                    if (TemPropriedade(convertida, nomeAtributoConvertido))
                    {
                        string tipoPropriedade = convertida.GetType().GetProperty(nomeAtributoConvertido).PropertyType.Name;
                        switch (tipoPropriedade)
                        {
                            case "DateTime":
                                {
                                    ConverterStringParaDateTime(convertida, nomeAtributoConvertido, propriedade.GetValue(convertida).ToString());
                                }; break;
                            default: 
                                {
                                    throw new SystemException($"Não existe método para a conversão do tipo {tipoPropriedade}");
                                }; 
                        }
                    }
                    else
                    {
                        throw new SystemException("Propriedade não foi encontrada na classe");
                    }
                }
            }
            return convertida;
        }

        /// <summary>
        /// Verifica se determinada propriedade tem valor
        /// </summary>
        /// <typeparam name="T">Tipo de classe genérico</typeparam>
        /// <param name="convertida">Classe que possui a propriedade que será verificada</param>
        /// <param name="propriedade">Propriedade para verificar se existe valor</param>
        /// <returns>True caso exista valor</returns>
        private static bool TemValor<T>(T convertida, PropertyInfo propriedade) where T : class
            => convertida.GetType().GetProperty(propriedade.Name).GetValue(convertida) != null;

        /// <summary>
        /// Verifica se a classe possui determinada propriedade
        /// </summary>
        /// <typeparam name="T">Tipo de classe genérico</typeparam>
        /// <param name="convertida">Classe que receberá verificação</param>
        /// <param name="propriedade">Nome da propriedade</param>
        /// <returns>True caso a classe contenha a propriedade</returns>
        private static bool TemPropriedade<T>(T convertida, string propriedade) where T : class
            => convertida.GetType().GetProperty(propriedade) != null;
        
        /// <summary>
        /// Converte String para DateTime
        /// </summary>
        /// <typeparam name="T">Tipo de classe genérico</typeparam>
        /// <param name="convertida">Classe que possui a propriedade que receberá a conversão</param>
        /// <param name="nomeAtributoConvertido">Nome do atributo que receberá a conversão</param>
        /// <param name="valor">Valor que será convertido</param>
        private static void ConverterStringParaDateTime<T>(T convertida, string nomeAtributoConvertido, string valor)
        {
            try
            {
                DateTime dataConvertida = DateTime.Parse(valor);
                convertida.GetType().GetProperty(nomeAtributoConvertido).SetValue(convertida, dataConvertida);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
