
namespace Sparta2ndTeam_TeamProject
{
    internal class Store
    {
        //인덱스 순서대로 소형 hp 포션, 대형 hp 포션, 소형 mp 포션, 대형 mp 포션
        static public int[] storePortionCnt = { 3, 3, 3, 3 };

        static int command = 0;
        static public void StoreMenu()
        {
            while (true)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ 상점 ■");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                ConsoleUtility.PrintTextHighlights("", $"[보유 골드 : {GameManager.player.Gold} G]\n");

                ConsoleUtility.PrintTextHighlights("", "[아이템 목록]\n");

                drawCurrentShoppingList(false);

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

                ConsoleUtility.PrintTextHighlights("", $"[보유 골드 : {GameManager.player.Gold} G]\n");


                ConsoleUtility.PrintTextHighlights("", "[아이템 목록]");


                drawCurrentShoppingList(true);
                
                Console.WriteLine();

                

                if (command == (int)SelectStoreMenu.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("잘못된 입력입니다.");
                    Console.ResetColor();
                    Console.WriteLine();
                }

                Console.WriteLine("\n\n\n0. 나가기\n");
                command = ConsoleUtility.PromptMenuChoice(0, GameManager.items.Count);

                if (command == (int)SelectStoreMenu.PreviousPage) return;
                else if(command != (int) SelectStoreMenu.WrongCommand)
                {
                    if (!GameManager.items[command - 1].isPurchased)
                    {
                        string str = GameManager.items[command - 1].Name;
                        int idx = -1;

                        if (GameManager.player.Gold >= GameManager.items[command - 1].Price)
                        {
                            if (GameManager.items[command-1]._type != ItemType.PORTION)
                            {
                                GameManager.items[command - 1].TogglePurchaseStatus();
                            }

                            else
                            {
                                switch (str)
                                {
                                    case "소형 체력 포션":
                                        idx = 0;
                                        break;
                                    case "대형 체력 포션":
                                        idx = 1;
                                        break;
                                    case "소형 마나 포션":
                                        idx = 2;
                                        break;
                                    case "대형 마나 포션":
                                        idx = 3;
                                        break;
                                    default:
                                        break;
                                }
                                if (storePortionCnt[idx] <= 1)
                                {
                                    GameManager.items[command - 1].TogglePurchaseStatus();
                                }
                            }

                            GameManager.player.Gold -= GameManager.items[command - 1].Price;
                            currentShopState = CurrentShopState.Success;


                            if (GameManager.items[command - 1]._type == ItemType.PORTION)
                            {
                                Inventory.portionCnt[idx]++;
                                storePortionCnt[idx]--;
 
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

                if (currentShopState != null)
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
                            Console.WriteLine("!! 재고가 모두 소진되었습니다. !!");
                            Console.ResetColor();
                            break;
                        default:
                            break;
                    }
                    Thread.Sleep(500);
                }
                currentShopState = null;
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
                Console.WriteLine("아이템을 판매하여 골드를 획득할 수 있습니다.\n");

                ConsoleUtility.PrintTextHighlights("", "[보유 골드]");
                Console.WriteLine($"{GameManager.player.Gold} G\n");

                ConsoleUtility.PrintTextHighlights("", "[아이템 목록]");


                for (int i = 0; i < GameManager.items.Count; i++)
                {
                    if (GameManager.items[i].isPurchased || GameManager.items[i].isInitItem)
                    {
                        storeItems.Add(GameManager.items[i]);
                    }
                }
                for(int i=0;i<GameManager.dropItems.Count;i++)
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
                        Console.Write($"{(int)Math.Round(0.85 * storeItems[i].Price)} G");

                        if (storeItems[i]._type == ItemType.PORTION)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;

                            if (storeItems[i].Name == "소형 체력 포션")
                                Console.Write($"\t| {Inventory.portionCnt[0]}개 보유중");
                            else if (storeItems[i].Name == "대형 체력 포션")
                                Console.Write($"\t| {Inventory.portionCnt[1]}개 보유중");
                            else if (storeItems[i].Name == "소형 마나 포션")
                                Console.Write($"\t| {Inventory.portionCnt[2]}개 보유중");
                            else if (storeItems[i].Name == "대형 마나 포션")
                                Console.Write($"\t| {Inventory.portionCnt[3]}개 보유중");

                            Console.ResetColor();
                        }
                        Console.WriteLine();
                    }
                }
                else
                    Console.WriteLine("~~ 보유중인 아이템이 없습니다. ~~");


                Console.WriteLine("\n0. 나가기\n\n");
                

                command = ConsoleUtility.PromptMenuChoice(0, storeItems.Count);


