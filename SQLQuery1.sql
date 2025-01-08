CREATE DATABASE final;


USE final;


CREATE TABLE Students (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Grade NVARCHAR(10) NOT NULL,
    Subject NVARCHAR(50) NOT NULL,
    Marks DECIMAL(5,2) NOT NULL CHECK (Marks >= 0 AND Marks <= 100),
    Attendance DECIMAL(5,2) NOT NULL CHECK (Attendance >= 0 AND Attendance <= 100)
);


INSERT INTO Students (Name, Grade, Subject, Marks, Attendance) VALUES
('Arsal', '9th', 'Mathematics', 85.5, 92.0),
('Mahad', '9th', 'Science', 78.0, 88.5),
('Fasih', '10th', 'Mathematics', 92.5, 95.0),
('Wasay', '10th', 'English', 88.0, 91.0),
('Abdullah', '11th', 'Physics', 76.5, 85.0),
('Sarib', '11th', 'Chemistry', 90.0, 94.5),
('Hanzala', '12th', 'Biology', 82.5, 89.0),
('Moiz', '12th', 'Mathematics', 95.0, 97.0),
('Talha', '9th', 'History', 79.0, 86.5),
('Mahateer', '10th', 'Geography', 87.5, 93.0);

