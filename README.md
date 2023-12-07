## Teste técnico
Esse repositório contém o teste técnico para a vaga de Desenvolvedor .NET Sênior na empresa [AMCom](https://amcom.com.br/).
O teste consiste em 5 questões que envolvem o conhecimentos de orientação a objetos, git, algoritmos e estrutura de dados, testes unitários e padrões arquiteturais, além de hands on com C#.

As respostas para os exercícios se encontram nas suas respectivas pastas.

## Recursos utilizadas
- .NET 6
- CQRS
- MediatR
- Swagger
- SQLite
- xUnit
- NSubstitute

## Ponderações
1) Na questão 2, o resultado esperado informado na questão está incorreto. O resultado abaixo foi obtido tanto através da implementação do código quanto validação manual do retorno da API via navegador.
    - Team Paris Saint-Germain scored 87 goals in 2013
    - Team Chelsea Scored 61 goals in 2014.
    
    
2) Como sugestão para utilização de uma camada de idempotência, o Redis seria uma boa solução pelos seguintes motivos:
    - Rápido acesso: Redis é um banco de dados em memória frequentemente utilizado para cache, por isso o acesso a dados é extremamente rápido.
    - TTL: O Redis possui a função de TTL (Time to Live), ou seja, é possível armazenar um registro no Redis com um determinado tempo de vida. Após esse tempo, o registro será removido automaticamente de maneira gerenciada pelo próprio cluster, sem necessidade de implementação via código para remoção do registro.]
    - Quando levamos para a casa dos milhões de requisições, economizar 20ms de resposta por request irá fazer total diferença.

