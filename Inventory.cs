
namespace Sparta2ndTeam_TeamProject
{
    internal class Inventory
    {
        static public int SmallPortionCnt = 3; //소형 포션의 개수 정보를 저장
        static public int LargePortionCnt = 0;  //대형 포션의 개수 정보를 저장 

        //초기에 소형 포션을 3개 가지고 있어야 하므로,
        //상점이나 던전 등의 경로로 포션이 인벤토리에 추가되지 않은 상태라면, → 이부분을 true, false 판단
        //임의로 소형 포션 인스턴스를 할당해주기 위한 변수 
        static public bool hasSmallPortion = false; 

        static int command; 

        //각각 포션과 장비 아이템을 저장하기 위한 리스트 
        static List<Item> portionItems;
        static List<Item> equipmentItems;
        internal static void InventoryMenu()
        {
            while (true)
            {
                //invenItems = new List<Item>();
                portionItems = new List<Item>();
                equipmentItems = new List<Item>();  

                Console.Clear();

                ConsoleUtility.ShowTitle("■ 인벤토리 ■");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                
                for (int i = 0; i < GameManager.items.Count; i++)
                {
                    //구매가 된 상태의 아이템들을 새로운 invenItems 리스트에 추가 
                    if (GameManager.items[i].isPurchased)
                    {
                        //invenItems.Add(GameManager.items[i]);

                        if (GameManager.items[i].Name == "소형 HP 포션")
                            hasSmallPortion = true;

                        if (GameManager.items[i]._type == ItemType.PORTION)
                        {
                            portionItems.Add(GameManager.items[i]);
                        }
                        else
                        {
                            equipmentItems.Add(GameManager.items[i]);
                        }
                    }
                }

                //현재 인벤토리에 포션이 없다면, (상점에서 구매, 던전에서 획득 등의 경로)
                if(!hasSmallPortion)
                {
                    //소형 포션 인스턴스를 추가
                    portionItems.Add(new Item("소형 HP 포션", "HP를 30만큼 회복합니다.", 0, 0, 30, 50, ItemType.PORTION, false,true));
                    hasSmallPortion = true;
                }

                ConsoleUtility.PrintTextHighlights("", "[장비 목록]");
                for(int i=0;i<equipmentItems.Count;i++)
                {
                    equipmentItems[i].PrintItemStatDesc(true, i + 1);
                    Console.WriteLine();
                }
                
                Console.WriteLine();

                ConsoleUtility.PrintTextHighlights("", "[포션 목록]");
                for(int i=0;i<portionItems.Count;i++)
                {
                    portionItems[i].PrintItemStatDesc(true, i + 1);
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                    
                    if (portionItems[i].Name == "소형 HP 포션")
                        Console.Write($"| {SmallPortionCnt}개 보유중");
                    else if (portionItems[i].Name == "대형 HP 포션")
                        Console.Write($"| {LargePortionCnt}개 보유중");

                    Console.ResetColor();

                    Console.WriteLine();
                }

                Console.WriteLine("\n\n1. 장착 관리\n2. 회복 아이템\n0. 나가기\n");

                if (command == (int)SelectInventoryMenu.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("잘못된 입력입니다.");
                    Console.ResetColor();
                    Console.WriteLine();
                }

                command = ConsoleUtility.PromptMenuChoice(0, 2);

                switch (command)
                {
                    case (int)SelectInventoryMenu.PreviousPage:
                        return;
                    case (int)SelectInventoryMenu.EquipMenu:
                        EquipMenu();
                        break;
                    case (int)SelectInventoryMenu.PortionMenu:
                        PortionMenu();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void PortionMenu()
        {
            while(true)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ 인벤토리 - 포션 관리 ■");
                Console.WriteLine("포션을 사용하여 체력이나 마나를 회복할 수 있습니다.\n\n");

                ConsoleUtility.PrintTextHighlights("", "[포션 목록]");
                for (int i = 0; i < portionItems.Count; i++)
                {
                    portionItems[i].PrintItemStatDesc(true, i + 1);

                    Console.ForegroundColor = ConsoleColor.Green;

                    if (portionItems[i].Name == "소형 HP 포션")
                        Console.Write($"| {SmallPortionCnt}개 보유중");
                    else if (portionItems[i].Name == "대형 HP 포션")
                        Console.Write($"| {LargePortionCnt}개 보유중");

                    Console.ResetColor();

                    Console.WriteLine();
                }

                switch (command)
                {
                    case (int)SelectInventoryMenu.WrongCommand:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("잘못된 입력입니다.");
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                    default:
                        break;
                }

                command = ConsoleUtility.PromptMenuChoice(0, portionItems.Count);

                switch (command)
                {
                    case (int)SelectInventoryMenu.PreviousPage:
                        return;
                    case (int)SelectInventoryMenu.WrongCommand:
                        break;
                    default:
                        usePotion(command);
                        break;
                }
            }
        }

        private static void EquipMenu()
        {
            while(true)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ 인벤토리 - 장착 관리 ■");
                Console.WriteLine("보유 중인 장비를 장착하거나 해제할 수 있습니다.\n");

                ConsoleUtility.PrintTextHighlights("", "[장비 목록]");
                for (int i = 0; i < equipmentItems.Count; i++)
                {
                    equipmentItems[i].PrintItemStatDesc(true, i + 1);
                    Console.WriteLine();
                }

                Console.WriteLine();

                switch (command)
                {
                    case (int)SelectInventoryMenu.WrongCommand:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("잘못된 입력입니다.");
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                    default:
                        break;
                }

                command = ConsoleUtility.PromptMenuChoice(0, equipmentItems.Count);

                switch (command)
                {
                    case (int)SelectInventoryMenu.PreviousPage:
                        return;
                    case (int)SelectInventoryMenu.WrongCommand:
                        break;
                    default:
                        setEquipItems(command);
                        break;
                }
            }
        }
        static void usePotion(int command)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("체력 회복이 완료되었습니다. {0} → ", GameManager.player.Hp);

            GameManager.player.Hp += portionItems[command - 1].HP;
            if (GameManager.player.Hp >= 100) GameManager.player.Hp = 100; //최대 체력은 100

            Console.WriteLine($"{GameManager.player.Hp}");
            Console.ResetColor();

            Thread.Sleep(500);

            //사용하고자 하는 포션을 이름 기준으로 분류
            if (portionItems[command - 1].Name == "소형 HP 포션")
            {
                SmallPortionCnt--;
                //포션을 모두 사용하였다면 invenItems 리스트에서 해당 정보를 삭제
                if (SmallPortionCnt <= 0)
                {
                    SmallPortionCnt = 0;
                    portionItems[command - 1].TogglePurchaseStatus();
                    portionItems.Remove(portionItems[command - 1]);
                }
            }
            else if (portionItems[command - 1].Name == "대형 HP 포션")
            {
                LargePortionCnt--;
                if (LargePortionCnt <= 0)
                {
                    LargePortionCnt = 0;
                    portionItems[command - 1].TogglePurchaseStatus();
                    portionItems.Remove(portionItems[command - 1]);
                }
            }

        }
        static void setEquipItems(int command)
        {
            equipmentItems[command - 1].ToggleEquipStatus();

            //현재 인벤토리에 있는 아이템 중, 
            for (int i = 0; i < equipmentItems.Count; i++)
            {
                //현재 착용한 아이템과 타입이 일치하는 아이템이
                if (i != (command - 1) && equipmentItems[command - 1]._type == equipmentItems[i]._type)
                {
                    //장착 상태라면, 
                    if (equipmentItems[i].isEquipped)
                    {
                        //장착 상태를 해제
                        equipmentItems[i].ToggleEquipStatus();
                    }
                }
            }
        }
        private enum SelectInventoryMenu
        {
            WrongCommand = -1,
            PreviousPage,
            EquipMenu,
            PortionMenu,
        }
    }
}
