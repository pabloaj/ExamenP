
function autocomplete(Idelemet, controller) {
    
        function split(v) {

            return v.split(/,/);
        }
        function extractLast(term) {
            return split(term).pop();
        }
        $('#' + Idelemet)
            
            .on("keydown", function (event) {
                if (event.keyCode === $.ui.keyCode.TAB &&
                    $(this).autocomplete("instance").menu.active) {
                    event.preventDefault();
                }
            })
            .autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: controller,
                        dataType: 'json',
                        method: 'POST',
                        data: { term: extractLast(request.term) },
                        success: function (data) {
                            response($.map(data, function (item) {
                                console.log(item._name)
                                return { value: item.Name };
                            }))
                        }
                    });
                },
                search: function () {
                    // custom minLength

                    var term = extractLast($('#' + Idelemet).val());
                    if (term.length < 2) {
                        return false;
                    }
                },
                focus: function () {
                    // prevent value inserted on focus
                    return false;
                },
                select: function (event, ui) {
                    var terms = split($('#' + Idelemet).val());
               
                    terms.pop();
                   
                    terms.push(ui.item.value);
                   
                    terms.push("");
                    $('#' + Idelemet).val(terms.join(","))
                    return false;
                }
            });


}
