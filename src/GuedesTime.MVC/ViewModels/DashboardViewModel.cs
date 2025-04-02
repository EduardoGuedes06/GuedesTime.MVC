using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<InstituicaoViewModel> Instituicoes { get; set; }
    }
}