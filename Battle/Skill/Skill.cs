using Sparta2ndTeam_TeamProject.GameFramework;
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
    public class Skill
    {

        public string SkillName { get; set; }
        public int SkillMana { get; set; }
        public int SkillDamage { get; set; }
        public bool SkillRange { get; set; }
        public int SkillOrder { get; set; }


        public Skill(string Name, int Mana, int Damage, bool Range, int Order)
        {
            SkillName = Name;
            SkillMana = Mana;
            SkillDamage = Damage;
            SkillRange = Range;
            SkillOrder = Order;
        }

        public virtual int PlayerSkillDamage()
        {
            return GameManager.player.Atk;
        }
    }

    public class AlphaStrike : Skill
    {
        public AlphaStrike(string Name, int Mana, int Damage, bool Range, int order) : base(Name, Mana, Damage, Range, order)
        {

        }

        public override int PlayerSkillDamage()
        {
            return (GameManager.player.Atk * 2);
        }
    }

    public class DoubleStrike : Skill
    {
        public DoubleStrike(string Name, int Mana, int Damage, bool Range, int order) : base(Name, Mana, Damage, Range, order)
        {

        }
        public override int PlayerSkillDamage()
        {
            return (GameManager.player.Atk * 2);
        }
    }

    public class EnergyBolt : Skill
    {
        public EnergyBolt(string Name, int Mana, int Damage, bool Range, int order) : base(Name, Mana, Damage, Range, order)
        {

        }
        public override int PlayerSkillDamage()
        {
            return (GameManager.player.Atk * 1);
        }
    }

    public class ThunderBolt : Skill
    {
        public ThunderBolt(string Name, int Mana, int Damage, bool Range, int order) : base(Name, Mana, Damage, Range, order)
        {

        }
        public override int PlayerSkillDamage()
        {
            return (GameManager.player.Atk * 3);
        }
    }
}
