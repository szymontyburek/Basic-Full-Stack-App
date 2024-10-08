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

const createHTML = function (nodeType, parent, innerHTML) {
    let elem = document.createElement(nodeType);
    parent.appendChild(elem);

    if (innerHTML) elem.innerHTML = innerHTML;
    return elem;
}

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
            if (i === 0) createHTML("th", headerTR, tableHeaders[header]);
            createHTML("td", tr, rowObj[header]);
        }
    }
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

        while (writeTable.firstChild) {
            writeTable.removeChild(writeTable.lastChild);
        }

        buildTable(res, writeTable);

        //supply tbx's for user to modify First Name, Last Name, and Department columns

        const tr = writeTable.children[1]; //not the header row, but rather the row with the data
        const cells = tr.children;

        for (let i = 0; i < cells.length; i++) {
            if (i === 0 || i === cells.length) continue; //if current cell is employee id or description

            const cell = cells[i];
            const cellTxt = cell.innerText;
            const cellWidthPreTbx = cell.offsetWidth;
            cell.innerText = "";
            const tbx = createHTML("input", cell);
            tbx.value = cellTxt;
            tbx.style.width = cellWidthPreTbx + "px";
        }

    }, self.previousElementSibling.value)
})

