
namespace Sparta2ndTeam_TeamProject
{
    internal class Store
    {
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

                Console.WriteLine("\n\n1. 아이템 구매\n2. 아이템 판매\n0. 나가기\n");

                int command = ConsoleUtility.PromptMenuChoice(0, 2);
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
            Insufficient,
            SoldOut,
        }
    }
    
}