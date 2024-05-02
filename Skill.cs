using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sparta2ndTeam_TeamProject
{
    internal class Skill
    {
        public string SkillName { get; }
        public int SkillMana { get; }
        public int SkillDamage { get; }
        public bool SkillRange { get; }
        public int SkillOrder { get; set; }

        public Skill(string Name, int Mana, int Damage, bool Range, int order)
        {
            SkillName = Name; SkillMana = Mana; SkillDamage = Damage; SkillRange = Range; SkillOrder = order;
        }
    }

}
