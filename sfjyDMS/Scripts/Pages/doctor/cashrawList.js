﻿
/*
 * 初始化control
 */
(function () {
    //short cut
    var functions = Page.functions;
    //-------日期-------
    functions.initialDatePicker($('#date1Container input'), $('#date2Container input'));
})();

(function () {
    //点击全选触发  
    $("[name='selectall']:checkbox").on('click', function () {
        $("[name='select']:checkbox").prop('checked', this.checked);
    });


    $("[name='select']:checkbox").on('click', function () {
        $(this).prop('checked', true);
    });

})();

(function () {

    //--------------获取提现申请列表----------------

    var $pager = $('#cashdrawpage');
    var $cashdrawListContainer = $('#cashdrawListContainer');

    var functions = Page.functions;

    //模板
    var template = $('#cashdrawTemplate').html();
    var source = Handlebars.compile(template);

    var searchCashdraw = function (pageindex) {
        $.ajax({
            url: "/Doctor/Cashdraws",
            type: 'GET',
            data: {
                pageIndex: pageindex,
            },
            success: function (data) {
                if (data && data.ResultList) {
                    var records = data.ResultList;

                    $.each(records, function (name, value) {
                        value.apptime = functions.datetimetostring(value.app_time);
                        value._optime = functions.datetimetostring(value.optime);
                    });


                    var html = source(records);
                    $cashdrawListContainer.html(html);

                    //分页
                    var totalPages = Math.ceil(data.TotalRecord / 20);
                    functions.initialPager($pager, totalPages, searchCashdraw)
                };
            }
        });
    }
    searchCashdraw(1);

    //查询列表异步分页 

    $('#action').on("click", function () {
        var statu = $("#statu").val();
        var date1Input = $("#date1Input").val();
        var date2Input = $("#date2Input").val();
        var $pager2 = $('#cashdrawpage');
        findCashdraw(1, date1Input, date2Input, statu, $pager2);
    });


    var findCashdraw = function (pageindex, date1, date2, statu, $pager2) {
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
                    var totalPages2 = Math.ceil(data.TotalRecord / 20);
                    functions.resetPager($pager2, totalPages2, findCashdraw)
                };
            }
        });
    }

    //批量处理提现申请
    $('#bulk').on("click", function () {
        var str = '';
        $("[name='select']:checked").each(function () {
            str += $(this).val() + ',';
        });

        var statu = $("#statu").val();
        if (str == "") {
            alert("请选择提现列表");
            return false;
        }

        if (statu == "1") {
            alert("已处理");
            return false;
        }
        else {
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
        }
    });
})();