//$(document).ready(function () {
//    $("td[colspan=2]").find("p").hide();
//    $("table").click(function (event) {
//        event.stopPropagation();
//        var $target = $(event.target);
//        if ($target.closest("td").attr("colspan") > 1) {
//            $target.slideUp();
//        } else {
//            $target.closest("tr").next().find("p").slideToggle();
//        }
//    });
//});

$(document).ready('.collapse').on('show.bs.collapse', function () {
    var groupId = $('#expander').attr('data-group-id');
    console.log(groupId);
    if (groupId) {
        $('#grandparentIcon').html('v');
    }
});

$('.collapse').on('hide.bs.collapse', function () {
    var groupId = $('#expander').attr('data-group-id');
    console.log(groupId);
    if (groupId) {
        $('#' + groupId + 'Icon').html('>');
    }
});
