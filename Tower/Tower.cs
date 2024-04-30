using Sparta2ndTeam_TeamProject.Battle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject.Tower
{
    public class Tower
    {
        BattleMenu battleMenu = new();

        public int TowerLv = 1;
        public int CombatCount = 0;
        private int command;

        public void EnterTower()
        {
            CombatCount = 0;
            battleMenu.Battle();
        }

        public void ClimbCheck(int count = 0)
        {
            CombatCount += count;

            if (CombatCount == 3)   // 3회 전투 했는지 체크
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ Battle!! ■\n");
                Console.WriteLine("3회 연승 시 {0}층으로 진행!", TowerLv + 1);
                Console.WriteLine("(현재 연승 횟수 : {0}", CombatCount);
                Console.WriteLine("\n축하합니다! {0}층으로 진행 할 수 있습니다.", TowerLv + 1);
                Console.WriteLine("마을로 복귀합니다.");
                Console.Write(">>");

                ClimbTower();

                ConsoleUtility.PromptMenuChoice(0, 0);

                GameManager.Instance.MainMenu();
            }
            else
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ Battle!! ■\n");
                Console.WriteLine("3회 연승 시 {0}층으로 진행!", TowerLv + 1);
                Console.WriteLine("(현재 연승 횟수 : {0}", CombatCount);

                Console.WriteLine("\n1. 전투 속행\n2. 아이템 사용\n\n0.후퇴");
                Console.Write(">>");

                if (command == (int)ClimbSelect.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("잘못된 입력입니다.");
                    Console.ResetColor();
                    Console.WriteLine();
                }

                command = ConsoleUtility.PromptMenuChoice(0, 2);

                switch ((ClimbSelect)command)
                {
                    case ClimbSelect.Retreat:
                        GameManager.Instance.MainMenu();
                        break;

                    case ClimbSelect.AdvanceCombat:
                        battleMenu.Battle();
                        break;

                    case ClimbSelect.UseItem:
                        break;

                    case ClimbSelect.WrongCommand:
                        ClimbCheck(0);
                        break;
                }
            }


            battleMenu.Battle();
        }

        public void ClimbTower()
        {
            TowerLv++;
        }

        private enum ClimbSelect
        {
            Retreat,
            AdvanceCombat,
            UseItem,
            WrongCommand = -1
        }
    }
}
