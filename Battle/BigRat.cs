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
            hp = 7;
            atk = 4;
            exp = 0;
            drops[0] = 0;
            drops[1] = 0;

            int lowLv = Math.Max(_towerLv - 1, 1);
            int highLv = Math.Min(_towerLv + 2, 6);

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
            exp += 1;
        }
    }
}
