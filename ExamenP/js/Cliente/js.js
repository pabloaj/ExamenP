var id_global;
jQuery(document).ready(function () {
    
    var table = $("#ClientesTable").DataTable({
        processing: true,

        bFilter: true,
        'iDisplayLength': 10,
        dom: 'Bfrtip',
        select: {
            style: 'os',
            items: 'row'
        },
        buttons: [
            {
                extend: 'excel',
                title: 'digital content'
            }, 'colvis'
        ],
        "ajax": {
            "url": "/MantenimientoClientes/ClienteTable",
            "type": "post",

            "data": function (d) {
                

            }
        },
        "columns": [
            { "data": "Id_Cliente" },
            { "data": "Nombre" },
            { "data": "Direccion" },
            { "data": "Telefono" },
            { "data": "Nit" },
           
            {
                "data": "id", "render": function (Id_Producto, type, full, meta) {
                    return '<input type ="button" class= "btn btn-outline btn-success btn-xs btn_Edit" value = "Edit" id="' + full.Id_Cliente + '"/>' +
                        '<input type ="button" class= "btn btn-outline btn-warning btn-xs btn_delete" value = "delete" id="' + full.Id_Cliente + '"/>';
                }
            }


        ],

    }).on('draw.dt', function () {
        $(".btn_Edit").click(function () {


            id_global = this.id;
          
            $.getJSON("/MantenimientoClientes/Get_Edit", { id: id_global })
                .done(function (data) {

                    $("#Input_nombre").val(data.data[0].Nombre);
                    $("#Input_Direccion").val(data.data[0].Direccion);
                    $("#Input_Telefono").val(data.data[0].Telefono);//Input_Precio
                    $("#Input_Nit").val(data.data[0].Nit);


                    $("#Dialog_").modal("show");
                    $("#btn_Update").show();
                    $("#btn_Save").hide();


                });
        });
        $(".btn_delete").click(function () {


            id_global = this.id;

            $.getJSON("/MantenimientoClientes/Get_Delete", { Id: id_global })
                .done(function (data) {

                    table.ajax.reload();


                });
        });
    });

    $("#btn_new").click(function () {
        $("#Dialog_").modal("show");
        $("#btn_Update").hide();
        $("#btn_save").show();

    });
    $("#btn_Update").click(function () {
     
        $.post("/MantenimientoClientes/GetUpdate", { Id: id_global, Nombre: $("#Input_nombre").val(), Direccion: $("#Input_Direccion").val(), telefono: $("#Input_Telefono").val(), Nit: $("#Input_Nit").val() })
            .done(function (result) {
                table.ajax.reload();
                $("#Dialog_").modal("hide");
                alert(result);
            });
    })

    $("#btn_Save").click(function () {
        alert("hi");
        $.post("/MantenimientoClientes/GetGuardar", { Nombre: $("#Input_nombre").val(), Direccion: $("#Input_Direccion").val(), Telefono: $("#Input_Telefono").val(), Nit: $("#Input_Nit").val() })
            .done(function (result) {
                table.ajax.reload();
                $("#Dialog_Productos").modal("hide");
                alert(result);
            });
    });

    
});