using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class Department: IEnumerable
    {

        List<Worker> list;
        public Department(List<Worker> w)
        {
            list = new List<Worker>();
            foreach (var e in w)
            {
                list.Add(e);
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < this.list.Count; i++)
            {
                yield return this.list[i];
            }
        }

    }
}
