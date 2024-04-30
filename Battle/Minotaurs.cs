using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject.Battle
{
    internal class Minotaurs : Enemy
    {
        public Minotaurs(int _towerLv) : base()
        {
            name = "미노타우르스";
            id = 4;
            lv = 0;
            hp = 100;
            atk = 30;

            int lowLv = _towerLv - 4;
            int highLv = _towerLv - 1;
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
            hp += 30;
            atk += 10;
        }
    }
}
