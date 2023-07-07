
const _modeloEmpregado = {
    idEmpregado: 0,
    nomeCompleto: "",
    idDepartamento: 0,
    saldo: 0,
    fechaContrato: ""
}

function MostrarEmpregados() {

    fetch("Home/ListaEmpregados")
        .then(response => {
            return response.ok ? response.json() : PromiseRejection(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {

                $("#tableEmpregados tbody").html("");


                responseJson.forEach((empregado) => {
                    $("#tableEmpregados tbody").append(
                        $("<tr>").append(
                            $("<td>").text(empregado.nomeCompleto),
                            $("<td>").text(empregado.refDepartamento.nome),
                            $("<td>").text(empregado.saldo),
                            $("<td>").text(empregado.fechaContrato),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm botao-editar-empregado").text("Editar").data("dataEmpregado", empregado),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 botao-eliminar-empregado").text("Eliminar").data("dataEmpregado", empregado),
                            )
                        )
                    )
                })

            }


        })

}

document.addEventListener("DOMContentLoaded", function () {

    MostrarEmpregados();

    fetch("/Home/ListaDepartamentos")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {

            if (responseJson.length > 0) {
                responseJson.forEach((item) => {

                    $("#cboDepartamento").append(
                        $("<option>").val(item.idDepartamento).text(item.nome)
                    )

                })
            }

        })

    $("#txtFechaContrato").datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        todayHighlight: true
    })


}, false)

function MostrarModal() {

    $("#txtNomeCompleto").val(_modeloEmpregado.nomeCompleto);
    $("#cboDepartamento").val(_modeloEmpregado.idDepartamento == 0 ? $("#cboDepartamento option:first").val() : _modeloEmpregado.idDepartamento)
    $("#txtSaldo").val(_modeloEmpregado.saldo);
    $("#txtFechaContrato").val(_modeloEmpregado.fechaContrato)


    $("#modalEmpregado").modal("show");

}

$(document).on("click", ".botao-novo-empregado", function () {

    _modeloEmpregado.idEmpregado = 0;
    _modeloEmpregado.nomeCompleto = "";
    _modeloEmpregado.idDepartamento = 0;
    _modeloEmpregado.saldo = 0;
    _modeloEmpregado.fechaContrato = "";

    MostrarModal();

})

$(document).on("click", ".botao-editar-empregado", function () {

    const _empregado = $(this).data("dataEmpregado");


    _modeloEmpregado.idEmpregado = _empregado.idEmpregado;
    _modeloEmpregado.nombreCompleto = _empregado.nomeCompleto;
    _modeloEmpregado.idDepartamento = _empregado.refDepartamento.idDepartamento;
    _modeloEmpregado.sueldo = _empregado.saldo;
    _modeloEmpregado.fechaContrato = _empregado.fechaContrato;

    MostrarModal();

})

$(document).on("click", ".botao-guarda-empregado", function () {

    const modelo = {
        idEmpregado: _modeloEmpregado.idEmpregado,
        nomeCompleto: $("#txtNomeCompleto").val(),
        refDepartamento: {
            idDepartamento: $("#cboDepartamento").val()
        },
        saldo: $("#txtSaldo").val(),
        fechaContrato: $("#txtFechaContrato").val()
    }

    if (_modeloEmpregado.idEmpregado == 0) {

        fetch("/Home/GuardarEmpregado", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalEmpregado").modal("hide");
                    Swal.fire("Feito!", "Empregado foi criado", "successo");
                    MostrarEmpregados();
                }
                else
                    Swal.fire("Sinto Muito", "Não foi possível criar", "erro");
            })

    } else {

        fetch("/Home/EditarEmpregado", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalEmpregado").modal("hide");
                    Swal.fire("Feito!", "Empregado foi editado", "successo");
                    MostrarEmpregados();
                }
                else
                    Swal.fire("Sinto Muito", "Não foi possível editar", "erro");
            })

    }

})

$(document).on("click", ".botao-eliminar-empregado", function () {

    const _empregado = $(this).data("dataEmpregado");

    Swal.fire({
        title: "Tem certeza?",
        text: `Eliminar Empregado "${_empregado.nomeCompleto}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sim, eliminar",
        cancelButtonText: "Não, voltar"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch(`/Home/EliminarEmpregeado?idEmpregado=${_empregado.idEmpregado}`, {
                method: "DELETE"
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        Swal.fire("Feito!", "Empregado foi elminado", "successo");
                        MostrarEmpregados();
                    }
                    else
                        Swal.fire("Sinto Muito", "Não foi possível eliminar", "erro");
                })
        }
    })

})