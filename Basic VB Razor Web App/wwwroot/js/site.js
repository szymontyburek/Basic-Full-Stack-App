const getRequestBtn = document.getElementById("getRequest");
const readOnlyTable = document.getElementById("readOnlyTable");
const modifyRow = document.getElementById("modifyRow");
const serverPort = "http://localhost:5000/";

const tableHeaders = {
    "id": "Employee ID",
    "firstName": "First Name",
    "lastName": "Last Name",
    "department": "Department",
    "description": "Description"
}

const getEmpInfo = function (querystring, cb) {
    let endpoint = serverPort + "getEmpInfo";
    if (querystring) endpoint += "/" + querystring;

    fetch(endpoint)
        .then((response) => {
            return response.json();
        })
        .then((res) => {
            console.log(res);
            cb(res.data);
        })
        .catch((error) => {
            console.error('There was a problem with the fetch operation:', error);
        });
}

getEmpInfo(false, function (data) {
    let columns = [];

    const headerTR = document.createElement("tr");
    readOnlyTable.appendChild(headerTR);

    for (let i = 0; i < data.length; i++) {
        const rowObj = data[i];
        const headers = Object.keys(rowObj);

        const tr = document.createElement("tr");
        readOnlyTable.appendChild(tr);

        for (const header of headers) {

            if (i === 0) {
                let th = document.createElement("th");
                th.innerHTML = tableHeaders[header];
                headerTR.appendChild(th);
            }

            let HTMLelem = document.createElement("td");
            HTMLelem.innerHTML = rowObj[header];
            tr.appendChild(HTMLelem);
        }
    }
})

getRequestBtn.addEventListener("click", function () {
    getEmpInfo(function (data) {
        debugger;
    })
})
