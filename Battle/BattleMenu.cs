
using Sparta2ndTeam_TeamProject.Items;
using Sparta2ndTeam_TeamProject.Scenes;
using Sparta2ndTeam_TeamProject.Tower;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Sparta2ndTeam_TeamProject.Battle
{
    internal class BattleMenu
    {
        private List<Enemy> currentEnemy;
        private Enemy enemy = new Enemy();
        Random random = new Random();
        private bool duringBattle = false;
        int defeatCount = 0;        // 적 쓰러뜨림 확인용
        int startHp = 0;
        int startMp = 0;
        int startMaxHp = 0;
        int startMaxMp = 0;
        int startGold = 0;
        int choice = 0;
        int towerLv;
        bool finalBattle;
        int SetSkill = 0;
        public BattleMenu()
        {
            currentEnemy = new();
        }

        public void Battle(bool finalTrigger = false)
        {
            towerLv = GameManager.tower.TowerLv;

            finalBattle = finalTrigger;         // 최종 전투 트리거

            Console.Clear();
            ConsoleUtility.ShowTitle("■ 전  투 ■\n");

            // 전투 돌입 or 전투 중
            if (!duringBattle)
            {
                startHp = GameManager.player.Hp;
                startMp = GameManager.player.Mp;
                startMaxHp = GameManager.player.Max_Hp;
                startMaxMp = GameManager.player.Max_Mp;
                startGold = GameManager.player.Gold;
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
            Console.WriteLine("Lv. {0:D2} {1} ({2})", GameManager.player.Level, GameManager.player.Name, job[GameManager.player.Job - 1]);
            Console.WriteLine();
            Console.WriteLine("체  력 : {0}/{1}", GameManager.player.Hp, GameManager.player.Max_Hp);
            Console.WriteLine("마  나 : {0}/{1}", GameManager.player.Mp, GameManager.player.Max_Mp);

            Console.WriteLine("\n1. 기본 공격\n2. 스킬\n"); // 스킬, 소모성 아이템 추가 할 수 있음
            Console.WriteLine("");

            if (choice == (int)BattleAction.WrongCommand)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("잘못된 입력입니다.");
                Console.ResetColor();
                Console.WriteLine();
            }

            choice = ConsoleUtility.PromptMenuChoice(1, 2);
            switch ((BattleAction)choice)
            {
                case BattleAction.BasicAttack:
                    AttackAction();
                    break;
                case BattleAction.SkillAttack:
                    SkillAction();
                    break;
                //case BattleAction.Inventory:
                //    Inventory.InventoryMenu(true);
                //    Battle();
                //    break;
                case BattleAction.WrongCommand:
                    duringBattle = true;
                    Battle();
                    break;
            }
        }


        private void SetMana(int mp)
        {
            GameManager.player.Mp = GameManager.player.Mp - mp;
        }
        private void BattleSet()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ 전  투 ■\n");
            for (int i = 0; i < currentEnemy.Count; i++)
            {
                currentEnemy[i].PrintCurrentEnemies(true, i + 1);
            }

            string[] job = { "전사", "마법사" };

            Console.WriteLine("\n[내 정보]");
            Console.WriteLine("Lv. {0:D2} {1} ({2})", GameManager.player.Level, GameManager.player.Name, job[GameManager.player.Job - 1]);
            Console.WriteLine();
            Console.WriteLine("체  력 : {0}/{1}", GameManager.player.Hp, GameManager.player.Max_Hp);
            Console.WriteLine("마  나 : {0}/{1}", GameManager.player.Mp, GameManager.player.Max_Mp);

        }

        private void SkillAction()
        {
            int sMp = 0;
            bool Mp = true;
            bool Range = false;
            BattleSet();
            Console.WriteLine("\n[내 스킬]");
            for (int i = 0; i < GameManager.skill.Count; i++)
            {
                if (GameManager.skill[i].SkillRange)
                {
                    Console.WriteLine("\n{0}. {1} - MP {2} \n{3}의 데미지로 모든 적을 공격합니다", i + 1, GameManager.skill[i].SkillName, GameManager.skill[i].SkillMana, GameManager.skill[i].SkillDamage, GameManager.skill[i].SkillRange);
                }
                else
                {
                    Console.WriteLine("\n{0}. {1} - MP {2} \n{3}의 데미지로 적 1명을 공격합니다", i + 1, GameManager.skill[i].SkillName, GameManager.skill[i].SkillMana, GameManager.skill[i].SkillDamage, GameManager.skill[i].SkillRange);
                }

            }
            Console.WriteLine("\n0. 취소");
            Console.WriteLine("");

            choice = ConsoleUtility.PromptMenuChoice(0, GameManager.skill.Count);

            if (choice == (int)SkillCount.WrongCommand)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("잘못된 입력입니다.");
                Console.ResetColor();
                Console.WriteLine();
            }
            switch ((SkillCount)choice)
            {
                case 0:
                    duringBattle = true;
                    Battle();
                    break;
                case SkillCount.FristSkill:
                    SetSkill = choice-1;
                    sMp = GameManager.skill[SetSkill].SkillMana;
                    if (GameManager.player.Mp < GameManager.skill[SetSkill].SkillMana)
                    {
                        Mp = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("마나가 부족합니다.");
                        Console.ResetColor();
                        Console.WriteLine();
                        Thread.Sleep(500);
                        SkillAction();
                        break;
                    }
                    else
                    {
                        Range = GameManager.skill[SetSkill].SkillRange;
                    }
                    break;
                case SkillCount.SecondSkill:
                    SetSkill = 1;
                    sMp = GameManager.skill[SetSkill].SkillMana;
                    if (GameManager.player.Mp < GameManager.skill[SetSkill].SkillMana)
                    {
                        Mp = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("마나가 부족합니다.");
                        Console.ResetColor();
                        Console.WriteLine();
                        Thread.Sleep(500);
                        SkillAction();
                        break;
                    }
                    else
                    {
                        Range = GameManager.skill[SetSkill].SkillRange;
                    }
                    break;
                //case SkillCount.ThirdSkill:
                //    SetSkill = 2;
                //    break;
                //case SkillCount.FourthSkill:
                //    SetSkill = 3;
                //    break;
                case SkillCount.WrongCommand:
                    duringBattle = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.ResetColor();
                    Console.WriteLine();
                    Thread.Sleep(500);
                    SkillAction();
                    break;
            }
            if (Mp)
            {

                Console.Clear();
                ConsoleUtility.ShowTitle("■ Battle!! ■\n");
                for (int i = 0; i < currentEnemy.Count; i++)
                {
                    currentEnemy[i].PrintCurrentEnemies(true, i + 1);
                }

                if (!Range)
                {
                    //AttackCeak();
                    Console.WriteLine("\n[내 정보]");
                    Console.WriteLine("Lv{0} {1} ({2})", GameManager.player.Level, GameManager.player.Name, GameManager.player.Job);
                    Console.WriteLine("HP {0}/{1}", GameManager.player.Hp, GameManager.player.Max_Hp);
                    Console.WriteLine("MP {0}/{1}", GameManager.player.Mp, GameManager.player.Max_Mp);
                    Console.WriteLine("\n대상을 선택해주세요.");
                    Console.WriteLine("\n0. 취소\n");
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
                            int i = keyInput - 1;
                            defeatCount += currentEnemy[i].PlayerSkillAttack(SetSkill); // 쓰러뜨렸을때 반환값 1, 아니라면 0을 쓰러뜨린 적 카운트에 넣어줌

                            if (defeatCount == 1)
                                CheckQuest(currentEnemy[i]);
                            SetMana(sMp);
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
                                duringBattle = true;
                                Battle();
                            }

                            break;
                    }
                }
                else
                {
                    int i = 0;
                    SetMana(sMp);
                    foreach (Enemy enem in currentEnemy)
                    {
                        if (enem.Hp <= 0)               // 적 체력 0이라면 건너뜀
                        {
                            continue;
                        }
                        else
                        {
                            defeatCount += currentEnemy[i].PlayerSkillAttack(SetSkill); // 쓰러뜨렸을때 반환값 1, 아니라면 0을 쓰러뜨린 적 카운트에 넣어줌
                        }
                        //죽은적이 있다면 
                        i++;
                    }
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
                        duringBattle = true;
                        Battle();
                    }
                }

            }

        }

        //private void AttackCeak()
        //{
        //    Console.WriteLine("\n대상을 선택해주세요.");
        //    Console.WriteLine("0. 취소\n");


        //    int keyInput = 0;
        //    while (true) // 대상이 죽었는지 체크
        //    {

        //        keyInput = ConsoleUtility.PromptMenuChoice(0, currentEnemy.Count);

        //        if (keyInput == (int)BattleAction.WrongCommand)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Red;
        //            Console.Write("잘못된 입력입니다.");
        //            Console.ResetColor();
        //            Console.WriteLine();
        //        }
        //        else if (keyInput == 0)
        //        {
        //            break;
        //        }
        //        else if (currentEnemy[keyInput - 1].IsDead)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Red;
        //            Console.WriteLine("이미 죽은 대상입니다.");
        //            Console.ResetColor();
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //    switch (keyInput)
        //    {
        //        case 0:
        //            duringBattle = true;
        //            Battle();
        //            break;

        //        default:
        //            int ret = currentEnemy[keyInput - 1].PlayerAttack();
        //            defeatCount += ret;     // 쓰러뜨렸을때 반환값 1, 아니라면 0을 쓰러뜨린 적 카운트에 넣어줌

        //            if (ret == 1)
        //                CheckQuest(currentEnemy[keyInput - 1]);


        //            foreach (Pet pet in PetCave.myPets)     // 펫 스킬 들어갈 타이밍
        //            {
        //                if (pet.isEquipped)
        //                {
        //                    if (pet.PetType == Items.PetType.Attack)
        //                    {
        //                        defeatCount += pet.PetAttack(currentEnemy);
        //                    }

        //                    if (pet.PetType == Items.PetType.Heal)
        //                    {
        //                        pet.PetHeal();
        //                    }
        //                }
        //            }

        //            foreach (Enemy enem in currentEnemy)
        //            {
        //                if (enem.Hp <= 0)               // 적 체력 0이라면 건너뜀
        //                {
        //                    continue;
        //                }

        //                EnemyPhase(enem);

        //                if (GameManager.player.Hp <= 0)     // 플레이어 체력 0이라면 적 페이즈 멈춤
        //                {
        //                    break;
        //                }

        //            }

        //            if (GameManager.player.Hp <= 0)
        //            {
        //                BattleResult(BattleStatus.Defeat);
        //            }
        //            else if ((defeatCount == currentEnemy.Count) && (GameManager.player.Hp > 0))
        //            {
        //                BattleResult(BattleStatus.Win);
        //            }
        //            else
        //            {
        //                duringBattle = true;
        //                Battle();
        //            }

        //            break;
        //    }
        //}

        private void AttackAction()
        {
            BattleSet();
            //AttackCeak();
            Console.WriteLine("\n대상을 선택해주세요.");
            Console.WriteLine("0. 취소\n");


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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("이미 죽은 대상입니다.");
                    Console.ResetColor();
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
                    defeatCount += currentEnemy[keyInput - 1].PlayerAttack(); // 쓰러뜨렸을때 반환값 1, 아니라면 0을 쓰러뜨린 적 카운트에 넣어줌
                    int ret = defeatCount;
                    if (ret == 1)
                        CheckQuest(currentEnemy[keyInput - 1]);
                    foreach (Pet pet in PetCave.myPets)     // 펫 스킬 들어갈 타이밍
                    {
                        if (pet.isEquipped)
                        {
                            if (pet.PetType == Items.PetType.Attack)
                            {
                                defeatCount += pet.PetAttack(currentEnemy);
                            }

                            if (pet.PetType == Items.PetType.Heal)
                            {
                                pet.PetHeal();
                            }
                        }
                    }

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
                        duringBattle = true;
                        Battle();
                    }

                    break;
            }



        }

        public static void CheckQuest(Enemy enemy)
        {
            foreach (Quest q in GameManager.quests)
            {
                if (q.type == 1)
                {
                    if (q.id == 10) // 적 종류에 상관없이
                    {
                        if ((q.isComplete == false) && (q.isAccept == true))
                        {
                            if (q.cur < q.cnt)
                                q.cur++;

                            if (q.cur == q.cnt)
                                q.isComplete = true;
                        }
                    }

                    else // 적 종류에 맞게
                    {
                        if ((q.id == enemy.id) && (q.isComplete == false) && (q.isAccept == true))
                        {
                            if (q.cur < q.cnt)
                                q.cur++;

                            if (q.cur == q.cnt)
                                q.isComplete = true;
                        }
                    }

                }
            }
        }
        private void BattleResult(BattleStatus result)
        {
            duringBattle = false; // 전투 초기화
            if (result == BattleStatus.Defeat)
            {
                GameManager.player.Hp = 1;                      // 체력1, 마나1, 골드 절반
                GameManager.player.Mp = 1;
                GameManager.player.Gold = (int)Math.Truncate(GameManager.player.Gold * 0.5);

                Console.Clear();
                ConsoleUtility.ShowTitle("■ 전투결과 ■\n");
                Console.WriteLine("You Lose\n");
                Console.WriteLine("Lv. {0:D2}, {1}", GameManager.player.Level, GameManager.player.Name);
                Console.WriteLine("체  력 : {0} -> 1", startHp);
                Console.WriteLine("마  나 : {0} -> 1", startMp);
                Console.WriteLine("Gold : {0} -> {1}", startGold, GameManager.player.Gold);

                Inventory.LimitRecover_HP = Inventory.MAXIMUM;
                Inventory.LimitRecover_MP = Inventory.MAXIMUM;

                ConsoleUtility.PromptReturn();
                GameManager.Instance.MainMenu();                                   // 패배 시 마을로
            }
            else
            {
                if (!finalBattle)                               // 일반 전투
                {
                    Console.Clear();
                    ConsoleUtility.ShowTitle("■ 전투결과 ■\n");
                    Console.WriteLine("Victory\n");
                    Console.WriteLine("탑에서 몬스터 {0}마리를 잡았습니다.\n", defeatCount);
                    Console.WriteLine("Lv. {0:D2}, {1}", GameManager.player.Level, GameManager.player.Name);
                    Console.WriteLine("체  력 : {0} -> {1}", startHp, GameManager.player.Hp);
                    GameManager.player.Mp += 10;
                    Console.WriteLine("마  나 : {0} -> {1} (마나 10회복)\n", startMp, GameManager.player.Mp);
                    
                    Console.WriteLine("\n[전리품]");                           // 전리품 설정
                    DropItems();
                    LevelCheck();
                    ConsoleUtility.PromptReturn();

                    GameManager.tower.ClimbCheck(1);
                }
                else                                          // 최종 전투 승리 시 게임 종료
                {
                    EndScene endScene = new();
                    endScene.EndGame();
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
                    Console.WriteLine(" - {0}", GameManager.dropItems[drop].Name);  // 드랍 아이템 중 무작위 결정
                    GameManager.dropItems[drop].DropItemActive();
                    Inventory.dropItemsCnt[drop]++;                         // 드랍 아이템 인벤토리 보유량 증가
                }
            }
        }

        private void LevelCheck(int getExp = 0)
        {
            int tempLv = GameManager.player.Level;
            int tempAtk = GameManager.player.Atk;
            int tempDef = GameManager.player.Def;
            int tempMaxHp = GameManager.player.Max_Hp;
            int tempMaxMp = GameManager.player.Max_Mp;

            for (int i = 0; i < currentEnemy.Count; i++)
            {
                getExp += currentEnemy[i].Exp;
            }

            GameManager.player.LevelUp(getExp);

            if (tempLv != GameManager.player.Level)
            {
                Console.Write("\n획득 경험치: ");

                ConsoleUtility.PrintTextHighlights("", getExp.ToString());

                Console.WriteLine("\n[레벨 업!]");
                Console.Write("Lv. {0:D2} -> ", tempLv);
                ConsoleUtility.PrintTextHighlights("", GameManager.player.Level.ToString("D2"));
                Console.Write("공격력 : {0} -> ", tempAtk);
                ConsoleUtility.PrintTextHighlights("", GameManager.player.Atk.ToString());
                Console.Write("방어력 : {0} -> ", tempDef);
                ConsoleUtility.PrintTextHighlights("", GameManager.player.Def.ToString());
                Console.Write("체  력 : {0}/{1} -> {2}/", GameManager.player.Hp, tempMaxHp, GameManager.player.Hp);
                ConsoleUtility.PrintTextHighlights("", GameManager.player.Max_Hp.ToString());
                Console.Write("마  나 : {0}/{1} -> {2}/", GameManager.player.Mp, tempMaxMp, GameManager.player.Mp);
                ConsoleUtility.PrintTextHighlights("", GameManager.player.Max_Mp.ToString());
                Console.WriteLine("경험치 : {0}/{1}", GameManager.player.CurrentExp, GameManager.player.RequiredExp);
                return;
            }
            else
            {
                Console.Write("\n획득 경험치 : ");
                ConsoleUtility.PrintTextHighlights("", getExp.ToString());
                Console.WriteLine("경험치 : {0}/{1}", GameManager.player.CurrentExp, GameManager.player.RequiredExp);
            }
        }


        private void EnemyPhase(Enemy enem)
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ 전  투 ■\n");
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
        private enum SkillCount
        {
            FristSkill = 1,
            SecondSkill,
            ThirdSkill,
            FourthSkill,
            WrongCommand = -1
        }

        public enum PetType
        {
            Attack,
            Defense,
            Heal
        }

    }
}