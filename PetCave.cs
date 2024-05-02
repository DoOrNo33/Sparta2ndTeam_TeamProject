using Sparta2ndTeam_TeamProject.Items;

namespace Sparta2ndTeam_TeamProject
{
    internal class PetCave
    {
        static public List<Pet> myPets = new List<Pet>();
        static int command = 0;
        static public void PetCaveMenu()
        {
            while(true)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ 수상한 동굴 ■");
                Console.WriteLine("동굴 속 노점상으로부터 펫을 얻을 수 있습니다.\n");

                ConsoleUtility.PrintTextHighlights("", "[보유 골드]");
                Console.WriteLine($"{GameManager.player.Gold} G\n");

                ConsoleUtility.PrintTextHighlights("", "[펫 목록]");

                for(int i=0;i<GameManager.pets.Count;i++)
                {
                    GameManager.pets[i].PrintItemStatDesc();

                    //펫이 아직 구매되지 않은 상태라면, 
                    if (!GameManager.pets[i].isPurchased)
                    {
                        Console.Write(" | ");
                        Console.WriteLine($"{GameManager.pets[i].Price} G");
                    }
                    else
                    {
                        ConsoleUtility.PrintTextHighlights(" | ", "판매 완료");
                    }
                }

                Console.WriteLine("\n\n1. 펫 구매\n2. 펫 동행\n0. 나가기\n");

                command = ConsoleUtility.PromptMenuChoice(0, 2);
                switch (command)
                {
                    case (int)SelectPetCaveMenu.PreviousPage:
                        return;
                    case (int)SelectPetCaveMenu.PurchaseMenu:
                        PurchaseMenu();
                        break;
                    case (int)SelectPetCaveMenu.EquipMenu:
                        EquipMenu();
                        break;
                    case (int)SelectPetCaveMenu.WrongCommand:
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

        private static void PurchaseMenu()
        {
            CurrentShopState? currentShopState = null;
            while (true)
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("■ 수상한 동굴 - 펫 구매 ■");
                Console.WriteLine("탑을 정복하는 데 도움이 되는 펫을 얻을 수 있습니다.\n");

                ConsoleUtility.PrintTextHighlights("", "[보유 골드]");
                Console.WriteLine($"{GameManager.player.Gold} G\n");

                ConsoleUtility.PrintTextHighlights("", "[펫 목록]");

                for (int i = 0; i < GameManager.pets.Count; i++)
                {
                    GameManager.pets[i].PrintItemStatDesc(true, i + 1);

                    //펫이 아직 구매되지 않은 상태라면, 
                    if (!GameManager.pets[i].isPurchased)
                    {
                        Console.Write(" | ");
                        Console.WriteLine($"{GameManager.pets[i].Price} G");
                    }
                    else
                    {
                        ConsoleUtility.PrintTextHighlights(" | ", "판매 완료");
                    }
                }

                Console.WriteLine();

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
                            Console.WriteLine("!! 이미 구매한 펫입니다. !!");
                            Console.ResetColor();
                            break;
                        default:
                            break;
                    }
                }
                currentShopState = null;

                Console.WriteLine("\n\n\n0. 나가기\n\n");
                command = ConsoleUtility.PromptMenuChoice(0, GameManager.pets.Count);

                if (command == (int)SelectPetCaveMenu.PreviousPage) return;
                else if(command == (int)SelectPetCaveMenu.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("잘못된 입력입니다.");
                    Console.ResetColor();
                    Console.WriteLine();
                    Thread.Sleep(500);
                }
                else
                {
                    //선택한 펫이 구매된 상태가 아니면서, 
                    if (!GameManager.pets[command-1].isPurchased)
                    {
                        //현재 소지 금액이 펫의 판매 금액보다 많다면, 
                        if (GameManager.player.Gold >= GameManager.pets[command-1].Price)
                        {
                            GameManager.pets[command - 1].TogglePurchaseStatus();

                            GameManager.player.Gold -= GameManager.pets[command - 1].Price;

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

                if (currentShopState != null)
                {
                    switch (currentShopState)
                    {
                        case CurrentShopState.Success:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("!! 구매를 완료했습니다. !!");
                            Console.ResetColor();
                            Thread.Sleep(500);
                            break;
                        case CurrentShopState.InsufficientGold:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("!! Gold가 부족합니다 !!");
                            Console.ResetColor();
                            Thread.Sleep(500);
                            break;
                        case CurrentShopState.SoldOut:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("!! 이미 구매한 펫입니다. !!");
                            Console.ResetColor();
                            Thread.Sleep(500);
                            break;
                        default:
                            break;
                    }
                }
                currentShopState = null;
            }
        }

        private static void EquipMenu()
        {
            while (true)
            {
                 myPets = new List<Pet>();

                for(int i=0;i<GameManager.pets.Count;i++)
                {
                    if (GameManager.pets[i].isPurchased)
                    {
                        myPets.Add(GameManager.pets[i]);
                    }
                }

                Console.Clear();

                ConsoleUtility.ShowTitle("■ 수상한 동굴 - 펫 동행 ■");
                Console.WriteLine("보유 중인 펫과 동행할 수 있습니다.\n");

                ConsoleUtility.PrintTextHighlights("", "[펫 목록]");
                for (int i = 0; i < myPets.Count; i++)
                {

                    myPets[i].PrintItemStatDesc(true, i + 1);
                    Console.WriteLine();
                }

                Console.WriteLine("\n\n\n0. 나가기\n");
                command = ConsoleUtility.PromptMenuChoice(0, myPets.Count);
                switch (command)
                {
                    case (int)SelectPetCaveMenu.WrongCommand:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("잘못된 입력입니다.");
                        Console.ResetColor();
                        Console.WriteLine();
                        Thread.Sleep(500);
                        break;
                    case (int)SelectPetCaveMenu.PreviousPage:
                        return;
                    default:
                        setEquipPets(command);
                        break;
                }
            }
        }

        private static void setEquipPets(int command)
        {
            myPets[command - 1].ToggleEquipStatus();

            for (int i=0;i< myPets.Count;i++)
            {
                //현재 선택한 펫을 제외하고 이미 장착중인 펫이 있다면, 
                if (i != (command - 1) && myPets[i].isEquipped)
                {
                    //해당 펫의 장착 상태를 해제 
                    myPets[i].ToggleEquipStatus();
                }
            }
        }

        private enum SelectPetCaveMenu
        {
            WrongCommand = -1,
            PreviousPage,
            PurchaseMenu,
            EquipMenu,
        }
        private enum CurrentShopState
        {
            Success,
            InsufficientGold,
            SoldOut,
        }
    }
}