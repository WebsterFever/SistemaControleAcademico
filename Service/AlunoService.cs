using SistemaControleAcademico.Models;
using SistemaControleAcademico.Repository;

namespace SistemaControleAcademico.Service;

public class AlunoService {
    private readonly AlunoRepository alunoRepository;
    private readonly TurmaRepository turmaRepository;
    private int proximoId = 1;

    public AlunoService(AlunoRepository alunoRepository, TurmaRepository turmaRepository) {
        this.alunoRepository = alunoRepository;
        this.turmaRepository = turmaRepository;
    }

    public int Criar(string nome) {
        int id = proximoId;
        Criar(id, nome);
        return id;
    }

    public void Criar(int id, string nome) {
        if (id <= 0) throw new ArgumentException("O ID do aluno deve ser maior que zero.");
        if (string.IsNullOrWhiteSpace(nome) || nome.Trim().Length <= 2)
            throw new ArgumentException("O nome do aluno deve ter mais de dois caracteres.");
        if (alunoRepository.ConsultarAluno(id) != null)
            throw new InvalidOperationException($"Já existe um aluno com ID {id}.");
        alunoRepository.Criar(new Aluno(id, nome.Trim()));
        if (id >= proximoId) proximoId = checked(id + 1);
    }

    public Aluno ConsultarAluno(int id) =>
        alunoRepository.ConsultarAluno(id) ?? throw new KeyNotFoundException($"Aluno {id} não encontrado.");

    public List<Aluno> ConsultarAlunos() => alunoRepository.ConsultarAlunos();
    public List<Turma> ConsultarTurmasDoAluno(int idAluno) {
        ConsultarAluno(idAluno);
        return turmaRepository.ConsultarTurmasDoAluno(idAluno);
    }
}
