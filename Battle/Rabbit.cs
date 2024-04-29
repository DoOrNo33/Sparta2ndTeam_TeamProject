using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject.Battle
{
    internal class Rabbit : Enemy
    {
        //private string name = "토끼";
        //private int id = 0;
        //private int lv = 1;
        //private int hp = 10;
        //private int atk = 5;
        //private bool isDead = ;

        public Rabbit(string sName, int sId, int sLv, int sHp, int sAtk, bool sIsDead = false)
            : base(sName, sId, sLv, sHp, sAtk, sIsDead)
        {

        }

    }
}
