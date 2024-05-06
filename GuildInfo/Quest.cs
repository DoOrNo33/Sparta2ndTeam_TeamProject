using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sparta2ndTeam_TeamProject.GameFramework;

namespace Sparta2ndTeam_TeamProject.GuildInfo
{
    class Quest
    {
        public string name { get; }
        public string info { get; }
        public bool isAccept { get; set; } = false;
        public bool isComplete { get; set; } = false;
        public int type { get; set; }
        public int gold { get; set; } = 0;

        public bool rewarded { get; set; } = false;
        public int id { get; set; } = -1;
        public int cnt { get; set; } = 0;
        public int cur { get; set; } = 0;

        public Quest(string name, string info, int type, int gold, int id = 0, int cnt = 0, int cur = 0)
        {
            this.name = name;
            this.info = info;
            this.type = type;
            this.gold = gold;
            this.id = id;
            this.cnt = cnt;
            this.cur = cur;
        }

        public void ClearQuest()
        {
            string[] types = { "튜토리얼", "임무" };
            Console.Clear();
            ConsoleUtility.ShowTitle("■ 모험가 길드 ■ - 퀘스트 완료");
            Console.WriteLine("퀘스트를 수락하거나 정산받을 수 있습니다.\n");

            ConsoleUtility.PrintTextHighlights("", $"[{types[type]} 완료]\n");


            if (rewarded == false)
            {
                Console.WriteLine("보상을 지급 받습니다...");

                Console.WriteLine($"\n골 드 {GameManager.player.Gold} -> ");

                ConsoleUtility.Animation2(14, 7, GameManager.player.Gold, GameManager.player.Gold + gold, 11, 50);
                GameManager.player.Gold += gold;
                Thread.Sleep(300);

                Console.WriteLine("정산이 완료되었습니다 !");
                rewarded = true;
            }
            else
            {
                Thread.Sleep(500);
                Console.WriteLine("...\n");

                Thread.Sleep(500);
                Console.WriteLine("...\n");
                Thread.Sleep(500);

                Console.WriteLine("\n이미 정산 처리된 건입니다.\n");
            }

            ConsoleUtility.PromptReturn();
        }
        public void Print()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ 모험가 길드 ■ - 퀘스트 ");
            Console.WriteLine("퀘스트를 수락하거나 정산받을 수 있습니다.\n");

            ConsoleUtility.PrintTextHighlights("", $"[{name}]\n");
            Console.WriteLine($"{info}\n");
            Console.WriteLine($"- 보상 : {gold} Gold\n");

            Console.WriteLine("\n1. 수락");
            Console.WriteLine("2. 거절");
            Console.WriteLine("0. 나가기\n");

            int command = ConsoleUtility.PromptMenuChoice(0, 2);

            if (command == -1)
                Print();

            else if (command == 0)
                return;

            else if (command == 1)
            {
                if (isAccept == true)
                {
                    Console.WriteLine("\n이미 수락한 퀘스트 입니다.");
                    ConsoleUtility.PromptReturn();
                }
                else
                {
                    isAccept = true;
                }

                return;
            }

            else
            {
                if (isAccept == false)
                {
                    Console.WriteLine("\n수락하지 않은 퀘스트 입니다.");
                    ConsoleUtility.PromptReturn();
                }
                else
                {
                    Console.WriteLine("정말로 퀘스트를 포기하시겠습니까? :  0 - 포기하지 않는다, 1 - 포기한다\n");
                    command = ConsoleUtility.PromptMenuChoice(0, 1);

                    if (command == -1)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                    else if (command == 0)
                        return;

                    else
                    {
                        if (type == 1)
                            cur = 0;
                        isAccept = false;
                        return;
                    }
                }
            }
        }
    }
}
