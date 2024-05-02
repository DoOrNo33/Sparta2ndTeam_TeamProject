using Sparta2ndTeam_TeamProject.Battle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject.Items
{
    internal class Pet : Item
    {
        protected PetType petType { get; set; }
        public Pet(string Name, string Desc, int Atk, int Def, int HP, int MP, int Price,
    ItemType _type, bool isEquipped = false, bool isPurchased = false, bool isInitItem = false) : base(Name, Desc, Atk, Def, HP, MP, Price, _type, isEquipped, isPurchased, isInitItem)
        {
            this.Name = Name; 
            this.Desc = Desc; 
            this._type = _type;
            this.isEquipped = isEquipped; 
            this.isPurchased = isPurchased; 
            this.isInitItem = isInitItem;

        }

        public PetType PetType {  get { return petType; } }

        public virtual int PetAttack(List<Enemy> currentEnemy)
        {
            return 0;
        }

        public virtual void PetHeal() { }

    }

    internal class RedSlime : Pet
    {
        
        public RedSlime(string Name, string Desc, int Atk, int Def, int HP, int MP, int Price,
ItemType _type, bool isEquipped = false, bool isPurchased = false, bool isInitItem = false) : base(Name, Desc, Atk, Def, HP, MP, Price, _type, isEquipped, isPurchased, isInitItem)
        {
            this.Name = Name;
            this.Desc = Desc;
            this._type = _type;
            this.isEquipped = isEquipped;
            this.isPurchased = isPurchased;
            this.isInitItem = isInitItem;
            petType = PetType.Attack;
        }

        public override int PetAttack(List<Enemy> currentEnemy)
        {
            Enemy[] weakest = new Enemy[1];
            int weakestHp = 0;
            int pDamage = 10;

            for (int i = 0; i < currentEnemy.Count; i++)        // 누가 제일 Hp가 적은가?
            {
                if (!currentEnemy[i].IsDead)
                {
                    if (currentEnemy[i].Hp < weakestHp)
                    {
                        weakest[0] = currentEnemy[i];
                        weakestHp = weakest[0].Hp;
                    }
                }
            }

            Console.Clear();
            ConsoleUtility.ShowTitle("■ 전  투 ■\n");
            Console.WriteLine("{0} 의 공격!", Name);
            Console.Write("Lv. {0} {1} 을(를) 맞췄습니다.", weakest[0].Lv, weakest[0].Name);
            Console.Write(" [데미지 : {0}]", pDamage);
            Console.WriteLine("\n\nLv. {0} {1}", weakest[0].Lv, weakest[0].Name);
            int tempHp = weakest[0].Hp;
            weakest[0].PetDamage(pDamage);

            if (weakest[0].Hp > 0)   // 적의 남은 hp가 0보다 큰지 작은지
            {
                Console.WriteLine("HP {0} -> {1}", tempHp, weakest[0].Hp);
                ConsoleUtility.PromptReturn();
                return 0;
            }
            else
            {
                weakest[0].Dead();
                Console.WriteLine("HP {0} -> {1}", tempHp, weakest[0].Hp);
                ConsoleUtility.PromptReturn();
                return 1;
            }
        }
    }

    internal class GreenSlime : Pet
    {
        public GreenSlime(string Name, string Desc, int Atk, int Def, int HP, int MP, int Price,
ItemType _type, bool isEquipped = false, bool isPurchased = false, bool isInitItem = false) : base(Name, Desc, Atk, Def, HP, MP, Price, _type, isEquipped, isPurchased, isInitItem)
        {
            this.Name = Name;
            this.Desc = Desc;
            this._type = _type;
            this.isEquipped = isEquipped;
            this.isPurchased = isPurchased;
            this.isInitItem = isInitItem;
            petType = PetType.Defense;
        }


    }

    internal class BlueSlime : Pet
    {
        public BlueSlime(string Name, string Desc, int Atk, int Def, int HP, int MP, int Price,
ItemType _type, bool isEquipped = false, bool isPurchased = false, bool isInitItem = false) : base(Name, Desc, Atk, Def, HP, MP, Price, _type, isEquipped, isPurchased, isInitItem)
        {
            this.Name = Name;
            this.Desc = Desc;
            this._type = _type;
            this.isEquipped = isEquipped;
            this.isPurchased = isPurchased;
            this.isInitItem = isInitItem;
            petType = PetType.Heal;
        }

        public override void PetHeal()
        {
            int pHeal = 10;

            if (GameManager.player.Hp < GameManager.player.Max_Hp)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ 전  투 ■\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("푸른 슬라임이 당신의 상처를 감쌉니다. {0} → ", GameManager.player.Hp);
                int prePlayerHP = GameManager.player.Hp;

                GameManager.player.Hp += pHeal;
                if (GameManager.player.Hp >= GameManager.player.Max_Hp) GameManager.player.Hp = GameManager.player.Max_Hp;

                // 현재 커서의 위치 확인
                int cursorLeft = Console.CursorLeft;
                int cursorTop = Console.CursorTop;
                ConsoleUtility.Animation(cursorLeft, cursorTop, prePlayerHP, GameManager.player.Hp);

                Console.ResetColor();
                //Thread.Sleep(500);
                ConsoleUtility.PromptReturn();
            }
            else
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ 전  투 ■\n");
                Console.Write("푸른 슬라임은 당신이 상처입길 기다리고 있습니다.", GameManager.player.Hp);
                //Thread.Sleep(500);
                ConsoleUtility.PromptReturn();
            }
        }
    }

    public enum PetType
    {
        Attack,
        Defense,
        Heal
    }
}
