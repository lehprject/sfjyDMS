$(function() {

    $('#side-menu').metisMenu();

});

//Loads the correct sidebar on window load,
//collapses the sidebar on window resize.
// Sets the min-height of #page-wrapper to window size
$(function() {
    $(window).bind("load resize", function() {
        topOffset = 50;
        width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
        if (width < 768) {
            $('div.navbar-collapse').addClass('collapse');
            topOffset = 100; // 2-row-menu
        } else {
            $('div.navbar-collapse').removeClass('collapse');
        }

        height = ((this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height) - 1;
        height = height - topOffset;
        if (height < 1) height = 1;
        if (height > topOffset) {
            $("#page-wrapper").css("min-height", (height) + "px");
        }
    });

    /*���ò˵�*/
    sideMenu();

    /*��ҳ*/
    pagerInitial();
    
});

/*���ò˵�*/
function sideMenu() {
    //��߲˵�
    var url = window.location;
     
    //Server�˰�ѡ���е�(��Ӧ��)�˵���li����Ϊactive

    var theli = element = $('ul.nav a.active').filter(function () {
        return this.href == url || url.href.indexOf(this.href) == 0;
    }).first();

    if (theli != null && theli != undefined) {

        theli.parentsUntil('#side-menu').filter(function () { return this.nodeName == 'UL'; }).addClass('in');
        theli.parentsUntil('#side-menu').filter(function () { return this.nodeName == 'LI'; }).addClass('active'); 
    }
    ////////////////////////////////////////////////////

    //�ұ߲˵�
    //��server������active���� 
}

/**��ҳ**/
function pagerInitial() {

    /*Ӧ�÷�ҳ�ؼ�,ȫ�ֱ���pagerInfo�ᱻ����,����û�з�ҳ*/
    if (typeof pagerInfo != 'undefined') {
        if (pagerInfo != null && pagerInfo != undefined) {

            var pageIndexNames = pagerInfo.pagerIndexName.split(',');
            pagerInfo.pagerId.split(',').forEach(function (item,index) {
                var pageIndexName = pageIndexNames[index];

                var containForm = $('#' + pagerInfo.pagerId).parents('form').first();

                $('#' + item + ' a').each(function () {
                    $(this).click(function (event) {
                        if (event.target == this) {

                            var hiddenField = createHiddenField('action', 'paging');

                            var val = $(this).attr('value');
                            $('input[name=' + pageIndexName + ']').val(val);

                            if (val == 0) {
                                event.preventDefault();
                                return;
                            }
                            var jq_obj = $();
                            jq_obj[0] = hiddenField;
                            jq_obj.length = 1;
                            containForm.append(jq_obj);
                            containForm.submit();

                        };
                        event.preventDefault();
                    })
                });

            });

            

        }
    }
}

/**������**/
    /*����������*/
function createHiddenField(name, value) {
    var hiddenField = document.createElement("input");
    hiddenField.setAttribute("type", "hidden");
    hiddenField.setAttribute("name", name);
    hiddenField.setAttribute("value", value);
    return hiddenField;
};

    /*��ѡ��*/
function booleanCheck() {
    var checkbox = $(this);
    var name = checkbox.attr('id');
    var inputValue = checkbox.siblings('input[name=' + name + ']').first();
    var checkvalue = inputValue.val();
    checkvalue = Math.abs(checkvalue - 1);  
    inputValue.val(checkvalue);
};



/**ȫ�ֱ���**/

var starcloudGrobalVariable = {
    UrlPrefix: '/scweb'
}

