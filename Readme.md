# Current Quoter

Projeto tem objetivo de obter as cotações de uma moeda nos últimos 30 dias, realizar o calculo de diferença de percentuais entre Dx e Dy, e Dx e D1(D = dia) e gravar esses registros numa base de dados.

## Por que Redis?

Foi utilizado a base de dados redis para esse projeto pela alta performance, dado que é um banco não relacional e méritos de velocidade da própria tecnologia em si. Também pela possível forma de armazenamento mais eficiente das informações, por se tratar de um banco chave-valor, foi possível armazenar uma lista de cotações como valor, e moeda, mês e ano como chave. Se tornando um processo muito mais simples principalmente para inserções de dados, caso comparado com um banco relacional onde cada uma das cotações resultariam em um insert diferente no banco.
