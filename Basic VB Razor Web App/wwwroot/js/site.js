const getRequestBtn = document.getElementById("getRequest");
const readOnlyTable = document.getElementById("readOnlyTable");
const writeTable = document.getElementById("writeTable");
const serverPort = "http://localhost:5000/";

const tableHeaders = {
    "id": "Employee ID",
    "firstName": "First Name",
    "lastName": "Last Name",
    "department": "Department",
    "description": "Description"
}

const getEmpInfo = function (cb) {
    let endpoint = serverPort + "getEmpInfo?id=";
    if (arguments.length > 1) endpoint += arguments[1];
    else endpoint += -1

    fetch(endpoint)
        .then((response) => {
            return response.json();
        })
        .then((res) => {
            console.log(res);
            cb(res);
        })
        .catch((error) => {
            console.error('There was a problem with the fetch operation:', error);
        });
}

getEmpInfo(function (res) {
    if (!res.success) return;
    buildTable(res, readOnlyTable);
})

getRequestBtn.addEventListener("click", function () {
    const self = this;
    getEmpInfo(function (res) {
        if (!res.success) {
            alert(res.message);
            return;
        }

        buildTable(res, writeTable);

    }, self.previousElementSibling.value)
})

const buildTable = function (res, table) {
    const data = res.data;
    const headerTR = document.createElement("tr");
    table.appendChild(headerTR);

    for (let i = 0; i < data.length; i++) {
        const rowObj = data[i];
        const headers = Object.keys(rowObj);

        const tr = document.createElement("tr");
        table.appendChild(tr);

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
}
