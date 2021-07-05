// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var triesCount = 8;
var triesLeft;

$(document).ready(function () {
    $('.start').on('click', function (e) {
        $(".front").css({ 'opacity': 0, 'z-index': 0 });
        $(".back").css('opacity', 1);
    })

    $("#guess-game-btn").prop("disabled", true);

    $("input").on("click", function () {
        $(this).select();
    });

    $('input').on('keyup', function () {
        $(this).next('input').focus().select();
        validate();
    });

    $('.open').on('click', function (e) {
        if (window.matchMedia('(min-width: 520px)').matches) {
            setLeaderboardWidth("500px");
        } else {
            setLeaderboardWidth("100%");
        }
        makeRequestToGetLeaderboard()
        e.stopPropagation();

    })

    $(window).on('resize', function () {
        if ($('.leaderboard').width() != 0) {
            if ($(this).width() >= 520) {
                setLeaderboardWidth("500px");
            }
            else {
                setLeaderboardWidth("100%");
                $('.current').css('font-size', 17);
            }
        }
    });

    $('.close').on('click', function () {
        $('.leaderboard').width(0);
    })

    $(".leaderboard").click(function (e) {
        e.stopPropagation();
    });

    $("body").click(function () {
        $('.leaderboard').width(0);
    });



    $("#guess-game-btn").on('click', function () {
        if ($(".guess")[0]) {

            var number = getNumber();

            triesLeft = parseInt($(".tries-left span").text());

            if (triesLeft > 0) {
                triesLeft--;
                $(".tries-left span").text(triesLeft);
            }

            if (triesLeft == 0) {
                startNewGame();
            }

            makeRequestToCheckUserAnswer({ Number: number, TriesLeft: triesLeft });
        }
        else {
            startNewGame()

            $(".tries-left span").text(triesCount);

            $(".winner, .loser").text("");

            $("input:not([type='submit'])").val("");

            $('.history__list').find('li').remove()

            $("#guess-game-btn").text("Guess");
            $("#guess-game-btn").addClass("guess");
            $("#guess-game-btn").prop("disabled", true);
        }
    });
});

function validate() {
    var inputsWithValues = 0;

    // get all input fields except for type='submit'
    var myInputs = $("input:not([type='submit'])");

    myInputs.each(function (e) {
        // if it has a value, increment the counter
        if ($(this).val()) {
            inputsWithValues += 1;
        }
    });

    $(".guess").prop("disabled", !(inputsWithValues == myInputs.length));
}

function getNumber() {
    var arrayOfDigits = $(".guess-number-form").serializeArray();
    var number = "";

    $.each(arrayOfDigits, function () {
        number += this.value;
    })

    return number;
}

function setLeaderboardWidth(width) {
    $('.leaderboard, .leaderboard__item').css('width', width);
}

function makeRequestToGetLeaderboard() {
    $.ajax({
        type: "GET",
        url: "Leaderboard/GetLeaderboard",
        contentType: "application/json; charset=utf-8",
    }).done(function (data) {
        if (!jQuery.isEmptyObject(data)) {
            setLeaderboard(data.currentUserName, data.users);
        }

    }).fail(function (xhr, ajaxOptions, error) {
        var err = eval("(" + xhr.responseText + ")");
        alert(err.Message);
    });
}

function setLeaderboard(currentUserName, users) {
    $(".leaderboard").find(".leaderboard__item").remove();
    for (let i = 0; i < users.length; i++) {
        var htmlClass = "";
        var style = "";
        if (users[i].userName === currentUserName) {
            htmlClass = "font-weight-bold current";
            style = `style="font-size:21px;"`;
        }
        $(".leaderboard").append(`<div class="${htmlClass} leaderboard__item" ${style}>${i + 1}. ${users[i].userName} (Success rate: ${users[i].successRate}% Total tries: ${users[i].totalTries})</div>`);
    }
}

function makeRequestToCheckUserAnswer(data) {
    $.ajax({
        type: "POST",
        url: "Game/CheckUserAnswer",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
    }).done(function (data) {
        if (!jQuery.isEmptyObject(data)) {
            setHistoryList(data.correctDigitsNotOnTheRightPlace, data.correctDigitsOnTheRightPlace);

            if (data.isWinner) {
                showWinnerOrLoser("winner", "You won!");
            }
            else if (triesLeft == 0) {
                showWinnerOrLoser("loser", `You lost! Correct number: ${data.correctNumber}`);
            }
        }
    }).fail(function (xhr, ajaxOptions, error) {
        var err = eval("(" + xhr.responseText + ")");
        alert(err.Message);
    });
}

function setHistoryList(correctDigitsNotOnTheRightPlace, correctDigitsOnTheRightPlace) {
    $(".history__list").append(`<li><span class="list__m">M:${correctDigitsNotOnTheRightPlace}</span> <span class="list__p">P:${correctDigitsOnTheRightPlace}</span></li>`)
}

function showWinnerOrLoser(htmlClass, text) {
    $(".guess-number-form").prepend(`<div class=${htmlClass}>${text}</div>`)
    startNewGame()
}

function startNewGame() {
    $("#guess-game-btn").text("Start new game");
    $("#guess-game-btn").removeClass("guess");
}