
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


})();

(function () {
    var $promotionname = $("#activity");
    $promotionname.select2({
        placeholder: "活动名称",
        language: "zh-CN",
        allowClear: true,
        ajax: {
            url: "/Promotion/SearchPromotionNameList",
            dataType: 'json',
            delay: 500,
            data: function (params) {
                return {
                    nameparams: params.term,
                    format: "select",
                }
            },
            processResults: function (data, page) {
                if (data == null || data == undefined) {
                    return;
                }
                return {
                    results: data
                };
            },
        },
        minimumInputLength: 1
    }).on("select2:select", function (e) {
    });

})();


(function () {
    //short cut

    //--------------获取提现申请列表----------------

    var $pager = $('#promotionpage');
    var $cashdrawListContainer = $('#promotionListContainer');

    var functions = Page.functions;

    //模板
    var template = $('#eventTemplate').html();
    var source = Handlebars.compile(template);

    var pageIndex = 1;

    var searchPromotion = function (pageindex) {
        $.ajax({
            url: "/Promotion/SearchPromotionList",
            type: 'GET',
            contentType: 'application/x-www-form-urlencoded',
            data: {
                pageIndex: pageindex,
            },
            success: function (data) {
                if (data && data.ResultList) {
                    var records = data.ResultList;

                    $.each(records, function (name, value) {
                        value.start = functions.datetimetostring(value.startdate);
                        value.end = functions.datetimetostring(value.enddate);
                    });

                    var html = source(records);
                    $cashdrawListContainer.html(html);

                    //分页
                    var totalPages = Math.ceil(data.TotalRecord / 20);
                    functions.initialPager($pager, totalPages, searchPromotion)
                };
            }
        });
    }
    searchPromotion(1);

    //查询列表异步分页 

    $('#action').on("click", function () {
        var name = $('#activity').val();
        var date1Input = $("#date1Input").val();
        var date2Input = $("#date2Input").val();
        findCashdraw(1, name, date1Input, date2Input, $pager);
    });


    var findCashdraw = function (pageindex,name, date1, date2, $pager2) {
        $.ajax({
            url: "/Promotion/PostSearchPromotionList",
            type: 'GET',
            data: {
                name: name,
                date1: date1,
                date2: date2,
                pageIndex: pageindex,
            },
            success: function (data) {
                if (data && data.ResultList) {
                    var records = data.ResultList;                    
                    $.each(records, function (name, value) {
                        value.start = functions.datetimetostring(value.startdate);
                        value.end = functions.datetimetostring(value.enddate);
                    });

                    var html = source(records);
                    $cashdrawListContainer.html(html);

                    //分页
                    var totalPages2 = Math.ceil(data.TotalRecord / 20);
                    functions.resetPager($pager2, totalPages2, findCashdraw)
                };
            }
        });
    }

})();