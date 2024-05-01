using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject.Battle
{
    internal class Orc : Enemy
    {
        public Orc(int _towerLv) : base()  // 3층 등장
        {
            name = "오크";
            id = 3;
            lv = 0;
            hp = 30;
            atk = 15;
            exp = 2;

            int lowLv = Math.Max(_towerLv - 3, 1);
            int highLv = Math.Min(_towerLv, 6);

            int enemyLv = random.Next(lowLv, highLv);

            for (int i = 0; i < enemyLv; i++)
            {
                SetLevel();
            }
        }

        protected override void SetLevel()
        {
            lv += 1;
            hp += 20;
            atk += 5;
            exp += 1;
        }
    }
}
