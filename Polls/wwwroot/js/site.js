var apiUrl = "/api/polls/set-userAnswers";

$("#SignUpBtn").click(function () {
    $(".Sign").load("Register");
});

$("#SignInBtn").click(function () {
    $(".Sign").load("Login");
});

function add() {
    var new_chq_no = parseInt($('#total_chq').val()) + 1;
    var new_input = "<input type='text' class='form-control mt-3' placeholder='Add Your Question' id='PollQuestion" + new_chq_no + "'><input type='text' class='form-control mt-3 w-75 ml-3' placeholder='Add Your Answer' id='PollQuestion" + new_chq_no + "Answer" + new_chq_no + "'></div></div>";
    $('#new_chq').append(new_input);
    $('#total_chq').val(new_chq_no);
}
function remove() {
    var last_chq_no = $('#total_chq').val();
    if (last_chq_no > 1) {
        $('#PollQuestion' + last_chq_no).remove();
        $('#total_chq').val(last_chq_no - 1);
    }
}

$(document).ready(async function () {
    $('.submit').click(async function () {
        let userAnswers = getUserAnswers();
        await getAndRenderDataByFilters(apiUrl, userAnswers);
    });
});

async function getAndRenderDataByFilters(apiUrl, userAnswers) {
    let response = await fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userAnswers),
    }).then(
        successResponse => {
            if (successResponse.ok) {
                window.location.href = '/Polls/Index';
            }
        }
    );
}

function getUserAnswers() {
    let filters = {};
    filters['answerIds'] = [];
    const answers = getAnswers();
    answers.map((index, answer) => {
        filters['answerIds'].push(answer.value);
    });
    filters['pollId'] = getPollId();
    return filters;
}

function getAnswers() {
    return $('.answer-choose:checked');
}
function getPollId() {
    return $('.poll-title').val();
}