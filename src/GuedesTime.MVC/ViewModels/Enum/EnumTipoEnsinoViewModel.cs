using System.ComponentModel.DataAnnotations;

namespace GuedesTime.MVC.ViewModels.Enum
{
	public enum EnumTipoEnsinoViewModel
	{
		[Display(Name = "Todos")]
		Todos = 0,

		[Display(Name = "Educação Infantil")]
		Ensino_Infantil = 1,

		[Display(Name = "Ensino Fundamental I")]
		Ensino_Fundamental_I = 2,

		[Display(Name = "Ensino Fundamental II")]
		Ensino_Fundamental_II = 3,

		[Display(Name = "Ensino Médio")]
		Ensino_Medio = 4
	}

}
