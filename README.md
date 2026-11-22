# Sistema de Controle Acadêmico (SCA)

Projeto acadêmico em **C# (.NET 8)**, baseado na atividade de modelagem e implementação das classes **Aluno** e **Turma**.

## Estrutura

- `Models`: classes de negócio com construtores, propriedades, operações e `ToString()`.
- `Repository`: armazenamento em listas em memória e consultas.
- `Service`: validações e regras de negócio.
- `UI`: operações de console e tratamento das exceções.
- `Program.cs`: injeção manual de dependências e cenários de testes com dados mockados.
- `Docs/Exercicios-1-3.md`: caso de uso, diagrama de sequência e multiplicidades.

A estrutura segue o padrão em camadas do exemplo JetPizza, sem banco de dados ou framework de DI.

## Regras do exercício

- IDs de alunos e turmas maiores que zero e únicos.
- Nomes com mais de dois caracteres.
- Cada turma comporta no máximo cinco alunos.
- Para abrir uma turma, são necessários pelo menos dois alunos.
- Somente turmas **não abertas** podem ser excluídas.
- As exceções são tratadas na camada `UI`.

## Executar no Visual Studio

1. Clone o repositório e abra `SistemaControleAcademico.csproj` no Visual Studio 2022 (com SDK .NET 8).
2. Defina o projeto como projeto de inicialização, se necessário.
3. Pressione **Ctrl + F5** para executar os cenários no console.

Ou pelo terminal: `dotnet run`.

## Exercícios atendidos

1. Caso de uso **Lançar Notas**, somente fluxo principal.
2. Diagrama de sequência sem fluxos alternativos ou laços.
3. Multiplicidades da associação Aluno–Turma.
4. Aplicação C# em camadas com injeção de dependências.
5. Programa principal demonstrando os métodos e verificações de erros.

> A implementação C# é limitada às classes Aluno e Turma solicitadas no exercício prático. Os diagramas de Lançar Notas são documentação conceitual do sistema maior.
