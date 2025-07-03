using System;

namespace GuedesTime.Domain.Models.Generics
{
	public abstract class Entity
	{
		protected Entity()
		{
			Id = Guid.NewGuid();
			DataCriacao = DateTime.UtcNow;
			Ativo = true;
		}

		public Guid Id { get; set; }
		public bool? Ativo { get; set; }
		public DateTime DataCriacao { get; set; }
		public DateTime? DataAlteracao { get; set; }
	}

}