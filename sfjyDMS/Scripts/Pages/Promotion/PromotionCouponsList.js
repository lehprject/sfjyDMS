
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
    //short cut

    //--------------获取优惠券列表-----------

    var $pager = $('#couponspage');
    var $couposonListContainer = $('#couponsListContainer');

    var functions = Page.functions;

    //模板
    var template = $('#couponsTemplate').html();
    var source = Handlebars.compile(template);

    //--------------获取优惠券明细列表-----------
    var $pager1 = $('#detailpage');
    var $detailListContainer = $('#detailListContainer');

    //模板
    var template1 = $('#detailTemplate').html();
    var source1 = Handlebars.compile(template1);

    var pageIndex = 1;
    var couponsid = 0;

    //--------------获取优惠券明细函数列表-----------
    var findDetail = function (pageindex, coupons_id, $pager) {
        $.ajax({
            url: "/Promotion/SearchPromotionCouponsDetail",
            type: 'GET',
            data: {
                coupons_id: couponsid,
                pageIndex: pageindex,
            },
            success: function (data) {
                if (data && data.ResultList) {
                    var records = data.ResultList;

                    $.each(records, function (name, value) {
                        value.send = functions.datetimetostring(value.sendtime);
                        value.use = functions.datetimetostring(value.usetime);
                    });

                    var html = source1(records);
                    $detailListContainer.html(html);

                    //分页
                    var totalPages2 = Math.ceil(data.TotalRecord / 20);
                    functions.initialPager($pager, totalPages2, findDetail);
                };
            }
        });
    };

    var searchPromotion = function (pageindex) {
        $.ajax({
            url: "/Promotion/PromotionCoupons",
            type: 'GET',
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
                    $couposonListContainer.html(html);

                    //分页
                    var totalPages = Math.ceil(data.TotalRecord / 20);
                    functions.initialPager($pager, totalPages, searchPromotion)


                    $('#tbList button[value=selecct]').on('click', function () {
                        if (event.target == this) {
                            $button = $(this);
                            couponsid = $button.attr('data-orderid');

                            $('#detaildiv').css("display", "");//明细列表

                            findDetail(1, couponsid, $pager1);

                        }
                    });
                };
            }
        });
    }
    searchPromotion(1);

    //查询列表异步分页 

    $('#action').on("click", function () {
        var name = $('#name').val();
        var values = $('#values').val();
        var status = $('#status').val();
        var date1Input = $("#date1Input").val();
        var date2Input = $("#date2Input").val();
        if (values == "") {
            values = 0;
        }

        findCashdraw(1, name, values, status, date1Input, date2Input, $pager);
    });


    var findCashdraw = function (pageindex, name, values,status, date1, date2, $pager) {
        $.ajax({
            url: "/Promotion/SearchPromotionCoupons",
            type: 'POST',
            contentType: 'application/x-www-form-urlencoded',
            data: {
                name: name,
                values: values,
                issue_status:status,
                startdate1: date1,
                startdate2: date2,
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
                    $couposonListContainer.html(html);

                    //分页
                    var totalPages2 = Math.ceil(data.TotalRecord / 20);
                    functions.resetPager($pager, totalPages2, findCashdraw);

                    $('#tbList button[value=selecct]').on('click', function () {
                        if (event.target == this) {
                            $button = $(this);
                            couponsid = $button.attr('data-orderid');

                            $('#detaildiv').css("display", "");//明细列表

                            findDetail(1, couponsid, $pager1);

                        }
                    });
                };
            }
        });
    }


    //--------------获取优惠券明细列表-----------

})();

