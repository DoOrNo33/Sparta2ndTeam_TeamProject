
using Sparta2ndTeam_TeamProject.Tower;
using System.Numerics;

namespace Sparta2ndTeam_TeamProject.Battle
{
    internal class BattleMenu
    {
        private List<Enemy> currentEnemy;
        Random random = new Random();
        private bool duringBattle = false;
        int defeatCount = 0;        // 적 쓰러뜨림 확인용
        int startHp = 0;
        int choice = 0;
        int towerLv;
        bool finalBattle;

       

        public BattleMenu()
        {
            currentEnemy = new();
        }

        public void Battle(bool finalTrigger = false)
        {
            towerLv = GameManager.tower.TowerLv;

            finalBattle = finalTrigger;         // 최종 전투 트리거

            Console.Clear();
            ConsoleUtility.ShowTitle("■ Battle!! ■\n");

            // 전투 돌입 or 전투 중
            if (!duringBattle)      
            {
                startHp = GameManager.player.Hp;
                currentEnemy.Clear();
                defeatCount = 0;            // 적 쓰러뜨림 초기화
                
                if (!finalBattle)           // 일반 전투
                {
                    int enemyCount = CreateEnemyCount();

                    for (int i = 0; i < enemyCount; i++)
                    {
                        CreateEnemy();        //어떤 적을 등장시킬 지

                        currentEnemy[i].PrintCurrentEnemies();
                    }
                }
                else                           // 최종 전투
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Minotaurs mino = new Minotaurs(towerLv + 1);
                        currentEnemy.Add(mino);
                        currentEnemy[i].PrintCurrentEnemies();
                    }

                    BloodThirster boss = new BloodThirster();
                    currentEnemy.Add(boss);
                    currentEnemy[2].PrintCurrentEnemies();

                    for (int i = 3; i < 5; i++)
                    {
                        Minotaurs mino = new Minotaurs(towerLv + 1);
                        currentEnemy.Add(mino);
                        currentEnemy[i].PrintCurrentEnemies();
                    }

                }
            }
            else
            {
                for (int i = 0; i < currentEnemy.Count; i++)
                {
                    currentEnemy[i].PrintCurrentEnemies();
                }
                
            }
            string[] job = { "전사", "마법사" };
            Console.WriteLine("\n[내 정보]");
            Console.WriteLine("Lv{0} {1} ({2})", GameManager.player.Level, GameManager.player.Name, job[GameManager.player.Job - 1]);
            Console.WriteLine("HP {0}/100", GameManager.player.Hp);

