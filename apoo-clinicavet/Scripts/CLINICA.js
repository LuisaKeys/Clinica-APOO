
        $("#btnAddExame").click(function () {
            //Váriavel para checar se já existe na lista
            var chkRptExame = false;

            $('#lstExame li').each(function () {
                haveSomeLi = true;
                var current = $(this).text();
                if (current == $("#Exames option:selected").text()) {
                    chkRptExame = true;
                }
            });

            if (!chkRptExame) {
                $("#lstExame").append("<li>" + $("#Exames option:selected").text() + "<input type='checkbox' name='chkExames' id='chkExames' class='chkExames' checked='checked' value='" + $("#Exames option:selected").val() + "'></li>");
            } else {
                alert("Exame Já inserido!");
            }
        });


    $('#lstExame').on('click', "li", function () {
        $(this).remove();
    //alert();
    return false;
        });

