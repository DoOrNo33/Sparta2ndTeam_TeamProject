﻿using Sparta2ndTeam_TeamProject.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sparta2ndTeam_TeamProject
{
    internal class Player
    {
        Random random = new Random();
        public string Name { get; }
        public int Job { get; set; }
        public int Level { get; set; } = 1;
        public int Atk { get; set; } = 10; 
        public int Def { get;  set; } = 5; 
        public int Hp { get; set; } = 100;
        public int Max_Hp { get; set; } = 100;
        public int Mp { get; set; } = 50;

        public int Max_Mp { get; set; } = 50;
        public int Gold { get; set; } = 1500;

        public int RequiredExp { get; set; } = 10;
        public int CurrentExp { get; set; } = 0;


        public Player(string name)
        {
            Name = name;
        }

        public void StatusMenu()
        {
            
            int added_ATK = 0;
            int added_DEF = 0;
            int added_HP = 0;

            string[] job = { "전사", "마법사" };

            Console.Clear();
            ConsoleUtility.ShowTitle("■ 상태 보기 ■");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();

            Console.WriteLine($"Lv. {Level.ToString("D2")}");
            Console.WriteLine($"{Name} ({job[Job - 1]})");

            foreach (Item item in GameManager.items)
            {
                if (item.isEquipped == true)
                {
                    added_ATK += item.Atk;
                    added_DEF += item.Def;
                    added_HP += item.HP;
                }
            }

            if (added_ATK == 0)
                Console.WriteLine($"공격력 : {Atk}");
            else
                Console.WriteLine($"공격력 : {Atk} (+{added_ATK})");

            if (added_DEF == 0)
                Console.WriteLine($"방어력 : {Def}");
            else
                Console.WriteLine($"방어력 : {Def} (+{added_DEF})");

            if (added_HP == 0)
                Console.WriteLine($"체 력 : {Hp} / {Max_Hp}");
            else
                Console.WriteLine($"체 력 : {Hp} (+{added_HP})");

            Console.WriteLine($"마 나 : {Mp} / {Max_Mp}");
            Console.WriteLine($"Gold : {Gold} G");

            ConsoleUtility.PromptReturn();

            return;
        }

        public bool Critical() // 치명타
        {
            int percent = random.Next(0, 100);

            if (percent < 15)
                return true;

            return false;
        }

        public bool Avoid(int? petAvoid = null, bool isEnemy = false) // 회피
        {
            if (petAvoid != null)
            {
                int percent = random.Next(0, 10);

                if (percent < 1)
                    return true;

                else if (percent < (1 + petAvoid))                                           //초록 슬라임 확인
                {
                    Console.Clear();
                    ConsoleUtility.ShowTitle("■ 전  투 ■\n");
                    Console.WriteLine("초록 슬라임이 당신을 감쌉니다!");
                    ConsoleUtility.PromptReturn();
                    return true;
                }

                return false;
            }

            else
            {
                int percent = random.Next(0, 10);

                if (percent < 1)
                    return true;

                return false;
            }

        }

        public virtual void LevelUp(int exp)
        {
            CurrentExp += exp;
            while(CurrentExp > RequiredExp)
            {
                CurrentExp -= RequiredExp;
                RequiredExp += 20 + (Level * 5);
                Level++;
                Atk += 2;
                Def += 1;
                Hp += 0;
                Max_Hp += 0;
                Mp += 0;
                Max_Mp += 0;
            }
        }

    }

    class Warrior : Player
    {
        public Warrior(string name) : base(name)
        {
            Job = 1;
            Atk = 8; // 전사의 공격력 설정
            Def = 10; // 전사의 방어력 설정
        }

        public int Alpha_Strike() // 데미지를 반환 ( -1 : 스킬 사용 실패)
        {
            if (Mp < 10)
            {
                Console.WriteLine("마나가 부족합니다..");
                return -1;
            }

            Mp -= 10;
            Console.WriteLine("알파-스트라이크 스킬 사용");
            return Atk * 2;
        }

        public int Double_Strike() // 데미지를 반환 ( -1 : 스킬 사용 실패)
        {
            if (Mp < 20)
            {
                Console.WriteLine("마나가 부족합니다..");
                return -1;
            }

            Mp -= 20;
            Console.WriteLine("더블-스트라이크 스킬 사용");
            return Atk * 2;
        }

        public override void LevelUp(int exp)
        {
            CurrentExp += exp;
            while (CurrentExp > RequiredExp)
            {
                CurrentExp -= RequiredExp;
                RequiredExp += 20 + (Level * 5);
                Level++;
                Atk += 1;
                Def += 2;
                Hp += 0;
                Max_Hp += 5;
                Mp += 0;
                Max_Mp += 0;
            }
        }
    }

    internal class Mage : Player
    {
        public Mage(string name) : base(name)
        {
            Job = 2;
            Atk = 12; // 마법사의 공격력 설정
            Def = 6; // 마법사의 방어력 설정
        }

        public int Energy_Bolt() // 데미지를 반환 ( -1 : 스킬 사용 실패)
        {
            if (Mp < 10)
            {
                Console.WriteLine("마나가 부족합니다..");
                return -1;
            }

            Mp -= 10;
            Console.WriteLine("에너지 볼트 스킬 사용");
            return Atk * 1;
        }

        public int Thunder_Bolt() // 데미지를 반환 ( -1 : 스킬 사용 실패)
        {
            if (Mp < 25)
            {
                Console.WriteLine("마나가 부족합니다..");
                return -1;
            }

            Mp -= 25;
            Console.WriteLine("썬더 볼트 스킬 사용");
            return Atk * 2;
        }
        public override void LevelUp(int exp)
        {
            CurrentExp += exp;
            while (CurrentExp > RequiredExp)
            {
                CurrentExp -= RequiredExp;
                RequiredExp += 20 + (Level * 5);
                Level++;
                Atk += 1;
                Def += 1;
                Hp += 0;
                Max_Hp += 0;
                Mp += 0;
                Max_Mp += 5;
            }
        }
    }

    enum SelectStatusMenu
    {
        PreviousPage = 0,
        WrongCommand = -1,
    }
}
