using Lab4_Control.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_Control.IO
{
    interface IIO
    {
        List<Student> Read();
        void Save(List<Student> students);
    }
}
