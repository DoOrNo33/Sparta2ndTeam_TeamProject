
using System.Numerics;

namespace Sparta2ndTeam_TeamProject.Battle
{
    internal class BattleMenu
    {
        //private List<Enemy> enemies;
        private Enemy[] enemies = new Enemy[2];
        private List<Enemy> currentEnemy;
        Random random;
        private bool duringBattle = false;

        public BattleMenu()
        {
            enemies[0] = new("토끼", 0, 1, 10, 5);
            enemies[1] = new("늑대", 1, 1, 20, 10);
            currentEnemy = new();
            random = new Random();
        }

        public void Battle()
        {
            
            Console.Clear();
            ConsoleUtility.ShowTitle("■ Battle!! ■\n");

            // 전투 돌입 or 전투 중
            if (!duringBattle)      
            {
                currentEnemy.Clear();          
                int enemyCount = random.Next(1, 5);

                for (int i = 1; i <= enemyCount; i++)
                {
                    // 어떤 적을 등장시킬 지
                    int id = random.Next(0, 2);
                    enemies[id].PrintCurrentEnemies();
                    currentEnemy.Add(enemies[id]);
                }
            }
            else
            {
                for (int i = 0; i < currentEnemy.Count; i++)
                {
                    currentEnemy[i].PrintCurrentEnemies();
                }
                
            }

            Console.WriteLine("\n\n[내 정보]");
            Console.WriteLine("정보");
            Console.WriteLine("체력");

            Console.WriteLine("\n1. 공격"); // 스킬, 소모성 아이템 추가 할 수 있음
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            switch ((BattleAction)ConsoleUtility.PromptMenuChoice(1, 1))
            {
                case BattleAction.Attack:
                    AttackAction();
                    break;
            }

        }

        private void AttackAction()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ Battle!! ■\n");
            for (int i = 0; i < currentEnemy.Count; i++)
            {
                currentEnemy[i].PrintCurrentEnemies(true, i + 1);
            }

            Console.WriteLine("\n\n[내 정보]");
            Console.WriteLine("정보");
            Console.WriteLine("체력");

            Console.WriteLine("\n0. 취소"); 
            Console.WriteLine("\n대상을 선택해주세요.");
            Console.Write(">>");

            int keyInput = ConsoleUtility.PromptMenuChoice(0, currentEnemy.Count);

            switch (keyInput)
            {
                case 0:
                    duringBattle = true;
                    Battle();
                    break;
                default:
                    //duringBattle = false; // 전투 끝날때 초기화하는데로 옮겨주자
                    currentEnemy[i - 1].PlayerAttack();
                    EnemyPhase();
                    break;
            }
        }

        private void EnemyPhase()
        {
            foreach (Enemy enem in currentEnemy)
            {

            }
        }

        private enum BattleAction
        {
            Attack = 1
        }
    }
}