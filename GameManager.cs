using System.Numerics;

namespace Sparta2ndTeam_TeamProject
{
    internal class GameManager
    {
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

        private void InitializeGame()
        {
            // 스타트 하면서 생성할 클래스 모음 - 아이템, 캐릭터
        }
        public void GameStart()
        {
            Console.Clear();
            // 스타트 화면
            ConsoleUtility.PrintGameHeader();
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

            // 2. 선택한 결과를 검증함
            Enum choice = (SelectMainMenu)ConsoleUtility.PromptMenuChoice(1, 3);

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
            }
            MainMenu();
        }

        private enum SelectMainMenu
        {
            StatusMenu = 1,
            InventoryMenu,
            StoreMenu
        }
    }
}