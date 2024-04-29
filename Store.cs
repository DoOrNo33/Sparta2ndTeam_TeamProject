

namespace Sparta2ndTeam_TeamProject
{
    internal class Store
    {
        static int command = 0;
        static CurrentShopState? currentShopState = null;

        static public void StoreMenu()
        {
            while (true)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("< 상점 >");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                ConsoleUtility.PrintTextHighlights("", "[보유 골드]");
                Console.WriteLine($"{GameManager.player.Gold} G\n");

                ConsoleUtility.PrintTextHighlights("", "[아이템 목록]");
                for (int i = 0; i < GameManager.items.Count; i++)
                {
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

                if(command == (int)SelectStoreMenu.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n잘못된 입력입니다.");
                    Console.ResetColor();
                }

                Console.WriteLine("\n1. 아이템 구매\n2. 아이템 판매\n0. 나가기\n");

                command = ConsoleUtility.PromptMenuChoice(0, 2);
                switch (command)
                {
                    case (int)SelectStoreMenu.PreviousPage:
                        return;
                    case (int)SelectStoreMenu.PurchaseMenu:
                        PurchaseMenu();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void PurchaseMenu()
        {
            while (true)
            {

                Console.Clear();
                ConsoleUtility.ShowTitle("< 상점 - 아이템 구매 >");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                ConsoleUtility.PrintTextHighlights("", "[보유 골드]");
                Console.WriteLine($"{GameManager.player.Gold} G\n");

                ConsoleUtility.PrintTextHighlights("", "[아이템 목록]");

                for (int i = 0; i < GameManager.items.Count; i++)
                {
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
                            Console.WriteLine("!! 구매를 완료했습니다. !!");
                            break;
                        case CurrentShopState.InsufficientGold:
                            Console.WriteLine("!! Gold가 부족합니다 !!");
                            break;
                        case CurrentShopState.SoldOut:
                            Console.WriteLine("!! 이미 구매한 아이템입니다. !!");
                            break;
                        default:
                            break;
                    }

                    currentShopState = null;
                }

                if (command == (int)SelectStoreMenu.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n잘못된 입력입니다.");
                    Console.ResetColor();
                    Console.WriteLine();
                }

                command = ConsoleUtility.PromptMenuChoice(0, GameManager.items.Count);
                if (command == 0) return;
                if (command == (int)SelectStoreMenu.WrongCommand) PurchaseMenu();

                //입력한 숫자가 범위 내에 포함하나,
                //골드 부족, 이미 판매가 완료 된 아이템에 대한 상태를 파악하는 메서드 
                CheckIsPossibleCommand(command);
            }
            
        }

        private static void CheckIsPossibleCommand(int command)
        {
            //해당 아이템이 판매되지 않은 상태이면서,
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

        private enum SelectStoreMenu
        {
            PreviousPage,
            PurchaseMenu,
            SalesMenu,
            WrongCommand = -1,
        }
        private enum CurrentShopState
        {
            Success, //정상적으로 구매가 가능한 상태
            InsufficientGold, //골드가 부족한 상태
            SoldOut, //아이템이 이미 판매 완료가 된 상태
        }
    }
}