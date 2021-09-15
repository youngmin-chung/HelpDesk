$(function () {//employeeaddupdate.js
    const getAll = async (msg) => {
        try {
            $('#EmployeeList').html('<h3>Finding Employee Information, please wait...</h3>');
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

    const setupForUpdate = (Id, data) => {
        $('#actionbutton').val('update');
        $('#modaltitle').html('<h4>update Employee</h4>');

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
            } // if
        }); // data.map
    } // setupForUpdate

    const setupForAdd = () => {
        $('#actionbutton').val('add');
        $('#modaltitle').html('<h4>add Employee</h4>');
        $('#theModal').modal('toggle');
        $('#modalstatus').text('add new Employee');
        clearModalFields();
    }// setupForAdd

    const clearModalFields = () => {
        $('#TextBoxTitle').val('');
        $('#TextBoxFirstname').val('');
        $('#TextBoxLastname').val('');
        $('#TextBoxPhone').val('');
        $('#TextBoxEmail').val('');
        localStorage.removeItem('Id');
        localStorage.removeItem('DepartmentId');
        localStorage.removeItem('Timer');
    } // clearModalFields 

    const buildEmployeeList = (data) => {
        $('#EmployeeList').empty();
        div = $(`<div class="list-group-item text-white bg-secondary row d-flex" id="status">Employee Info</div>
                    <div class= "list-group-item row d-flex text-center" id="heading">
                    <div class="col-4 h4">Title</div>
                    <div class="col-4 h4">First</div>
                    <div class="col-4 h4">Last</div>
                    </div>`);
        div.appendTo($('#EmployeeList'))
        localStorage.setItem('allEmployees', JSON.stringify(data));
        btn = $(`<button class="list-group-item row d-flex" id="0"><div class="col-12 text-left">...click to add Employee</div></button>`);
        btn.appendTo($('#EmployeeList'));
        data.map(emp => {
            btn = $(`<button class="list-group-item row d-flex" id="${emp.Id}">`);
            btn.html(`<div class="col-4" id="Employeetitle${emp.Id}">${emp.Title}</div>
                      <div class="col-4" id="Employeefname${emp.Id}">${emp.Firstname}</div>
                      <div class="col-4" id="Employeelastnam${emp.Id}">${emp.Lastname}</div>`
            );
            btn.appendTo($('#EmployeeList'));
        }); // map
    } // buildEmployeeList

    const update = async () => {
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
                //$('#status').text(data);
                getAll(data);
            } else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            }//else
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        }//try/catch
    } // update

    const add = async () => {
        try {
            emp = new Object();
            emp.Title = $('#TextBoxTitle').val();
            emp.Firstname = $('#TextBoxFirstname').val();
            emp.Lastname = $('#TextBoxLastname').val();
            emp.Phoneno = $('#TextBoxPhone').val();
            emp.Email = $('#TextBoxEmail').val();
            emp.DepartmentId = 100;
            emp.Id = -1;
            //send the updated bank to the server asynchronously using PUT
            let response = await fetch('api/Employees', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                body: JSON.stringify(emp)
            });
            if (response.ok)    // or check for response.status
            {
                let data = await response.json();
                //$('#status').text(data);
                getAll(data);
            } else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            }//else
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        }//try/catch
    } // add

    $("#actionbutton").click(() => {
        $("#actionbutton").val() === "update" ? update() : add();
    }); // actionbutton click

    $('#EmployeeList').click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === 'EmployeeList' || Id === '') {
            Id = e.target.id;
        } // clicked on row somewhere else

        if (Id !== 'status' && Id !== 'heading') {
            let data = JSON.parse(localStorage.getItem('allEmployees'));
            Id === '0' ? setupForAdd() : setupForUpdate(Id, data);
        } else {
            return false; // ignore if they clicked on heading or status
        }
    });

    // filter the stored JSON based on srch contents
    const filterData = () => {
        allData = JSON.parse(localStorage.getItem('allemployees'));
        // tilde below same as stu.Lastname.indexOf($('#srch').val > -1)
        let filteredData = allData.filter((stu) => ~stu.Lastname.indexOf($('#srch').val()));
        buildemployeeList(filteredData, false);
    } // filterData

    $('#srch').keyup(filterData); // srch key press
    getAll(''); // first grab the data from the server
}); // Jquery ready method
