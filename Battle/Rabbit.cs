using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject.Battle
{
    internal class Rabbit : Enemy
    {

        public Rabbit(string sName, int sId, int sLv, int sHp, int sAtk, bool sIsDead = false)
            : base(sName, sId, sLv, sHp, sAtk, sIsDead)
        {

        }

    }
}
