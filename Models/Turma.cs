namespace SistemaControleAcademico.Models;

public class Turma {
    private readonly List<Aluno> alunos = new();
    public int IdTurma { get; private set; }
    public string Nome { get; private set; }
    public IReadOnlyList<Aluno> Alunos => alunos.AsReadOnly();
    public bool Aberta { get; private set; } = false;

    public Turma(int idTurma, string nome) {
        IdTurma = idTurma;
        Nome = nome;
    }

    public void Matricular(Aluno aluno) {
        if (alunos.Any(a => a.IdAluno == aluno.IdAluno))
            throw new InvalidOperationException($"Aluno {aluno.IdAluno} já está matriculado na turma {IdTurma}.");
        if (alunos.Count >= 5)
            throw new InvalidOperationException("Uma turma pode ter no máximo cinco alunos.");
        alunos.Add(aluno);
    }

    public void Abrir() {
        if (Aberta) throw new InvalidOperationException("A turma já está aberta.");
        if (alunos.Count < 2)
            throw new InvalidOperationException("Uma turma precisa ter pelo menos dois alunos para ser aberta.");
        Aberta = true;
    }

    public override string ToString() {
        return $"Turma: {IdTurma} - {Nome} | Aberta: {Aberta} | Alunos: {alunos.Count}";
    }
}