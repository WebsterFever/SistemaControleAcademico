using SistemaControleAcademico.Service;

namespace SistemaControleAcademico.UI;

public class TurmaUI {
    private readonly TurmaService turmaService;
    public TurmaUI(TurmaService turmaService) => this.turmaService = turmaService;

    public void Criar(string nome) => Executar(() => {
        int id = turmaService.Criar(nome);
        Console.WriteLine($"Turma cadastrada com sucesso! ID gerado automaticamente: {id}");
    });

    public void Criar(int id, string nome) => Executar(() => {
        turmaService.Criar(id, nome);
        Console.WriteLine($"Turma {id} cadastrada com sucesso.");
    });

    public void ExibirTurmas() => Executar(() => {
        Console.WriteLine("\nTURMAS:");
        foreach (var turma in turmaService.ConsultarTurmas()) Console.WriteLine(turma);
    });

    public void Matricular(int idTurma, int idAluno) => Executar(() => {
        turmaService.Matricular(idTurma, idAluno);
        Console.WriteLine($"Aluno {idAluno} matriculado na turma {idTurma}.");
    });

    public void Abrir(int idTurma) => Executar(() => {
        turmaService.Abrir(idTurma);
        Console.WriteLine($"Turma {idTurma} aberta com sucesso.");
    });

    public void Fechar(int idTurma) => Executar(() => {
        turmaService.Fechar(idTurma);
        Console.WriteLine($"Turma {idTurma} fechada com sucesso.");
    });

    public void ExibirPauta(int idTurma) => Executar(() => {
        Console.WriteLine($"\nPAUTA DA TURMA {idTurma}:");
        var alunos = turmaService.ConsultarPauta(idTurma);
        if (alunos.Count == 0) Console.WriteLine("Nenhum aluno matriculado.");
        foreach (var aluno in alunos) Console.WriteLine(aluno);
    });

    public void Excluir(int idTurma) => Executar(() => {
        turmaService.Excluir(idTurma);
        Console.WriteLine($"Turma {idTurma} excluída com sucesso.");
    });

    private static void Executar(Action acao) {
        try { acao(); }
        catch (Exception ex) { Console.WriteLine($"Erro: {ex.Message}"); }
    }
}
