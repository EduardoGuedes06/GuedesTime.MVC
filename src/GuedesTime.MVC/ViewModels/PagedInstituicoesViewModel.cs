﻿namespace GuedesTime.MVC.ViewModels
{
    public class PagedInstituicoesViewModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public string? Search { get; set; }
        public IEnumerable<InstituicaoViewModel> Instituicoes { get; set; }
    }
}
