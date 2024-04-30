using Sparta2ndTeam_TeamProject.Tower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject.Battle
{
    internal class BigRat : Enemy
    {
        public BigRat(int _towerLv) : base()
        {
            name = "큰 쥐";
            id = 0;
            lv = 0;
            hp = 10;
            atk = 5;

            int lowLv = _towerLv - 1;
            int highLv = _towerLv + 2;
            if (lowLv < 1)
            {
                lowLv = 1;
            }
            if (highLv > 6)
            {
                highLv = 6;
            }
            int enemyLv = random.Next(lowLv, highLv);

            for (int i = 0; i < enemyLv; i++)
            {
                SetLevel();
            }
        }

        protected override void SetLevel()
        {
            lv += 1;
            hp += 3;
            atk += 1;
        }
    }
}
