﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GuedesTime.MVC.Extensions
{
    public static class RazorExtensions
    {
        public static string FormataCPF(this RazorPage page,string cpf)
        {
            if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
                return cpf; // Retornar o valor original se não for válido ou já estiver formatado

            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }

        public static string FormataDocumento(this RazorPage page, int tipoPessoa, string documento)
        {
            return tipoPessoa == 1 ? Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string FormataCNPJ(this RazorPage page,string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj) || cnpj.Length != 14)
                return cnpj; // Retornar o valor original se não for válido ou já estiver formatado

            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }
        public static string MarcarOpcao(this RazorPage page, int tipoPessoa, int valor)
        {
            return tipoPessoa == valor ? "checked" : "";
        }
		public static string EnumDisplayName(this RazorPage page, Enum value)
		{
			if (value == null) return string.Empty;

			var field = value.GetType().GetField(value.ToString());

			var attribute = field?.GetCustomAttribute<DisplayAttribute>();

			return attribute?.Name ?? value.ToString();
		}

	}
}