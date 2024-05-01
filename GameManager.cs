using System.Collections.Generic;
using Sparta2ndTeam_TeamProject.Battle;
using Newtonsoft.Json;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using Sparta2ndTeam_TeamProject.Tower;

namespace Sparta2ndTeam_TeamProject
{
    internal class GameManager
    {
        static public List<Item> items = new List<Item>();
        // ◆ ↓ GameManager 최상단에 이부분도 추가 !!!! ◆
        static public List<Item> dropItems = new List<Item>();

        public static Tower.Tower tower = new();

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
            // 방어구 인덱스 0~3                                       공격 | 방어 | 체력 | 마나 | 판매 가격 | 아이템 타입
            items.Add(new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 5, 0, 0, 1000, ItemType.ARMOR));
            items.Add(new Item("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 9, 0, 0, 200, ItemType.ARMOR));
            items.Add(new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 15, 0, 0, 3500, ItemType.ARMOR));
            items.Add(new Item("저주받은 갑옷", "그 어떠한 공격도 막아낼 수 있으나...", -30, 40, 0, 0, 8000, ItemType.ARMOR));
            // 무기 인덱스 4~7
            items.Add(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 2, 0, 0, 0, 600, ItemType.WEAPON));
            items.Add(new Item("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 5, 0, 0, 0, 1500, ItemType.WEAPON));
            items.Add(new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 0, 0, 0,2000, ItemType.WEAPON));
            items.Add(new Item("타노스의 건틀렛", "큰 힘에는 큰 책임이 따릅니다.", 20, -10, 0, 0, 5000, ItemType.WEAPON));
            // 포션 인덱스 8~9                  공격 | 방어 | 체력 | 마나 | 판매 가격 | 아이템 타입 | 착용 여부 | 판매 여부 | 초기 지급 아이템 여부
            items.Add(new Item("소형 HP 포션", "HP를 30만큼 회복합니다.", 0, 0, 30, 0, 50, ItemType.PORTION, false, false, true));
            items.Add(new Item("대형 HP 포션", "HP를 70만큼 회복합니다.", 0, 0, 70, 0, 100, ItemType.PORTION));
            items.Add(new Item("소형 MP 포션", "MP를 10만큼 회복합니다.", 0, 0, 0, 10, 50, ItemType.PORTION));
            items.Add(new Item("대형 MP 포션", "MP를 30만큼 회복합니다.", 0, 0, 0, 30, 130, ItemType.PORTION));

            // dropItems[0] ~ dropItems[2]
            // dropItems[i].Name으로 드랍 아이템의 이름 접근 가능 
            // ex) Console.WriteLine($"쓰러뜨린 몬스터로부터 {dropItems[0].Name}을(를) 획득했다!");
            dropItems.Add(new Item("작은 혈석 조각", "몬스터에게서 떨어져 나온 의문의 혈석 조각입니다.", 0, 0, 0, 15, 1500, ItemType.MONSTER_DROP));
            dropItems.Add(new Item("일반 혈석", "몬스터에게서 떨어져 나온 의문의 혈석입니다.", 0, 0, 0, 30, 3000, ItemType.MONSTER_DROP, false, true));
            dropItems.Add(new Item("거대한 혈석", "일반 혈석과는 달리 거대한 크기의 자색 혈석입니다.", 0, 0, 0, 50, 5000, ItemType.MONSTER_DROP));

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
                } while (isInt == false);

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
            Console.WriteLine("4. 탑 입장 (현재 진행 : {0}층)", tower.TowerLv);

            // 2. 선택한 결과를 검증함
            Enum choice = (SelectMainMenu)ConsoleUtility.PromptMenuChoice(1, 4);

            // 3. 선택한 결과에 따라 보내줌
            switch (choice)
            {
                case SelectMainMenu.StatusMenu:
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
                case SelectMainMenu.EnterTower:
                    tower.EnterTower();                   
                    break;
            }
            MainMenu();
        }
        
        private enum SelectMainMenu
        {
            StatusMenu = 1,
            InventoryMenu,
            StoreMenu,
            EnterTower
        }
    }
}