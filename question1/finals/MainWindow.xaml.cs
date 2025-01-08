using System;
using System.Windows;

namespace finals
{
    public partial class MainWindow : Window
    {
        private StudentDataAccess dataAccess = new StudentDataAccess(); // Data access layer

        public MainWindow()
        {
            InitializeComponent();
            var grades = new[] { "9th", "10th", "11th", "12th" };
            var subjects = new[] { "Mathematics", "Science", "English", "History" , "Physics" , "Chemistry" , "Biology"};

            // Populating ComboBoxes
            GradeComboBox.ItemsSource = grades;
            SubjectComboBox.ItemsSource = subjects;
            GradeComboBox.SelectedIndex = 0;
            SubjectComboBox.SelectedIndex = 0;
            LoadStudents();
        }

        // Method to load students into the DataGrid
        private void LoadStudents(string grade = null, string subject = null)
        {
            var students = dataAccess.GetStudents(grade, subject);
            StudentsDataGrid.ItemsSource = students;
        }

        // Filter by grade selection in ComboBox
        private void GradeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selectedGrade = GradeComboBox.SelectedItem?.ToString();
            LoadStudents(grade: selectedGrade);
        }

        // Filter by subject selection in ComboBox
        private void SubjectComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selectedSubject = SubjectComboBox.SelectedItem?.ToString();
            LoadStudents(subject: selectedSubject);
        }

        // Add a new student to the database
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var student = new Student { Name = "New Student", Grade = "9th", Subject = "Mathematics", Marks = 0, Attendance = 0 };
            dataAccess.AddStudent(student);
            LoadStudents(); // Refresh the student list
        }

        // Edit the selected student from the DataGrid
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsDataGrid.SelectedItem is Student selectedStudent)
            {
                // Edit student logic, show a dialog to update details
                selectedStudent.Marks += 10; // Just an example of updating marks
                dataAccess.UpdateStudent(selectedStudent);
                LoadStudents(); // Refresh the student list
            }
        }

        // Delete the selected student from the DataGrid
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsDataGrid.SelectedItem is Student selectedStudent)
            {
                dataAccess.DeleteStudent(selectedStudent.Id);
                LoadStudents(); // Refresh the student list
            }
        }
    }
}
