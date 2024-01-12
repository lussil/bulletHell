using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IAttributes
{
    int Damage { get; set; }
    int Life { get; }

    string Debuff { get; }

}

