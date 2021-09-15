$(function () { //lab10

    $('#getbutton').click(async (e) => { // get button click event handler
        try {
            let lastname = $('#TextBoxFindLastname').val();
            $('#status').text('please wait...');
            $('#theModal').modal('toggle'); // pop the modal
            let response = await fetch(`api/Employees/${lastname}`);
            if (!response.ok) // or check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json(); //this returns a promise, so we await it
            if (data.Lastname !== 'not found') {
                $('#TextBoxEmail').val(data.Email);
                $('#TextBoxTitle').val(data.Title);
                $('#TextBoxFirstname').val(data.Firstname);
                $('#TextBoxLastname').val(data.Lastname);
                $('#TextBoxPhone').val(data.Phoneno);
                $('#status').text('Employee found');
                localStorage.setItem('Id', data.Id);
                localStorage.setItem('DepartmentId', data.DepartmentId);
                localStorage.setItem('Timer', data.Timer);
            } else {
                $('#TextBoxEmail').val();
                $('#TextBoxTitle').val();
                $('#TextBoxFirstname').val();
                $('#TextBoxLastname').val();
                $('#TextBoxPhone').val();
                $('#status').text('not such Employee');
            }//else
        } catch (error) {
            $('#status').text(error.message);
        }//try/catch
    }); // get button click

    $('#updatebutton').click(async (e) => { // get button click event handler
        try {
            //set up a new client side instance of Employee
            stu = new Object();
            //populate the properties
            stu.Title = $('#TextBoxTitle').val();
            stu.Firstname = $('#TextBoxFirstname').val();
            stu.Lastname = $('#TextBoxLastname').val();
            stu.Phoneno = $('#TextBoxPhone').val();
            stu.Email = $('#TextBoxEmail').val();
            // we stored these 3 earlier
            stu.Id = localStorage.getItem('Id');
            stu.DepartmentId = localStorage.getItem('DepartmentId');
            stu.Timer = localStorage.getItem('Timer');

            //send the updated bank to the server asynchronously using PUT
            let response = await fetch('api/Employees', {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json; charset=utf-8' },
                body: JSON.stringify(stu)
            });

            if (response.ok)    // or check for response.status
            {
                let data = await response.json();
                $('#status').text(data);
            } else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            }//else
        } catch (error) {
            $('#status').text(error.message);
        }//try/catch
    }); // update button click
}); // Jquery