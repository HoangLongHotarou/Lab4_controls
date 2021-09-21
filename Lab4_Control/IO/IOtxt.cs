using Lab4_Control.Module;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_Control.IO
{
    class IOtxt : IIO
    {
        private const string path = "Data/data.txt";

        public List<Student> Read()
        {
            List<Student> students = new List<Student>();
            string line = "";
            if (File.Exists(path))
            {
                using (var stream = new FileStream(path,FileMode.Open,FileAccess.Read))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] str = line.Split('^');
                            students.Add(new Student()
                            {
                                ID = str[0].Trim(),
                                Name = str[1].Trim(),
                                Sex = str[2].Trim(),
                                BirthDay = DateTime.Parse(str[3]),
                                Class = str[4].Trim(),
                                PhoneNum = str[5].Trim(),
                                Email = str[6].Trim(),
                                Address = str[7].Trim(),
                                Character = str[8].Trim()
                            });
                        }
                    }
                }
            }
            return students;
        }

        public void Save(List<Student> students)
        {
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                using (var write = new StreamWriter(stream))
                {
                    foreach (var st in students)
                    {
                        write.WriteLine(String.Format($"{st.ID}^{st.Name}^{st.Sex}^{st.BirthDay}^{st.Class}^{st.PhoneNum}^{st.Email}^{st.Address}^{st.Character}"));
                    }
                }
            }
        }
    }
}
