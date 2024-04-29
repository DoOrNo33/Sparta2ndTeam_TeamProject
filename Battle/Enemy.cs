using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject.Battle
{
    internal class Enemy
    {
        private string name;
        private int id;
        private int lv;
        private int hp;
        private int atk;
        private bool dead;

        public string Name { get { return name; } }
        public int Id { get { return id; } }
        public int Lv { get { return lv; } }
        public int HP { get { return hp; } }
        public int Atk { get { return atk; } }
        public bool Dead { get { return dead; } }

        public Enemy(string sName, int sId, int sLv, int sHp, int sAtk, bool sDead = false)
        {
            name = sName;
            id = sId;
            lv = sLv;
            hp = sHp;
            atk = sAtk;
            dead = sDead;
        }
    }
}
