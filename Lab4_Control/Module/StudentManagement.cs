using Lab4_Control.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_Control.Module
{
    class StudentManagement
    {
        private List<Student> _students;
        private IIO _studentIO;

        public List<Student> Students
        {
            get
            {
                return _students;
            }
        }

        public StudentManagement()
        {
            _studentIO = new IOtxt();
            Reload();
        }

        public void Reload()
        {
            _students = _studentIO.Read();
        }

        public void SaveList()
        {
            _studentIO.Save(_students);
        }

        public bool Save(Student student)
        {
            Student st = _students.Find(x => x.ID == student.ID);
            if (st == null)
            {
                _students.Add(student);
                return true;
            }
            return false;
        }

        public void Update(Student student) {
            int index =_students.FindIndex(x => x.ID == student.ID);
            _students[index] = student;
        }

        public void Delete(string ID)
        {
            _students.RemoveAll(x => x.ID == ID);
        }
    }
}
