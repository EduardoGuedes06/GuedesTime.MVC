using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models.Generics
{
	public abstract class EntityExpanded : Entity
	{
		protected EntityExpanded()
		{
		}

		public string Codigo { get; set; }
	}
}
