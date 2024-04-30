﻿
namespace Sparta2ndTeam_TeamProject
{
    internal class Store
    {
        static int command = 0;
        static public void StoreMenu()
        {
            while (true)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ 상점 ■");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                ConsoleUtility.PrintTextHighlights("", "[보유 골드]");
                Console.WriteLine($"{GameManager.player.Gold} G\n");

                ConsoleUtility.PrintTextHighlights("", "[아이템 목록]");
                for (int i = 0; i < GameManager.items.Count; i++)
                {
                    if (i == 0) 
                    {
                        ConsoleUtility.PrintTextHighlights("++++ ", GameManager.items[i]._type.ToString(), " ++++");
                    }
                    else if (i < GameManager.items.Count - 1)
                    {
                        if (GameManager.items[i]._type != GameManager.items[i - 1]._type)
                        {
                            ConsoleUtility.PrintTextHighlights("\n++++ ", GameManager.items[i]._type.ToString(), " ++++");
                        }
                    }

                    GameManager.items[i].PrintItemStatDesc();
                    //아이템이 아직 구매되지 않은 상태라면, 
                    if (!GameManager.items[i].isPurchased)
                    {
                        Console.Write(" | ");
                        Console.WriteLine($"{GameManager.items[i].Price} G");
                    }
                    //아이템이 구매가 된 상태라면, 
                    else
                    {
                        ConsoleUtility.PrintTextHighlights(" | ", "판매 완료");
                    }

                }

                Console.WriteLine("\n\n1. 아이템 구매\n2. 아이템 판매\n0. 나가기\n");

                if (command == (int)SelectStoreMenu.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("잘못된 입력입니다.");
                    Console.ResetColor();
                    Console.WriteLine();
                }
                command = ConsoleUtility.PromptMenuChoice(0, 2);

                switch (command)
                {
                    case (int)SelectStoreMenu.PreviousPage:
                        return;
                    case (int)SelectStoreMenu.PurchaseMenu:
                        PurchaseMenu();
                        break;
                    case (int)SelectStoreMenu.SalesMenu:
                        SalesMenu();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void PurchaseMenu()
        {

            CurrentShopState? currentShopState = null;

            while (true)
            {

                Console.Clear();

                ConsoleUtility.ShowTitle("■ 상점 - 아이템 구매 ■");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                ConsoleUtility.PrintTextHighlights("", "[보유 골드]");
                Console.WriteLine($"{GameManager.player.Gold} G\n");

                ConsoleUtility.PrintTextHighlights("", "[아이템 목록]");

                for (int i = 0; i < GameManager.items.Count; i++)
                {
                    if (i == 0)
                    {
                        ConsoleUtility.PrintTextHighlights("++++ ", GameManager.items[i]._type.ToString(), " ++++");
                    }
                    else if (i < GameManager.items.Count - 1)
                    {
                        if (GameManager.items[i]._type != GameManager.items[i - 1]._type)
                        {
                            ConsoleUtility.PrintTextHighlights("\n++++ ", GameManager.items[i]._type.ToString(), " ++++");
                        }
                    }

                    GameManager.items[i].PrintItemStatDesc(true, i + 1);
                    //아이템이 아직 구매되지 않은 상태라면, 
                    if (!GameManager.items[i].isPurchased)
                    {
                        Console.Write(" | ");
                        Console.WriteLine($"{GameManager.items[i].Price} G");
                    }
                    //아이템이 구매가 된 상태라면, 
                    else
                    {
                        ConsoleUtility.PrintTextHighlights(" | ", "판매 완료");
                    }
                }
                
                Console.WriteLine();

                if(currentShopState != null)
                {
                    switch (currentShopState)
                    {
                        case CurrentShopState.Success:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("!! 구매를 완료했습니다. !!");
                            Console.ResetColor();
                            break;
                        case CurrentShopState.InsufficientGold:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("!! Gold가 부족합니다 !!");
                            Console.ResetColor();
                            break;
                        case CurrentShopState.SoldOut:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("!! 이미 구매한 아이템입니다. !!");
                            Console.ResetColor();
                            break;
                        default:
                            break;
                    }
                }
                currentShopState = null;

                if (command == (int)SelectStoreMenu.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("잘못된 입력입니다.");
                    Console.ResetColor();
                    Console.WriteLine();
                }

                command = ConsoleUtility.PromptMenuChoice(0, GameManager.items.Count);

                if (command == (int)SelectStoreMenu.PreviousPage) return;
                else if(command != (int) SelectStoreMenu.WrongCommand)
                {
                    if (!GameManager.items[command - 1].isPurchased)
                    {
                        //현재 소지 금액이 아이템의 판매 금액보다 많다면, 
                        if (GameManager.player.Gold >= GameManager.items[command - 1].Price)
                        {
                            //현재 아이템의 판매 상태를 true로 변경
                            GameManager.items[command - 1].TogglePurchaseStatus();

                            //캐릭터의 소지 금액에서 아이템의 가격만큼 차감
                            GameManager.player.Gold -= GameManager.items[command - 1].Price;

                            currentShopState = CurrentShopState.Success;

                            //현재 구매한 아이템의 종류가 포션이라면, 
                            if (GameManager.items[command - 1]._type == ItemType.PORTION)
                            {
                                //인벤토리에 포션의 수를 추가
                                if (GameManager.items[command - 1].Name == "소형 HP 포션")
                                    Inventory.SmallPortionCnt++;
                                else if (GameManager.items[command - 1].Name == "대형 HP 포션")
                                    Inventory.LargePortionCnt++;
                            }
                        }
                        else
                        {
                            currentShopState = CurrentShopState.InsufficientGold;
                        }
                    }
                    else
                    {
                        currentShopState = CurrentShopState.SoldOut;
                    }
                }

            }
        }

        private static void SalesMenu()
        {
            bool sellComplete = false;
            while (true)
            {
                List<Item> storeItems = new List<Item>();

                Console.Clear();
                ConsoleUtility.ShowTitle("■ 상점 - 아이템 판매 ■");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                ConsoleUtility.PrintTextHighlights("", "[보유 골드]");
                Console.WriteLine($"{GameManager.player.Gold} G\n");

                ConsoleUtility.PrintTextHighlights("", "[아이템 목록]");


                for (int i = 0; i < GameManager.items.Count; i++)
                {
                    if (GameManager.items[i].isPurchased)
                    {
                        storeItems.Add(GameManager.items[i]);
                    }
                }

                if (storeItems.Count > 0)
                {
                    for (int i = 0; i < storeItems.Count; i++)
                    {
                        storeItems[i].PrintItemStatDesc(true, i + 1);
                        Console.Write(" | ");
                        Console.WriteLine($"{(int)Math.Round(0.85 * storeItems[i].Price)} G"); //아이템의 판매 가격을 화면에 표시
                    }
                }
                else
                    Console.WriteLine("~~ 보유중인 아이템이 없습니다. ~~");


                Console.WriteLine();

                if (command == (int)SelectStoreMenu.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor();
                }
                if (sellComplete)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("!! 판매를 완료하였습니다. !!");
                    Console.ResetColor();
                    sellComplete = false;
                }

                command = ConsoleUtility.PromptMenuChoice(0, storeItems.Count);

                if (command == (int)SelectStoreMenu.PreviousPage) return;
                else if (command != (int)SelectStoreMenu.WrongCommand)
                {
                    foreach (Item item in storeItems)
                    {
                        //판매 관련 작업 
                        if ((command - 1) == storeItems.IndexOf(item))
                        {
                            //현재 판매하려는 아이템이 포션이라면, 
                            if(storeItems[command - 1]._type == ItemType.PORTION)
                            {
                                //포션의 종류를 파악한 개수가 2개 이상이라면 개수를 차감하는 방식으로,
                                //개수가 1개라면 다른 아이템과 동일한 방식으로 판매 기능을 작업
                                if (storeItems[command - 1].Name == "소형 HP 포션" && Inventory.SmallPortionCnt >=2 )
                                {
                                    Inventory.SmallPortionCnt--; 
                                }
                                else if (storeItems[command - 1].Name == "대형 HP 포션" && Inventory.LargePortionCnt >=2)
                                {
                                    Inventory.LargePortionCnt--;
                                }
                            }
                            else
                            {
                                if (item.isEquipped)
                                {
                                    item.ToggleEquipStatus();
                                    if (item.Atk != 0 || item.Def != 0 || item.HP != 0)
                                    {
                                        GameManager.player.Atk -= item.Atk;
                                        GameManager.player.Def -= item.Def;
                                        GameManager.player.Hp -= item.HP;
                                    }
                                }
                                item.TogglePurchaseStatus();
                            }

                            int refund = (int)Math.Round(0.85 * storeItems[command - 1].Price);
                            GameManager.player.Gold += refund;
                            sellComplete = true;
                            
                        }
                    }
                }
            }
        }

        private enum SelectStoreMenu
        {
            PreviousPage,
            PurchaseMenu,
            SalesMenu,
            WrongCommand = -1,
        }
        private enum CurrentShopState
        {
            Success,
            InsufficientGold,
            SoldOut,
        }
    }

}