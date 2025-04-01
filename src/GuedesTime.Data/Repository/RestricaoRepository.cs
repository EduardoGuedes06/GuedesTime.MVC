using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuedesTime.Data.Context;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GuedesTime.Data.Repository
{
    public class RestricaoRepository : Repository<Restricao>, IRestricaoRepository
    {
        public RestricaoRepository(MeuDbContext context) : base(context) { }

    }
}