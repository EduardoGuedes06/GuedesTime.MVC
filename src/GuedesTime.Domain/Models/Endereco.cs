﻿using GuedesTime.Domain.Models.Generics;
using System;

namespace GuedesTime.Domain.Models
{
    public class Endereco : Entity
    {
        public Guid InstituicaoId { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string? Complemento { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        /* EF Relation */
        public Instituicao Instituicao { get; set; }
    }
}