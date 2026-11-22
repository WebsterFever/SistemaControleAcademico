using SistemaControleAcademico.Models;

namespace SistemaControleAcademico.Repository;

public class AlunoRepository {
    private readonly List<Aluno> alunos = new();

    public void Criar(Aluno aluno) => alunos.Add(aluno);
    public Aluno? ConsultarAluno(int id) => alunos.FirstOrDefault(a => a.IdAluno == id);
    public List<Aluno> ConsultarAlunos() => alunos.ToList();
}