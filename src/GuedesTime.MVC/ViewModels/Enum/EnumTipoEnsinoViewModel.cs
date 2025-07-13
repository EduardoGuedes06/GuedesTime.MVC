using System.ComponentModel.DataAnnotations;

namespace GuedesTime.MVC.ViewModels.Enum
{
	public enum EnumTipoEnsinoViewModel
	{
		[Display(Name = "Todos")]
		Todos = 0,

		[Display(Name = "Educação Infantil")]
		Ensino_Infantil = 1,

		[Display(Name = "Ensino Fundamental")]
		Ensino_Fundamental = 2,

		[Display(Name = "Ensino Médio")]
		Ensino_Medio = 3
	}

}
