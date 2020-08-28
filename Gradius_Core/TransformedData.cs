using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gradius_Core
{
    public class TransformedData
    {
        public List<string[]> Table { get; set; }
        public void Load(string filePath)
        {
           
            this.Table = new List<string[]>();
            var lines = System.IO.File.ReadAllLines(filePath);
            foreach (string line in lines)
                this.Table.Add(line.Split(';'));


        }

        public FilterOptions GetFilterOptions()
        {
            FilterOptions filterOptions = new FilterOptions();
            filterOptions.Ano = getDistincColumnValues(getColumn("Ano"));
            filterOptions.Mes = getDistincColumnValues(getColumn("Mes"));
            filterOptions.DiaSemana = getDistincColumnValues(getColumn("DiaSemana"));
            filterOptions.Pais = getDistincColumnValues(getColumn("Pais"));
            filterOptions.Estado = getDistincColumnValues(getColumn("Estado"));
            filterOptions.Cidade = getDistincColumnValues(getColumn("Cidade"));
            filterOptions.Distrito = getDistincColumnValues(getColumn("Distrito"));

            return filterOptions;
        }

        private List<string> getColumn(string ColumnName)
        {
            List<string> column = new List<string>();
            string[] header = this.Table[0];
            int pos = header.ToList<string>().IndexOf(ColumnName);
            for(int i = 1; i < this.Table.Count; i++)
            {
                column.Add(this.Table[i][pos]);
            }


            return column;
        }

        public List<string> getDistincColumnValues(List<string> Column)
        {
            return Column.Distinct().ToList();
        }

        public string getMaxValor()
        {
            List<string> Valores = getColumn("Valor");
            double maior = Convert.ToDouble(Valores[0]);
            string Max = Valores[0];
            foreach(string valor in Valores)
            {
                if(Convert.ToDouble(valor) > maior)
                {
                    maior = Convert.ToDouble(valor);
                    Max = valor;
                }
            }

            return Max;

        }

        public string getMinValor()
        {
            List<string> Valores = getColumn("Valor");
            double menor = Convert.ToDouble(Valores[0]);
            string Min = Valores[0];
            foreach (string valor in Valores)
            {
                if (Convert.ToDouble(valor) < menor)
                {
                    menor = Convert.ToDouble(valor);
                    Min = valor;
                }
            }

            return Min;

        }

        public void Concat(TransformedData add)
        {
            add.Table.RemoveAt(0);
            this.Table.AddRange(add.Table);
        }

        public static TransformedData ConvertRawtoTransformed(RawData rawData)
        {
            TransformedData transformedData = new TransformedData();
            string[] header = rawData.Table[0];

            transformedData.Table = new List<string[]>();

            string tHead = "Valor;DiaSemana;Mes;Ano;Pais;Estado;Cidade;Distrito;Qtd";
            transformedData.Table.Add(tHead.Split(";"));

            List<T_Row> T_Table = new List<T_Row>();

            for (int i = 1;i < rawData.Table.Count; i++)
            {
               
                T_Row row = new T_Row();
                int pos = header.ToList<string>().IndexOf("Valor");
                row.Valor = rawData.Table[i][pos];

                pos = header.ToList<string>().IndexOf("Data");
                string sData = rawData.Table[i][pos];
                DateTime data = Convert.ToDateTime(sData);

                row.DiaSemana = data.DayOfWeek.ToString();
                row.Mes = data.Month.ToString();
                row.Ano = data.Year.ToString();

                pos = header.ToList<string>().IndexOf("Pais");
                row.Pais = rawData.Table[i][pos];

                pos = header.ToList<string>().IndexOf("Estado");
                row.Estado = rawData.Table[i][pos];

                pos = header.ToList<string>().IndexOf("Cidade");
                row.Cidade = rawData.Table[i][pos];

                pos = header.ToList<string>().IndexOf("Distrito");
                row.Distrito = rawData.Table[i][pos];

                row.Qtd = 1;

                T_Table.Add(row);


            }

            List<T_Row> G_Table = new List<T_Row>();

            var queryGroup = T_Table.GroupBy(
                r => new { r.Valor, r.DiaSemana, r.Mes, r.Ano, r.Pais, r.Estado, r.Cidade, r.Distrito },
                (d, m) => new T_Row { Valor = d.Valor, DiaSemana = d.DiaSemana, Mes = d.Mes, Ano = d.Ano, Pais = d.Pais, Estado = d.Estado, Cidade = d.Cidade, Distrito = d.Distrito, Qtd = m.Select(r => r.Qtd).Sum() }

                );

            G_Table = queryGroup.ToList();

            foreach(T_Row row in G_Table)
            {
                string[] line = new string[9];
                line[0] = row.Valor;
                line[1] = row.DiaSemana;
                line[2] = row.Mes;
                line[3] = row.Ano;
                line[4] = row.Pais;
                line[5] = row.Estado;
                line[6] = row.Cidade;
                line[7] = row.Distrito;
                line[8] = Convert.ToString(row.Qtd);

                transformedData.Table.Add(line);
            }



            return transformedData;
        }
    }

    public class T_Row
    {
        public string Valor { get; set; }
        public string DiaSemana { get; set; }
        public string Mes { get; set; }
        public string Ano { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Distrito { get; set; }
        public int Qtd { get; set; }
    }
}
