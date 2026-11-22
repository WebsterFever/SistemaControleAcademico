using SistemaControleAcademico.Repository;
using SistemaControleAcademico.Service;
using SistemaControleAcademico.UI;

namespace SistemaControleAcademico;

public class Program
{
    public static void Main(string[] args)
    {
        var (alunoUI, turmaUI) = InjetarDependencias();
        while (true)
        {
            Console.WriteLine("\n=== SISTEMA DE CONTROLE ACADÊMICO ===");
            Console.WriteLine("1 - Cadastrar aluno");
            Console.WriteLine("2 - Listar alunos");
            Console.WriteLine("3 - Criar turma");
            Console.WriteLine("4 - Listar turmas");
            Console.WriteLine("5 - Matricular aluno em turma");
            Console.WriteLine("6 - Abrir turma");
            Console.WriteLine("7 - Exibir pauta de uma turma");
            Console.WriteLine("8 - Exibir turmas de um aluno");
            Console.WriteLine("9 - Excluir turma");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            string? opcao = Console.ReadLine();
            if (opcao is null || opcao == "0")
            {
                Console.WriteLine("Encerrando o sistema.");
                return;
            }

            switch (opcao)
            {
                case "1":
                    alunoUI.Criar(LerInteiro("ID do aluno: "), LerTexto("Nome do aluno: "));
                    break;
                case "2":
                    alunoUI.ExibirAlunos();
                    break;
                case "3":
                    turmaUI.Criar(LerInteiro("ID da turma: "), LerTexto("Nome da turma: "));
                    break;
                case "4":
                    turmaUI.ExibirTurmas();
                    break;
                case "5":
                    turmaUI.Matricular(LerInteiro("ID da turma: "), LerInteiro("ID do aluno: "));
                    break;
                case "6":
                    turmaUI.Abrir(LerInteiro("ID da turma: "));
                    break;
                case "7":
                    turmaUI.ExibirPauta(LerInteiro("ID da turma: "));
                    break;
                case "8":
                    alunoUI.ExibirTurmasDoAluno(LerInteiro("ID do aluno: "));
                    break;
                case "9":
                    turmaUI.Excluir(LerInteiro("ID da turma: "));
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    private static int LerInteiro(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string? entrada = Console.ReadLine();
            if (int.TryParse(entrada, out int valor))
                return valor;
            Console.WriteLine("Digite um número inteiro válido.");
        }
    }

    private static string LerTexto(string mensagem)
    {
        Console.Write(mensagem);
        return Console.ReadLine() ?? string.Empty;
    }

    public static (AlunoUI, TurmaUI) InjetarDependencias()
    {
        AlunoRepository alunoRepository = new AlunoRepository();
        TurmaRepository turmaRepository = new TurmaRepository();
        AlunoService alunoService = new AlunoService(alunoRepository, turmaRepository);
        TurmaService turmaService = new TurmaService(turmaRepository, alunoRepository);
        return (new AlunoUI(alunoService), new TurmaUI(turmaService));
    }
}
