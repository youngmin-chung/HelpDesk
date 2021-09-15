$(function () {//Employeelist.js
    let getAll = async (msg) => {
        try {
            $('#status').text('Finding Employee Information...');
            let response = await fetch(`api/Employees/`);
            if (!response.ok) // or check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json(); // this returns a promise, so we await it
            buildEmployeeList(data);
            (msg === '') ? // are we appending to an existing message
                $('#status').text('Employees Loaded') : $('#status').text(`${msg} - Employees Loaded`);
        } catch (error) {
            $('#status').text(error.message);
        }
    }// get All

    let clearModalFields = () => {
        $('#TextBoxTitle').val('');
        $('#TextBoxFirstname').val('');
        $('#TextBoxLastname').val('');
        $('#TextBoxPhone').val('');
        $('#TextBoxEmail').val('');
        localStorage.removeItem('Id');
        localStorage.removeItem('DepartmentId');
        localStorage.removeItem('Timer');
    }// clearModalFields

    $('#EmployeeList').click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === 'EmployeeList' || Id === '') {
            Id = e.target.id;
        }// clicked on row somewhere else
        if (Id !== 'status' && Id !== 'heading') {
            let data = JSON.parse(localStorage.getItem('allEmployees'));
            clearModalFields();
            data.map(Employee => {
                if (Employee.Id === parseInt(Id)) {
                    $('#TextBoxTitle').val(Employee.Title);
                    $('#TextBoxFirstname').val(Employee.Firstname);
                    $('#TextBoxLastname').val(Employee.Lastname);
                    $('#TextBoxPhone').val(Employee.Phoneno);
                    $('#TextBoxEmail').val(Employee.Email);
                    localStorage.setItem('Id', Employee.Id);
                    localStorage.setItem('DepartmentId', Employee.DepartmentId);
                    localStorage.setItem('Timer', Employee.Timer);
                    $('#modalstatus').text('update data');
                    $('#theModal').modal('toggle');
                } //if
            }); // data.map
        } else {
            return false;  //ignore if they clicked on heading or status
        }
    }); // EmployeeListClick

    $('#updatebutton').click(async (e) => {
        try {
            emp = new Object();
            emp.Title = $('#TextBoxTitle').val();
            emp.Firstname = $('#TextBoxFirstname').val();
            emp.Lastname = $('#TextBoxLastname').val();
            emp.Phoneno = $('#TextBoxPhone').val();
            emp.Email = $('#TextBoxEmail').val();
            emp.Id = localStorage.getItem('Id');
            emp.DepartmentId = localStorage.getItem('DepartmentId');
            emp.Timer = localStorage.getItem('Timer');
            //send the updated bank to the server asynchronously using PUT
            let response = await fetch('api/Employees', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                body: JSON.stringify(emp)
            });
            if (response.ok)    // or check for response.status
            {
                let data = await response.json();
                $('#status').text(data);
            } else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            }//else
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        }//try/catch

    }); // update button click

    let buildEmployeeList = (data) => {
        $('#EmployeeList').empty();
        div = $(`<div class="list-group-item text-white bg-secondary row d-flex" id="status">Employee Info</div>
                    <div class= "list-group-item row d-flex text-center" id="heading">
                    <div class="col-4 h4">Title</div>
                    <div class="col-4 h4">First</div>
                    <div class="col-4 h4">Last</div>
                    </div>`);
        div.appendTo($('#EmployeeList'))
        localStorage.setItem('allEmployees', JSON.stringify(data));
        data.map(emp => {
            btn = $(`<button class="list-group-item row d-flex" id=${emp.Id}">`);
            btn.html(`<div class="col-4" id=Employeetitle${emp.Id}">${emp.Title}</div>
                      <div class="col-4" id=Employeefname${emp.Id}">${emp.Firstname}</div>
                      <div class="col-4" id=Employeelastnam${emp.Id}">${emp.Lastname}</div>`
            );
            btn.appendTo($('#EmployeeList'));
        }); // map
    } // buildDynamicList

    getAll('');
}); // Jquery ready method
