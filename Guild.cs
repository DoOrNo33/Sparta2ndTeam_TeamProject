using Sparta2ndTeam_TeamProject;
using Sparta2ndTeam_TeamProject.Battle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                ConsoleUtility.ShowTitle("■ 모험가 길드 ■");
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
                ConsoleUtility.ShowTitle("■ 모험가 길드 - 휴식 하기 ■");
                Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {GameManager.player.Gold} G)\n");

                ConsoleUtility.PrintTextHighlights("", $"[현재 체력 : {GameManager.player.Hp}]\n");

                Console.WriteLine("1. 회복하기");
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
            ConsoleUtility.ShowTitle("■ 모험가 길드 - 휴식 하기 ■");
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {GameManager.player.Gold} G)\n");

            ConsoleUtility.PrintTextHighlights("", $"[현재 체력 : {GameManager.player.Hp}]\n");

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

                ConsoleUtility.Animation(12, 8, GameManager.player.Hp, GameManager.player.Max_Hp);
                GameManager.player.Hp = 100;

                Thread.Sleep(300);
                GameManager.player.Gold -= 500;
                Console.WriteLine($"\n\n남은 골드 : {GameManager.player.Gold}");
                ConsoleUtility.PromptReturn();
            }
        }

        static public void QuestMenu()
        {
            while (true)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ 모험가 길드 ■ - 퀘스트 보기");
                Console.WriteLine("튜토리얼과 임무를 수락하거나 정산받을 수 있습니다.\n");

                ConsoleUtility.PrintTextHighlights("", "[튜토리얼 목록]\n");

                int length = GameManager.quests.Count;
                for (int i = 0; i < length; ++i)
                {
                    Console.Write($"{i + 1}. ");
                    Console.Write(ConsoleUtility.PadRightForMixedText(GameManager.quests[i].name, 20));

                    if (GameManager.quests[i].rewarded == true)
                        Console.WriteLine("... [완료]");

                    else if (GameManager.quests[i].isComplete == true)
                        Console.WriteLine("... [정산 가능]");

                    else if (GameManager.quests[i].isAccept == true)
                        Console.WriteLine("... [진행중]");
                    
                    else
                        Console.WriteLine("");

                    if (i != length - 1 && GameManager.quests[i].type != GameManager.quests[i + 1].type)
                    {
                        ConsoleUtility.PrintTextHighlights("", "\n[임무 목록]\n");
                    }
                }

                Console.WriteLine("\n0. 나가기\n");
                command = ConsoleUtility.PromptMenuChoice(0, GameManager.quests.Count );

                if (command == 0)
                    return;

                else if (command == -1)
                    continue;

                else if (GameManager.quests[command-1].isComplete == true)
                    GameManager.quests[command - 1].ClearQuest();
                else
                    GameManager.quests[command - 1].Print();
                
            }
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
        public string name { get; }
        public string info { get; }
        public bool isAccept { get; set; } = false;
        public bool isComplete { get; set; } = false;
        public int type { get; set; } // type = 0 : 튜토리얼 ( 장비 착용, 포션 사용 ) , type = 1 : 임무 ( 적 처치 , n 층 등반 )
        public int gold { get; set; } = 0;
        public bool condition { get; set; } = false;

        public bool rewarded { get; set; } = false;

        public Quest(string name, string info, int gold)
        {
            this.name = name;
            this.info = info;
            this.gold = gold;
        }

        public void ClearQuest()
        {
            string[] types = { "튜토리얼", "임무" };
            Console.Clear();
            ConsoleUtility.ShowTitle("■ 모험가 길드 ■ - 퀘스트 완료");
            Console.WriteLine("튜토리얼과 임무를 수락하거나 정산받을 수 있습니다.\n");

            ConsoleUtility.PrintTextHighlights("", $"[{types[type]} 완료]\n");


            if(rewarded == false)
            {
                Console.WriteLine("보상을 지급 받습니다...");

                Console.WriteLine($"\n골 드 {GameManager.player.Gold} -> ");

                ConsoleUtility.Animation2(14, 7, GameManager.player.Gold, GameManager.player.Gold + gold,5,100);
                GameManager.player.Gold += gold;
                Thread.Sleep(300);

                Console.WriteLine("정산이 완료되었습니다 !");
                rewarded = true;
            }
            else
            {
                Thread.Sleep(500);
                Console.WriteLine("...\n");
                
                Thread.Sleep(500);
                Console.WriteLine("...\n");
                Thread.Sleep(500);

                Console.WriteLine("\n이미 정산 처리된 건입니다.\n");
            }

            ConsoleUtility.PromptReturn();
        }
        public virtual void Print() { }
        public virtual void Check() { }
    }
    class Tutorial : Quest
    {
        public Tutorial(string name, string info,int gold ,int type ) : base(name, info, gold )
        {
            type = 0;
        }

        public override void Check()
        {
            if(condition == true)
            {
                GameManager.player.Gold += gold;
            }
        }

        public override void Print()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ 모험가 길드 ■ - 튜토리얼 ");
            Console.WriteLine("튜토리얼과 임무를 수락하거나 정산받을 수 있습니다.\n");

            ConsoleUtility.PrintTextHighlights("", $"[{name}]\n");
            Console.WriteLine($"{info}\n");
            Console.WriteLine($"- 보상 : {gold} Gold\n");

            Console.WriteLine("\n1. 수락");
            Console.WriteLine("2. 거절");
            Console.WriteLine("0. 나가기\n");

            int command = ConsoleUtility.PromptMenuChoice(0, 3);

            if (command == 0)
                return;

            else if (command == 1)
            {
                if(isAccept == true)
                {
                    Console.WriteLine("\n이미 수락한 퀘스트 입니다.");
                    ConsoleUtility.PromptReturn();
                }
                isAccept = true;

            }
            else if (command == 2)
            {
                if(isAccept == false)
                {
                    Console.WriteLine("\n수락하지 않은 퀘스트 입니다.");
                    ConsoleUtility.PromptReturn();
                }
                isAccept = false;
            }
            else
                Print();

        }

    }
    class Mission : Quest
    {
        public int id { get; set; } = -1; // type = 1 일 때, 처치해야할 적 id
        public int cnt { get; set; } = 0; // typq = 1 일 때, 처치해야할 적 숫자
        public int cur { get; set; } = 0;

        public Mission(string name, string info,int type, int id, int cnt, int gold  ) : base(name, info, gold)
        {
            this.type = type;
            this.id = id;
            this.cnt = cnt;
        }

        public override void Print()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ 모험가 길드 ■ - 임무 ");
            Console.WriteLine("튜토리얼과 임무를 수락하거나 정산받을 수 있습니다.\n");

            ConsoleUtility.PrintTextHighlights("", $"[{name}]\n");
            Console.WriteLine($"{info}\n");
            Console.WriteLine($"- 보상 : {gold} Gold\n");

            Console.WriteLine("\n1. 수락");
            Console.WriteLine("2. 거절");
            Console.WriteLine("0. 나가기\n");

            int command = ConsoleUtility.PromptMenuChoice(0, 3);

            if (command == 0)
                return;
            else if (command == 1)
            {
                if (isAccept == true)
                {
                    Console.WriteLine("\n이미 수락한 퀘스트 입니다.");
                    ConsoleUtility.PromptReturn();
                }
                isAccept = true;

            }
            else if (command == 2)
            {
                if (isAccept == false)
                {
                    Console.WriteLine("\n수락하지 않은 퀘스트 입니다.");
                    ConsoleUtility.PromptReturn();
                }
                isAccept = false;
                cur = 0;
            }
            else
                Print();
        }

    }
}
