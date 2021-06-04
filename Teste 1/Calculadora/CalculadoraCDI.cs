using System;
using Calculadora.Model;
using System.Collections.Generic;

namespace Calculadora
{
    public class CalculadoraCDI
    {
        private Dictionary<DateTime,double> CotacoesCDI {get;set;}
        public CalculadoraCDI(Dictionary<DateTime,double> cotacoesCDI) {
            CotacoesCDI = cotacoesCDI;
        }

        public List<PosicaoCDI> CalcularPosicoes(List<OperacaoCDI> operacoes, DateTime dataBase) {
            List<PosicaoCDI> posicao = new List<PosicaoCDI>();

            foreach(OperacaoCDI operacao in operacoes) {
                try
                {
                    PosicaoCDI posicaoCalculada = CalcularPosicaoCDI(operacao, dataBase);

                    if (posicaoCalculada != null)
                        posicao.Add(posicaoCalculada);
                }
                catch { 
                    continue;
                }               
            }

            return posicao;
        }
        
        private PosicaoCDI CalcularPosicaoCDI(OperacaoCDI operacao, DateTime dataBase) {
            if(DateTime.Compare(operacao.DataInicio,dataBase) > 0 || DateTime.Compare(dataBase,operacao.DataFim) > 0) 
                return null;

            else {
                operacao.ValorCorrigido = Math.Round(operacao.ValorInvestido * CalculaFator(dataBase,operacao.DataInicio,operacao.PorcentagemCDI,1),2);               
                
                return new PosicaoCDI{Operacao = operacao, DataBase=dataBase ,IdPosicao = Guid.NewGuid() };
            }
        }

        private double CalculaFator(DateTime dataBase, DateTime dataCalculo, double percentCDI, double fatorAnterior) {
            try
            {
                if (!CotacoesCDI.ContainsKey(dataCalculo))
                    if (dataCalculo >= dataBase)
                        return fatorAnterior;
                    else
                        return CalculaFator(dataBase, dataCalculo.AddDays(1), percentCDI, fatorAnterior);
                else
                {
                    var cotacaoDia = CotacoesCDI[dataCalculo] / 100;
                    double fatorDiario = Math.Round(Math.Pow(1 + cotacaoDia, (double)1 / 252) - 1, 8);
                    double fator = Math.Round((fatorDiario * percentCDI + 1) * fatorAnterior, 8);

                    if (dataCalculo >= dataBase)
                        return fator;
                    else
                        return CalculaFator(dataBase, dataCalculo.AddDays(1), percentCDI, fator);
                }
            }
            catch {
                return 0;
            }
        }
    }
}
