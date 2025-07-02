namespace AppSemTemplate.Services
{
    public class Operacao : IOperacao
    {
        public Guid OperacaoId { get; private set; }

        public Operacao()
        {
            OperacaoId = Guid.NewGuid();
        }
    }

    public interface IOperacao
    {
        Guid OperacaoId { get; }
    }
}