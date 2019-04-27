using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class WorkerFixedRate : Worker
    {
        public WorkerFixedRate(int id, string name, double rate) : base(id, name, rate) { }

        public override double Salary
        {
            get { return Rate; }
        }
        public override double CalcMonthWage()
        {
            return Rate;
        }
        public override string ToString()
        {
            return $"Работник с фиксированной ставкой {base.ToString()}";
        }
    }
}
