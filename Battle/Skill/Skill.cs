using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Sparta2ndTeam_TeamProject.Battle.Skill
{
    internal class Skill
    {

        public string SkillName { get; }
        public int SkillMana { get; }
        public int SkillDamage { get; }
        public bool SkillRange { get; }
        public int SkillOrder { get; set; }


        public Skill(string Name, int Mana, int Damage, bool Range, int Order)
        {
            SkillName = Name;
            SkillMana = Mana;
            SkillDamage = Damage;
            SkillRange = Range;
            SkillOrder = Order;
        }


    }

}
