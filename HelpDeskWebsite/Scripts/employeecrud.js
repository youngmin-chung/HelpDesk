$(function () {//employeecrud.js
    const getAll = async (msg) => {
        try {
            $('#employeeList').html('<h3>Finding employee Information, please wait...</h3>');
            let response = await fetch(`api/employees/`);
            if (!response.ok) // or check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json(); // this returns a promise, so we await it
            buildemployeeList(data, true);
            (msg === '') ? // are we appending to an existing message
                $('#status').text('employees Loaded') : $('#status').text(`${msg} - employees Loaded`);
            //    response = await fetch(`api/departments/`);
            //    if (!response.ok) //or check for response.status
            //        throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            //    let divs = await response.json(); // this returns a promise, so we await it
            //    localStorage.setItem('alldepartments', JSON.stringify(divs));
        } catch (error) {
            $('#status').text(error.message);
        }
    }// get All

    const loaddepartmentDDL = (studiv) => {
        html = '';
        $('#ddlDivs').empty();
        let alldepartments = JSON.parse(localStorage.getItem('alldepartments'));
        alldepartments.map(div => html += `<option value="${div.Id}">${div.Name}</option>`);
        $('#ddlDivs').append(html);
        $('#ddlDivs').val(studiv);
    }// loaddepartmentDDL

    // filter the stored JSON based on srch contents
    const filterData = () => {
        allData = JSON.parse(localStorage.getItem('allemployees'));
        // tilde below same as stu.Lastname.indexOf($('#srch').val > -1)
        let filteredData = allData.filter((stu) => ~stu.Lastname.indexOf($('#srch').val()));
        buildemployeeList(filteredData, false);
    } // filterData

    const setupForUpdate = (Id, data) => {
        $('#actionbutton').val('update');
        $('#modaltitle').html('<h4>update employee</h4>');

        clearModalFields();
        data.map(employee => {
            if (employee.Id === parseInt(Id)) {
                $('#TextBoxTitle').val(employee.Title);
                $('#TextBoxFirstname').val(employee.Firstname);
                $('#TextBoxLastname').val(employee.Lastname);
                $('#TextBoxPhone').val(employee.Phoneno);
                $('#TextBoxEmail').val(employee.Email);
                localStorage.setItem('Id', employee.Id);
                localStorage.setItem('departmentId', employee.departmentId);
                localStorage.setItem('Timer', employee.Timer);
                $('#modalstatus').text('update data');
                loaddepartmentDDL(employee.departmentId.toString());
                $('#theModal').modal('toggle');
            } // if
        }); // data.map
    } // setupForUpdate

    const setupForAdd = () => {
        $('#actionbutton').val('add');
        $('#modaltitle').html('<h4>add employee</h4>');
        $('#theModal').modal('toggle');
        $('#modalstatus').text('add new employee');
        loaddepartmentDDL(-1);
        clearModalFields();
    }// setupForAdd

    const clearModalFields = () => {
        $('#TextBoxTitle').val('');
        $('#TextBoxFirstname').val('');
        $('#TextBoxLastname').val('');
        $('#TextBoxPhone').val('');
        $('#TextBoxEmail').val('');
        localStorage.removeItem('Id');
        localStorage.removeItem('departmentId');
        localStorage.removeItem('Timer');
    } // clearModalFields 

    const buildemployeeList = (data, allemployees) => {
        $('#employeeList').empty();
        div = $(`<div class="list-group-item text-white bg-secondary row d-flex" id="status">employee Info</div>
                    <div class= "list-group-item row d-flex text-center" id="heading">
                    <div class="col-4 h4">Title</div>
                    <div class="col-4 h4">First</div>
                    <div class="col-4 h4">Last</div>
                    </div>`);
        div.appendTo($('#employeeList'));
        allemployees ? localStorage.setItem('allemployees', JSON.stringify(data)) : null;
        btn = $(`<button class="list-group-item row d-flex" id="0"><div class="col-12 text-left">...click to add employee</div></button>`);
        btn.appendTo($('#employeeList'));
        data.map(stu => {
            btn = $(`<button class="list-group-item row d-flex" id="${stu.Id}">`);
            btn.html(`<div class="col-4" id="employeetitle${stu.Id}">${stu.Title}</div>
                      <div class="col-4" id="employeefname${stu.Id}">${stu.Firstname}</div>
                      <div class="col-4" id="employeelastnam${stu.Id}">${stu.Lastname}</div>`
            );
            btn.appendTo($('#employeeList'));
        }); // map
    } // buildemployeeList

    const update = async () => {
        try {
            stu = new Object();
            stu.Title = $("#TextBoxTitle").val();
            stu.Firstname = $("#TextBoxFirstname").val();
            stu.Lastname = $("#TextBoxLastname").val();
            stu.Phoneno = $("#TextBoxPhone").val();
            stu.Email = $("#TextBoxEmail").val();
            stu.Id = localStorage.getItem("Id");
            stu.departmentId = $('#ddlDivs').val();
            stu.Timer = localStorage.getItem('Timer');
            //send the updated bank to the server asynchronously using PUT
            let response = await fetch('api/employees', {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json; charset=utf-8' },
                body: JSON.stringify(stu)
            });
            if (response.ok)    // or check for response.status
            {
                let data = await response.json();
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
            stu = new Object();
            stu.Title = $("#TextBoxTitle").val();
            stu.Firstname = $("#TextBoxFirstname").val();
            stu.Lastname = $("#TextBoxLastname").val();
            stu.Phoneno = $("#TextBoxPhone").val();
            stu.Email = $("#TextBoxEmail").val();
            stu.departmentId = $('#ddlDivs').val(); // hard code it for now, we'll add a dropdown later
            stu.Id = -1;
            //send the updated bank to the server asynchronously using POST
            let response = await fetch('api/employees', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                body: JSON.stringify(stu)
            });
            if (response.ok)    // or check for response.status
            {
                let data = await response.json();
                getAll(data);
            } else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            }//else
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        }//try/catch
    } // add

    let _delete = async () => {
        try {
            let response = await fetch(`api/employees/${localStorage.getItem('Id')}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            });
            if (response.ok) // or check for response.status
            {
                let data = await response.json();
                getAll(data);
            } else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            }//else
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        }
    }

    $("#actionbutton").click(() => {
        $("#actionbutton").val() === "update" ? update() : add();
    }); // actionbutton click

    $('[data-toggle=confirmation]').confirmation({ rootSelector: '[data-toggle=confirmation]' });

    $('#deletebutton').click(() => _delete()); // deletebutton click

    $('#employeeList').click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === 'employeeList' || Id === '') {
            Id = e.target.id;
        } // clicked on row somewhere else

        if (Id !== 'status' && Id !== 'heading') {
            let data = JSON.parse(localStorage.getItem('allemployees'));
            Id === '0' ? setupForAdd() : setupForUpdate(Id, data);
        } else {
            return false; // ignore if they clicked on heading or status
        }
    });

    $('#srch').keyup(filterData); // srch key press

    getAll(''); // first grab the data from the server
}); // Jquery ready method
