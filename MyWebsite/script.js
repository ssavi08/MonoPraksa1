const pcForm = document.getElementById("pc-form");
const pcList = document.getElementById("pc-list");
const pcNameInput = document.getElementById("pcName");
const pcCpuInput = document.getElementById("pcCpu");
const pcGpuInput = document.getElementById("pcGpu");

function getPCs() {
    return JSON.parse(localStorage.getItem("pcs")) || [];
}

function savePCs(pcs) {
    localStorage.setItem("pcs", JSON.stringify(pcs));
}

function displayPCs(){
    const pcs = getPCs();
    const pcList = document.getElementById("pc-list");
    pcList.innerHTML = "";
    
    const table = document.createElement("table");

    const headerRow = document.createElement("tr");
    headerRow.innerHTML = `
        <th>Name</th>
        <th>CPU</th>
        <th>GPU</th>
    `;
    table.appendChild(headerRow);

    pcs.forEach((pc, index) => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${pc.name}</td>
            <td>${pc.cpu}</td>
            <td>${pc.gpu}</td>
            <td><button onclick="deletePC(${index})">Delete</button></td>
        `;
        table.appendChild(row);
    });

    pcList.appendChild(table);
}

pcForm.addEventListener("submit", function (event) {
    const pcs = getPCs();
    pcs.push({
        name: pcNameInput.value,
        cpu: pcCpuInput.value,
        gpu: pcGpuInput.value
    });
    savePCs(pcs);
    pcForm.reset();
    displayPCs();
});

function deletePC(index) {
    if(confirm("Are you sure you want to delete this PC?")){
        const pcs = getPCs();
        pcs.splice(index, 1);
        savePCs(pcs);
        displayPCs();
    }
}

displayPCs();