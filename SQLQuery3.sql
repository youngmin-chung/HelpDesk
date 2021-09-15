create table Calls(
 Id int identity(100,100) NOT NULL,
 EmployeeId INT NOT NULL,
 ProblemId INT NOT NULL,
 TechId INT NOT NULL, 
 DateOpened SMALLDATETIME NOT NULL,
 DateClosed SMALLDATETIME,
 OpenStatus BIT NOT NULL,
 Notes VARCHAR(250) NOT NULL,
 Timer ROWVERSION,
 CONSTRAINT PK_Call Primary key(id),
 CONSTRAINT FK_CallHasTech foreign key(TechId) references Employees(id),
 CONSTRAINT FK_CallHasEmployee foreign key(EmployeeId) references Employees(id),
 CONSTRAINT FK_CallHasProblem foreign key(ProblemId) references Problems(id),
)
GO