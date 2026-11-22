namespace SistemaControleAcademico.Models;

public class Aluno {
    public int IdAluno { get; private set; }
    public string Nome { get; private set; }

    public Aluno(int idAluno, string nome) {
        IdAluno = idAluno;
        Nome = nome;
    }

    public override string ToString() {
        return $"Aluno: {IdAluno} - {Nome}";
    }
}