            Console.WriteLine("\n1. 기본 공격\n2. 스킬\n3. 인벤토리"); // 스킬, 소모성 아이템 추가 할 수 있음
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            if (choice == (int)BattleAction.WrongCommand)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("잘못된 입력입니다.");
                Console.ResetColor();
                Console.WriteLine();
            }

            choice = ConsoleUtility.PromptMenuChoice(1, 3);

            switch ((BattleAction)choice)
            {
                case BattleAction.BasicAttack:
                    AttackAction();
                    break;
                case BattleAction.SkillAttack:
                    AttackAction();
                    break;
                case BattleAction.Inventory:
                    duringBattle = true;
                    Inventory.InventoryMenu(true);
                    Battle();
                    break;
                case BattleAction.WrongCommand:
                    duringBattle = true;
                    Battle();
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

            string[] job = { "전사", "마법사" };

            Console.WriteLine("\n[내 정보]");
            Console.WriteLine("Lv{0} {1} ({2})", GameManager.player.Level, GameManager.player.Name, job[GameManager.player.Job-1]);
            Console.WriteLine("HP {0}/100", GameManager.player.Hp);

            Console.WriteLine("\n0. 취소"); 
            Console.WriteLine("\n대상을 선택해주세요.");
            Console.Write(">>");

            int keyInput = 0;

            while (true) // 대상이 죽었는지 체크
            {
                keyInput = ConsoleUtility.PromptMenuChoice(0, currentEnemy.Count);
                
                if (keyInput == (int)BattleAction.WrongCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("잘못된 입력입니다.");
                    Console.ResetColor();
                    Console.WriteLine();
                }
                else if (keyInput == 0)
                {
                    break;
                }
                else if (currentEnemy[keyInput - 1].IsDead)
                {
                    Console.WriteLine("이미 죽은 대상입니다.");
                    Console.Write(">>");
                }
                else
                {
                    break;
                }
            }
            
            switch (keyInput)
            {
                case 0:
                    duringBattle = true;
                    Battle();
                    break;
                default:


                    defeatCount += currentEnemy[keyInput - 1].PlayerAttack();     // 쓰러뜨렸을때 반환값 1, 아니라면 0을 쓰러뜨린 적 카운트에 넣어줌

                    foreach (Enemy enem in currentEnemy)
                    {
                        if (enem.Hp <= 0)               // 적 체력 0이라면 건너뜀
                        {
                            continue;
                        }

                        EnemyPhase(enem);

                        if (GameManager.player.Hp <= 0)     // 플레이어 체력 0이라면 적 페이즈 멈춤
                        {
                            break;
                        }

                    }
                    
                    if (GameManager.player.Hp <= 0)
                    {
                        BattleResult(BattleStatus.Defeat);
                    }
                    else if ((defeatCount == currentEnemy.Count) && (GameManager.player.Hp > 0))
                    {
                        BattleResult(BattleStatus.Win);
                    }
                    else
                    {
                        AttackAction();
                    }

                    break;
            }
        }

        private void BattleResult(BattleStatus result)
        {
            duringBattle = false; // 전투 끝날때 초기화하는데로 옮겨주자
            if (result == BattleStatus.Defeat)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ Battle!! ■ - Result\n");
                Console.WriteLine("You Lose\n");
                Console.WriteLine("Lv.{0}, {1}", GameManager.player.Level, GameManager.player.Name);
                Console.WriteLine("HP {0} -> 0\n", startHp);
                Console.WriteLine("0. 다음");
                Console.Write("\n>>");

                ConsoleUtility.PromptMenuChoice(0, 0);   
                
                Environment.Exit(0);                                    // 패배 시 게임 종료
            }
            else
            {
                if (!finalBattle)                               // 일반 전투
                {
                    Console.Clear();
                    ConsoleUtility.ShowTitle("■ Battle!! ■ - Result\n");
                    Console.WriteLine("Victory\n");
                    Console.WriteLine("던전에서 몬스터 {0}마리를 잡았습니다.\n", defeatCount);
                    Console.WriteLine("Lv.{0}, {1}", GameManager.player.Level, GameManager.player.Name);
                    Console.WriteLine("HP {0} -> {1}\n", startHp, GameManager.player.Hp);

                    Console.WriteLine("\n[전리품]");                           // 전리품 설정
                    DropItems();


                    Console.WriteLine("\n<Press Any Key>");
                    Console.Write("\n>>");

                    ConsoleUtility.PromptMenuChoice(0, 0);

                    GameManager.tower.ClimbCheck(1);
                }
                else                                          // 최종 전투 승리 시 게임 종료
                {
                    EndGame();
                }

            }
        }

        private void DropItems()
        {
            for (int i = 0; i < defeatCount; i++)
            {
                int DropCheckPoint = random.Next(0, 10);
                if (DropCheckPoint < (currentEnemy[i].Lv * 2))             // 레벨 2배수로 확률 증가
                {
                    int drop = currentEnemy[i].Drops[random.Next(0, 2)];
                    Console.WriteLine("{0}", GameManager.dropItems[drop].Name);  // 드랍 아이템 중 무작위 결정
                    GameManager.dropItems[drop].DropItemActive();
                    Inventory.dropItemsCnt[drop]++;                         // 드랍 아이템 인벤토리 보유량 증가
                }
            } 
        }

        private void EnemyPhase(Enemy enem)
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ Battle!! ■\n");
            enem.EnemyAttack();  // 플레이어 체력, 플레이어 이름
        }

        private int CreateEnemyCount()
        {
            Enum count = (combatCount)GameManager.tower.CombatCount;

            switch (count)
            {
                case combatCount.first:
                    return random.Next(1, 3);  // 첫 전투는 1~2 적, 2 : 2~3, 3 : 3~4

                case combatCount.second:
                    return random.Next(2, 4);

                case combatCount.third:
                    return random.Next(3, 5);

                default:
                    return 0;               // 예외 : 생성 안됨
            }
        }


        private void CreateEnemy()
        {
            // 어떤 적을 등장시킬 지
            int lowType = Math.Max(towerLv - 4, 0);
            int highType = Math.Min(towerLv + 1, 5);

            int type = (random.Next(lowType, highType)); // 던전 레벨에 맞는 적 생성

            switch ((EnemyType)type)
            {
                case EnemyType.BigRat:              // 큰 쥐 1~4층
                    BigRat bigRat = new BigRat(towerLv);
                    currentEnemy.Add(bigRat);
                    break;

                case EnemyType.Goblin:              // 고블린 1~5층
                    Goblin gob = new Goblin(towerLv);
                    currentEnemy.Add(gob);
                    break;

                case EnemyType.Wolf:                // 늑대 2~6층
                    Wolf wolf = new Wolf(towerLv);
                    currentEnemy.Add(wolf);
                    break;

                case EnemyType.Orc:                // 오크 3~7층
                     Orc orc = new Orc(towerLv);
                    currentEnemy.Add(orc);
                    break;

                case EnemyType.Minotarus:                // 미노타우르스 4~8층
                    Minotaurs mino = new Minotaurs(towerLv);
                    currentEnemy.Add(mino);
                    break;
            }
        }

        private void EndGame()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ Battle!! ■ - Result\n");
            Console.WriteLine("Victory\n");
            Console.WriteLine("{0} 은(는) 타워를 정복했습니다.");
            Console.WriteLine("타워는 사라졌고, 당신은 타워 입구가 있던 자리로 이동되었습니다.");
            Console.WriteLine("Happy Ending?");
            Environment.Exit(0);
        }


        private enum BattleAction
        {
            BasicAttack = 1,
            SkillAttack,
            Inventory,
            WrongCommand = -1
        }

        private enum BattleStatus
        {
            Win,
            Defeat               
        }

        private enum NextButton
        {
            Press
        }

        private enum EnemyType
        {
            BigRat,
            Goblin,
            Wolf,
            Orc,
            Minotarus,
            BloodGod
        }

        private enum combatCount
        {
            first,
            second,
            third
        }

        //            if (command == (int) SelectInventoryMenu.WrongCommand)
        //{
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    Console.Write("잘못된 입력입니다.");
        //    Console.ResetColor();
        //    Console.WriteLine();
        //}

    }

}