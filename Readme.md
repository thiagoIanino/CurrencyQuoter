# Current Quoter

Projeto tem objetivo de obter as cota��es de uma moeda nos �ltimos 30 dias, realizar o calculo de diferen�a de percentuais entre Dx e Dy, e Dx e D1(D = dia) e gravar esses registros numa base de dados.

## Por que Redis?

Foi utilizado a base de dados redis para esse projeto pela alta performance, dado que � um banco n�o relacional e m�ritos de velocidade da pr�pria tecnologia em si. Tamb�m pela poss�vel forma de armazenamento mais eficiente das informa��es, por se tratar de um banco chave-valor, foi poss�vel armazenar uma lista de cota��es como valor, e moeda, m�s e ano como chave. Se tornando um processo muito mais simples principalmente para inser��es de dados, caso comparado com um banco relacional onde cada uma das cota��es resultariam em um insert diferente no banco.
