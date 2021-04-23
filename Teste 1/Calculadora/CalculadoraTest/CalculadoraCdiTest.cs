using System;
using Xunit;
using Calculadora.Model;
using Calculadora;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
namespace CalculadoraTest
{
    public class CalculadoraCdiTest
    {
        CalculadoraCDI Calculadora {get;set;}
        public CalculadoraCdiTest() {
            Dictionary<DateTime,double> cotacoesCDI = new Dictionary<DateTime, double>();
            using(StreamReader sr = new StreamReader(@"..\..\..\..\CotacoesCDI.csv")){
                while (sr.Peek() >= 0)
                {
                    string[] dados = sr.ReadLine().Split(";");
                    string[] data = dados[0].Split("/");
                    cotacoesCDI.Add(DateTime.ParseExact(dados[0], "dd/MM/yyyy", CultureInfo.InvariantCulture), Convert.ToDouble(dados[1]));
                }
            }
            Calculadora = new CalculadoraCDI(cotacoesCDI);
        }
        [Fact]
        public void Test1()
        {
            List<OperacaoCDI> operacoes = new List<OperacaoCDI>();
            using (StreamReader sr = new StreamReader(@"..\..\..\..\OperacoesCarteira.csv"))
            {
                while (sr.Peek() >= 0)
                {
                    string[] dados = sr.ReadLine().Split(";");
                    operacoes.Add(new OperacaoCDI
                    {
                        Contrato = Convert.ToInt32(dados[0]),
                        DataInicio = DateTime.ParseExact(dados[1], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        DataFim = DateTime.ParseExact(dados[2], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        PorcentagemCDI = Convert.ToDouble(dados[3]) / 100,
                        ValorInvestido = Convert.ToDouble(dados[4])
                    });
                }
            }
            
            List<PosicaoCDI> posicaoEsperada = new List<PosicaoCDI>();
            using (StreamReader sr = new StreamReader(@"..\..\..\..\PosicoesEsperadas.csv"))
            {
                while (sr.Peek() >= 0)
                {
                    string[] dados = sr.ReadLine().Split(";");
                    posicaoEsperada.Add(new PosicaoCDI
                    {
                        DataBase = DateTime.ParseExact(dados[0], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        IdPosicao = Convert.ToInt32(dados[1]),
                        Operacao = new OperacaoCDI
                        {
                            Contrato = Convert.ToInt32(dados[2]),
                            DataInicio = DateTime.ParseExact(dados[3], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            DataFim = DateTime.ParseExact(dados[4], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            PorcentagemCDI = Convert.ToDouble(dados[5]),
                            ValorInvestido = Convert.ToDouble(dados[6]),
                            ValorCorrigido = Convert.ToDouble(dados[7])
                        }
                    });
                }
            }

            List<PosicaoCDI> posicoes = Calculadora.CalcularPosicoes(operacoes, DateTime.ParseExact("18/01/2016", "dd/MM/yyyy", CultureInfo.InvariantCulture));

            for(int i = 0; i<posicaoEsperada.Count; i++)
            {
                Assert.Equal(posicaoEsperada[i].DataBase, posicoes[i].DataBase);
                Assert.Equal(posicaoEsperada[i].Operacao.Contrato, posicoes[i].Operacao.Contrato);
                Assert.Equal(posicaoEsperada[i].Operacao.DataFim, posicoes[i].Operacao.DataFim);
                Assert.Equal(posicaoEsperada[i].Operacao.DataInicio, posicoes[i].Operacao.DataInicio);
                Assert.Equal(posicaoEsperada[i].Operacao.PorcentagemCDI, posicoes[i].Operacao.PorcentagemCDI);
                Assert.Equal(posicaoEsperada[i].Operacao.ValorCorrigido, posicoes[i].Operacao.ValorCorrigido);
                Assert.Equal(posicaoEsperada[i].Operacao.ValorInvestido, posicoes[i].Operacao.ValorInvestido);
            }
        }
    }
}
