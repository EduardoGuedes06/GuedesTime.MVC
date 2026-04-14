using GuedesTime.Domain.Models;

namespace GuedesTime.MVC.ViewModels
{
	public class UsuarioPerfilViewModel
	{
		public string Nome { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string CpfMascarado { get; set; } = string.Empty;
		public string TipoConta { get; set; } = string.Empty;
		public string MembroDesdeTexto { get; set; } = "—";
		public string AvatarUrl { get; set; } = string.Empty;
		public List<string> AvataresDisponiveis { get; set; } = new();

		public List<InstituicaoResumoVm> Instituicoes { get; set; } = new();

		public class InstituicaoResumoVm
		{
			public Guid Id { get; set; }
			public string Nome { get; set; } = string.Empty;
			public string Avatar { get; set; } = string.Empty;
		}
	}
}

