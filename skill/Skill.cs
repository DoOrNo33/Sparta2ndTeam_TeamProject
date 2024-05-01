using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sparta2ndTeam_TeamProject.skill
{
    internal class Skill
    {
        public string SkillName { get; set; }
        public int SkillMana { get; set; }
        public int SkillDamage { get; set; }
        public int SkillRange { get; set; }

        public Skill(string Name, int Mana, int Damage, int Range)
        {
            this.SkillName = Name; this.SkillMana = Mana; SkillDamage = Damage; this.SkillRange = Range;
        }
    }

}
