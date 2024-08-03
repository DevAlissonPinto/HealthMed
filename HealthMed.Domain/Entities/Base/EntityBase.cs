namespace HealthMed.Domain.Entities.Base;

public abstract class EntityBase : ValidatableObject
{
    public EntityBase()
    {
        this.Ativar();
        this.DataInclusao = DateTime.Now;
    }

    public int Id { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataInclusao { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public int? UsuarioInclusao { get; set; }
    public int? UsuarioAlteracao { get; set; }

    #region Metados
    public virtual void AtualizarId(int id)
    {
        if (id == 0)
            AddException(nameof(EntityBase), nameof(this.Id), "campoObrigatorio", "id");

        if (string.IsNullOrEmpty(id.ToString()))
            AddException(nameof(EntityBase), nameof(this.Id), "campoObrigatorio", "id");

        this.Id = id;
    }
    public virtual void AtualizarUsuarioInclusao(int usuarioId)
    {
        if (usuarioId <= 0)
            AddException(nameof(EntityBase), nameof(this.UsuarioInclusao), "campoObrigatorioId", "usuario");

        this.UsuarioInclusao = usuarioId;
    }
    public virtual void AtualizarUsuarioAlteracao(int? usuarioId)
    {
        if (usuarioId <= 0)
            AddException(nameof(EntityBase), nameof(this.UsuarioAlteracao), "campoObrigatorioId", "usuario");
        this.UsuarioAlteracao = usuarioId;
    }
    public virtual void AtualizarDataInclusao(DateTime? data = null) => this.DataInclusao = data.HasValue ? data.GetValueOrDefault() : DateTime.Now;
    public virtual void AtualizarDataAlteracao(DateTime? data = null) => this.DataAlteracao = data.HasValue ? data.GetValueOrDefault() : DateTime.Now;
    public virtual void Ativar() => this.Ativo = true;
    public virtual void Inativar() => this.Ativo = false;
    #endregion
}
