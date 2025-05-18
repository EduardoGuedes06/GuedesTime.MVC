using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuedesTime.Data.Context;
using GuedesTime.Data.Repository.Utils;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GuedesTime.Data.Repository
{
    public class TarefasRepository : Repository<Tarefas>, ITarefasRepository
    {
        public TarefasRepository(MeuDbContext context) : base(context) { }


    }
}