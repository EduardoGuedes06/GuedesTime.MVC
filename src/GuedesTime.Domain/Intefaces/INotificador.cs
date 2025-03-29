
using GuedesTime.Domain.Notificacoes;
using System.Collections.Generic;

namespace GuedesTime.Domain.Intefaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}