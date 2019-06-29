jQuery(document).ready(function () {
    var table = $('#TableFacturacion').DataTable({
        processing: true,

        bFilter: true,
        'iDisplayLength': 10,
        dom: 'Bfrtip',
        select: {
            style: 'os',
            items: 'row'
        }, "footerCallback": function (row, data, start, end, display) {
            var api = this.api();
            nb_cols = api.columns().nodes().length;
            var j = 6;
            while (j < nb_cols) {
                var pageTotal = api
                    .column(j, { page: 'current' })
                    .data()
                    .reduce(function (a, b) {
                        return Number(a) + Number(b);
                    }, 0);
                // Update footer
                $(api.column(j).footer()).html(pageTotal);
                j++;
            }
        },
        buttons: [
            {
                extend: 'excel',
                title: 'digital content'
            }, 'colvis'
        ],
    }).on('draw.dt', function () {
        $(".btn_Edit").click(function () {


            id_global = this.id;
            table.row($(this).parents('tr')).remove().draw(false);
            var data = table.rows().data();
            data.each(function (value, index) {
                console.log(`For index ${index}, data value is ${value}`);
            });
            alert(Table.cell(this, 2).data());
            //$.getJSON("/MantenimentoProducto/Get_Edit", { Id: id_global })
            //    .done(function (data) {

            //        $("#Input_nombre").val(data.data[0].Nombre);
            //        $("#Input_Codigo").val(data.data[0].Codigo);
            //        $("#Input_Cantidad").val(data.data[0].Cantidad);//Input_Precio
            //        $("#Input_Precio").val(data.data[0].Precio);
            //        $("#Input_Medida").val(data.data[0].Medida);

            //        $("#Dialog_Productos").modal("show");
            //        $("#btn_Update").show();
            //        $("#btn_Save").hide();


            //    });
        });
        $(".btn_delete").click(function () {


            id_global = this.id;

            $.getJSON("/MantenimentoProducto/Get_Delete", { id: id_global })
                .done(function (data) {

                    table.ajax.reload();


                });
        });
    });
    $("#InputNit").keypress(function (e) {
        //no recuerdo la fuente pero lo recomiendan para
        //mayor compatibilidad entre navegadores.
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            $.getJSON("/Facturacion/Get_Cliente", { Nit: $("#InputNit").val() })
                .done(function (data) {

                    $("#InputNombre").val(data.data[0].Nombre);
                    $("#InPutDireccion").val(data.data[0].Direccion);
                    $("#InputTelefono").val(data.data[0].Telefono);//Input_Precio
                    $('#InputCodigo').focus();

                });
        }
    });
    $("#InputCodigo").keypress(function (e) {
        //no recuerdo la fuente pero lo recomiendan para
        //mayor compatibilidad entre navegadores.
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            $.getJSON("/Facturacion/Get_Producto", { Codigo: $("#InputCodigo").val() })
                .done(function (data) {

                    $("#InputDescripcion").val(data.data[0].Nombre);
                   
                   $('#InputCantidad').focus();


                });
        }
    });
    $("#InputCantidad").keypress(function (e) {
        //no recuerdo la fuente pero lo recomiendan para
        //mayor compatibilidad entre navegadores.
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            $.getJSON("/Facturacion/Get_Cantidad", { Codigo: $("#InputCodigo").val(), Cantidad: $("#InputCantidad").val()})
                .done(function (data) {
                    var htmlv = '<td><input type ="button" class= "btn btn-outline btn-success btn-xs btn_Edit" value = "Edit" id="' + '"/></td>';
                    var cantidadInt = parseInt($("#InputCantidad").val());
                    var fprecio = parseFloat(data.data[0].Precio);
                    var total = fprecio * cantidadInt;
                    
                    table.row.add([
                        data.data[0].Id_Producto,
                        data.data[0].Codigo,
                        data.data[0].Nombre,
                        data.data[0].Medida,
                        cantidadInt,
                        fprecio,
                       total,
                        htmlv
                        



                    ]).draw();;


                    $('#InputCodigo').focus();


                });
        }
    });
});
    
  
