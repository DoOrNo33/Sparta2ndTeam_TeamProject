using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sparta2ndTeam_TeamProject.GameFramework;

namespace Sparta2ndTeam_TeamProject.Scenes
{
    internal class EndScene
    {
        public void EndGame()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ 전투결과 ■\n");
            ConsoleUtility.PrintTextHighlights("", "Victory\n");
            Console.Write("Happy Ending");
            Thread.Sleep(1000);
            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            Console.Write("?");
            Thread.Sleep(2000);

            Console.Clear();
            ConsoleUtility.PrintTextBlood("", "■ 전투결과 ■\n");
            ConsoleUtility.PrintTextBlood("", "Victory\n");
            ConsoleUtility.PrintTextBlood("", "Happy Ending...?\n");
            Thread.Sleep(3000);

            Epilogue();
        }

        public void Epilogue()
        {
            Console.Clear();

            Console.Write("탑의");
            Thread.Sleep(100);
            Console.Write(" 지배자를");
            Thread.Sleep(100);
            Console.Write(" 처치하자");
            Thread.Sleep(100);
            Console.Write(" 탑은");
            Thread.Sleep(100);
            Console.WriteLine(" 사라졌고");
            Console.WriteLine();


            Thread.Sleep(2000);

            Console.Clear();
            ConsoleUtility.PrintTextBlood("", "탑의 지배자를 처치하자 탑은 사라졌고\n");
            Console.Write("당신은");
            Thread.Sleep(100);
            Console.Write(" 탑의");
            Thread.Sleep(100);
            Console.Write(" 입구가");
            Thread.Sleep(100);
            Console.Write(" 있던");
            Thread.Sleep(100);
            Console.Write(" 장소로");
            Thread.Sleep(100);
            Console.Write(" 이동");
            Thread.Sleep(100);
            Console.WriteLine(" 되었습니다.");
            Console.WriteLine();


            Thread.Sleep(2000);

            Console.Clear();
            ConsoleUtility.PrintTextBlood("", "탑의 지배자를 처치하자 탑은 사라졌고\n");
            ConsoleUtility.PrintTextBlood("", "당신은 탑의 입구가 있던 장소로 이동 되었습니다.\n");
            Console.Write("비록");
            Thread.Sleep(100);
            Console.Write(" 탑의");
            Thread.Sleep(100);
            Console.Write(" 숨겨진");
            Thread.Sleep(100);
            Console.Write(" 비밀을");
            Thread.Sleep(100);
            Console.Write(" 알아내진");
            Thread.Sleep(100);
            Console.WriteLine(" 못했지만");
            Console.WriteLine();


            Thread.Sleep(2000);

            Console.Clear();
            ConsoleUtility.PrintTextBlood("", "탑의 지배자를 처치하자 탑은 사라졌고\n");
            ConsoleUtility.PrintTextBlood("", "당신은 탑의 입구가 있던 장소로 이동 되었습니다.\n");
            ConsoleUtility.PrintTextBlood("", "비록 탑의 숨겨진 비밀을 알아내진 못했지만\n");
            Console.Write("영웅으로");
            Thread.Sleep(100);
            Console.Write(" 칭송받으리라");
            Thread.Sleep(100);
            Console.Write(" 기대하며");
            Thread.Sleep(100);
            Console.Write(" 당신은");
            Thread.Sleep(100);
            Console.Write(" 길드로");
            Thread.Sleep(100);
            Console.WriteLine(" 향합니다.");
            Console.WriteLine();


            Thread.Sleep(2000);

            Console.Clear();
            ConsoleUtility.PrintTextBlood("", "탑의 지배자를 처치하자 탑은 사라졌고\n");
            ConsoleUtility.PrintTextBlood("", "당신은 탑의 입구가 있던 장소로 이동 되었습니다.\n");
            ConsoleUtility.PrintTextBlood("", "비록 탑의 숨겨진 비밀을 알아내진 못했지만\n");
            ConsoleUtility.PrintTextBlood("", "영웅으로 칭송받으리라 기대하며 당신은 길드로 향합니다.\n");
            Console.Write("하지만");
            Thread.Sleep(100);
            Console.Write(" 소식을");
            Thread.Sleep(100);
            Console.Write(" 접한");
            Thread.Sleep(100);
            Console.WriteLine(" 길드에서는");
            Console.WriteLine();


            Thread.Sleep(2000);

            Console.Clear();
            ConsoleUtility.PrintTextBlood("", "탑의 지배자를 처치하자 탑은 사라졌고\n");
            ConsoleUtility.PrintTextBlood("", "당신은 탑의 입구가 있던 장소로 이동 되었습니다.\n");
            ConsoleUtility.PrintTextBlood("", "비록 탑의 숨겨진 비밀을 알아내진 못했지만\n");
            ConsoleUtility.PrintTextBlood("", "영웅으로 칭송받으리라 기대하며 당신은 길드로 향합니다.\n");
            ConsoleUtility.PrintTextBlood("", "하지만 소식을 접한 길드에서는\n");
            Console.Write("오히려");
            Thread.Sleep(100);
            Console.Write(" 당신을");
            Thread.Sleep(100);
            Console.WriteLine(" 공격하였고,");
            Console.WriteLine();


            Thread.Sleep(2000);

            Console.Clear();
            ConsoleUtility.PrintTextBlood("", "탑의 지배자를 처치하자 탑은 사라졌고\n");
            ConsoleUtility.PrintTextBlood("", "당신은 탑의 입구가 있던 장소로 이동 되었습니다.\n");
            ConsoleUtility.PrintTextBlood("", "비록 탑의 숨겨진 비밀을 알아내진 못했지만\n");
            ConsoleUtility.PrintTextBlood("", "영웅으로 칭송받으리라 기대하며 당신은 길드로 향합니다.\n");
            ConsoleUtility.PrintTextBlood("", "하지만 소식을 접한 길드에서는\n");
            ConsoleUtility.PrintTextBlood("", "오히려 당신을 공격하였고,\n");
            Console.Write("당신은");
            Thread.Sleep(100);
            Console.Write(" 영문도");
            Thread.Sleep(100);
            Console.Write(" 모른 채");
            Thread.Sleep(100);
            Console.Write(" 도망갈");
            Thread.Sleep(100);
            Console.Write(" 수 밖에");
            Thread.Sleep(100);
            Console.WriteLine(" 없었습니다.");
            Console.WriteLine();


            Thread.Sleep(2000);

            Console.Clear();
            ConsoleUtility.PrintTextBlood("", "탑의 지배자를 처치하자 탑은 사라졌고\n");
            ConsoleUtility.PrintTextBlood("", "당신은 탑의 입구가 있던 장소로 이동 되었습니다.\n");
            ConsoleUtility.PrintTextBlood("", "비록 탑의 숨겨진 비밀을 알아내진 못했지만\n");
            ConsoleUtility.PrintTextBlood("", "영웅으로 칭송받으리라 기대하며 당신은 길드로 향합니다.\n");
            ConsoleUtility.PrintTextBlood("", "하지만 소식을 접한 길드에서는\n");
            ConsoleUtility.PrintTextBlood("", "오히려 당신을 공격하였고,\n");
            ConsoleUtility.PrintTextBlood("", "당신은 영문도 모른 채 도망갈 수 밖에 없었습니다.\n");
            Console.WriteLine("");
            Console.WriteLine("");
            ConsoleUtility.PrintTextBlood("", "<Press Any Key>\n");

            Console.ReadKey();
            Credit();

        }

