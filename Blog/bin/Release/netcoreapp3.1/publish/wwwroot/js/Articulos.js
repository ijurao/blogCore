
    var dataTable;
        $(document).ready(function () {
        cargarDatatable();
        });
        function cargarDatatable() {
        dataTable = $("#tbArticulos").DataTable({
            "ajax": {
                "url": "/Admin/Articulos/GetAll",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "id", "width": "5%" },
                { "data": "titulo", "width": "5%" },
                { "data": "descripcion", "width": "10%" },
                { "data": "categoria.nombre", "width": "10%" },
                {
                    "data": "urlImagen",
                    "render": function (data) {

                        return '<img src="' + data + '" />';

                    }
                },
                { "data": "fechaCreacion", "width": "10%" },
                {
                    "data": "id",
                    "render": function (data) {
                        return `<div class="text-center">
            <a href='/Admin/Articulos/Edit/${data}' class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                <i class='fas fa-edit'></i> Editar
            </a>

            <a onclick=Delete("/Admin/Articulos/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                <i class='fas fa-trash-alt'></i> Borrar
            </a>
            `;
                    }, "width": "25%"
                }
            ],
            "language": {
                "emptyTable": "No hay registros"
            },
            "width": "100%"
        });
        }


        function Delete(url) {
        swal({
            title: "Esta seguro de borrar?",
            text: "Este contenido no se puede recuperar!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Si, borrar!",
            closeOnconfirm: true
        }, function () {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        });
        }

