
/*
 * 初始化control
 */
(function () {
    //short cut
    var functions = Page.functions
    ;
    //-------日期-------
    functions.initialDatePicker($('#date1Container input'), $('#date2Container input'));
})();

(function () {
    //点击全选触发  
    $("[name='select']:checkbox").on('click', function () {
        if (this.checked)
        {
            $(this).attr("value", "1");
        }
        else {
            $(this).attr("value", "0");
        }
    });

})();