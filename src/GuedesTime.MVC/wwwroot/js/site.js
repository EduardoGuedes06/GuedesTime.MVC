function SetModal() {

    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $('#myModalContent').load(this.href,
                        function () {
                            $('#myModal').modal({
                                    keyboard: true
                                },
                                'show');
                            bindForm(this);
                        });
                    return false;
                });
        });
    });
}

function toggleSection(sectionId, button) {
    var container = document.getElementById(sectionId);
    var icon = button.querySelector("i");

    if (container.style.display === "none" || container.style.display === "") {
        container.style.display = "block";
        icon.classList.remove("fa-chevron-down");
        icon.classList.add("fa-chevron-up");
    } else {
        container.style.display = "none";
        icon.classList.remove("fa-chevron-up");
        icon.classList.add("fa-chevron-down");
    }
}

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $('#EnderecoTarget').load(result.url);
                } else {
                    $('#myModalContent').html(result);
                    bindForm(dialog);
                }
            }
        });

        SetModal();
        return false;
    });
}

function BuscaCep() {
    $(document).ready(function () {

        function limpa_formulário_cep() {
            // Limpa valores do formulário de cep.
            $("#Endereco_Logradouro").val("");
            $("#Endereco_Bairro").val("");
            $("#Endereco_Cidade").val("");
            $("#Endereco_Estado").val("");
        }

        //Quando o campo cep perde o foco.
        $("#Endereco_Cep").blur(function () {

            //Nova variável "cep" somente com dígitos.
            var cep = $(this).val().replace(/\D/g, '');

            //Verifica se campo cep possui valor informado.
            if (cep != "") {

                //Expressão regular para validar o CEP.
                var validacep = /^[0-9]{8}$/;

                //Valida o formato do CEP.
                if (validacep.test(cep)) {

                    //Preenche os campos com "..." enquanto consulta webservice.
                    $("#Endereco_Logradouro").val("...");
                    $("#Endereco_Bairro").val("...");
                    $("#Endereco_Cidade").val("...");
                    $("#Endereco_Estado").val("...");

                    //Consulta o webservice viacep.com.br/
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (dados) {

                            if (!("erro" in dados)) {
                                //Atualiza os campos com os valores da consulta.
                                $("#Endereco_Logradouro").val(dados.logradouro);
                                $("#Endereco_Bairro").val(dados.bairro);
                                $("#Endereco_Cidade").val(dados.localidade);
                                $("#Endereco_Estado").val(dados.uf);
                            } //end if.
                            else {
                                //CEP pesquisado não foi encontrado.
                                limpa_formulário_cep();
                                alert("CEP não encontrado.");
                            }
                        });
                } //end if.
                else {
                    //cep é inválido.
                    limpa_formulário_cep();
                    alert("Formato de CEP inválido.");
                }
            } //end if.
            else {
                //cep sem valor, limpa formulário.
                limpa_formulário_cep();
            }
        });
    });
}

function ValidarNome() {
    $(document).ready(function () {
        debugger
        $("#Nome").on("input", function () {
            var nameField = $(this);
            var nameError = $("#name-error");
            var nameValue = nameField.val();
            debugger
            var regex = /^[A-Za-zÀ-ÖØ-öø-ÿ. ]*$/;

            if (!regex.test(nameValue)) {
                nameError.text("O nome só pode conter letras e ponto.");
                nameField.val(nameValue.replace(/[^A-Za-zÀ-ÖØ-öø-ÿ. ]/g, ""));
                return;
            }

            var correctedName = nameValue
                .toLowerCase()
                .replace(/(^|[\s.])([a-zà-öø-ÿ])/g, function (_, sep, char) {
                    return sep + char.toUpperCase();
                });

            nameField.val(correctedName);
            nameError.text("");
        });
    });
}

function ValidarNomesMultiplos() {
    $(document).ready(function () {
        $("#Nomes").on("input", function () {
            var nomesField = $(this);
            var nomesValue = nomesField.val();

            var regex = /[^A-Za-zÀ-ÖØ-öø-ÿ. ]/g;

            var nomes = nomesValue.split(',').map(function (nome) {
                nome = nome.trim().replace(regex, "");

                return nome
                    .toLowerCase()
                    .replace(/(^|[\s.])([a-zà-öø-ÿ])/g, function (_, sep, char) {
                        return sep + char.toUpperCase();
                    });
            });

            nomesField.val(nomes.join(", "));
        });
    });
}


function ValidarCnpj() {
    $(document).ready(function () {
        $("#Cnpj").mask("00.000.000/0000-00");

        $("#Cnpj").on("input", function () {
            const cnpjValue = $(this).val().replace(/\D/g, "");
            const cnpjError = $("#cnpj-error");

            if (cnpjValue.length === 0) {
                cnpjError.text("");
                return;
            }

            if (cnpjValue.length < 14) {
                cnpjError.text("CNPJ incompleto.");
                return;
            }

            if (!validarCNPJ(cnpjValue)) {
                cnpjError.text("CNPJ inválido.");
            } else {
                cnpjError.text("");
            }
        });

        $("form").on("submit", function (e) {
            const cnpjField = $("#Cnpj");
            const cnpjValue = cnpjField.val().replace(/\D/g, "");
            const cnpjError = $("#cnpj-error");

            if (cnpjValue.length > 0 && (!validarCNPJ(cnpjValue) || cnpjValue.length !== 14)) {
                e.preventDefault();
                cnpjError.text("CNPJ inválido.");
                cnpjField.focus();
            } else {
                cnpjError.text("");
            }
        });
    });
}

function validarCNPJ(cnpj) {
    if (cnpj.length !== 14 || /^(\d)\1{13}$/.test(cnpj)) return false;

    let tamanho = cnpj.length - 2;
    let numeros = cnpj.substring(0, tamanho);
    let digitos = cnpj.substring(tamanho);
    let soma = 0;
    let pos = tamanho - 7;

    for (let i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2) pos = 9;
    }

    let resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0)) return false;

    tamanho++;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;

    for (let i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2) pos = 9;
    }

    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    return resultado == digitos.charAt(1);
}
