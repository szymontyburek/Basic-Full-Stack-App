const getRequestBtn = document.getElementById("getRequest");
const serverPort = "http://localhost:5000";

getRequestBtn.addEventListener("click", function () {
    fetch(serverPort)
        .then((response) => {
            return response.json();
        })
        .then((data) => {
            console.log(data);
        })
        .catch((error) => {
            console.error('There was a problem with the fetch operation:', error);
        });

})