                if (command == (int)SelectStoreMenu.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor();
                    Thread.Sleep(500);
                }
                else if (command == (int)SelectStoreMenu.PreviousPage) return;
                else
                {
                    foreach (Item item in storeItems)
                    {
                        if ((command - 1) == storeItems.IndexOf(item))
                        {
                            ConsoleUtility.PrintTextHighlights("정말로 ", item.Name, "을(를) 판매하시겠습니까? ('예'입력 시 그대로 진행.) ");

                            Console.Write(">> ");
                            if("예" == Console.ReadLine())
                            {
                                if (item._type == ItemType.PORTION)
                                {
                                    if (item.Name == "소형 체력 포션")
                                    {
                                        Inventory.portionCnt[0]--;

                                        if (Inventory.portionCnt[0] <= 0)
                                        {
                                            Inventory.portionCnt[0] = 0;
                                            item.TogglePurchaseStatus();
                                        }
                                    }
                                    else if (item.Name == "대형 체력 포션")
                                    {
                                        Inventory.portionCnt[1]--;

                                        if (Inventory.portionCnt[1] <= 0)
                                        {
                                            Inventory.portionCnt[1] = 0;
                                            item.TogglePurchaseStatus();
                                        }
                                    }
                                }
                                else if (item._type == ItemType.MONSTER_DROP)
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.Write("나보고 ");
                                    Thread.Sleep(300);
                                    Console.Write("이런 ");
                                    Thread.Sleep(300);
                                    Console.Write("돌 덩어리를 ");
                                    Thread.Sleep(300);
                                    Console.Write("사라고? \n");
                                    Thread.Sleep(500);
                                    Console.Write("누굴 ");
                                    Thread.Sleep(300);
                                    Console.Write("바보로 ");
                                    Thread.Sleep(300);
                                    Console.Write("아나! \n");
                                    Thread.Sleep(500);
                                    Console.Write("길드 사람들은 ");
                                    Thread.Sleep(300);
                                    Console.Write("꼭 이런 돌에 ");
                                    Thread.Sleep(300);
                                    Console.Write("환장을 하던데 ");
                                    Thread.Sleep(300);
                                    Console.Write("... \n");
                                    Thread.Sleep(500);
                                    Console.Write("마을에 ");
                                    Thread.Sleep(300);
                                    Console.Write("괴짜가 ");
                                    Thread.Sleep(300);
                                    Console.Write("늘었구만. ");
                                    Thread.Sleep(500);
                                    Console.Write("*쯧*\n ");
                                    Thread.Sleep(2000);
                                    continue;
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

                                int refund = (int)Math.Round(0.85 * item.Price);
                                GameManager.player.Gold += refund;
                                sellComplete = true;
                            }
                            else
                            {
                                ConsoleUtility.PrintTextHighlights("", "판매 작업을 취소합니다. ");
                                Thread.Sleep(500);
                            }

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

        private static void drawCurrentShoppingList(bool isPurchaseActive)
        {
            for (int i = 0; i < GameManager.items.Count; i++)
            {
                if (i == 0)
                {
                    ConsoleUtility.PrintTextHighlights("[", GameManager.items[i]._type.ToString(), "]");
                }
                else if (i < GameManager.items.Count - 1)
                {
                    if (GameManager.items[i]._type != GameManager.items[i - 1]._type)
                    {
                        ConsoleUtility.PrintTextHighlights("\n[", GameManager.items[i]._type.ToString(), "]");
                    }
                }

                if (!isPurchaseActive)
                    GameManager.items[i].PrintItemStatDesc();
                else
                    GameManager.items[i].PrintItemStatDesc(true, i + 1);

                if (GameManager.items[i]._type == ItemType.PORTION)
                {
                    string str = GameManager.items[i].Name;
                    int idx = 0;

                    if (!GameManager.items[i].isPurchased)
                    {
                        Console.Write(" | ");
                        Console.Write($"{GameManager.items[i].Price} G");

                        switch (str)
                        {
                            case "소형 체력 포션":
                                idx = 0;
                                break;
                            case "대형 체력 포션":
                                idx = 1;
                                break;
                            case "소형 마나 포션":
                                idx = 2;
                                break;
                            case "대형 마나 포션":
                                idx = 3;
                                break;
                            default:
                                break;
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\t| 재고: {storePortionCnt[idx]}개");
                        Console.ResetColor();
                    }
                    else
                    {
                        ConsoleUtility.PrintTextHighlights(" | ", "판매 완료");
                    }
                }
                else
                {
                    if (!GameManager.items[i].isPurchased)
                    {
                        Console.Write(" | ");
                        Console.WriteLine($"{GameManager.items[i].Price} G");
                    }
                    else
                    {
                        ConsoleUtility.PrintTextHighlights(" | ", "판매 완료");
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