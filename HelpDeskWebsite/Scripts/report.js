$(function () {
    $('#employeebutton').click(async (e) => {
        try {
            $('#lblstatus').text('generating report on server - please wait...');
            let response = await fetch('api/employeereport');
            if (!response.ok) // check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json(); // this returns a promise, so we await it
            (data === 'report generated')
                ? window.open('/Pdfs/Employee.pdf')
                : $('#lblstatus').text('problem generating report');

        } catch (error) {
            $('#lblstatus').text(error.message);
        }// try/catch
    }); // button click

    $('#callbutton').click(async (e) => {
        try {

            $('#lblstatus').text('generating report on server - please wait...');
            let Callresponse = await fetch('api/callreport');
            if (!Callresponse.ok) // check for response.status
                throw new Error(`Status - ${Callresponse.status}, Text - ${Callresponse.statusText}`);
            let Calldata = await Callresponse.json(); // this returns a promise, so we await it
            (Calldata === 'report generated')
                ? window.open('/Pdfs/Call.pdf')
                : $('#lblstatus').text('problem generating report');

        } catch (error) {
            $('#lblstatus').text(error.message);
        }// try/catch
    }); // button click

}); // jQuery




