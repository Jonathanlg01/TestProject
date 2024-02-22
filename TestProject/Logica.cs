using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestProject
{
    public class Logica
    {
        /// <summary>
        /// Metodo recebe um numero em texto usando separador . como separador de milhar e , como separador decimal
        /// </summary>
        /// <param name="numeroString"></param>
        /// <returns></returns>
        public int ConverteStringParaInt(string numeroString)
        {
            int numeroInteiro;

            int.TryParse(numeroString, out numeroInteiro);

            return numeroInteiro;
        }



        /// <summary>
        /// Metodo recebe um numero em texto usando separador . como separador de milhar e , como separador decimal
        /// </summary>
        /// <param name="numeroString"></param>
        /// <returns></returns>
        internal decimal ConverteStringParaDecimal(string numeroString)
        {

            string numeroFormatado = numeroString.Replace(".", "").Replace(",", ".");


            decimal numeroDecimal = decimal.Parse(numeroFormatado, CultureInfo.InvariantCulture);

            return numeroDecimal;
        }

        /// <summary>
        /// Metodo recebe uma data em texto no formato dd/MM/yyyy e retorna a data convertida
        /// </summary>
        /// <param name="dataString"></param>
        /// <returns></returns>
        internal DateTime ConverteStringParaData(string dataString)
        {
            DateTime data;

            DateTime.TryParse(dataString, out data);

            return data;

        }

        /// <summary>
        /// Vendedor Gustavo
        /// Código Produto	quantidade    valor total          Data venda
        /// ARA-1012        17 UN          R$ 3.642,17              08/04/2021
        /// </summary>
        /// <param name="produtosString"></param>
        /// <returns></returns>
        /// Dica, pode ser usado regex para separar a string pro padrões.


        internal List<VendaTO> ConverteStringParaVendas(string produtosString)
        {
            string vendedor = "";

            string[] linhas = produtosString.Split('\n');

            List<VendaTO> vendas = new List<VendaTO>();

            foreach (string linha in linhas)
            {

                if (linha.Trim().StartsWith("Vendedor"))
                {
                    vendedor = linha.Substring(9);
                }

                else if (linha != "" && !linha.Contains("C�digo Produto") && linha != "\r")
                {

                    string[] colunas = linha.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


                    VendaTO venda = new VendaTO();
                    venda.Vendedor = vendedor.Replace("Vendedor","").Trim();
                    venda.Codigo = colunas[0];
                    venda.Quantidade = int.Parse(colunas[1]);
                    venda.Valor = decimal.Parse(colunas[4], NumberStyles.Currency);
                    venda.Data = DateTime.Parse(colunas[5].Replace(@"\r", ""));


                    vendas.Add(venda);
                }
            }
            return vendas;
        }
    }
}
