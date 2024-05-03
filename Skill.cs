using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Sparta2ndTeam_TeamProject
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
            this.SkillName = Name;
            this.SkillMana = Mana;
            this.SkillDamage = Damage;
            this.SkillRange = Range;
            this.SkillOrder = Order;
            public void PrintSkill()
            {


                for (int i = 0; i < GameManager.skill.Count; i++)
                {
                    if (GameManager.skill[i].SkillRange)
                    {
                        Console.WriteLine("\n{0}. {1} - MP {2} \n{3}의 데미지로 모든 적을 공격합니다", i + 1, GameManager.skill[i].SkillName, GameManager.skill[i].SkillMana, GameManager.skill[i].SkillDamage, GameManager.skill[i].SkillRange);
                    }
                    else
                    {
                        Console.WriteLine("\n{0}. {1} - MP {2} \n{3}의 데미지로 적 1명을 공격합니다", i + 1, GameManager.skill[i].SkillName, GameManager.skill[i].SkillMana, GameManager.skill[i].SkillDamage, GameManager.skill[i].SkillRange);
                    }

                }
                Console.WriteLine("\n0. 취소");
                Console.WriteLine("");
            }
        }
        
    }

}
