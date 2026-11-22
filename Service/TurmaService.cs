using SistemaControleAcademico.Models;
using SistemaControleAcademico.Repository;

namespace SistemaControleAcademico.Service;

public class TurmaService {
    private readonly TurmaRepository turmaRepository;
    private readonly AlunoRepository alunoRepository;
    private int proximoId = 1;

    public TurmaService(TurmaRepository turmaRepository, AlunoRepository alunoRepository) {
        this.turmaRepository = turmaRepository;
        this.alunoRepository = alunoRepository;
    }

    public int Criar(string nome) {
        int id = proximoId;
        Criar(id, nome);
        proximoId++;
        return id;
    }

    public void Criar(int id, string nome) {
        if (id <= 0) throw new ArgumentException("O ID da turma deve ser maior que zero.");
        if (string.IsNullOrWhiteSpace(nome) || nome.Trim().Length <= 2)
            throw new ArgumentException("O nome da turma deve ter mais de dois caracteres.");
        if (turmaRepository.ConsultarTurma(id) != null)
            throw new InvalidOperationException($"Já existe uma turma com ID {id}.");
        turmaRepository.Criar(new Turma(id, nome.Trim()));
        if (id >= proximoId) proximoId = checked(id + 1);
    }

    public Turma ConsultarTurma(int id) =>
        turmaRepository.ConsultarTurma(id) ?? throw new KeyNotFoundException($"Turma {id} não encontrada.");
    public List<Turma> ConsultarTurmas() => turmaRepository.ConsultarTurmas();

    public void Matricular(int idTurma, int idAluno) {
        var turma = ConsultarTurma(idTurma);
        var aluno = alunoRepository.ConsultarAluno(idAluno)
                    ?? throw new KeyNotFoundException($"Aluno {idAluno} não encontrado.");
        turma.Matricular(aluno);
    }

    public void Abrir(int idTurma) => ConsultarTurma(idTurma).Abrir();

    public List<Aluno> ConsultarPauta(int idTurma) => ConsultarTurma(idTurma).Alunos.ToList();

    public void Excluir(int idTurma) {
        var turma = ConsultarTurma(idTurma);
        if (turma.Aberta) throw new InvalidOperationException("Não é possível excluir uma turma aberta.");
        turmaRepository.Excluir(turma);
    }
}
