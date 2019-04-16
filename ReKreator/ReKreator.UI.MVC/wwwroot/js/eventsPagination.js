var filter = InitializeFilter();

$(document).ready(function () {

    PaginationTemplate(filter);
    SearchInputsEvents(filter);
});

function GetPageData(filter) {
    $.ajax({
        type: "POST",
        url: "/Event/EventsPage",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(filter),

        success: function (data) {
            $("#paged-data").html(data);
            $("#pagination").empty();
            PaginationTemplate(filter);
        }
    });
}
function PaginationTemplate(filter) {
    var maxPages = Math.ceil(parseInt($('input#total-size').val()) / filter.PageSize);
    var firstPaginationPage = parseInt(filter.PageNumber) - 2 <= 1 ? 1 : parseInt(filter.PageNumber) - 2;
    var lastPaginationPage = parseInt(filter.PageNumber) + 2 >= maxPages ? maxPages : parseInt(filter.PageNumber) + 2;

    if (firstPaginationPage < lastPaginationPage) {
        var pagination = '<ul class="pagination">';
        if (filter.PageNumber > 1) {
            pagination = pagination + '<li><a aria-label="Previous" onclick="UpdatePage(' + (filter.PageNumber - 1) + ')" href="#"><span aria-hidden="true">«</span></a></li>';
        }
        for (var i = firstPaginationPage; i <= lastPaginationPage; i++) {
            pagination = pagination + '<li ' + (i === parseInt(filter.PageNumber) ? 'class="active"' : '') + '><a onclick="UpdatePage(' + i + ')" href="#">' + i + '</a></li>';
        }
        if (filter.PageNumber < maxPages) {
            pagination = pagination + '<li><a aria-label="Next" onclick="UpdatePage(' + (filter.PageNumber + 1) + ')" href="#"><span aria-hidden="true">»</span></a></li>';
        }
        pagination = pagination + '</ul>';
        $("#pagination").append(pagination);
    }
}
function InitializeFilter() {
    return {
        PageNumber: 1,
        PageSize: 12,
        Keyword: null,
        BottomLineDate: null,
        TopLineDate: null,
        Type: $("#event-type")[0].value,
        Genres: null,
        IsShowObsoletes: false
};
}
function ClearSearchInputs() {
    $("#events-keyword-search")[0].value = "";
    $("#events-bottom-line-date-search")[0].value = "";
    $("#events-top-line-date-search")[0].value = "";
    $("#events-genre-search")[0].value = 0;
    $("#events-obsolete-search")[0].checked = false;
}

function UpdatePage(pageNumber) {
    filter.PageNumber = pageNumber;
    GetPageData(filter);
}

function SearchInputsEvents(filter) {
    $("#events-keyword-search").bind("blur", function () {
        filter.Keyword = $(this).val();
        GetPageData(filter);
    });
    $("#events-bottom-line-date-search").bind("change paste keyup", function () {
        filter.BottomLineDate = $(this).val();
        GetPageData(filter);
    });
    $("#events-top-line-date-search").bind("change paste keyup", function () {
        filter.TopLineDate = $(this).val();
        GetPageData(filter);
    });
    $("#events-genre-search").bind("change paste keyup", function () {
        filter.Genres = $(this).val();
        GetPageData(filter);
    });
    $("#events-obsolete-search").bind("change paste keyup", function () {
        filter.IsShowObsoletes = $(this).is(':checked');
        GetPageData(filter);
    });
    $("#events-clear-filter").click(function () {
        filter = InitializeFilter();
        ClearSearchInputs();
        GetPageData(filter);
    });
}