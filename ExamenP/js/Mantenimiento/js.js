var id_global;
jQuery(document).ready(function () {
  
    var table = $("#ProductosTable").DataTable({
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
            "url": "/MantenimentoProducto/ProductoTable",
            "type": "post",

            "data": function (d) {


            }
        },
        "columns": [
            { "data": "Id_Producto" },
            { "data": "Nombre" },
            { "data": "Codigo" },
            {"data":"Medida"},
            { "data": "Cantidad" },
            { "data": "Precio" },
            {
                "data": "id", "render": function (Id_Producto, type, full, meta) {
                    return '<input type ="button" class= "btn btn-outline btn-success btn-xs btn_Edit" value = "Edit" id="' + full.Id_Producto + '"/>' +
                        '<input type ="button" class= "btn btn-outline btn-warning btn-xs btn_delete" value = "delete" id="' + full.Id_Producto + '"/>';
                }
            }


        ],

    }).on('draw.dt', function () {
        $(".btn_Edit").click(function () {
        
           
                id_global = this.id;
       
            $.getJSON("/MantenimentoProducto/Get_Edit", { Id: id_global })
                .done(function (data) {
            
                    $("#Input_nombre").val(data.data[0].Nombre);
                    $("#Input_Codigo").val(data.data[0].Codigo);
                    $("#Input_Cantidad").val(data.data[0].Cantidad);//Input_Precio
                    $("#Input_Precio").val(data.data[0].Precio);
                    $("#Input_Medida").val(data.data[0].Medida);

                       $("#Dialog_Productos").modal("show");
                    $("#btn_Update").show();
                    $("#btn_Save").hide();

                   
        });
        });
        $(".btn_delete").click(function () {


            id_global = this.id;

            $.getJSON("/MantenimentoProducto/Get_Delete", { id: id_global })
                .done(function (data) {

                    table.ajax.reload();


                });
        });
        });

    $("#btn_new").click(function () {
        $("#Dialog_Productos").modal("show");
        $("#btn_Update").hide();
        $("#btn_Save").show();

    });
    $("#btn_Update").click(function () {
       
        $.post("/MantenimentoProducto/GetUpdate", { Id: id_global, Nombre: $("#Input_nombre").val(), Codigo: $("#Input_Codigo").val(), Cantidad: $("#Input_Cantidad").val(), Precio: $("#Input_Precio").val(), Medida: $("#Input_Medida" )})
            .done(function (result) {
                table.ajax.reload();
                $("#Dialog_Productos").modal("hide");
                alert(result);
            });
    })

    $("#btn_Save").click(function () {
     
        $.post("/MantenimentoProducto/GetGuardar", { Nombre: $("#Input_nombre").val(), Codigo: $("#Input_Codigo").val(), Cantidad: $("#Input_Cantidad").val(), Precio: $("#Input_Precio").val(), Medida: $("#Input_Medida").val() })
            .done(function (result) {
                table.ajax.reload();
                $("#Dialog_Productos").modal("hide");
                alert(result);
            });
    })
});