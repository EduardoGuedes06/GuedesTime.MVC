using GuedesTime.Data.Context;
using GuedesTime.Data.Repository.Utils;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Data.Repository
{
    public class PlanejamentoDeAulaRepository : Repository<PlanejamentoDeAula>, IPlanejamentoDeAulaRepository
    {
        public PlanejamentoDeAulaRepository(MeuDbContext context) : base(context) { }

    }
}