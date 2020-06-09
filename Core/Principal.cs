using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    /// <summary>
    /// Para visualização do funcionamento do algoritmo
    /// </summary>
    public static class Principal
    {
        /// <summary>
        /// Execução da aplicãção
        /// </summary>
        static void Main()
        {
            Pessoa pessoa = new Pessoa();
            PreencherClasseTeste(pessoa);
            Conversor.ConverterPropriedades(pessoa);
            MostrarValorPropriedade(pessoa);
        }

        /// <summary>
        /// Atribuindo valores às propriedades da classe de teste Pessoa
        /// </summary>
        /// <param name="pessoa">Classe Pessoa</param>
        private static void PreencherClasseTeste(Pessoa pessoa)
        {
            pessoa.sNome = "Henrique";
            pessoa.sDataNascimento = "1995-04-23";
        }

        /// <summary>
        /// Percorre as propriedades de uma classe e exibe o valor
        /// </summary>
        /// <typeparam name="T">Tipo genérico, apenas para class</typeparam>
        /// <param name="objeto">Instacia de classe</param>
        private static void MostrarValorPropriedade<T>(T objeto) where T : class
        {
            foreach (var item in objeto.GetType().GetProperties())
            {
                Console.WriteLine(item.GetValue(objeto));
            }
        }
    }
}
