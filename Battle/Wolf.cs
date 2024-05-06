using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject.Battle
{
    internal class Wolf : Enemy
    {
        public Wolf(int _towerLv) : base()      // 2층부터 등장
        {
            name = "늑대";
            id = 2;
            lv = 0;
            hp = 20;
            atk = 7;
            exp = 0;
            drops[0] = 1;
            drops[1] = 1;

            int lowLv = Math.Max(_towerLv - 2, 1);
            int highLv = Math.Min(_towerLv + 1, 6);

            int enemyLv = random.Next(lowLv, highLv);

            for (int i = 0; i < enemyLv; i++)    //등장 층수에서 레벨이 1로 시작하도록 세팅
            {
                SetLevel();
            }
        }

        protected override void SetLevel()
        {
            lv += 1;            
            hp += 10;
            atk += 3;
            exp += 8;
        }
    }
}
