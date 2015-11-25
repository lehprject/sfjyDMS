
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
    var $promotionname = $("#name");
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
                    functions.initialPager($pager, totalPages, searchPromotion)
                };
            }
        });
    }
    searchPromotion(1);

    //查询列表异步分页 

    $('#action').on("click", function () {
        first = 1;
        var name = $('#name').text;
        var date1Input = $("#date1Input").val();
        var date2Input = $("#date2Input").val();
        findCashdraw(1, date1Input, date2Input, name, $pager);
    });


    var findCashdraw = function (pageindex, date1, date2, statu, $pager2) {
        $.ajax({
            url: "/Promotion/PostSearchPromotionList",
            type: 'GET',
            data: {
                name:"",
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
                    var totalPages2 = Math.ceil(data.TotalRecord / 2);
                    functions.resetPager($pager2, totalPages2, findCashdraw)
                };
            }
        });
    }

})();