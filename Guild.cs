using Sparta2ndTeam_TeamProject;
using Sparta2ndTeam_TeamProject.Battle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel.Design;
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
                Console.WriteLine("4. 혈석 판매");
                Console.WriteLine("0. 나가기\n");


                if (command == (int)SelectBreakMenu.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("잘못된 입력입니다.");
                    Console.ResetColor();
                    Console.WriteLine();
                }
                command = ConsoleUtility.PromptMenuChoice(0, 4);

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
                    case (int)SelectBreakMenu.SellDropItem:
                        SalesMenu();
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
                    Healing(1);

                else if (GameManager.player.Gold < 500) // 회복하기 (2) - 돈이 없음
                    Healing(2);

                else // 회복
                    Healing(3);

                return;
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

                    else if (GameManager.quests[i].type == 0 && GameManager.quests[i].isAccept == true)
                        Console.WriteLine("... [진행중]");

                    else if (GameManager.quests[i].type == 1 && GameManager.quests[i].isAccept == true)
                    {
                        Console.WriteLine($"... [진행중] {GameManager.quests[i].cur} / {GameManager.quests[i].cnt}");
                    }


                    else
                        Console.WriteLine("");

                    if (i != length - 1 && GameManager.quests[i].type != GameManager.quests[i + 1].type)
                    {
                        ConsoleUtility.PrintTextHighlights("", "\n[임무 목록]\n");
                    }
                }

                Console.WriteLine("\n0. 나가기\n");
                command = ConsoleUtility.PromptMenuChoice(0, length);

                if (command == 0)
                    return;

                else if (command == -1)
                    continue;

                else if (GameManager.quests[command - 1].isComplete == true)
                    GameManager.quests[command - 1].ClearQuest();
                else
                    GameManager.quests[command - 1].Print();

            }
        }
        private static void SalesMenu()
        {
            bool sellComplete = false;
            while (true)
            {
                List<Item> storeItems = new List<Item>();

                Console.Clear();
                ConsoleUtility.ShowTitle("■ 탐험가 길드 - 혈석 판매 ■");
                Console.WriteLine("혈석을 고가로 판매할 수 있는 길드 상점입니다.\n");

                ConsoleUtility.PrintTextHighlights("", "[보유 골드]");
                Console.WriteLine($"{GameManager.player.Gold} G\n");

                ConsoleUtility.PrintTextHighlights("", "[보유 혈석 목록]");

                for (int i = 0; i < GameManager.dropItems.Count; i++)
                {
                    //인벤토리에 들어있는 혈석의 정보를 리스트에 추가 
                    if (GameManager.dropItems[i].isPurchased)
                    {
                        storeItems.Add(GameManager.dropItems[i]);
                    }
                }

                if (storeItems.Count > 0)
                {
                    for (int i = 0; i < storeItems.Count; i++)
                    {
                        storeItems[i].PrintItemStatDesc(true, i + 1);
                        Console.Write(" | ");
                        Console.Write($"{(int)Math.Round(1.3 * storeItems[i].Price)} G");

                        Console.ForegroundColor = ConsoleColor.Green;

                        if (storeItems[i].Name == "작은 혈석 조각")
                            Console.Write($"\t| {Inventory.dropItemsCnt[0]}개 보유중");

                        else if (storeItems[i].Name == "일반 혈석")
                            Console.Write($"\t| {Inventory.dropItemsCnt[1]}개 보유중");

                        else if (storeItems[i].Name == "거대한 혈석")
                            Console.Write($"\t| {Inventory.dropItemsCnt[2]}개 보유중");

                        Console.WriteLine();
                        Console.ResetColor();
                    }
                }
                else
                    Console.WriteLine("~~ 보유중인 혈석이 없습니다. ~~");

                if (command == (int)SelectBreakMenu.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("잘못된 입력입니다.");
                    Console.ResetColor();
                    Console.WriteLine();
                }


                Console.WriteLine("\n0. 나가기");
                command = ConsoleUtility.PromptMenuChoice(0, storeItems.Count);

                if (command == (int)SelectBreakMenu.PreviousPage) return;
                //정상적인 입력이 들어왔다면, 
                else if (command != (int)SelectBreakMenu.WrongCommand)
                {
                    foreach (Item item in storeItems)
                    {
                        if ((command - 1) == storeItems.IndexOf(item))
                        {
                            int idx = 0;

                            switch (item.Name)
                            {
                                case "작은 혈석 조각":
                                    idx = 0;
                                    break;
                                case "일반 혈석":
                                    idx = 1;
                                    break;
                                case "거대한 혈석":
                                    idx = 2;
                                    break;
                                default: break;
                            }

                            Inventory.dropItemsCnt[idx]--;
                            if (Inventory.dropItemsCnt[idx] <= 0)
                            {
                                Inventory.dropItemsCnt[idx] = 0;

                                //혈석을 모두 판매했다면 해당 혈석에 대한 정보를 현재의 리스트에서 삭제 
                                //storeItems.Remove(storeItems[command - 1]);

                                //구매 여부를 false로 변경해야 하나, 이는 배틀 후 보상 로직을 한 번 봐야할 것 같음 
                                storeItems[command - 1].TogglePurchaseStatus();
                            }

                            //길드는 원래 혈석의 값어치에서 1.3배만큼의 가격으로 구매해줌
                            int refund = (int)Math.Round(1.3 * item.Price);
                            GameManager.player.Gold += refund;
                            sellComplete = true;

                        }
                    }
                }

                if (sellComplete)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("!! 판매를 완료하였습니다. !!");
                    Console.ResetColor();
                    sellComplete = false;
                    Thread.Sleep(500);
                }
            }
        }

        private enum SelectBreakMenu
        {
            PreviousPage,
            RestMenu,
            SaveData,
            QuestMenu,
            SellDropItem,
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

        public bool rewarded { get; set; } = false;
        public int id { get; set; } = -1; // type = 1 일 때, 처치해야할 적 id
        public int cnt { get; set; } = 0; // typq = 1 일 때, 처치해야할 적 숫자
        public int cur { get; set; } = 0;

        public Quest(string name, string info, int type, int gold, int id = 0, int cnt = 0, int cur = 0)
        {
            this.name = name;
            this.info = info;
            this.type = type;
            this.gold = gold;
            this.id = id;
            this.cnt = cnt;
            this.cur = cur;
        }

        public void ClearQuest()
        {
            string[] types = { "튜토리얼", "임무" };
            Console.Clear();
            ConsoleUtility.ShowTitle("■ 모험가 길드 ■ - 퀘스트 완료");
            Console.WriteLine("퀘스트를 수락하거나 정산받을 수 있습니다.\n");

            ConsoleUtility.PrintTextHighlights("", $"[{types[type]} 완료]\n");


            if (rewarded == false)
            {
                Console.WriteLine("보상을 지급 받습니다...");

                Console.WriteLine($"\n골 드 {GameManager.player.Gold} -> ");

                ConsoleUtility.Animation2(14, 7, GameManager.player.Gold, GameManager.player.Gold + gold, 11, 50);
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
        public void Print()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ 모험가 길드 ■ - 퀘스트 ");
            Console.WriteLine("퀘스트를 수락하거나 정산받을 수 있습니다.\n");

            ConsoleUtility.PrintTextHighlights("", $"[{name}]\n");
            Console.WriteLine($"{info}\n");
            Console.WriteLine($"- 보상 : {gold} Gold\n");

            Console.WriteLine("\n1. 수락");
            Console.WriteLine("2. 거절");
            Console.WriteLine("0. 나가기\n");

            int command = ConsoleUtility.PromptMenuChoice(0, 2);

            if (command == -1)
                Print();

            else if (command == 0)
                return;

            else if (command == 1)
            {
                if (isAccept == true)
                {
                    Console.WriteLine("\n이미 수락한 퀘스트 입니다.");
                    ConsoleUtility.PromptReturn();
                }
                else
                {
                    isAccept = true;
                }

                return;
            }

            else
            {
                if (isAccept == false)
                {
                    Console.WriteLine("\n수락하지 않은 퀘스트 입니다.");
                    ConsoleUtility.PromptReturn();
                }
                else // 수락한 퀘스트를 포기
                {
                    Console.WriteLine("정말로 퀘스트를 포기하시겠습니까? :  0 - 포기하지 않는다, 1 - 포기한다\n");
                    command = ConsoleUtility.PromptMenuChoice(0, 1);

                    if (command == -1)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                    else if (command == 0)
                        return;

                    else
                    {
                        if (type == 1)
                            cur = 0;
                        isAccept = false;
                        return;
                    }
                }
            }
        }
    }
}
