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
                Console.WriteLine("회복과 저장, 그리고 퀘스트를 수락할 수 있습니다.\n");

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
                        DataManager.SaveData();
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
                Console.WriteLine($"500 G 를 내면 체력과 마나를 회복할 수 있습니다. (보유 골드 : {GameManager.player.Gold} G)\n");

                ConsoleUtility.PrintTextHighlights("", $"[현재 체력 : {GameManager.player.Hp} / 현재 마나 : {GameManager.player.Mp}]\n");

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

                if (command == 0)
                    return;

                else if (command == -1)
                    continue;

                if (GameManager.player.Hp == GameManager.player.Max_Hp && GameManager.player.Mp == GameManager.player.Max_Mp)
                    Healing(1);

                else if (GameManager.player.Gold < 500)
                    Healing(2);

                else
                    Healing(3);

                return;
            }
        }

        static public void Healing(int num)
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ 모험가 길드 - 휴식 하기 ■");
            Console.WriteLine($"500 G 를 내면 체력과 마나를 회복할 수 있습니다. (보유 골드 : {GameManager.player.Gold} G)\n");

            ConsoleUtility.PrintTextHighlights("", $"[현재 체력 : {GameManager.player.Hp} / 현재 마나 : {GameManager.player.Mp}]\n");

            if (num == 1)
            {
                Console.WriteLine("\n이미 건강한 상태입니다.");
                ConsoleUtility.PromptReturn();
            }
            else if (num == 2)
            {
                Console.WriteLine("\nGold 가 부족합니다.");
                ConsoleUtility.PromptReturn();
            }
            else if (num == 3)
            {
                Thread.Sleep(200);
                Console.WriteLine("\n휴식을 취하는 중입니다...\n");

                
                Console.Write($"체 력 {GameManager.player.Hp} -> ");
                int start_hp = GameManager.player.Hp;

                GameManager.player.Hp = GameManager.player.Max_Hp;

                if (start_hp < GameManager.player.Max_Hp)
                    ConsoleUtility.Animation(13, 8, start_hp, GameManager.player.Max_Hp);

                else
                    Console.Write($"{GameManager.player.Max_Hp}\n");
                Console.WriteLine();
                
                Thread.Sleep(150);

                Console.Write($"마 나 {GameManager.player.Mp} -> ");
                int start_mp = GameManager.player.Mp;

                GameManager.player.Mp = GameManager.player.Max_Mp;

                if (start_mp < GameManager.player.Max_Mp)
                    ConsoleUtility.Animation(13, 10, start_mp, GameManager.player.Max_Mp);

                else
                    Console.WriteLine($"{GameManager.player.Max_Mp}\n"); 
                Thread.Sleep(150);

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
                        Console.WriteLine($"... [진행중] {GameManager.quests[i].cur} / {GameManager.quests[i].cnt}");

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

                ConsoleUtility.PrintTextHighlights("", $"[보유 골드 : {GameManager.player.Gold} G]\n\n");


                ConsoleUtility.PrintTextHighlights("", "[보유 혈석 목록]\n");

                for (int i = 0; i < GameManager.dropItems.Count; i++)
                {
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

                


                Console.WriteLine("\n0. 나가기");
                command = ConsoleUtility.PromptMenuChoice(0, storeItems.Count);

                if (command == (int)SelectBreakMenu.PreviousPage) 
                    return;

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
                                storeItems[command - 1].TogglePurchaseStatus();
                            }

                            int refund = (int)Math.Round(1.3 * item.Price);
                            GameManager.player.Gold += refund;
                            sellComplete = true;

                        }
                    }
                }
                if (command == (int)SelectBreakMenu.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("잘못된 입력입니다.");
                    Console.ResetColor();
                    Console.WriteLine();
                    Thread.Sleep(500);
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
}
