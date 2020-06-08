
function onRecaptchaLoad(apiKey, element) {
    console.log(element)
    var input = document.getElementById(element);
    var params = {
        action: input.dataset['action']
    };
    grecaptcha.execute(apiKey, params)
        .then(function(token) {
            input.value = token;
        });
}