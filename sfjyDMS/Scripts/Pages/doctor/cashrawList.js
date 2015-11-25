
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
    ////点击全选触发  
    //$("[name='selectall']:checkbox").on('click', function () {
    //    $("[name='select']:checkbox").attr('checked', this.checked);
    //});

    ////点击每一个item触发  
    //$("[name='hobby']:checkbox").on('click', function () {
    //    var $items = $("[name='hobby']:checkbox");
    //    $("[name='checkAll']:checkbox").attr('checked', $items.length == $items.filter("[name='hobby']:checked").length);
    //});

    //$(document).ready(function () {

    //    $("[name='select']:checkbox").on('click', function () {
    //        $(this).attr('checked', true);
    //    });
    //});

    $("[name='selectall']:checkbox").on('click', function () {
        var value = $(this).val();
        if (value == '0') {
            $(this).attr('checked', true);
            $("[name='select']:checkbox").attr('checked', true);
            $(this).attr("value", "1");
        }
        else {
            $(this).attr('checked', false);
            $("[name='select']:checkbox").attr('checked', false);
            $(this).attr("value", "0");
        }
    });

    $("[name='select']:checkbox").on('click', function () {
        //$(this).prop("checked",true);;
        $(this).attr('checked', true);
    });

    //$("[name='select']:checkbox").each(function () {
    //    var check = $(this);
    //    check.on('click', function () {
    //        alert("dfdf");
    //        $(this).attr('checked', 'checked');
    //    });
    //});
})();

(function () {
    //short cut
    var cacheData = Page.cacheData,
        jqueryMap = Page.jqueryMap,
        varData = Page.varData,
        viewModel = Page.viewModel;


    //--------------获取提现申请列表----------------

    var $pager = $('#cashdrawpage');
    var $cashdrawListContainer = $('#cashdrawListContainer');

    var functions = Page.functions;
    jqueryMap.$cashdrawListContainer = $cashdrawListContainer;

    //模板
    var template = $('#cashdrawTemplate').html();
    var source = Handlebars.compile(template);

    var pageIndex = 1;
    var pageSize = 20;
    var first = 0;

    var searchCashdraw = function (pageindex, pagesize) {
        $.ajax({
            url: "/Doctor/Cashdraws",
            type: 'GET',
            data: {
                pageIndex: pageindex,
            },
            success: function (data) {
                if (data && data.ResultList) {
                    var records = data.ResultList;
                    var html = source(records);
                    $cashdrawListContainer.html(html);

                    //分页
                    var totalPages = Math.ceil(data.TotalRecord / 2);
                    functions.initialPager($pager, totalPages, searchCashdraw)
                };
            }
        });
    }
    if (first == 0) {
        searchCashdraw(1, pageSize);
    }
 
    //查询列表异步分页 

    $('#action').on("click", function () {
        first = 1;
        var statu = $("#statu").val();
        var date1Input = $("#date1Input").val();
        var date2Input = $("#date2Input").val();
        $('#cashdrawpage').css("display", "none");
        $('#cashdrawpage2').css("display", "");
        findCashdraw(1, date1Input, date2Input, statu);
    });

    var $pager2 = $("#cashdrawpage2");
    var findCashdraw = function (pageindex, date1, date2, statu) {
        $.ajax({
            url: "/Doctor/CashdrawList1",
            type: 'GET',
            data: {             
                date1: date1,
                date2: date2,
                statu: statu,
                pageIndex: pageindex,
            },
            success: function (data) {
                if (data && data.ResultList) {
                    var records = data.ResultList;

                    var html = source(records);
                    $cashdrawListContainer.html(html);

                    //分页
                    var totalPages = Math.ceil(data.TotalRecord / 2);
                    functions.initialPager($pager2, totalPages, findCashdraw)
                };
            }
        });
    }


    $('#bulk').on("click", function () {
        var str = '';
        $("[name='select']:checkbox").each(function () {
            if ($(this).attr('checked')) {
                str += $(this).val() + ',';
            }
        });
        $.ajax({
            url: "/Doctor/UpdateCashdrawList",
            type: 'POST',
            data: {
                idstr: str,
            },
            success: function (data) {
                if (data) {
                    alert("批量处理成功");
                };
            }
        });
    });
})();