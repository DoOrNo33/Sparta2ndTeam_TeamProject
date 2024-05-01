using Sparta2ndTeam_TeamProject.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject
{
    internal class Guild
    {
        static int command = 0;
        static public void GuildMenu()
        {
            while (true)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ 탐험가 길드 ■");
                Console.WriteLine("체력 회복과 저장, 그리고 퀘스트를 수락할 수 있습니다.\n");
                
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("2. 저장하기");
                Console.WriteLine("3. 퀘스트 보기");
                Console.WriteLine("0. 나가기\n");


                if (command == (int)SelectBreakMenu.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("잘못된 입력입니다.");
                    Console.ResetColor();
                    Console.WriteLine();
                }
                command = ConsoleUtility.PromptMenuChoice(0, 3);

                switch (command)
                {
                    case (int)SelectBreakMenu.PreviousPage:
                        return;
                    case (int)SelectBreakMenu.RestMenu:
                        RestMenu();
                        break;
                    case (int)SelectBreakMenu.SaveData:
                        GameManager.SaveData();
                        break;
                    case (int)SelectBreakMenu.QuestMenu:
                        QuestMenu();
                        break;
                }
            }
        }

        static public void RestMenu()
        {
            while (true)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ 탐험가 길드 - 휴식 하기 ■");
                Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {GameManager.player.Gold} G)\n");

                ConsoleUtility.PrintTextHighlights("", $"[ 현재 체력 : {GameManager.player.Hp} ]\n");

                Console.WriteLine("\n\n1. 회복하기");
                Console.WriteLine("0. 나가기\n");

                if (command == (int)SelectBreakMenu.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("잘못된 입력입니다.");
                    Console.ResetColor();
                    Console.WriteLine();
                }

                command = ConsoleUtility.PromptMenuChoice(0, 1);

                if (command == 0) // 나가기
                    return;
                
                else if (command == -1)// 잘못된 입력
                    continue;
                
                if (GameManager.player.Hp == GameManager.player.Max_Hp) // 회복하기 (1) - 이미 풀피
                {
                    Healing(1);
                    return;
                }

                
                else if (GameManager.player.Gold < 500) // 회복하기 (2) - 돈이 없음
                {
                    Healing(2);
                    return;
                }

                else // 회복
                {
                    Healing(3);
                    return;
                }

            }
        }

        static public void Healing(int num)
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ 탐험가 길드 - 휴식 하기 ■");
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {GameManager.player.Gold} G)\n");

            ConsoleUtility.PrintTextHighlights("", $"[ 현재 체력 : {GameManager.player.Hp} ]\n");

            if (num == 1)
            {
                Console.WriteLine("\n이미 건강한 상태입니다.\n");
                ConsoleUtility.PromptReturn();
            }
            else if (num == 2) // 돈이 부족
            {
                Console.WriteLine("\nGold 가 부족합니다.\n");
                ConsoleUtility.PromptReturn();
            }
            else if (num == 3) // 회복 가능
            {
                Thread.Sleep(200);
                Console.WriteLine("\n휴식을 취하는 중입니다...\n");

                Console.Write($"체 력 {GameManager.player.Hp} -> ");

                //while (GameManager.player.Hp < GameManager.player.Max_Hp)
                //{
                //    Thread.Sleep(50);
                //    GameManager.player.Hp++;
                //    Console.SetCursorPosition(12, 8);
                //    Console.Write($"{GameManager.player.Hp}");
                //}

                ConsoleUtility.Animation(12,8,GameManager.player.Hp,GameManager.player.Max_Hp);
                
                Thread.Sleep(300);
                GameManager.player.Gold -= 500;
                Console.WriteLine($"\n\n남은 골드 : {GameManager.player.Gold}");
                ConsoleUtility.PromptReturn();
            }
        }

        static public void QuestMenu()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ 탐험가 길드 ■ - 퀘스트 보기");
            Console.WriteLine("체력 회복과 저장, 그리고 퀘스트를 수락할 수 있습니다.\n");

            ConsoleUtility.PrintTextHighlights("", "[퀘스트 목록]");

            Console.WriteLine("구현중,,,");

            return;
        }


        private enum SelectBreakMenu
        {
            PreviousPage,
            RestMenu,
            SaveData,
            QuestMenu,
            WrongCommand = -1
        }
    }

    class Quest
    {
        public string name { get; set; }
        public string info { get; set; }
        public bool isAccept { get; set; } = false;
        public bool isComplete { get; set; } = false;
    }
}
