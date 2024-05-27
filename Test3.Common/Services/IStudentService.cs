using Test3.Domain.Models;

namespace Test3.Common.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAllStudents();

        Student GetStudentById(int id);

        Student AddStudent(Student student);

        void DeleteStudent(int id);
    }
}