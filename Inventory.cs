

namespace Sparta2ndTeam_TeamProject
{
    internal class Inventory
    {
        internal static void InventoryMenu()
        {
            while (true)
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("< 인벤토리 >\n");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                for(int i=0;i<GameManager.items.Count;i++) 
                {
                    //구매가 된 상태의 아이템들을 인벤토리 메뉴에 표기
                    if (GameManager.items[i].isPurchased)
                        GameManager.items[i].PrintItemStatDesc();
                }

                Console.WriteLine("\n\n1. 장착 관리\n0. 나가기\n");

                int command = ConsoleUtility.PromptMenuChoice(0, 1);

                switch(command)
                {
                    case (int)SelectInventoryMenu.PreviousPage:
                        return;
                    case (int)SelectInventoryMenu.EquipMenu:
                        EquipMenu();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        private static void EquipMenu()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("< 인벤토리 - 장착 관리 >\n");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            for (int i = 0; i < GameManager.items.Count; i++)
            {
                //구매가 된 상태의 아이템들을 인벤토리 메뉴에 표기
                if (GameManager.items[i].isPurchased)
                    GameManager.items[i].PrintItemStatDesc(true, i+1);
            }

            int command = ConsoleUtility.PromptMenuChoice(0, GameManager.items.Count);

            switch (command)
            {
                case (int)SelectInventoryMenu.PreviousPage:
                    return;
                case (int)SelectInventoryMenu.WrongCommand:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor();
                    break;
                default:
                    GameManager.items[command - 1].ToggleEquipStatus();
                    EquipMenu();
                    break;
            }
        }

        private enum SelectInventoryMenu
        {
            PreviousPage,
            EquipMenu,
            WrongCommand=-1,
        }
    }
}