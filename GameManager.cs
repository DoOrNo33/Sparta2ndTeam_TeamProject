using System.Collections.Generic;
using Sparta2ndTeam_TeamProject.Battle;
using Newtonsoft.Json;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using Sparta2ndTeam_TeamProject.Tower;
using Sparta2ndTeam_TeamProject.Scenes;
using Sparta2ndTeam_TeamProject.Items;

namespace Sparta2ndTeam_TeamProject
{
    internal class GameManager
    {
        static public List<Item> items = new List<Item>();
        static public List<Item> dropItems = new List<Item>();
        static public List<Quest> quests = new List<Quest>();
        static public List<Pet> pets = new List<Pet>();

        public static Tower.Tower tower = new();
        public IntroScene introScene = new();

        public GameManager()
        {
            InitializeGame();
        }

        private static GameManager instance;
        public static Player player;

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


        private void InitializeGame()
        {
            // 스타트 하면서 생성할 클래스 모음 - 아이템, 캐릭터
            // 방어구 인덱스 0~3                                       공격 | 방어 | 체력 | 마나 | 판매 가격 | 아이템 타입
            items.Add(new Item("가죽 갑옷", "초보자가 착용하기에 적절한 갑옷입니다.", 0, 5, 0, 0, 1000, ItemType.ARMOR));
            items.Add(new Item("사슬 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 9, 0, 0, 2000, ItemType.ARMOR));
            items.Add(new Item("강철 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 15, 0, 0, 3500, ItemType.ARMOR));
            items.Add(new Item("저주받은 갑옷", "어떠한 공격도 막아낼 수 있으나 무게가 상당합니다.", -10, 40, 0, 0, 8000, ItemType.ARMOR));
            // 무기 인덱스 4~7
            items.Add(new Item("청동 검", "쉽게 볼 수 있는 청동으로 만든 검입니다.", 5, 0, 0, 0, 600, ItemType.WEAPON));
            items.Add(new Item("강철 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 10, 0, 0, 0, 1500, ItemType.WEAPON));
            items.Add(new Item("용사의 창", "한 때 유명한 용사가 사용했다는 창의 모조품입니다.", 15, 0, 0, 0, 2000, ItemType.WEAPON));
            items.Add(new Item("붉은 검", "붉은 기가 돕니다. 방어를 도외시하고 적을 공격하게 됩니다.", 30, -10, 0, 0, 5000, ItemType.WEAPON));
            // 포션 인덱스 8~9                  공격 | 방어 | 체력 | 마나 | 판매 가격 | 아이템 타입 | 착용 여부 | 판매 여부 | 초기 지급 아이템 여부
            items.Add(new Item("소형 체력 포션", "모험가의 필수품 입니다. ", 0, 0, 30, 0, 50, ItemType.PORTION, false, false, true));
            items.Add(new Item("대형 체력 포션", "모험가의 필수품 입니다. ", 0, 0, 70, 0, 100, ItemType.PORTION));
            items.Add(new Item("소형 마나 포션", "다른 곳에선 본 적 없는 포션입니다. 붉은 기가 돕니다.", 0, 0, 0, 10, 50, ItemType.PORTION));
            items.Add(new Item("대형 마나 포션", "다른 곳에선 본 적 없는 포션입니다. 붉은 기가 돕니다.", 0, 0, 0, 30, 130, ItemType.PORTION));

            // dropItems[0] ~ dropItems[2]
            // dropItems[i].Name으로 드랍 아이템의 이름 접근 가능 
            // ex) Console.WriteLine($"쓰러뜨린 몬스터로부터 {dropItems[0].Name}을(를) 획득했다!");
            dropItems.Add(new Item("작은 혈석 조각", "몬스터에게서 떨어져 나온 의문의 혈석 조각입니다.", 0, 0, 0, 5, 300, ItemType.MONSTER_DROP));
            dropItems.Add(new Item("일반 혈석", "몬스터에게서 떨어져 나온 의문의 혈석입니다.", 0, 0, 0, 10, 700, ItemType.MONSTER_DROP, false, true));
            dropItems.Add(new Item("거대한 혈석", "일반 혈석과는 달리 거대한 크기의 자색 혈석입니다.", 0, 0, 0, 15, 1000, ItemType.MONSTER_DROP));

            //펫 종류
            pets.Add(new RedSlime("붉은 슬라임","가장 취약한 적을 본능적으로 공격합니다.", 0, 0, 0, 0, 4000, ItemType.Pet));
            pets.Add(new GreenSlime("초록 슬라임","적의 공격을 맞아 주곤 합니다.", 0, 0, 0, 0, 3000, ItemType.Pet));
            pets.Add(new BlueSlime("푸른 슬라임","상처 부위에 달라붙곤 합니다.", 0, 0, 0, 0, 5000, ItemType.Pet));


            // 퀘스트 목록
            quests.Add(new Quest("미궁 속으로", "어느 날, 의문의 탑이 생겼다.\n탑의 몬스터들에게선 수상한 혈석이 나온다고 하니 얼른 들어가보자.", 0, 100));
            quests.Add(new Quest("더욱 더 단단해지기", "안 아프게 맞기\n방어구를 착용하여 공격으로부터 몸을 보호해야겠다.", 0, 200));
            quests.Add(new Quest("더욱 더 강해지기", "선수 필승이다.\n무기를 착용하여 적들을 혼내주자.", 0, 200));

            quests.Add(new Quest("쥐 잡이", "탑에서 나오는 큰 쥐들이 마을의 식량 창고를 털고 있다네.\n본보기로 큰 쥐 5 마리를 처치해주게나.", 1, 500, 0, 5));
            quests.Add(new Quest("붉은 달이 뜨기전에", "붉은 달이 뜨면 늑대들이 더 흉포해지네.\n붉은 달이 뜨기까지 얼마 남지 않았으니 늑대 개체 수를 줄여줘!\n3 마리 정도만 처치해주게.", 1, 500, 2, 3));
            quests.Add(new Quest("전설의 모험가", "탑에서 끝도 없이 나오는 몬스터 때문에 항상 마을 사람들이 겁에 떨고 있어.\n종류에 상관 없이 30 마리 정도만 처치해주게.", 1, 1000, 10, 30));
        }



        static public void SaveData()
        {
            Console.Clear();
            Console.WriteLine("=============================================================================");
            Console.WriteLine("                         플레이어 데이터 저장 중...!                         ");
            Console.WriteLine("=============================================================================");
            Thread.Sleep(300);

            player.towerLv = tower.TowerLv;
            string playerDataName = "playerStatData.json";
            // 데이터 경로 저장. (C드라이브, Documents)
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string playerDataPath = Path.Combine(path, playerDataName);

            string playerJson = JsonConvert.SerializeObject(player, Formatting.Indented);

            File.WriteAllText(playerDataPath, playerJson);

            Console.WriteLine("=============================================================================");
            Console.WriteLine("                플레이어 데이터를 성공적으로 저장하였습니다!                 ");
            Console.WriteLine("=============================================================================");
            Thread.Sleep(300);
            Console.WriteLine();
            Console.WriteLine("=============================================================================");
            Console.WriteLine("                          아이템 데이터 저장 중...!                          ");
            Console.WriteLine("=============================================================================");
            Thread.Sleep(300);

            string itemDataName = "itemData.json";
            // 데이터 경로 저장. (C드라이브, Documents)
            string itemDataPath = Path.Combine(path, itemDataName);

            string itemJson = JsonConvert.SerializeObject(items, Formatting.Indented);

            File.WriteAllText(itemDataPath, itemJson);

            Console.WriteLine("=============================================================================");
            Console.WriteLine("                 아이템 데이터를 성공적으로 저장하였습니다!                  ");
            Console.WriteLine("=============================================================================");

            string questDataName = "questData.json";
            string questDataPath = Path.Combine(path, questDataName);
            string questJson = JsonConvert.SerializeObject(quests, Formatting.Indented);
            File.WriteAllText(questDataPath, questJson);

            Thread.Sleep(300);
        }


        public void LoadData()
        {
            Console.Clear();
            string playerDataName = "playerStatData.json";
            string itemDataName = "itemData.json";
            string questDataName = "questData.json";

            // C 드라이브 - MyDocuments 폴더
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string playerDataPath = Path.Combine(path, playerDataName);
            string itemDataPath = Path.Combine(path, itemDataName);
            string questDataPath = Path.Combine(path, questDataName);

            if (File.Exists(playerDataPath)) // 데이터 존재
            {
                ConsoleUtility.PrintGameHeader(); // 스타트 화면 출력
                Console.Clear();
                string playerJson = File.ReadAllText(playerDataPath);
                player = JsonConvert.DeserializeObject<Player>(playerJson);
                Console.ForegroundColor = ConsoleColor.Yellow;
                tower.TowerLv = player.towerLv;

                Console.WriteLine("=============================================================================");
                Console.WriteLine("                 플레이어 데이터를 성공적으로 불러왔습니다!                  ");
                Console.WriteLine("=============================================================================");
                Console.ResetColor();
                Thread.Sleep(300);

                if (File.Exists(itemDataPath))
                {
                    string itemJson = File.ReadAllText(itemDataPath);
                    items = JsonConvert.DeserializeObject<List<Item>>(itemJson);
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.WriteLine("=============================================================================");
                    Console.WriteLine("             플레이어의 아이템 데이터를 성공적으로 불러왔습니다!             ");
                    Console.WriteLine("=============================================================================");
                    Console.ResetColor();
                    Thread.Sleep(300);
                }


                if (File.Exists(questDataPath))
                {
                    string questJson = File.ReadAllText(questDataPath);
                    quests = JsonConvert.DeserializeObject<List<Quest>>(questJson);
                }
            }
            else
            {
                //introScene.PlayIntro();
                Console.Clear();
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

            LoadData(); // 세이브 불러오기

            MainMenu(); // 메인 화면 출력
        }

        public void MainMenu()
        {
            // 본격적인 게임 시작 메뉴
            Console.Clear();

            Console.WriteLine("■ 부러진 화살 마을 ■");
            Console.WriteLine("");

            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 탑 입장 (현재 진행 : {0}층)", tower.TowerLv);
            Console.WriteLine("5. 모험가 길드");
            Console.WriteLine("6. 수상한 동굴\n");

            // 2. 선택한 결과를 검증함
            Enum choice = (SelectMainMenu)ConsoleUtility.PromptMenuChoice(1, 6);

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
                case SelectMainMenu.EnterTower:
                    tower.EnterTower();
                    break;
                case SelectMainMenu.GuildMenu:
                    Guild.GuildMenu();
                    break;
                case SelectMainMenu.PetCave:
                    PetCave.PetCaveMenu();
                    break;
            }
            MainMenu();
        }

        private enum SelectMainMenu
        {
            StatusMenu = 1,
            InventoryMenu,
            StoreMenu,
            EnterTower,
            GuildMenu,
            PetCave,
        }
    }
}