(function () {
    var rowindex = 1;
    var addrow = function () {
        var table=document.getElementById("tab1");
        var row;
        row=table.insertRow();
        var newTd0 = row.insertCell();
        var newTd1 = row.insertCell();
        newTd0.innerHTML = ++rowindex;
        newTd1.innerHTML = '<input type="text" name="remark" class="form-control"/>';

    }

    $("#add").click(function () {
        addrow();
    });

    $("#btnSave").click(function () {
        var name = $("#name").val();
        var content = $("#content").val();
        var date1Input = $("#date1Input").val();
        var date2Input = $("#date1Input").val();

        var str = "";
        $('input[name=remark]').each(function () {
            var v = $(this).val();
            str += v + ",";
        });

        $.ajax({
            url: "/Promotion/CreatePromotionEvent",
            type: 'POST',
            data: {
                name: name,
                date1: date1Input,
                date2: date2Input,
                content: content,
                remark: str,
            },
            success: function (data) {
                if (data && data.ResultList) {
                };
            }
        });
    });

})();