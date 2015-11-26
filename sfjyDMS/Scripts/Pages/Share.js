//全局变量
var gvar = gvar || {};
gvar.urlprefix = '/wap';

(function ($) {
    var oldAjax = $.ajax;
    $.ajax = function () {
        var url = arguments[0].url;
        if (url != null && url != undefined && url[0] == '/') {
            arguments[0].url = gvar.urlprefix + arguments[0].url;
        }
        oldAjax.call(this, arguments);
    }
})(jQuery);

(function () {
    "use strict";

    //初始化page共享对象
    if (typeof Page == 'undefined') {
        window.Page = {};
    }
    Page = Page || {};
    Page.functions = Page.functions || {};
    Page.jqueryMap = Page.jqueryMap || {};
    Page.varData = Page.varData || {};
    Page.containerModel = Page.containerModel || {};
    Page.viewModel = Page.viewModel || {};
    Page.cacheData = Page.cacheData || {};
    Page.jsonData = Page.jsonData || {};

    //short cut for Page
    var functions = Page.functions,
        jqueryMap = Page.jqueryMap,
        varData = Page.varData
    ;

    if ($.fn.select2) {
        //select2 控件初始化

        //--------basic---------
        Page.functions.customMatch = function (params, data) {
            // If there are no search terms, return all of the data
            if ($.trim(params.term) === '') {
                return data;
            }
            var term = params.term;
            var result = null;
            if (data.additionitems != null && data.additionitems != undefined) {
                var prop = null;
                for (prop in data.additionitems) {
                    if (data.additionitems[prop].indexOf(term) > -1) {
                        return data;
                    }
                }
            }
            return result;
        };

        Page.functions.initialDropDown = function ($ddl, name, value) {
            $ddl.append(new Option(name, value));
            $ddl.val(value).trigger('change');
        };

        Page.functions.defaultDropDown = function ($ddl) {
            $ddl.val('').trigger('change');
        };


    };

    //-------分页控件初始化-------------
    Page.functions.initialPager = function ($pager, totalPages, onPageClick) {
        if (totalPages == 0)
            totalPages = 1;
        $pager.twbsPagination({
            totalPages: totalPages,
            onPageClick: function (event, page) {
                if (onPageClick && typeof onPageClick == 'function') {
                    onPageClick(page);
                }
            }
        });
    };

    Page.functions.resetPager = function ($pager,totalPages, onPageClick) {
        $pager.twbsPagination('destroy');
        if (totalPages == 0)
            totalPages = 1;
        $pager.twbsPagination({
            totalPages: totalPages,
            onPageClick: function (event, page) {
                if (onPageClick && typeof onPageClick == 'function') {
                    onPageClick(page);
                }
            }
        });
    };

    //-------日期控件-------
    Page.functions.initialDatePicker = function ($date1, $date2) {
        $date1.data('relateddatepicker', $date2)
        $date1.datepicker({
            format: 'yyyy-mm-dd',
            todayBtn: "linked",
            language: "zh-CN",
            multidate: 1,
            multidateSeparator: ",",
            forceParse: false,
            autoclose: true,
            todayHighlight: true
        }).on('hide', function (e) {
            var $date1 = $(this);
            var $data2 = $date1.data('relateddatepicker');
            if ($date2) {
                var selecteddate = e.dates[0];
                var selecteddateStr = $(this).datepicker('getFormattedDate');
                if (selecteddate != null && selecteddate != undefined) {
                    $date2.datepicker('setStartDate', selecteddate);
                    var date2value = new Date($date2.val());
                    if (date2value && isNaN(date2value.getDate()) == false) {
                        if (selecteddate > date2value) {
                            //起始日期 大于 结束日期,重新设置
                            $date2.val(selecteddateStr)
                        }
                    }
                }
            }
        });
        if ($date2) {
            $date2.datepicker({
                format: 'yyyy-mm-dd',
                todayBtn: "linked",
                language: "zh-CN",
                multidate: 1,
                multidateSeparator: ",",
                forceParse: false,
                autoclose: true,
                todayHighlight: true
            });
        }
    }
})();