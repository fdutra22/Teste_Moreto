
$(document).ready(function () {
    $('#BtnBeneficiario').click(function () {
        ModalBeneficiario("Beneficiário", "TESTE");
    });


    $('#contacts').on('click', '.excluir', function () {

        var $td = $(this).closest('tr').find('td');
        var cpf = $td.eq(1).text();
        //alert(part_name);
        $.ajax({
            url: urlBeneficiatioDeletar,
            method: "DELETE",
            data: {
                "CPF": cpf
            },
            error:
                function (r) {
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r)
                    $("#formCadastro")[0].reset();
                    GetBeneficiario();
                    //window.location.href = urlRetorno;
                }
        });
    });

    $('#contacts').on('click', '.alterar', function () {

        var $td = $(this).closest('tr').find('td');
        var id = $td.eq(4).text();
        
        var cpf = $td.eq(1).text();
        var nome = $td.eq(0).text();
        $("#CpfB").val(cpf);
        $("#NomeB").val(nome);
        $("#idB").val(id);
        
    });
    GetBeneficiario();
});

function GetBeneficiario() {
    $.ajax({
        url: urlBeneficiatioList,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (data) {
            var row = "";
            $.each(data, function (index, item) {
                row += "<tr><td>" + item.Nome + "</td><td>" + item.Cpf + "</td><td><input type='button' class='btn btn-success alterar' value='Alterar' /></td>><td><input type='button' class='btn btn-error excluir' value='Exlcuir' /></td><td hidden='true'>" + item.Id +"</td></tr>";
            });
            $("#contacts").html(row);
        },
        error: function (result) {
            alert("Error");
        }
    });
}

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}
