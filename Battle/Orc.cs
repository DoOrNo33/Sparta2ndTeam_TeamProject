﻿using System;
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
            hp = 50;
            atk = 20;

            int lowLv = _towerLv - 3;
            int highLv = _towerLv;
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
            hp += 20;
            atk += 5;
        }
    }
}
