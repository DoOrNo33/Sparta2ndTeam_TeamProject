

namespace Sparta2ndTeam_TeamProject
{
    internal class Inventory
    {
        static int command; 
        static List<Item> invenItems;
        internal static void InventoryMenu()
        {
            while (true)
            {
                invenItems = new List<Item>();
                Console.Clear();

                ConsoleUtility.ShowTitle("■ 인벤토리 ■");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                ConsoleUtility.PrintTextHighlights("", "[아이템 목록]");
                
                for (int i = 0; i < GameManager.items.Count; i++)
                {
                    //구매가 된 상태의 아이템들을 새로운 storeItems 리스트에 추가 
                    if (GameManager.items[i].isPurchased)
                    {
                        invenItems.Add(GameManager.items[i]);
                    }
                }

                for (int i = 0; i < invenItems.Count; i++)
                {
                    invenItems[i].PrintItemStatDesc(true, i + 1);
                    Console.WriteLine();
                }

                Console.WriteLine("\n\n1. 장착 관리\n0. 나가기\n");

                if (command == (int)SelectInventoryMenu.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("잘못된 입력입니다.");
                    Console.ResetColor();
                    Console.WriteLine();
                }

                command = ConsoleUtility.PromptMenuChoice(0, 1);

                switch (command)
                {
                    case (int)SelectInventoryMenu.PreviousPage:
                        return;
                    case (int)SelectInventoryMenu.EquipMenu:
                        EquipMenu();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void EquipMenu()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ 인벤토리 - 장착 관리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            ConsoleUtility.PrintTextHighlights("", "[아이템 목록]");
            for (int i = 0; i < invenItems.Count; i++)
            {
                invenItems[i].PrintItemStatDesc(true, i+1);
                Console.WriteLine();

            }

            Console.WriteLine();
            if (command == (int)SelectInventoryMenu.WrongCommand)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("잘못된 입력입니다.");
                Console.ResetColor();
                Console.WriteLine();
            }

            command = ConsoleUtility.PromptMenuChoice(0, invenItems.Count);

            switch (command)
            {
                case (int)SelectInventoryMenu.PreviousPage:
                    return;
                case (int)SelectInventoryMenu.WrongCommand:
                    EquipMenu();
                    break;
                default:
                    setEquipItems(command);
                    EquipMenu();
                    break;
            }
        }

        static void setEquipItems(int command)
        {
            invenItems[command - 1].ToggleEquipStatus();

            if (invenItems[command-1].Atk!=0 || invenItems[command-1].Def!=0 || invenItems[command-1].HP!=0) 
            {
                GameManager.player.Atk += invenItems[command - 1].Atk;
                GameManager.player.Def += invenItems[command - 1].Def;
                GameManager.player.Hp += invenItems[command - 1].HP;
            }
            //현재 인벤토리에 있는 아이템 중, 
            for (int i = 0; i < invenItems.Count; i++)
            {
                //현재 착용한 아이템과 타입이 일치하는 아이템이
                if (i != (command-1) && invenItems[command - 1]._type == invenItems[i]._type)
                {
                    //장착 상태라면, 
                    if (invenItems[i].isEquipped)
                    {
                        //장착 상태를 해제
                        invenItems[i].ToggleEquipStatus();
                        if (invenItems[command - 1].Atk != 0 || invenItems[command - 1].Def != 0 || invenItems[command - 1].HP != 0)
                        {
                            GameManager.player.Atk -= invenItems[i].Atk;
                            GameManager.player.Def -= invenItems[i].Def;
                            GameManager.player.Hp -= invenItems[i].HP;
                        }
                    }
                }
            }
        }
        private enum SelectInventoryMenu
        {
            PreviousPage,
            EquipMenu,
            WrongCommand = -1,
        }
    }
}