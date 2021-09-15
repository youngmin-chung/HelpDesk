# Help Desk - YC C# /w jQuery,JavaScript Project

"Any problems on your devices in the office? Please open ticket on Help Desk!!!"


## Getting Started

In order to get started with this project, you will need to run it on Visusal Studio 2019 or similar IDE like Visual Studio Community. Then navigate to `http://localhost:2580/` or to different port number to view the help desk web app. 

### Prerequisites

What you need to install:

__Client: houses the traditional Presentation layer__
- Web Pages: HTML, CSS, JavaScript /w Jquary, Bootstrap 4, 

__Sever: houses the traditional Business layer__
- ASP .NET Framework 4.8
  * Controllers (Web API)
  * ViewModel Objects (C# .dll)
  * Entity and Model Objects (C# .dll) - Data Access Layer

__Data__
- SQL Server Tables

### Installing

Clone project and make sure to follow next stop in Visual Studio.
- Create local DB to `((LocalDB)\MSSQLLocalDB` on SQL Server Object Exploer

- Open Package Manager Console: 
   insatll NuGet Packages
  - `bootstrap`, `BouncyCastle`
  - `EntityFramework`, `iTextSharp`
  - `jQuery`, `jQuery.Validation`
  - `Newtonsoft.Json`, `popper.js`
  
   Enter `add-migration first` and then `update-database`

## Deployment

To Be Deployed on Azure

## Versioning

Help Desk version 0.0.1

## Authors

* Youngmin Chung: C# | ASP.NET Framwork 4.8 | JQuery | HTML/CSS/JS | Bootstrap 4 | SQL



## License

This project is licensed under the YC License

## Acknowledgments

* Learning C# with the latest ASP.NET Framework to create help desk web application
* Understanding Client/Server Framework with manupulating Data and testing CRUD model 
* Professors in Fanshawe College, family, and friends for their sincere support 
* All people who posted neccessary inforamtion about this application on StackOverFlow and their support and suggestions



## App Flow

__Welcome Page! - The first page of Helpdesk web app__
!["Welcome"](https://github.com/youngmin-chung/capture/blob/master/HD_welcome.PNG)

__Employee list / Employee Modal__
!["Employee"](https://github.com/youngmin-chung/capture/blob/master/HD_emp.PNG)

!["Modal"](https://github.com/youngmin-chung/capture/blob/master/HD_emp_modal.PNG)

__Call(ticket) list / Modal __
!["Call"](https://github.com/youngmin-chung/capture/blob/master/HD_call_list.PNG)

!["Call Modal"](https://github.com/youngmin-chung/capture/blob/master/HD_call_modal.PNG)

!["Call Modal Close"](https://github.com/youngmin-chung/capture/blob/master/HD_call_modal_close.PNG)

__Report list / PDF sample __
!["Report"](https://github.com/youngmin-chung/capture/blob/master/HD_report_list.PNG)

!["PDF 1"](https://github.com/youngmin-chung/capture/blob/master/HD_report_emp.PNG)

!["PDF 2"](https://github.com/youngmin-chung/capture/blob/master/HD_report_call.PNG)
