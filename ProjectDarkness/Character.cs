using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDarkness
{
    public class Character
    {
            public string Name { get; set; }
            public int MaxHealth { get; set; }
            public int Health { get; set; }
            public int AttackPower { get; set; }
            public bool IsAlive => Health > 0;
    }
}