        public void Credit()
        {
            Console.Clear();
            Console.WriteLine("  _______                     _          _   _      __          __        _      __       __               _ ");
            Console.WriteLine(" |__   __|                   | |        | | ( )     \\ \\        / /       | |    /_ |     /_ |             | |");
            Console.WriteLine("    | | ___  __ _ _ __ ___   | |     ___| |_|/ ___   \\ \\  /\\  / /__  _ __| | __  | |______| | __ _ _ __ __| |");
            Console.WriteLine("    | |/ _ \\/ _` | '_ ` _ \\  | |    / _ \\ __| / __|   \\ \\/  \\/ / _ \\| '__| |/ /  | |______| |/ _` | '__/ _` |");
            Console.WriteLine("    | |  __/ (_| | | | | | | | |___|  __/ |_  \\__ \\    \\  /\\  / (_) | |  |   <   | |      | | (_| | | | (_| |");
            Console.WriteLine("    |_|\\___|\\__,_|_| |_| |_| |______\\___|\\__| |___/     \\/  \\/ \\___/|_|  |_|\\_\\  |_|      |_|\\__,_|_|  \\__,_|");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("                                                       권태하");
            Console.WriteLine("                                                       길선호");
            Console.WriteLine("                                                       김민우");
            Console.WriteLine("                                                       최세은");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("                                             플레이 해주셔서 감사합니다!");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
