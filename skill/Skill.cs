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
        public string SkillName { get; }
        public int SkillMana { get;  }
        public int SkillDamage { get; }
        public int SkillRange { get;  }

        public Skill(string Name, int Mana, int Damage, int Range)
        {
            this.SkillName = Name; this.SkillMana = Mana; SkillDamage = Damage; this.SkillRange = Range;
        }
    }

}
