$(function () {
    
    var PlaceHolderElement = $('#PlaceHolderHere');
    $('button[data-toggle="ajax-modal"]').click(function (event){
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })
    bindForm(this);
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
            return false;
        });
    }
    
    PlaceHolderElement.on('click', '[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (data) {
            PlaceHolderElement.find('.modal').modal('hide');
        })
    })
    
})

function BuscaCep() {
    $(document).ready(function () {
        function limpa_formulario_cep() {
            $("#Endereco_Logradouro").val("");
            $("#Endereco_Bairro").val("");
            $("#Endereco_Cidade").val("");
            $("#Endereco_Estado").val("");
        }
        
        $("#Endereco_Cep").blur(function () {
            var cep = $(this).val().replace(/\D/g, '');
            
            if (cep != "") {
                var validaCep = /^[0-9]{8}$/;
                
                if (validaCep.test(cep)) {
                    $("#Endereco_Logradouro").val("...");
                    $("#Endereco_Bairro").val("...");
                    $("#Endereco_Cidade").val("...");
                    $("#Endereco_Estado").val("...");
                    
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (dados) {
                        
                            if (!("erro" in dados)) {
                                $("#Endereco_Logradouro").val(dados.logradouro);
                                $("#Endereco_Bairro").val(dados.bairro);
                                $("#Endereco_Cidade").val(dados.localidade);
                                $("#Endereco_Estado").val(dados.uf);
                            } else {
                                limpa_formulario_cep();
                                alert("CEP não encontrado.");
                            }
                        })
                }
                else {
                    limpa_formulario_cep();
                    alert("Formato de CEP inválido");
                }
            } else {
                limpa_formulario_cep();
            }
        })
    })
}