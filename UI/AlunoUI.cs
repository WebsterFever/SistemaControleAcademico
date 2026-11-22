using SistemaControleAcademico.Service;

namespace SistemaControleAcademico.UI;

public class AlunoUI {
    private readonly AlunoService alunoService;
    public AlunoUI(AlunoService alunoService) => this.alunoService = alunoService;

    public void Criar(string nome) => Executar(() => {
        int id = alunoService.Criar(nome);
        Console.WriteLine($"Aluno cadastrado com sucesso! ID gerado automaticamente: {id}");
    });

    public void Criar(int id, string nome) => Executar(() => {
        alunoService.Criar(id, nome);
        Console.WriteLine($"Aluno {id} cadastrado com sucesso.");
    });

    public void ExibirAlunos() => Executar(() => {
        Console.WriteLine("\nALUNOS:");
        foreach (var aluno in alunoService.ConsultarAlunos()) Console.WriteLine(aluno);
    });

    public void ExibirTurmasDoAluno(int idAluno) => Executar(() => {
        Console.WriteLine($"\nTURMAS DO ALUNO {idAluno}:");
        var turmas = alunoService.ConsultarTurmasDoAluno(idAluno);
        if (turmas.Count == 0) Console.WriteLine("Nenhuma turma encontrada.");
        foreach (var turma in turmas) Console.WriteLine(turma);
    });

    private static void Executar(Action acao) {
        try { acao(); }
        catch (Exception ex) { Console.WriteLine($"Erro: {ex.Message}"); }
    }
}
