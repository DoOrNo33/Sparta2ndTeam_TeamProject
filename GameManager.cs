
using Sparta2ndTeam_TeamProject.Battle;
using Newtonsoft.Json;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Sparta2ndTeam_TeamProject
{
    internal class GameManager
    {
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

                player = new Player(name, job, 1, 10, 5, 100, 1500);
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

            // 2. 선택한 결과를 검증함
            Enum choice = (SelectMainMenu)ConsoleUtility.PromptMenuChoice(1, 4);

            // 3. 선택한 결과에 따라 보내줌
            switch (choice)
            {
                case SelectMainMenu.StatusMenu:
                    // 스태이터스 메뉴 이동
                    break;
                case SelectMainMenu.InventoryMenu:
                    // 인벤토리 메뉴 이동
                    break;
                case SelectMainMenu.StoreMenu:
                    // 스토어 메뉴 이동
                    break;
                case SelectMainMenu.BattleMenu:
                    if (battleMenu == null)
                    {
                        battleMenu = new BattleMenu();
                    }
                    battleMenu.Battle();
                    break;
            }
            MainMenu();
        }

        private enum SelectMainMenu
        {
            StatusMenu = 1,
            InventoryMenu,
            StoreMenu,
            BattleMenu
        }
    }
}