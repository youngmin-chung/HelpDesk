
$(function () {    //call.js

    //validate
    $("#CallModalForm").validate({
        rules: {
            ddlPros: { required: true },
            ddlEmps: { required: true },
            ddlTechs: { required: true },
            textAreaNotes: { maxlength: 250, minlength: 1, required: true }
        },
        errorElement: "div",
        messages: {
            ddlPros: {
                required: "select Problem"
            },
            ddlEmps: {
                required: "select Employee"
            },
            ddlTechs: {
                required: "select Tech"
            },
            textAreaNotes: {
                required: "required 1-250 chars.", minlength: "required 1-250 chars.", maxlength: "required 1-250 chars."
            }
        }
    });


    //getall()
    const getAll = async (msg) => {
        try {
            $('#status').html('<h3>Observing Call Information, please wait...</h3>');
            let response = await fetch(`api/calls/`);
            if (!response.ok) // check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json(); // this returns a promise, so we await it
            buildCallList(data, true);
            (msg === '') ? // appending to an existing message
                $('#status').text('Call Loaded') : $('#status').text(`${msg} - Call Loaded`);

            //get employee information
            response = await fetch(`api/employees/`);
            if (!response.ok) // check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let emps = await response.json();
            localStorage.setItem('allemployees', JSON.stringify(emps));

            //get problem information
            response = await fetch(`api/problems/`);
            if (!response.ok) // check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let pros = await response.json();
            localStorage.setItem('allproblems', JSON.stringify(pros));



        } catch (error) {
            $('#status').text(error.message);
        }
    };// end getAll()

    //loadProblemDDL : populate the html select with options
    const loadProblemDDL = (prodiv) => { // Problem 
        html = '';
        $('#ddlPros').empty();
        let allproblems = JSON.parse(localStorage.getItem('allproblems'));
        allproblems.map(pro => html += `<option value="${pro.Id}">${pro.Description}</option>`);
        $('#ddlPros').append(html);
        $('#ddlPros').val(prodiv);
    }; // end loadProblemDDL


    //loadEmployeeDDL : populate the html select with options
    const loadEmployeeDDL = (empdiv) => { // Employee 
        html = '';
        $('#ddlEmps').empty();
        let allemployees = JSON.parse(localStorage.getItem('allemployees'));
        allemployees.map(emp => html += `<option value="${emp.Id}">${emp.Lastname}</option>`);
        $('#ddlEmps').append(html);
        $('#ddlEmps').val(empdiv);
    }; // end loadEmployeeDDL


    //loadTechnicianDDL : populate the html select with options
    const loadTechnicianDDL = (tech) => {  // Technician 
        html = '';
        $('#ddlTechs').empty();
        let alltechnicians = JSON.parse(localStorage.getItem('alltechnicians'));
        alltechnicians.map(tech => html += `<option value="${tech.Id}">${tech.Lastname}</option>`);
        $('#ddlTechs').append(html);
        $('#ddlTechs').val(tech);
    };// end loadTechnicianDDL


    //filter the data on search contents
    const filterData = () => {
        allData = JSON.parse(localStorage.getItem('allcalls'));
        let filteredData = allData.filter((cal) => ~cal.EmployeeName.indexOf($('#srch').val()));
        buildCallList(filteredData, false);
    }; //end filterData()


    //buildCallList() : create call list dynamically
    const buildCallList = (data, allcalls) => {
        $('#callList').empty();
        div = $(`<div class="list-group-item text-black row d-flex" id="status" >Call Info</div>
                <div class= "list-group-item row d-flex text-center" id="heading">
                <div class="col-4 h4">Date</div>
                <div class="col-4 h4">For</div>
                <div class="col-4 h4">Problem</div>
                </div>`);
        div.appendTo($('#callList'))
        //check allcalls if it is true set Item to JSON - as allcalls OR not
        allcalls ? localStorage.setItem('allcalls', JSON.stringify(data)) : null;

        //add button here
        btn = $(`<button class="list-group-item row d-flex" id="0" style="background-color: rgba(255,140,0,0.3);><div class="col-12 text-left">...click to add call </div></button>`);
        btn.appendTo($('#callList'));

        //make a call list in html
        data.map(cal => {
            btn = $(`<button style="background-color: rgba(140,100,0,0.3);"class="list-group-item row d-flex" id="${cal.Id}">`);
            btn.html(`<div class="col-4" id="calldate${cal.Id}">${formatDate(cal.DateOpened)}</div>
                      <div class="col-4" id="callemployeename${cal.Id}">${cal.EmployeeName}</div>
                      <div class="col-4" id="callproblem${cal.Id}">${cal.ProblemDescription}</div>`
            );
            btn.appendTo($('#callList'));
        });
    };  //end buildCallList()


    //clearModalFields
    const clearModalFields = () => {
        $('#ddlPros').val('');
        $('#ddlEmps').val('');
        $('#ddlTechs').val('');
        $('#labelDateOpened').text('');
        $('#dateOpened').val('');
        $('#labelDateClosed').text('');
        $('#dateClosed').val('');
        $('#modalstatus').text('');

        $('#checkBoxClose').prop('checked', false);
        $('#textAreaNotes').val('');
        localStorage.removeItem('Id');
        localStorage.removeItem('Timer');

        $('#ddlPros').prop('disabled', false);
        $('#ddlEmps').prop('disabled', false);
        $('#ddlTechs').prop('disabled', false);
        $('#textAreaNotes').attr('readonly', false);
        $('#checkBoxClose').prop('disabled', false);
        $('#actionbutton').show(); // show add/update button
        $('#deletebutton').show(); // show delete button

        // delete validate information
        let validator = $('#CallModalForm').validate();
        validator.resetForm();

    }; //end clearModalFields()

    // get date information 
    const formatDate = (date) => {
        let d;
        (date === undefined) ? d = new Date() : d = new Date(Date.parse(date));
        let _day = d.getDate();
        let _month = d.getMonth() + 1;
        let _year = d.getFullYear();
        let _hour = d.getHours();
        let _min = d.getMinutes();
        let _sec = d.getMinutes();
        if (_min < 10) { _min = "0" + _min; }
        if (_sec < 10) { _sec = "0" + _sec; }
        return _year + "-" + _month + "-" + _day + " " + _hour + ":" + _min + ":" + _sec;
    }; //end formatDate


    //setupForAdd()
    const setupForAdd = () => {
        $('#actionbutton').val('add');
        $('#modaltitle').html('<h4>Add/Change Call Information</h4>');
        $('#theModal').modal('toggle');
        $('#modalstatus').text('add new call');
        clearModalFields();  //clear modal
        $('#deletebutton').hide(); //  hide delete button

        loadProblemDDL(-1); //pass the number -1 : to preset empty for dropdown
        loadEmployeeDDL(-1);
        loadTechnicianDDL(-1);
        //set current date and time to DateOpened
        $('#labelDateOpened').text(formatDate());
        $('#dateOpened').val(formatDate());

    }; //end setupForAdd()


    //set up data for update
    const setupForUpdate = (Id, data) => {
        $('#actionbutton').val('update');
        $('#modaltitle').html('<h4>Add/Change Call Information</h4>');

        clearModalFields();

        data.map(call => {
            if (call.Id === parseInt(Id)) {
                $('#ddlPros').val(call.ProblemDescription);
                $('#ddlEmps').val(call.EmployeeName);
                $('#ddlTechs').val(call.TechId);
                $('#textAreaNotes').val(call.Notes);

                loadEmployeeDDL(call.EmployeeId);
                loadProblemDDL(call.ProblemId);
                loadTechnicianDDL(call.TechId);

                $('#labelDateOpened').text(formatDate(call.DateOpened));
                $('#dateOpened').val(call.DateOpened);
                //  check if the call is open
                if (!call.OpenStatus) {
                    $('#labelDateClosed').text(formatDate(call.DateClosed));
                    $('#dateClosed').val(call.DateClosed);
                    $('#checkBoxClose').prop('checked', true);
                    $('#checkBoxClose').prop('disabled', true);
                    $('#ddlPros').prop('disabled', true);
                    $('#ddlEmps').prop('disabled', true);
                    $('#ddlTechs').prop('disabled', true);
                    $('#textAreaNotes').attr('readonly', 'readonly');
                    $('#actionbutton').hide();
                }
                localStorage.setItem('Id', call.Id);
                localStorage.setItem('Timer', call.Timer);

                $('#modalstatus').text('update data');
                $('#theModal').modal('toggle');
            }
        });
    }; //setupForUpdate


    //add function
    const add = async () => {
        try {
            //create new object and set data from the textbox and select
            cal = new Object();
            cal.ProblemId = $('#ddlPros').val();
            cal.EmployeeId = $('#ddlEmps').val();
            cal.TechId = $('#ddlTechs').val();
            cal.DateOpened = $('#dateOpened').val();
            cal.DateClosed = $('#dateClosed').val();

            if ($('#checkBoxClose').is(':checked')) {
                cal.OpenStatus = false;
            }
            else {
                cal.OpenStatus = true;
            }

            cal.Notes = $('#textAreaNotes').val();

            cal.Id = -1;  //hard code it server overwrites it when the data is inserted into the db

            //send the updated back to the server asynchronusly using PUT
            let response = await fetch('api/calls', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                body: JSON.stringify(cal)
            });

            if (response.ok) {
                let data = await response.json();
                getAll(data);
            } else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            } //else
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        }// try/catch
    }; //add



    //update function
    const update = async () => {
        try {
            cal = new Object();
            cal.ProblemId = $('#ddlPros').val();
            cal.EmployeeId = $('#ddlEmps').val();
            cal.TechId = $('#ddlTechs').val();
            cal.DateOpened = $('#dateOpened').val();
            cal.DateClosed = $('#dateClosed').val();

            if ($('#checkBoxClose').is(':checked')) {
                cal.OpenStatus = false;
            }
            else {
                cal.OpenStatus = true;
            }

            cal.Notes = $('#textAreaNotes').val();
            // get call id
            cal.Id = localStorage.getItem('Id');
            // get call timer
            cal.Timer = localStorage.getItem('Timer');

            //send the updated back to the server asynchronusly using PUT
            let response = await fetch('api/calls', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                body: JSON.stringify(cal)
            });

            if (response.ok) {
                let data = await response.json();
                getAll(data);
            } else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            } //else
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        }	// try/catch
    }; //update


    //delete function
    let _delete = async () => {
        try {
            let response = await fetch(`api/calls/${localStorage.getItem('Id')}`, {
                method: 'DELETE',
                headers: {
                    'Content-type': 'application/json; charset=utf-8'
                }
            });
            if (response.ok) //or check for response.status
            {
                let data = await response.json();
                getAll(data);
            } else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            } //else
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        } // try/catch
    }; //end delete()


    //if user click the call list 
    $('#callList').click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === 'callList' || Id === '') {
            Id = e.target.id;
        } //clicked on row somewhere else

        //If Id is a 0 doing an add, > 0 doing an update
        if (Id !== 'status' && Id !== 'heading') {
            let data = JSON.parse(localStorage.getItem('allcalls'));
            Id === '0' ? setupForAdd() : setupForUpdate(Id, data);
        } else {
            return false; //ignore if they clicked on heading or status
        }
    });

    $('#actionbutton').click((e) => {
        if ($('#CallModalForm').valid()) {
            $('#actionbutton').val() === "Update" ? update() : add();
        } else {
            $('#modalstatus').text('Fix Errors');
            e.preventDefault;
        }
    }); //actionbutton click

    //if user push delete button, the confirmation popup show up
    $('[data-toggle=confirmation]').confirmation({ rootSelector: '[data-toggle=confirmation]' });

    //if yes was chosen, call delete function
    $('#deletebutton').click(() => _delete());


    $('#srch').keyup(filterData); //srch key press


    //checkBoxClose
    $('#checkBoxClose').click(() => {
        if ($('#checkBoxClose').is(':checked')) {
            $('#labelDateClosed').text(formatDate());
            $('#dateClosed').val(formatDate());
        }
        else {
            $('#labelDateClosed').text('');
            $('#dateClosed').val('');
        }
    }); //end checkBoxClose


    getAll('');  //get all the data from the server

}); // end jQuery