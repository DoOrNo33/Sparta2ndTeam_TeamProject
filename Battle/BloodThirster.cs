using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject.Battle
{
    internal class BloodThirster : Enemy
    {
        public BloodThirster() : base()
        {
            name = "피의 탐식자";
            id = 8;
            lv = 8;
            hp = 888;
            atk = 88;

        }

        protected override void SetLevel()
        {
            lv += 0;
            hp += 0;
            atk += 0;
        }
    }
}
