
using System.Numerics;

namespace Sparta2ndTeam_TeamProject.Battle
{
    internal class BattleMenu
    {
        private List<Enemy> combatEnemy;
        Random random;

        public BattleMenu()
        {
            combatEnemy = new List<Enemy>();
            combatEnemy.Add(new("토끼", 0, 1, 10, 5));
            random = new Random();
        }

        public void Battle()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ Battle!! ■");

            int enemyCount = random.Next(1, 5);
            
            for (int i = 1; i <= enemyCount; i++)
            {
                // 어떤 적을 등장시킬 지
                int enemyId = random.Next(0, 1);

                for (int j = 0; j <= enemyId; j++)
                {
                    // 적 이름 표시 구현(ConsoleUtility)
                    Console.Write(combatEnemy[j].Lv);
                    Console.Write(combatEnemy[j].Name);
                    Console.Write(combatEnemy[j].Hp);
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
            throw new NotImplementedException();
        }

        private enum BattleAction
        {
            Attack
        }
    }
}