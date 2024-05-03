namespace Sparta2ndTeam_TeamProject
{
    internal class Inventory
    {
        //다양한 혈석의 개수 정보를 저장 
        //인덱스 순서대로 작은 혈석 조각, 일반 혈석, 거대한 혈석
        static public int[] dropItemsCnt = { 0, 0, 0 };

        //인덱스 순서대로 소형 hp 포션, 대형 hp 포션, 소형 mp 포션, 대형 mp 포션
        static public int[] portionCnt = { 3, 0, 0, 0 };
        static public string[] portionName = { "소형 체력 포션", "대형 체력 포션", "소형 마나 포션", "대형 마나 포션" };

        static int command;

        //아이템을 저장하기 위한 리스트 
        static List<Item> portionItems = new List<Item>(); //포션
        static List<Item> equipmentItems = new List<Item>(); //장비
        static public List<Item> monstorDropItems = new List<Item>(); //몬스터 드랍 아이템 

        public const int MAXIMUM = 3;
        static public int LimitRecover_HP = MAXIMUM;
        static public int LimitRecover_MP = MAXIMUM;

        static bool isFromBattle = false;
        internal static void InventoryMenu(bool callFromBattle = false)
        {
            isFromBattle = false;
            isFromBattle = callFromBattle;
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
                    //현재 아이템이 포션이면서, 
                    if (GameManager.items[i]._type == ItemType.PORTION)
                    {
                        int idx = -1;

                        //어떤 포션인지 파악하기 위한 반복문(소형 체력, 소형 마나 등등...)
                        for (int j = 0; j < portionName.Length; j++)
                        {
                            if (GameManager.items[i].Name == portionName[j])
                            {
                                idx = j;
                            }
                        }

                        if (portionItems.Count != 0)
                        {
                            bool checkExist = false;
                            for (int j = 0; j < portionItems.Count; j++)
                            {
                                if (portionItems[j].Name == portionName[idx])
                                {
                                    checkExist = true;
                                }

                            }
                            if (!checkExist && portionCnt[idx] >= 1)
                            {
                                portionItems.Add(GameManager.items[i]);
                            }

                        }
                        else
                        {
                            if (portionCnt[idx] >= 1)
                                portionItems.Add(GameManager.items[i]);
                        }

                    }
                    //포션이 아닌 경우에는 구매 여부만 따져서 저장 
                    else if (GameManager.items[i]._type != ItemType.PORTION && GameManager.items[i].isPurchased)
                    {
                        equipmentItems.Add(GameManager.items[i]);
                    }
                }

                for (int i = 0; i < GameManager.dropItems.Count; i++)
                {
                    //몬스터 드랍 아이템(혈석)은 상점에서 구매가 불가하지만,
                    //인벤토리에 저장되기 위한 조건으로 isPurchase를 true 상태로 만들어 줘야 함
                    if (GameManager.dropItems[i].isPurchased)
                    {
                        monstorDropItems.Add(GameManager.dropItems[i]);
                    }
                }


                ConsoleUtility.PrintTextHighlights("", "[장비 목록]");
                for (int i = 0; i < equipmentItems.Count; i++)
                {
                    equipmentItems[i].PrintItemStatDesc(true, i + 1);
                    Console.WriteLine();
                }

                Console.WriteLine();
                ConsoleUtility.PrintTextHighlights("", "[포션 목록]");
                for (int i = 0; i < portionItems.Count; i++)
                {
                    portionItems[i].PrintItemStatDesc(true, i + 1);

                    Console.ForegroundColor = ConsoleColor.Green;

                    for (int j = 0; j < portionName.Length; j++)
                    {
                        if (portionItems[i].Name == portionName[j])
                        {
                            Console.Write($"| {portionCnt[j]}개 보유중");
                            break;
                        }

                    }

                    Console.ResetColor();
                    Console.WriteLine();
                }

                Console.WriteLine();
                ConsoleUtility.PrintTextHighlights("", "[던전 획득 아이템]");
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
                    case (int)SelectInventoryMenu.WrongCommand:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("잘못된 입력입니다.");
                        Console.ResetColor();
                        Console.WriteLine();
                        Thread.Sleep(500);
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

                command = ConsoleUtility.PromptMenuChoice(0, monstorDropItems.Count);

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
                        if (isFromBattle)
                        {
                            if (LimitRecover_MP > 0)
                            {
                                useMpPotion(command, ItemType.MONSTER_DROP);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("이번 대전에서는 더 이상 MP를 회복할 수 없습니다!");
                                Console.ResetColor();
                                Console.WriteLine();
                                Thread.Sleep(500);
                            }
                        }
                        else
                        {
                            useMpPotion(command, ItemType.MONSTER_DROP);
                        }
                        break;
                }
            }
        }


        private static void PortionMenu()
        {
            while (true)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ 인벤토리 - 회복 아이템 ■");
                Console.WriteLine("포션을 사용하여 체력이나 마나를 회복할 수 있습니다.\n");

                ConsoleUtility.PrintTextHighlights("", "[포션 목록]");
                for (int i = 0; i < portionItems.Count; i++)
                {
                    portionItems[i].PrintItemStatDesc(true, i + 1);

                    Console.ForegroundColor = ConsoleColor.Green;

                    for (int j = 0; j < portionName.Length; j++)
                    {
                        if (portionItems[i].Name == portionName[j])
                        {
                            Console.Write($"| {portionCnt[j]}개 보유중");
                            break;
                        }
                    }

                    Console.ResetColor();
                    Console.WriteLine();
                }

                Console.WriteLine("\n\n\n0. 나가기\n");


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
                        //배틀 중인 상태에서만 회복에 제한을 둠. 
                        if (isFromBattle)
                        {
                            CheckPossiblePortion(command);
                        }
                        else
                        {
                            if (portionItems[command - 1].Name == "소형 체력 포션" || portionItems[command - 1].Name == "대형 체력 포션")
                                useHpPotion(command);
                            else
                                useMpPotion(command, ItemType.PORTION);
                        }
                        break;
                }
            }
        }
        static void CheckPossiblePortion(int command)
        {
            if (portionItems[command - 1].Name == "소형 체력 포션" || portionItems[command - 1].Name == "대형 체력 포션")
            {
                //회복할 수 있는 기회가 남아있다면 회복 기능을 실행
                if (LimitRecover_HP > 0)
                {
                    useHpPotion(command);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("이번 대전에서는 더 이상 HP를 회복할 수 없습니다!");
                    Console.ResetColor();
                    Console.WriteLine();
                    Thread.Sleep(500);
                }
            }
            else
            {
                if (LimitRecover_MP > 0)
                {
                    useMpPotion(command, ItemType.PORTION);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("이번 대전에서는 더 이상 MP를 회복할 수 없습니다!");
                    Console.ResetColor();
                    Console.WriteLine();
                    Thread.Sleep(500);
                }
            }
        }
        private static void EquipMenu()
        {

            while (true)
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

                Console.WriteLine("\n\n\n0. 나가기\n");

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
            if (GameManager.player.Hp < GameManager.player.Max_Hp)
            {
                if (isFromBattle)
                    LimitRecover_HP--;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("체력 회복이 완료되었습니다. {0} → ", GameManager.player.Hp);
                int prePlayerHP = GameManager.player.Hp;

                GameManager.player.Hp += portionItems[command - 1].HP;
                if (GameManager.player.Hp >= GameManager.player.Max_Hp) GameManager.player.Hp = GameManager.player.Max_Hp;

                // 현재 커서의 위치 확인
                int cursorLeft = Console.CursorLeft;
                int cursorTop = Console.CursorTop;
                ConsoleUtility.Animation(cursorLeft, cursorTop, prePlayerHP, GameManager.player.Hp);

                Console.ResetColor();
                Thread.Sleep(500);

                int idx = 0;

                switch (portionItems[command - 1].Name)
                {
                    case "소형 체력 포션":
                        idx = 0;
                        break;
                    case "대형 체력 포션":
                        idx = 1;
                        break;
                    default:
                        break;
                }

                portionCnt[idx]--;

                if (portionCnt[idx] <= 0)
                {
                    portionCnt[idx] = 0;
                    portionItems.Remove(portionItems[command - 1]);
                }
            }
            else
            {
                ConsoleUtility.PrintTextHighlights("", "!!이미 건강한 상태입니다.\n");
                Thread.Sleep(500);
            }
        }
        private static void useMpPotion(int command, ItemType itemType)
        {
            if (GameManager.player.Mp < GameManager.player.Max_Mp)
            {
                if (isFromBattle)
                    LimitRecover_MP--;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("마나 회복이 완료되었습니다. {0} → ", GameManager.player.Mp);
                int prePlayerMP = GameManager.player.Mp;

                if (itemType == ItemType.MONSTER_DROP)
                {
                    int idx = 0;
                    GameManager.player.Mp += monstorDropItems[command - 1].MP;

                    switch (monstorDropItems[command - 1].Name)
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
                        case "소형 마나 포션":
                            idx = 2;
                            break;
                        case "대형 마나 포션":
                            idx = 3;
                            break;
                        default: break;
                    }

                    portionCnt[idx]--;

                    if (portionCnt[idx] <= 0)
                    {
                        portionCnt[idx] = 0;
                        portionItems.Remove(portionItems[command - 1]);
                    }
                }

                if (GameManager.player.Mp >= GameManager.player.Max_Mp) GameManager.player.Mp = GameManager.player.Max_Mp;

                int cursorLeft = Console.CursorLeft;
                int cursorTop = Console.CursorTop;
                ConsoleUtility.Animation(cursorLeft, cursorTop, prePlayerMP, GameManager.player.Mp);

                Thread.Sleep(500);
                Console.ResetColor();
            }
            else
            {
                ConsoleUtility.PrintTextHighlights("", "!!이미 건강한 상태입니다.\n");
                Thread.Sleep(500);
            }
        }

        static void setEquipItems(int command)
        {
            equipmentItems[command - 1].ToggleEquipStatus();

            if (equipmentItems[command - 1].isEquipped == true)
            {
                if (GameManager.quests[1].isAccept == true && GameManager.quests[1].isComplete == false && equipmentItems[command - 1]._type == ItemType.ARMOR)
                {
                    GameManager.quests[1].isComplete = true;
                }
                if (GameManager.quests[2].isAccept == true && GameManager.quests[2].isComplete == false && equipmentItems[command - 1]._type == ItemType.WEAPON)
                {
                    GameManager.quests[2].isComplete = true;
                }
            }

            if (equipmentItems[command - 1].Atk != 0 || equipmentItems[command - 1].Def != 0 || equipmentItems[command - 1].HP != 0)
            {
                GameManager.player.Atk += equipmentItems[command - 1].Atk;
                GameManager.player.Def += equipmentItems[command - 1].Def;
                GameManager.player.Hp += equipmentItems[command - 1].HP;
            }
            for (int i = 0; i < equipmentItems.Count; i++)
            {
                if (i != (command - 1) && equipmentItems[command - 1]._type == equipmentItems[i]._type)
                {
                    if (equipmentItems[i].isEquipped)
                    {
                        equipmentItems[i].ToggleEquipStatus();
                        if (equipmentItems[command - 1].Atk != 0 || equipmentItems[command - 1].Def != 0 || equipmentItems[command - 1].HP != 0)
                        {
                            GameManager.player.Atk -= equipmentItems[i].Atk;
                            GameManager.player.Def -= equipmentItems[i].Def;
                            GameManager.player.Hp -= equipmentItems[i].HP;
                        }

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
