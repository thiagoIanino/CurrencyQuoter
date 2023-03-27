# Current Quoter

Projeto tem objetivo de obter as cota��es de uma moeda nos �ltimos 30 dias, realizar o calculo de diferen�a de percentuais entre Dx e Dy, e Dx e D1(D = dia) e gravar esses registros numa base de dados.

## Por que Redis?

Foi utilizado a base de dados redis para esse projeto pela alta performance, dado que � um banco n�o relacional e m�ritos de velocidade da pr�pria tecnologia em si. Tamb�m pela poss�vel forma de armazenamento mais eficiente das informa��es, por se tratar de um banco chave-valor, foi poss�vel armazenar uma lista de cota��es como valor, e moeda, m�s e ano como chave. Se tornando um processo muito mais simples principalmente para inser��es de dados, caso comparado com um banco relacional onde cada uma das cota��es resultariam em um insert diferente no banco.

## Como rodar o Redis no docker?

Para rodar o redis no projeto, basta rodar o seguinte comando no terminal docker run -d --name redis-stack -p 6379:6379 -p 8001:8001 redis/redis-stack:latest.
Caso voc� n�o tenha o Docker configurado no computador, siga esse tutorial para instalar: https://www.bing.com/search?q=docker+balta&cvid=a83f508946844b4f96736c61b46d1063&aqs=edge.0.69i59l2j69i57j0l6.1847j0j9&FORM=ANAB01&PC=U531
