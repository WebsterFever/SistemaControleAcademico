using SistemaControleAcademico.Models;

namespace SistemaControleAcademico.Repository;

public class TurmaRepository {
    private readonly List<Turma> turmas = new();

    public void Criar(Turma turma) => turmas.Add(turma);
    public Turma? ConsultarTurma(int id) => turmas.FirstOrDefault(t => t.IdTurma == id);
    public List<Turma> ConsultarTurmas() => turmas.ToList();
    public List<Turma> ConsultarTurmasDoAluno(int idAluno) =>
        turmas.Where(t => t.Alunos.Any(a => a.IdAluno == idAluno)).ToList();
    public void Excluir(Turma turma) => turmas.Remove(turma);
}