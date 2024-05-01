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
            hp = 70;
            atk = 20;
            exp = 4;
            drops[0] = 2;
            drops[1] = 2;

            int lowLv = Math.Max(_towerLv - 4, 1);
            int highLv = Math.Min(_towerLv - 1, 6);

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
            exp += 1;
        }
    }
}
