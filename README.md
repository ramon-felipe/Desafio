# Desafio

### Teste 1 - Calculadora de CDI
Existe uma biblioteca responsável por calcular diariamente o juros dos investimentos dos clientes baseados em CDI (Certificado de Depósito Interbancário). Esses cálculos geram posições que são disponibilizados para outros sistemas que precisam da informação.

Cada operação na carteira tem os atributos:
* Contrato - Número do contrato com o cliente
* Data Início - Data que o cliente fez o investimento.
* Data Fim - Dataque o cliente receberá o dinheiro corrigodo
* % do CDI: Percentual do CDI que será usado para calcular o valor final da operação
* Valor Investido: O valor que o cliente investiu e será valorizado até o a data final do contrato.
* Valor Corrigido: O valor Investido mais o juros até o dia do cálculo

Cada operação deverá obrigatóriamente gerar uma posição caso o contrato esteja vigente, ou seja, o dia do cálculo estiver entre o início e fim do contrato.

A área de negócio verificou na conciliação que algumas operações não estão gerando posição. Você precisa entender o que está acontecendo e corrigir o mais rápido possível pois além das outras áreas estamos afetando os clientes.

### Teste 2 - API de Feriados
Precisamos que você crie uma API REST que permita a consulta (todos ou passando o mês e ano), a adição, a remoção e a atualização de feriados. A API deve ser construida em .Net Core >= 3.1. Durante o funcionamento da API você pode guardar os dados em memória, SQLite ou o que achar mais apropriado. 
