namespace Sparta2ndTeam_TeamProject
{
    internal class Inventory
    {

        //다양한 혈석의 개수 정보를 저장 
        //인덱스 순서대로 작은 혈석 조각, 일반 혈석, 거대한 혈석
        static public int[] dropItemsCnt = { 0, 2, 0 };

        //인덱스 순서대로 소형 hp 포션, 대형 hp 포션, 소형 mp 포션, 대형 mp 포션
        static public int[] portionCnt = { 3, 0, 0, 0 };

        static int command; 

        //아이템을 저장하기 위한 리스트 
        static List<Item> portionItems = new List<Item>(); //포션
        static List<Item> equipmentItems = new List<Item>(); //장비
        static List<Item> monstorDropItems = new List<Item>(); //몬스터 드랍 아이템 
        internal static void InventoryMenu()
        {
            while (true)
            {
                //매 반복마다 아이템들에 대한 정보를 갱신 
                portionItems = new List<Item>();
                equipmentItems = new List<Item>();
                monstorDropItems = new List<Item>();

                Console.Clear();

                ConsoleUtility.ShowTitle("■ 인벤토리 ■");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");


                for (int i = 0; i < GameManager.items.Count; i++)
                {
                    //현재 아이템이 초기에 지급되는 아이템인 경우(현재는 우선 소형 포션만 이에 해당) 이면서,
                    if (GameManager.items[i].isInitItem)
                    {
                        //현재 포션의 개수가 0보다 클 때에만 상태를 유지
                        if (portionCnt[0] > 0)
                            portionItems.Add(GameManager.items[i]);
                    }
                    else if (GameManager.items[i].isPurchased)
                    {
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

                for(int i=0;i<GameManager.dropItems.Count;i++)
                {
                    //몬스터 드랍 아이템(혈석)은 상점에서 구매가 불가하지만,
                    //인벤토리에 저장되기 위한 조건으로 isPurchase를 true 상태로 만들어 줘야 함
                    if (GameManager.dropItems[i].isPurchased)
                    {
                        monstorDropItems.Add(GameManager.dropItems[i]);
                    }
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
                        Console.Write($"| {portionCnt[0]}개 보유중");

                    else if (portionItems[i].Name == "대형 HP 포션")
                        Console.Write($"| {portionCnt[1]}개 보유중");

                    else if (portionItems[i].Name == "소형 MP 포션")
                        Console.Write($"| {portionCnt[2]}개 보유중");

                    else if (portionItems[i].Name == "대형 MP 포션")
                        Console.Write($"| {portionCnt[3]}개 보유중");

                    Console.ResetColor();
                    Console.WriteLine();
                }

                Console.WriteLine();
                ConsoleUtility.PrintTextHighlights("", "[던전 획득 아이템]");
                for(int i=0;i<monstorDropItems.Count;i++)
                {
                    monstorDropItems[i].PrintItemStatDesc(true, i + 1);

                    Console.ForegroundColor = ConsoleColor.Green;

                    if (monstorDropItems[i].Name == "작은 혈석 조각")
                        Console.Write($"| {dropItemsCnt[0]}개 보유중");

                    else if (monstorDropItems[i].Name == "일반 혈석")
                        Console.Write($"| {dropItemsCnt[1]}개 보유중");

                    else if (monstorDropItems[i].Name == "거대한 혈석")
                        Console.Write($"| {dropItemsCnt[2]}개 보유중");

                    Console.ResetColor();
                    Console.WriteLine();
                }

                Console.WriteLine("\n\n1. 장착 관리\n2. 회복 아이템\n3. 던전 아이템\n0. 나가기\n");

                if (command == (int)SelectInventoryMenu.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("잘못된 입력입니다.");
                    Console.ResetColor();
                    Console.WriteLine();
                }

                command = ConsoleUtility.PromptMenuChoice(0, 3);

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
                    case (int)SelectInventoryMenu.DropItemsMenu:
                        DropItemMenu();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void DropItemMenu()
        {
            while (true)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ 인벤토리 - 던전 아이템 ■");
                Console.WriteLine("혈석을 사용하여 마나를 회복할 수 있습니다.\n");

                ConsoleUtility.PrintTextHighlights("", "[던전 아이템 목록]");
                for (int i = 0; i < monstorDropItems.Count; i++)
                {
                    monstorDropItems[i].PrintItemStatDesc(true, i + 1);

                    Console.ForegroundColor = ConsoleColor.Green;

                    if (monstorDropItems[i].Name == "작은 혈석 조각")
                        Console.Write($"| {dropItemsCnt[0]}개 보유중");

                    else if (monstorDropItems[i].Name == "일반 혈석")
                        Console.Write($"| {dropItemsCnt[1]}개 보유중");

                    else if (monstorDropItems[i].Name == "거대한 혈석")
                        Console.Write($"| {dropItemsCnt[2]}개 보유중");

                    Console.ResetColor();
                    Console.WriteLine();
                }
                
                Console.WriteLine();

                command = ConsoleUtility.PromptMenuChoice(0, portionItems.Count);

                switch (command)
                {
                    case (int)SelectInventoryMenu.PreviousPage:
                        return;
                    case (int)SelectInventoryMenu.WrongCommand:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("잘못된 입력입니다.");
                        Console.ResetColor();
                        Console.WriteLine();
                        Thread.Sleep(500);
                        break;
                    default:
                        useMpPotion(command, ItemType.MONSTER_DROP);
                        break;
                }
            }
        }

        
        private static void PortionMenu()
        {
            while(true)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ 인벤토리 - 회복 아이템 ■");
                Console.WriteLine("포션을 사용하여 체력이나 마나를 회복할 수 있습니다.\n");

                ConsoleUtility.PrintTextHighlights("", "[포션 목록]");
                for (int i = 0; i < portionItems.Count; i++)
                {
                    portionItems[i].PrintItemStatDesc(true, i + 1);

                    Console.ForegroundColor = ConsoleColor.Green;

                    if (portionItems[i].Name == "소형 HP 포션")
                        Console.Write($"| {portionCnt[0]}개 보유중");

                    else if (portionItems[i].Name == "대형 HP 포션")
                        Console.Write($"| {portionCnt[1]}개 보유중");

                    else if (portionItems[i].Name == "소형 MP 포션")
                        Console.Write($"| {portionCnt[2]}개 보유중");

                    else if (portionItems[i].Name == "대형 MP 포션")
                        Console.Write($"| {portionCnt[3]}개 보유중");

                    Console.ResetColor();

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

                command = ConsoleUtility.PromptMenuChoice(0, portionItems.Count);

                switch (command)
                {
                    case (int)SelectInventoryMenu.PreviousPage:
                        return;
                    case (int)SelectInventoryMenu.WrongCommand:
                        break;
                    default:
                        if (portionItems[command - 1].Name == "소형 HP 포션" || portionItems[command - 1].Name == "대형 HP 포션")
                            useHpPotion(command);
                        else
                            useMpPotion(command, ItemType.PORTION);
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
        static void useHpPotion(int command)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("체력 회복이 완료되었습니다. {0} → ", GameManager.player.Hp);

            GameManager.player.Hp += portionItems[command - 1].HP;
            if (GameManager.player.Hp >= 100) GameManager.player.Hp = 100; //최대 체력은 100

            Console.WriteLine($"{GameManager.player.Hp}");
            Console.ResetColor();

            Thread.Sleep(500);

            int idx = 0;
            switch(portionItems[command-1].Name)
            {
                case "소형 HP 포션":
                    idx = 0;
                    break;
                case "대형 HP 포션":
                    idx = 1;
                    break;
                case "소형 MP 포션":
                    idx = 2;
                    break;
                default:
                    break;
            }

            portionCnt[idx]--;

            if (portionCnt[idx] <= 0)
            {
                portionCnt[idx] = 0;
                portionItems[command - 1].TogglePurchaseStatus();
                portionItems.Remove(portionItems[command - 1]);
            }

        }
        private static void useMpPotion(int command, ItemType itemType)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("마나 회복이 완료되었습니다. {0} → ", GameManager.player.Mp);

            if (itemType == ItemType.MONSTER_DROP)
            {
                int idx = 0;
                GameManager.player.Mp += monstorDropItems[command - 1].MP;

                switch(monstorDropItems[command-1].Name)
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

                dropItemsCnt[idx]--;

                //혈석을 모두 사용하였다면 invenItems 리스트에서 해당 정보를 삭제
                if (dropItemsCnt[idx] <= 0)
                {
                    dropItemsCnt[idx] = 0;
                    monstorDropItems[command - 1].TogglePurchaseStatus();
                    monstorDropItems.Remove(monstorDropItems[command - 1]);
                }
            }
            else if (itemType == ItemType.PORTION)
            {
                int idx = 0;

                GameManager.player.Mp += portionItems[command - 1].MP;

                switch (portionItems[command - 1].Name)
                {
                    case "소형 MP 포션":
                        idx = 2;
                        break;
                    case "대형 MP 포션":
                        idx = 3;
                        break;
                    default: break;
                }

                portionCnt[idx]--;

                if (portionCnt[idx] <= 0)
                {
                    portionCnt[idx] = 0;
                    portionItems[command - 1].TogglePurchaseStatus();
                    portionItems.Remove(portionItems[command - 1]);
                }
            }

            if (GameManager.player.Mp >= 50) GameManager.player.Mp = 50; //최대 마나는 50

            Console.WriteLine($"{GameManager.player.Mp}");
            Console.ResetColor();

            Thread.Sleep(500);

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
            DropItemsMenu,
        }
    }
}
