using SistemaControleAcademico.Repository;
using SistemaControleAcademico.Service;
using SistemaControleAcademico.UI;

namespace SistemaControleAcademico;

public class Program {
    public static void Main(string[] args) {
        var (alunoUI, turmaUI) = InjetarDependencias();

        Console.WriteLine("=== SISTEMA DE CONTROLE ACADÊMICO ===");
        alunoUI.Criar(1, "Ana Silva");
        alunoUI.Criar(2, "Bruno Lima");
        alunoUI.Criar(3, "Carla Souza");
        alunoUI.Criar(4, "Daniel Costa");
        alunoUI.Criar(5, "Eva Martins");
        alunoUI.Criar(6, "Felipe Rocha");
        alunoUI.ExibirAlunos();

        turmaUI.Criar(10, "Programação Orientada a Objetos");
        turmaUI.Criar(20, "Banco de Dados");
        turmaUI.Criar(30, "Engenharia de Software");
        turmaUI.ExibirTurmas();

        Console.WriteLine("\n=== TESTE: turma sem dois alunos não pode ser aberta ===");
        turmaUI.Abrir(10);
        turmaUI.Matricular(10, 1);
        turmaUI.Abrir(10);

        Console.WriteLine("\n=== TESTE: matrícula e abertura ===");
        turmaUI.Matricular(10, 2);
        turmaUI.Matricular(10, 3);
        turmaUI.Matricular(10, 4);
        turmaUI.Matricular(10, 5);
        turmaUI.Abrir(10);
        turmaUI.ExibirPauta(10);
        alunoUI.ExibirTurmasDoAluno(1);

        Console.WriteLine("\n=== TESTE: limite máximo de cinco alunos ===");
        turmaUI.Matricular(10, 6);
        Console.WriteLine("\n=== TESTE: matrícula duplicada ===");
        turmaUI.Matricular(10, 1);

        Console.WriteLine("\n=== TESTE: exclusão permitida apenas para turma não aberta ===");
        turmaUI.Excluir(10);
        turmaUI.Excluir(20);
        turmaUI.ExibirTurmas();

        Console.WriteLine("\n=== TESTES DE VALIDAÇÃO ===");
        alunoUI.Criar(1, "Outro Aluno");
        alunoUI.Criar(0, "Nome Inválido");
        alunoUI.Criar(7, "Ab");
        turmaUI.Criar(10, "Outra Turma");
        turmaUI.Criar(-1, "Turma Inválida");
        turmaUI.Criar(40, "AB");
    }

    public static (AlunoUI, TurmaUI) InjetarDependencias() {
        AlunoRepository alunoRepository = new AlunoRepository();
        TurmaRepository turmaRepository = new TurmaRepository();
        AlunoService alunoService = new AlunoService(alunoRepository, turmaRepository);
        TurmaService turmaService = new TurmaService(turmaRepository, alunoRepository);
        AlunoUI alunoUI = new AlunoUI(alunoService);
        TurmaUI turmaUI = new TurmaUI(turmaService);
        return (alunoUI, turmaUI);
    }
}