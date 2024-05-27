using Test3.Domain.Models;
using Microsoft.Extensions.Caching.Memory;
namespace Test3.Common.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMemoryCache _memoryCache;
        private const string StudentsCacheKey = "StudentsCacheKey";

        public StudentService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;

            // Initialize the cache with some students if it's empty
            if (!_memoryCache.TryGetValue(StudentsCacheKey, out List<Student> _))
            {
                var initialStudents = new List<Student>
            {
                new Student { Id = 1, Name = "John", Age = 20 },
                new Student { Id = 2, Name = "Alice", Age = 22 },
                new Student { Id = 3, Name = "Bob", Age = 21 }
            };

                _memoryCache.Set(StudentsCacheKey, initialStudents);
            }
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _memoryCache.Get<List<Student>>(StudentsCacheKey);
        }

        public Student GetStudentById(int id)
        {
            var students = _memoryCache.Get<List<Student>>(StudentsCacheKey);
            return students?.FirstOrDefault(s => s.Id == id);
        }

        public Student AddStudent(Student student)
        {
            var students = _memoryCache.Get<List<Student>>(StudentsCacheKey);
            student.Id = students.Count > 0 ? students.Max(s => s.Id) + 1 : 1;
            students.Add(student);
            _memoryCache.Set(StudentsCacheKey, students);
            return student;
        }

        public void DeleteStudent(int id)
        {
            var students = _memoryCache.Get<List<Student>>(StudentsCacheKey);
            var student = students?.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                students.Remove(student);
                _memoryCache.Set(StudentsCacheKey, students);
            }
        }
    }
}
