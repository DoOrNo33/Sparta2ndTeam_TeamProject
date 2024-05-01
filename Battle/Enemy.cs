﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sparta2ndTeam_TeamProject.Battle
{
    internal class Enemy
    {
        protected string name;
        protected int id;
        protected int lv;
        protected int hp;
        protected int atk;
        protected bool isDead = false;
        protected int exp;
        protected int[] drops = new int[2];
        protected Random random = new Random();

        public string Name { get { return name; } }
        public int Id { get { return id; } }
        public int Lv { get { return lv; } }
        public int Hp { get { return hp; } }
        public int Atk { get { return atk; } }
        public bool IsDead { get { return isDead; } }
        public int[] Drops { get { return drops; } }



        protected virtual void SetLevel()
        {
            lv += 0;
            hp += 0;
            atk += 0;
            exp += 0;
        }

        public void PrintCurrentEnemies(bool withNumber = false, int idx = 0)
        {
            if (isDead)     // 죽었다면 모두 검은색으로 처리
            { 
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                if (withNumber)
                {
                    Console.Write(" {0} ", idx);
                }
                else
                {
                    Console.Write(" - ");
                }
                Console.Write("Lv.{0} ", Lv);
                Console.Write("{0} ", Name);
                if (!IsDead)
                {
                    Console.WriteLine("HP {0}", Hp);
                }
                else
                {
                    Console.WriteLine("Dead");
                }
                Console.ResetColor();
            }
            else                                // 아니라면
            {
                if (withNumber)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" {0} ", idx);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(" - ");
                }
                Console.Write("Lv.{0} ", Lv);
                Console.Write("{0} ", Name);
                if (!IsDead)
                {
                    Console.WriteLine("HP {0}", Hp);
                }
                else
                {
                    Console.WriteLine("Dead");
                }
            }
        }


        public (bool, int) Critical(int Atk) // 치명타 판정
        {
            bool isCri = false;
            if (GameManager.player.Critical())
            {
                Atk = (int)Math.Ceiling(Atk * 1.6f);                                                      // 치명타 데미지
                isCri = true;
            }
            return (isCri, Atk);
        }


        public int PlayerSkillAttack(int order)
        {
            int pMp = GameManager.player.Mp;
            string pName = GameManager.player.Name;
            string sName = GameManager.player.skill[order].SkillName;
            int Damege = GameManager.player.Atk;
            int adAtk;
            int pAtk;
            int Range;
            bool isCri;
            if (pMp < GameManager.player.skill[order].SkillMana) //마나가 부족하면
            {
                return -1;
            }
            else //마나가 있다면 공격
            {
                
                // 적의 회피 판정
                if (GameManager.player.Avoid())                 // 적이 회피했다면(10%)
                {
                    Console.Clear();
                    ConsoleUtility.ShowTitle("■ Battle!! ■\n");
                    Console.WriteLine("{0} 의 {1} 공격!", pName, sName);
                    Console.WriteLine("Lv.{0} {1} 을(를) 공격했지만 아무일도 일어나지 않았습니다.", Lv, Name);
                    Console.WriteLine("\n0. 다음");
                    Console.Write("\n>>");

                    switch ((PlayerPhase)ConsoleUtility.PromptMenuChoice(0, 0))
                    {
                        case PlayerPhase.ToEnemyPhase:
                            break;
                    }
                    return 0;
                }
                else                                            // 적이 맞았다면
                {
                    Damege = GameManager.player.skill[order].SkillDamage;
                    Range = GameManager.player.skill[order].SkillRange;

                    adAtk = (int)Math.Ceiling(Damege * 0.1f);                                   //보정 공격, 10%의 올림치
                    pAtk = random.Next((Damege - adAtk), (Damege + adAtk + 1)); //보정 공격치
                    
                     
                    Console.Clear();
                    ConsoleUtility.ShowTitle("■ Battle!! ■\n");
                    Console.WriteLine("{0} 의 {1} 공격!", pName, sName);
                    Console.Write("Lv.{0} {1} 을(를) 맞췄습니다.", Lv, Name);
                    (isCri, pAtk) = Critical(pAtk);  //치명타 유무,치명타 공격력 받기
                    if (isCri)
                    {
                        Console.Write(" - 치명타 공격!!");
                    }
                    Console.Write(" [데미지 : {0}]", pAtk);
                    Console.WriteLine("\n\nLv.{0} {1}", Lv, Name);

                    int tempHp = hp;
                    hp -= pAtk;

                    if (hp > 0)   // 적의 남은 hp가 0보다 큰지 작은지
                    {
                        Console.WriteLine("HP {0} -> {1}", tempHp, hp);
                        Console.WriteLine("\n0. 다음");
                        Console.Write("\n>>");

                        switch ((PlayerPhase)ConsoleUtility.PromptMenuChoice(0, 0))
                        {
                            case PlayerPhase.ToEnemyPhase:
                                break;
                        }
                        return 0;
                    }
                    else
                    {
                        hp = 0;
                        Dead();
                        Console.WriteLine("HP {0} -> {1}", tempHp, hp);
                        Console.WriteLine("\n0. 다음");
                        Console.Write("\n>>");

                        switch ((PlayerPhase)ConsoleUtility.PromptMenuChoice(0, 0))
                        {
                            case PlayerPhase.ToEnemyPhase:
                                break;
                        }
                        return 1;
                    }
                }
            }

        }

        public int PlayerAttack()                  // 플레이어 공격 체크
        {
            string pName = GameManager.player.Name;
            int adAtk = (int)Math.Ceiling(GameManager.player.Atk * 0.1f);                                   //보정 공격, 10%의 올림치
            int pAtk = random.Next((GameManager.player.Atk - adAtk), (GameManager.player.Atk + adAtk + 1)); //보정 공격치
            bool isCri = false;

            // 치명타 판정
            if (GameManager.player.Critical())
            {
                pAtk = (int)Math.Ceiling(pAtk * 1.6f);                                                      // 치명타 데미지
                isCri = true;
            }

            // 적의 회피 판정
            if (GameManager.player.Avoid())                 // 적이 회피했다면(10%)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ Battle!! ■\n");
                Console.WriteLine("{0} 의 공격!", pName);
                Console.WriteLine("Lv.{0} {1} 을(를) 공격했지만 아무일도 일어나지 않았습니다.", Lv, Name);
                Console.WriteLine("\n0. 다음");
                Console.Write("\n>>");

                switch ((PlayerPhase)ConsoleUtility.PromptMenuChoice(0, 0))
                {
                    case PlayerPhase.ToEnemyPhase:
                        break;
                }
                return 0;
            }

            else                                            // 적이 맞았다면
            {

                Console.Clear();
                ConsoleUtility.ShowTitle("■ Battle!! ■\n");
                Console.WriteLine("{0} 의 공격!", pName);
                Console.Write("Lv.{0} {1} 을(를) 맞췄습니다.", Lv, Name);
                Console.Write(" [데미지 : {0}]", pAtk);

                if (isCri)
                {
                    Console.Write(" - 치명타 공격!!");
                }

                Console.WriteLine("\n\nLv.{0} {1}", Lv, Name);

                int tempHp = hp;
                hp -= pAtk;

                if (hp > 0)   // 적의 남은 hp가 0보다 큰지 작은지
                {
                    Console.WriteLine("HP {0} -> {1}", tempHp, hp);
                    Console.WriteLine("\n0. 다음");
                    Console.Write("\n>>");

                    switch ((PlayerPhase)ConsoleUtility.PromptMenuChoice(0, 0))
                    {
                        case PlayerPhase.ToEnemyPhase:
                            break;
                    }
                    return 0;
                }
                else
                {
                    hp = 0;
                    Dead();
                    Console.WriteLine("HP {0} -> {1}", tempHp, hp);
                    Console.WriteLine("\n0. 다음");
                    Console.Write("\n>>");

                    switch ((PlayerPhase)ConsoleUtility.PromptMenuChoice(0, 0))
                    {
                        case PlayerPhase.ToEnemyPhase:
                            break;
                    }
                    return 1;
                }
            }
            




        }

        public void EnemyAttack()  // 플레이어 이름, 플레이어 체력
        {

            if (!isDead && GameManager.player.Hp > 0)
            {
                if (GameManager.player.Avoid())                         // 플레이어 회피
                {
                    Console.Clear();
                    ConsoleUtility.ShowTitle("■ Battle!! ■\n");
                    Console.WriteLine("Lv.{0} {1} 의 공격!", Lv, Name);
                    Console.WriteLine("{0} 을(를) 공격했지만 아무일도 일어나지 않았습니다.", GameManager.player.Name);
                    //Console.WriteLine("\n0. 다음");
                    Console.Write("\n<Press Any Key>");

                    switch ((EnemyPhase)ConsoleUtility.PromptMenuChoice(0, 0))
                    {
                        case EnemyPhase.Next:
                            break;
                    }
                }

                else                                                    // 플레이어 피격
                {
                    float damageReduce = Atk * (1 - (GameManager.player.Def * 0.01f)); // 플레이어의 방어력%만큼 피해 경감
                    if (damageReduce < 2)
                    {
                        damageReduce = 2;
                    }
                    int damageRange = random.Next((int)Math.Ceiling(damageReduce) - 1, (int)Math.Ceiling(damageReduce) + 2);     // 경감 데미지에서 -1 ~ +1 값 설정
                    int adAtk = damageRange;             

                    Console.Clear();
                    ConsoleUtility.ShowTitle("■ Battle!! ■\n");
                    Console.WriteLine("Lv.{0} {1} 의 공격!", Lv, Name);
                    Console.Write("{0} 을(를) 맞췄습니다. ", GameManager.player.Name);
                    Console.WriteLine("[데미지 : {0}]", adAtk);

                    int tempHp = GameManager.player.Hp;
                    GameManager.player.Hp -= adAtk;

                    if (GameManager.player.Hp > 0)   // 플레이어의 남은 hp가 0보다 큰지 작은지
                    {
                        Console.WriteLine("HP {0} -> {1}", tempHp, GameManager.player.Hp);
                    }
                    if (GameManager.player.Hp <= 0)
                    {
                        GameManager.player.Hp = 0;
                        Console.WriteLine("HP {0} -> {1}", tempHp, GameManager.player.Hp);
                    }

                    //Console.WriteLine("0. 다음");
                    Console.WriteLine("\n<Press Any Key>");
                    //Console.Write(">>");

                    switch ((EnemyPhase)ConsoleUtility.PromptMenuChoice(0, 0))
                    {
                        case EnemyPhase.Next:
                            break;
                    }
                }
            }
        }

        private void Dead()
        {
            isDead = true;
        }

        public enum PlayerPhase
        {
            ToEnemyPhase
        }

        public enum EnemyPhase
        {
            Next
        }
        public enum SkillList
        {
            Skillone,
            Skilltwo

        }

    }
}
