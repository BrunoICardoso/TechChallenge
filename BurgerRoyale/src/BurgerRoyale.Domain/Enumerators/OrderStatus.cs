using System.ComponentModel;

namespace BurgerRoyale.Domain.Enumerators;

public enum OrderStatus
{
    [Description("Recebido")]
    Recebido,

    [Description("Pagamento aprovado")]
    PagamentoAprovado,

    [Description("Em preparação")]
    EmPreparacao,

    [Description("Pronto")]
    Pronto,

    [Description("Finalizado")]
    Finalizado
}