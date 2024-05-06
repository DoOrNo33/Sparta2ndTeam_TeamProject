using Sparta2ndTeam_TeamProject.GuildInfo;
using Sparta2ndTeam_TeamProject.Scenes;

namespace Sparta2ndTeam_TeamProject.GameFramework
{
    internal class ConsoleUtility
    {
        public static void PrintGameHeader()
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("===================================================================================================");
            Console.WriteLine(" /$$$$$$$  /$$                           /$$        /$$$$$$   /$$");
            Console.WriteLine("| $$__  $$| $$                          | $$       /$$__  $$ | $$");
            Console.WriteLine("| $$  \\ $$| $$  /$$$$$$   /$$$$$$   /$$$$$$$      | $$  \\__//$$$$$$    /$$$$$$  /$$$$$$$   /$$$$$$");
            Console.WriteLine("| $$$$$$$ | $$ /$$__  $$ /$$__  $$ /$$__  $$      |  $$$$$$|_  $$_/   /$$__  $$| $$__  $$ /$$__  $$");
            Console.WriteLine("| $$__  $$| $$| $$  \\ $$| $$  \\ $$| $$  | $$       \\____  $$ | $$    | $$  \\ $$| $$  \\ $$| $$$$$$$$");
            Console.WriteLine("| $$  \\ $$| $$| $$  | $$| $$  | $$| $$  | $$       /$$  \\ $$ | $$ /$$| $$  | $$| $$  | $$| $$_____/");
            Console.WriteLine("| $$$$$$$/| $$|  $$$$$$/|  $$$$$$/|  $$$$$$$      |  $$$$$$/ |  $$$$/|  $$$$$$/| $$  | $$|  $$$$$$$");
            Console.WriteLine("|_______/ |__/ \\______/  \\______/  \\_______/       \\______/   \\___/   \\______/ |__/  |__/ \\_______/");
            Console.WriteLine("===================================================================================================");
            Console.WriteLine("                                      PRESS ANYKEY TO START                                        ");
            Console.WriteLine("===================================================================================================");
            Console.ReadKey();
        }

        public static void PromptReturn()
        {
            Console.Write("\n\n<Press Any Key>");
            Console.ReadKey();
        }

        public static int PromptMenuChoice(int min, int max)
        {
            while (true)
            {
                Console.Write("원하시는 번호를 입력해주세요: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                else return -1;
            }
        }

        private static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2;
                }
                else
                {
                    length += 1;
                }
            }
            return length;
        }

        internal static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }

        internal static void PrintTextHighlights(string s1, string s2, string s3 = "")
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }


        internal static void PrintTextBlood(string s1, string s2, string s3 = "")
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }

        internal static void Animation(int left, int top, int start, int target)
        {
            // 시작점 -> 목표치까지 50ms 마다 1 씩 증가
            while (start < target)
            {
                Thread.Sleep(50);
                start++;
                Console.SetCursorPosition(left, top);
                Console.WriteLine($"{start}");
            }
        }

        internal static void Animation2(int left, int top, int start, int target, int seg, int t)
        {
            // 시작점 -> 목표치까지 (t)ms 마다 seg 씩 증가
            while (start < target)
            {
                Thread.Sleep(t);
                start += seg;
                if (start > target)
                    start = target;
                Console.SetCursorPosition(left, top);
                Console.WriteLine($"{start}");
            }
        }

        internal static void ShowTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(title);
            Console.ResetColor();
        }

        internal static void AnimationMinus(int left, int top, int start, int target, int seg = 1, int t = 100)
        {
            // 시작점 -> 목표치까지 (t)ms 마다 seg 씩 증가
            while (start > target)
            {
                Thread.Sleep(t);
                start -= seg;
                if (start < target)
                    start = target;

                string s = new string(' ', Console.CursorLeft - left);
                Console.SetCursorPosition(left, top);
                Console.Write(s);
                Console.SetCursorPosition(left, top);
                Console.Write("{0}", start);

            }
        }

        static public string checkQuestCompleted(List<Quest> q)
        {
            string str = "";

            for (int i = 0; i < q.Count; i++)
            {
                if (q[i].isComplete && !q[i].rewarded)
                    str = " !!";
            }
            return str;
        }
        static void clearCurrentLine()
        {
            string s = "\r";
            s += new string(' ', Console.CursorLeft);
            s += "\r";
            Console.Write(s);
        }
    }
}