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
function getSearchResult() {
    $.ajax({
        url: "/Shows/Search",
        type: "GET",
        data: { "searchString": $("#searchBox").val() },
        success: function (result) {
            console.log(result);
            $("#tmp").html(result);
        }
    });
}

$(document).ready('.collapse').on('show.bs.collapse', function () {
    var groupId = $('#expander').attr('data-group-id');
    console.log(groupId);
    //if (groupId) {
    //    $('#grandparentIcon').html('v');
    //}
});

$('.collapse').on('hide.bs.collapse', function () {
    var groupId = $('#expander').attr('data-group-id');
    console.log(groupId);
    //if (groupId) {
    //    $('#' + groupid + 'icon').html('>');
    //}
});

function getShowDetails(id) {
    $.ajax({
        url: "/Shows/ShowDetails",
        type: "GET",
        data: { "id": id },
        success: function (result) {
            console.log(result);
            $("#detailsView" + id).html(result);
        }
    });
}

function getEpisodes(id) {
    $.ajax({
        url: "/Shows/Episodes",
        type: "GET",
        data: { "id": id },
        success: function (result) {
            console.log(result);
            $("#episodeView" + id).html(result);
        }
    });
}
