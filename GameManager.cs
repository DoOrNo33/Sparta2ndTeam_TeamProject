using System.Collections.Generic;
using Sparta2ndTeam_TeamProject.Battle;
using Newtonsoft.Json;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace Sparta2ndTeam_TeamProject
{
    internal class GameManager
    {
        static public List<Item> items = new List<Item>();
        BattleMenu battleMenu;
        public GameManager()
        {
            InitializeGame();
        }

        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        public static Player player;
        private void InitializeGame()
        {
            // 스타트 하면서 생성할 클래스 모음 - 아이템, 캐릭터

            items.Add(new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 5, 0, 1000, ItemType.ARMOR));
            items.Add(new Item("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 9, 0, 200, ItemType.ARMOR));
            items.Add(new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 15, 0, 3500, ItemType.ARMOR));
            items.Add(new Item("저주받은 갑옷", "그 어떠한 공격도 막아낼 수 있으나...", -30, 40, 0, 8000, ItemType.ARMOR));
            items.Add(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 2, 0, 0, 600, ItemType.WEAPON));
            items.Add(new Item("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 5, 0, 0, 1500, ItemType.WEAPON));
            items.Add(new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 0, 0, 2000, ItemType.WEAPON));
            items.Add(new Item("타노스의 건틀렛", "큰 힘에는 큰 책임이 따릅니다.", 20, -10, 0, 5000, ItemType.WEAPON));

            items.Add(new Item("HP 포션", "HP를 30만큼 회복합니다.", 0, 0, 30, 50, ItemType.PORTION));

        }
        public void SaveData()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=============================================================================");
            Console.WriteLine("                         플레이어 데이터 저장 중...!                         ");
            Console.WriteLine("=============================================================================");
            Console.ResetColor();
            Thread.Sleep(300);

            string playerDataName = "playerStatData.json";
            // 데이터 경로 저장. (C드라이브, Documents)
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string playerDataPath = Path.Combine(path, playerDataName);

            string playerJson = JsonConvert.SerializeObject(player, Formatting.Indented);

            File.WriteAllText(playerDataPath, playerJson);

            Console.WriteLine("=============================================================================");
            Console.WriteLine("                플레이어 데이터를 성공적으로 저장하였습니다!                 ");
            Console.WriteLine("=============================================================================");
            Console.ReadKey();

        }
        public void LoadData()
        {
            Console.Clear();
            string playerDataName = "playerStatData.json";

            // C 드라이브 - MyDocuments 폴더
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string playerDataPath = Path.Combine(path, playerDataName);

            if (File.Exists(playerDataPath)) // 데이터 존재
            {
                string playerJson = File.ReadAllText(playerDataPath);
                player = JsonConvert.DeserializeObject<Player>(playerJson);
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("=============================================================================");
                Console.WriteLine("                 플레이어 데이터를 성공적으로 불러왔습니다!                  ");
                Console.WriteLine("=============================================================================");
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("=============================================================================");
                Console.WriteLine("                     저장된 플레이어 데이터가 없습니다.                      ");
                Console.WriteLine("=============================================================================");
                Console.WriteLine("                     생성할 캐릭터의 이름을 정해주세요.                      ");
                Console.WriteLine("=============================================================================");

                string name = Console.ReadLine();
                int job;
                bool isInt;
                do
                {

                    Console.WriteLine();
                    Console.WriteLine("=============================================================================");
                    Console.WriteLine("          생성할 캐릭터의 직업을 선택해주세요 (전사 : 1 / 마법사 : 2)        ");
                    Console.WriteLine("=============================================================================");

                    isInt = int.TryParse(Console.ReadLine(), out job);
                } while (isInt == false || job > 2 || job < 1);


                if (job == 1)
                    player = new Warrior(name);
                else if (job == 2)
                    player = new Mage(name);
                Console.WriteLine();
                Console.WriteLine("=============================================================================");
                Console.WriteLine("                        캐릭터를 생성하고 있습니다..                         ");
                Console.WriteLine("=============================================================================");
                Thread.Sleep(300);
            }

        }

        public void GameStart()
        {
            Console.Clear();
            // 스타트 화면
            ConsoleUtility.PrintGameHeader();

            // 세이브 불러오기
            LoadData();

            MainMenu();
        }

        public void MainMenu()
        {
            // 본격적인 게임 시작 메뉴
            Console.Clear();

            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("");

            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 전투하기");
            Console.WriteLine("5. 저장하기");

            // 2. 선택한 결과를 검증함
            Enum choice = (SelectMainMenu)ConsoleUtility.PromptMenuChoice(1, 5);

            // 3. 선택한 결과에 따라 보내줌
            switch (choice)
            {
                case SelectMainMenu.StatusMenu:
                    player.StatusMenu();
                    // 스태이터스 메뉴 이동
                    break;
                case SelectMainMenu.InventoryMenu:
                    Inventory.InventoryMenu();
                    // 인벤토리 메뉴 이동
                    break;
                case SelectMainMenu.StoreMenu:
                    Store.StoreMenu();
                    // 스토어 메뉴 이동
                    break;
                case SelectMainMenu.BattleMenu:
                    if (battleMenu == null)
                    {
                        battleMenu = new BattleMenu();
                    }
                    battleMenu.Battle();
                    break;
                case SelectMainMenu.SaveData:
                    SaveData();
                    break;
            }
            MainMenu();
        }

        private enum SelectMainMenu
        {
            StatusMenu = 1,
            InventoryMenu,
            StoreMenu,
            BattleMenu,
            SaveData
        }
    }
}