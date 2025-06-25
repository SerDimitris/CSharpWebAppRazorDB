USE StudentsDB;
GO

CREATE TABLE Students (
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Firstname NVARCHAR(50) NOT NULL,
	Lastname NVARCHAR(50) NOT NULL,
)
GO

INSERT INTO Students (Firstname, Lastname)
VALUES
	('Dimitris' , 'Serafeimidis'),
	('Katerina' , 'Serafeimidi'),
	('Maria' , 'Zacharopoulou');