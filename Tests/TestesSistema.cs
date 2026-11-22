using SistemaControleAcademico.Repository;
using SistemaControleAcademico.Service;

namespace SistemaControleAcademico.Tests;

public static class TestesSistema
{
    public static void Executar()
    {
        int aprovados = 0, reprovados = 0;
        void Testar(string descricao, Action teste)
        {
            try { teste(); Console.WriteLine($"[OK] {descricao}"); aprovados++; }
            catch (Exception ex) { Console.WriteLine($"[FALHOU] {descricao}: {ex.Message}"); reprovados++; }
        }
        static void Verificar(bool condicao, string mensagem)
        {
            if (!condicao) throw new Exception(mensagem);
        }
        static void EsperarErro<T>(Action acao) where T : Exception
        {
            try { acao(); }
            catch (T) { return; }
            throw new Exception($"Era esperada a exceção {typeof(T).Name}.");
        }

        var alunosRepo = new AlunoRepository();
        var turmasRepo = new TurmaRepository();
        var alunos = new AlunoService(alunosRepo, turmasRepo);
        var turmas = new TurmaService(turmasRepo, alunosRepo);

        Console.WriteLine("\n=== TESTES DO SISTEMA DE CONTROLE ACADÊMICO ===");
        Testar("Cadastrar e listar alunos", () =>
        {
            Verificar(alunos.Criar("Ana") == 1, "ID automático incorreto.");
            Verificar(alunos.Criar("Bruno") == 2, "ID automático incorreto.");
            Verificar(alunos.ConsultarAlunos().Count == 2, "Quantidade incorreta.");
        });
        Testar("Rejeitar ID e nome inválidos de aluno", () =>
        {
            EsperarErro<ArgumentException>(() => alunos.Criar(0, "Carlos"));
            EsperarErro<ArgumentException>(() => alunos.Criar(8, "Al"));
            EsperarErro<InvalidOperationException>(() => alunos.Criar(1, "Outro"));
        });
        Testar("Criar e listar turmas", () =>
        {
            Verificar(turmas.Criar("Csharp") == 1, "ID automático incorreto.");
            Verificar(turmas.ConsultarTurmas().Count == 1, "Turma não listada.");
        });
        Testar("Rejeitar turma com ID ou nome inválido", () =>
        {
            EsperarErro<ArgumentException>(() => turmas.Criar(0, "Java"));
            EsperarErro<ArgumentException>(() => turmas.Criar(9, "JS"));
            EsperarErro<InvalidOperationException>(() => turmas.Criar(1, "Java"));
        });
        Testar("Não abrir turma com menos de 2 alunos", () =>
            EsperarErro<InvalidOperationException>(() => turmas.Abrir(1)));
        Testar("Matricular alunos e exibir pauta", () =>
        {
            turmas.Matricular(1, 1);
            turmas.Matricular(1, 2);
            Verificar(turmas.ConsultarPauta(1).Count == 2, "Pauta incorreta.");
        });
        Testar("Impedir matrícula duplicada", () =>
            EsperarErro<InvalidOperationException>(() => turmas.Matricular(1, 1)));
        Testar("Exibir turmas de um aluno", () =>
            Verificar(alunos.ConsultarTurmasDoAluno(1).Count == 1, "Turma do aluno não encontrada."));
        Testar("Abrir turma com 2 alunos", () =>
        {
            turmas.Abrir(1);
            Verificar(turmas.ConsultarTurma(1).Aberta, "Turma não abriu.");
        });
        Testar("Impedir exclusão de turma aberta", () =>
            EsperarErro<InvalidOperationException>(() => turmas.Excluir(1)));
        Testar("Respeitar limite de 5 alunos", () =>
        {
            for (int i = 3; i <= 6; i++)
            {
                alunos.Criar($"Aluno {i}");
                if (i <= 5) turmas.Matricular(1, i);
            }
            Verificar(turmas.ConsultarPauta(1).Count == 5, "Quantidade incorreta.");
            EsperarErro<InvalidOperationException>(() => turmas.Matricular(1, 6));
        });
        Testar("Fechar turma sem perder matrículas", () =>
        {
            turmas.Fechar(1);
            Verificar(!turmas.ConsultarTurma(1).Aberta, "Turma continua aberta.");
            Verificar(turmas.ConsultarPauta(1).Count == 5, "Matrículas perdidas.");
        });
        Testar("Excluir turma não aberta", () =>
        {
            turmas.Excluir(1);
            Verificar(turmas.ConsultarTurmas().Count == 0, "Turma não excluída.");
        });
        Console.WriteLine($"\nResultado: {aprovados} aprovado(s), {reprovados} reprovado(s).");
        Console.WriteLine("Os testes usam dados temporários e não modificam os cadastros do menu.");
    }
}
