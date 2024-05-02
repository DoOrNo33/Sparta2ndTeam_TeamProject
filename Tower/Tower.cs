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

            if (GameManager.quests[0].isComplete == false && GameManager.quests[0].isAccept == true)
                GameManager.quests[0].isComplete = true;

            battleMenu.Battle();
        }

        public void ClimbCheck(int count = 0)
        {
            CombatCount += count;

            if (CombatCount == 3)   // 3회 전투 했는지 체크
            {
                //이번 배틀이 종료되면 사용 가능한 회복 수를 충전 
                Inventory.LimitRecover_HP = Inventory.MAXIMUM;
                Inventory.LimitRecover_MP = Inventory.MAXIMUM;

                if (TowerLv < 8)
                {
                    Console.Clear();
                    ConsoleUtility.ShowTitle("■ 전투결과 ■\n");
                    Console.WriteLine("3회 연승 시 {0}층으로 진행!", TowerLv + 1);
                    Console.WriteLine("(현재 연승 횟수 : {0})", CombatCount);
                    Console.WriteLine("\n축하합니다! {0}층으로 진행 할 수 있습니다.", TowerLv + 1);
                    Console.WriteLine("마을로 복귀합니다.");
                    ConsoleUtility.PromptReturn();
                    GameManager.Instance.MainMenu();
                }
                else if (TowerLv == 8)
                {
                    Console.Clear();
                    ConsoleUtility.ShowTitle("■ 전투결과 ■\n");
                    Console.WriteLine("3회 연승 시 타워 정복?!");
                    Console.WriteLine("(현재 연승 횟수 : {0})", CombatCount);
                    Console.WriteLine("\n아무일도 일어나지 않았습니다.", TowerLv + 1);
                    Thread.Sleep(1000);
                    Console.Write("\n.");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    Console.WriteLine(".");
                    Thread.Sleep(1000);
                    Console.WriteLine("\n{0} 은(는) 갑자기 어디론가 소환되었습니다.", GameManager.player.Name);
                    ConsoleUtility.PromptReturn();
                    battleMenu.Battle(true);
                }
            }
            else
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ 전투결과 ■\n");
                if (TowerLv < 8)
                {
                    Console.WriteLine("3회 연승 시 {0}층으로 진행!", TowerLv + 1);
                }
                else if (TowerLv == 8)
                {
                    Console.WriteLine("3회 연승 시 타워 정복?!");
                }
                Console.WriteLine("(현재 연승 횟수 : {0})", CombatCount);

                Console.WriteLine("\n1. 전투 속행\n2. 인벤토리\n\n0.후퇴\n");

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

                    case ClimbSelect.Inventory:
                        Inventory.InventoryMenu(true);
                        ClimbCheck(0);
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
            GameManager.player.towerLv++;
        }

        private enum ClimbSelect
        {
            Retreat,
            AdvanceCombat,
            Inventory,
            WrongCommand = -1
        }
    }
}
