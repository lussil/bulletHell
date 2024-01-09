using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDamegeable
{
    event Action<int> DeathEvent;
    int damage { get; }

    void takeDamage(int damage);

}
