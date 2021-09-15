$(function () {  // employee.js

    $("#EmployeeModalForm").validate({
        rules: {
            TextBoxTitle: { maxlength: 4, required: true, validTitle: true },
            TextBoxFirstname: { maxlength: 25, required: true },
            TextBoxLastname: { maxlength: 25, required: true },
            TextBoxEmail: { maxlength: 40, required: true, email: true },
            TextBoxPhone: { maxlength: 15, required: true },
            ddlDeps: { required: true }
        },
        errorElement: "div",
        messages: {
            TextBoxTitle: {
                required: "required 1-4 chars.", maxlength: "required 1-4 chars.", validTitle: "Mr. Ms. Mrs. or Dr."
            },
            TextBoxFirstname: {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxLastname: {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxPhone: {
                required: "required 1-15 chars.", maxlength: "required 1-15 chars."
            },
            TextBoxEmail: {
                required: "required 1-40 chars.", maxlength: "required 1-40 chars.", email: "need vaild email format"
            },
            ddlDeps: {
                required: "select Department"
            }
        }
    });

    $.validator.addMethod("validTitle", function (value, element) { // custom rule
        return this.optional(element) || (value == "Mr." || value == "Ms." || value == "Mrs." || value == "Dr.");
    }, "");


    const getAll = async (msg) => {
        try {

            $('#employeeList').html('<h3>Fiding Employee Information, please wait..</h3>');
            let response = await fetch(`api/Employees/`);
            if (!response.ok) // or check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json(); // this returns a promise, so we await it
            buildEmployeeList(data, true); // call the buildEmployeeList function build employeelist
            (msg === '') ? // are we appending to an existing messgae
                $('#status').text('Employees Loaded') : $('#status').text(`${msg} - Employees Loaded`);
            response = await fetch(`api/departments/`);
            if (!response.ok)// or check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let divs = await response.json(); // this returns a promise, so we await it
            localStorage.setItem('alldepartments', JSON.stringify(divs));


        } catch (error) {
            $('#status').text(error.message);
        }
    }// get All

    const loadDepartmentDDL = (empdiv) => {  // department 
        html = '';
        $('#ddlDeps').empty();
        let alldepartments = JSON.parse(localStorage.getItem('alldepartments'));
        alldepartments.map(div => html += `<option value="${div.Id}">${div.DepartmentName}</option>`);
        $('#ddlDeps').append(html);
        $('#ddlDeps').val(empdiv);
    }//loadDivisionDDL

    const setupForUpdate = (Id, data) => {
        $('#actionbutton').val('update');
        $('#modaltitle').html('<h4>update employee</h4>');

        clearModalFields();  // clear the fields
        data.map(employee => {
            if (employee.Id === parseInt(Id)) {
                $('#TextBoxTitle').val(employee.Title);
                $('#TextBoxFirstname').val(employee.Firstname);
                $('#TextBoxLastname').val(employee.Lastname);
                $('#TextBoxPhone').val(employee.Phoneno);
                $('#TextBoxEmail').val(employee.Email);
                $('#ImageHolder').html(`<img height="120" width="110" src="data:image/png;base64,${employee.StaffPicture64}"/>`)
                localStorage.setItem('Id', employee.Id);
                localStorage.setItem('DepartmentId', employee.DepartmentId);
                localStorage.setItem('StaffPicture', employee.StaffPicture64);
                localStorage.setItem('Timer', employee.Timer);
                $('#modalstatus').text('update data');
                loadDepartmentDDL(employee.DepartmentId.toString());
                $('#theModal').modal('toggle');
            }// if
        }); // data.map
    }// setupForUpdate

    const setupForAdd = () => {
        $('#actionbutton').val('add');
        $('#modaltitle').html('<h4>add employee</h4>');
        $('#theModal').modal('toggle');
        $('#modalstatus').text('add new employee');
        loadDepartmentDDL(-1);
        clearModalFields();
    }// setupForAdd

    let clearModalFields = () => {
        $('#TextBoxTitle').val('');
        $('#TextBoxFirstname').val('');
        $('#TextBoxLastname').val('');
        $('#TextBoxPhone').val('');
        $('#TextBoxEmail').val('');
        localStorage.removeItem('Id');
        localStorage.removeItem('DepartmentId');
        localStorage.removeItem('Timer');
        localStorage.removeItem('StaffPicture');


    }//  clearModalFields

    const buildEmployeeList = (data, allemployees) => {  // build the employee list 
        $('#EmployeeList').empty();
        div = $(`<div class="list-group-item text-black row d-flex" id="status" >Employee Info</div>
                   <div class="list-group-item row d-flex text-center" id="heading">
                   <div class="col-4 h4">Title</div>
                   <div class="col-4 h4">First</div>
                   <div class="col-4 h4">Last</div>
                </div>`);
        div.appendTo($('#EmployeeList'));
        allemployees ? localStorage.setItem('allemployees', JSON.stringify(data)) : null;
        btn = $(`<button class="list-group-item row d-flex" id="0" style="background-color: rgba(255,140,0,0.3);><div class="col-12 text-left">...click to add employee</div></button>`);
        btn.appendTo($('#EmployeeList'));
        data.map(emp => {
            btn = $(`<button style="background-color: rgba(140,100,0,0.3);"class="list-group-item row d-flex" id="${emp.Id}">`);
            btn.html(`<div class="col-4" id="employeetitle${emp.Id}">${emp.Title}</div>
                      <div class="col-4" id="employeefname${emp.Id}">${emp.Firstname}</div>
                      <div class="col-4" id="employeelastnam${emp.Id}">${emp.Lastname}</div>`
            );
            btn.appendTo($('#EmployeeList'));
        });//map
    }//buildEmployeeList

    const update = async () => {  // update function
        try {
            emp = new Object();
            emp.Title = $('#TextBoxTitle').val();
            emp.Firstname = $('#TextBoxFirstname').val();
            emp.Lastname = $('#TextBoxLastname').val();
            emp.Phoneno = $('#TextBoxPhone').val();
            emp.Email = $('#TextBoxEmail').val();
            emp.Id = localStorage.getItem('Id');
            emp.DepartmentId = $('#ddlDeps').val();
            emp.Timer = localStorage.getItem('Timer');
            localStorage.getItem('StaffPicture')
                ? emp.StaffPicture64 = localStorage.getItem('StaffPicture')
                : null;
            //send the updated back to the server asynchronously using PUT
            let response = await fetch('api/Employees', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                body: JSON.stringify(emp)
            });
            if (response.ok)//or check for response.status
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
    }// update

    const add = async () => {  // add function
        try {
            emp = new Object();
            emp.Title = $('#TextBoxTitle').val();
            emp.Firstname = $('#TextBoxFirstname').val();
            emp.Lastname = $('#TextBoxLastname').val();
            emp.Phoneno = $('#TextBoxPhone').val();
            emp.Email = $('#TextBoxEmail').val();
            emp.DepartmentId = $('#ddlDeps').val();
            emp.Id = -1;
            localStorage.getItem('StaffPicture')
                ? emp.StaffPicture64 = localStorage.getItem('StaffPicture')
                : null;
            //send the employee info to the server asynchronously using POST
            let response = await fetch('api/Employees', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                body: JSON.stringify(emp)
            });
            if (response.ok)//or check for response.status
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
    }// add

    $("#actionbutton").click((e) => {
        if ($("#EmployeeModalForm").valid()) {
            $("#actionbutton").val() === "update" ? update() : add();
        } else {
            $("#modalstatus").text("Fix Errors");
            e.preventDefault;
        }

    });//actionbutton click

    $('[data-toggle=confirmation]').confirmation({ rootSelector: '[data-toggle=confirmation]' });

    $('#deletebutton').click(() => _delete());// if yes was chosen //deletebutton click

    $('#EmployeeList').click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === 'EmployeeList' || Id === '') {
            Id = e.target.id;

        }// clicked on row somewhere else

        let validator = $('#EmployeeModalForm').validate();
        validator.resetForm();

        if (Id !== 'status' && Id !== 'heading') {
            let data = JSON.parse(localStorage.getItem('allemployees'));
            Id === '0' ? setupForAdd() : setupForUpdate(Id, data);
        } else {
            return false// ignore if they clickd on heading or status
        }
    });

    const _delete = async () => {  // delete function
        try {
            let response = await fetch(`api/Employees/${localStorage.getItem('Id')}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            });
            if (response.ok) //or check for response.status
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



    //filter the stored JSON based on srch contents
    const filterData = () => {
        allData = JSON.parse(localStorage.getItem('allemployees'));
        //tilde below same as emp.Lastname.indexOf($('#srch').val > -1)
        let filteredData = allData.filter((emp) => ~emp.Lastname.indexOf($('#srch').val()));
        buildEmployeeList(filteredData, false);
    }//filterData

    $('#srch').keyup(filterData); // srch key press

    $('input:file').change(() => {
        const reader = new FileReader();
        // FileReader: JavaScript object we use to process the student graphic files
        const file = $('#fileUpload')[0].files[0];

        file ? reader.readAsBinaryString(file) : null;
        //readAsBinaryString: we execute to actually read the graphic files

        reader.onload/*onload: event fired when the file is read*/ = (readerEvt) => {
            // get binary data then convert to encoded string
            const binaryString = reader.result;
            // btoa: intrinsic JavaScript function converts binary data read in
            // from the method in Q6 to an encoded string
            const encodedString = btoa(binaryString);
            localStorage.setItem('StaffPicture', encodedString);
        }
    }); // input file change 

    getAll('');//first grab the data from the server
});// jQuery ready method