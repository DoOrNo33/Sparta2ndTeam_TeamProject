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
            //ConsoleUtility.PrintGameHeader();
            MainMenu();
        }

        public void MainMenu()
        {
            // 본격적인 게임 시작 메뉴
            
        }
    }
}