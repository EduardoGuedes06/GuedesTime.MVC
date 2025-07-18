﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace GuedesTime.MVC.Configurations
{
    public static class MvcConfig
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {

            services.AddControllersWithViews(o =>
            {
                o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "Todos os campos precisa ser preenchido.");
                o.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Todos os campos precisa ser preenchido.");
                o.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisição não esteja vazio.");
                o.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser numérico");
                o.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "O campo deve ser numérico.");
                o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "Todos os campos precisa ser preenchido.");

                o.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddRazorPages();

			return services;
        }
    }
}