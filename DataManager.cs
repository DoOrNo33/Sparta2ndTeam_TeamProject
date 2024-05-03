using Newtonsoft.Json;
using Sparta2ndTeam_TeamProject.Tower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject
{
    class DataManager
    {
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


        static public void SaveData()
        {
            Console.Clear();
            GameManager.player.towerLv = GameManager.tower.TowerLv;

            Thread.Sleep(300);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=============================================================================");
            Console.WriteLine("                          데이터를 저장 중입니다...                          ");
            Console.WriteLine("=============================================================================");

            Thread.Sleep(500);

            string playerDataName = "playerStatData.json";
            string playerDataPath = Path.Combine(path, playerDataName);
            string playerJson = JsonConvert.SerializeObject(GameManager.player, Formatting.Indented);
            File.WriteAllText(playerDataPath, playerJson);


            string itemDataName = "itemData.json";
            string itemDataPath = Path.Combine(path, itemDataName);
            string itemJson = JsonConvert.SerializeObject(GameManager.items, Formatting.Indented);
            File.WriteAllText(itemDataPath, itemJson); 

            string questDataName = "questData.json";
            string questDataPath = Path.Combine(path, questDataName);
            string questJson = JsonConvert.SerializeObject(GameManager.quests, Formatting.Indented);
            File.WriteAllText(questDataPath, questJson);

            Console.WriteLine("=============================================================================");
            Console.WriteLine("                     데이터를 성공적으로 저장하였습니다!                     ");
            Console.WriteLine("=============================================================================");
            Console.ResetColor();
            Thread.Sleep(500);
        }


        static public void LoadData()
        {
            Console.Clear();

            string playerDataName = "playerStatData.json";
            string itemDataName = "itemData.json";
            string questDataName = "questData.json";

            string playerDataPath = Path.Combine(path, playerDataName);
            string itemDataPath = Path.Combine(path, itemDataName);
            string questDataPath = Path.Combine(path, questDataName);


            if (File.Exists(playerDataPath) && File.Exists(itemDataPath) && File.Exists(questDataPath))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                ConsoleUtility.PrintGameHeader();
                Console.Clear();

                string playerJson = File.ReadAllText(playerDataPath);
                GameManager.player = JsonConvert.DeserializeObject<Player>(playerJson);

                GameManager.tower.TowerLv = GameManager.player.towerLv;

                Console.WriteLine("=============================================================================");
                Console.WriteLine("                 플레이어 데이터를 성공적으로 불러왔습니다!                  ");
                Console.WriteLine("=============================================================================");

                Thread.Sleep(300);

                string itemJson = File.ReadAllText(itemDataPath);
                GameManager.items = JsonConvert.DeserializeObject<List<Item>>(itemJson);

                Console.WriteLine("=============================================================================");
                Console.WriteLine("             플레이어의 아이템 데이터를 성공적으로 불러왔습니다!             ");
                Console.WriteLine("=============================================================================");
                Thread.Sleep(300);

                string questJson = File.ReadAllText(questDataPath);
                GameManager.quests = JsonConvert.DeserializeObject<List<Quest>>(questJson);

                Console.WriteLine("=============================================================================");
                Console.WriteLine("             플레이어의 퀘스트 데이터를 성공적으로 불러왔습니다!             ");
                Console.WriteLine("=============================================================================");
                Thread.Sleep(300);
                Console.ResetColor();
            }

            else
            {
                //introScene.PlayIntro();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Clear();
                Console.WriteLine("=============================================================================");
                Console.WriteLine("                     저장된 플레이어 데이터가 없습니다.                      ");
                Console.WriteLine("=============================================================================");
                Console.WriteLine("                     생성할 캐릭터의 이름을 정해주세요.                      ");
                Console.WriteLine("=============================================================================");
                Console.ResetColor();
                bool isInt;
                int command;

                string name = Console.ReadLine();

                while (true)
                {
                    Console.WriteLine($"{name} 이(가) 맞나요? (예 : 1 / 아니요 : 0)");
                    isInt = int.TryParse(Console.ReadLine(), out command);
                    if (command == 1)
                        break;

                    Console.WriteLine("다시 작성해주세요.");
                    name = Console.ReadLine();
                }

                do
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine();
                    Console.WriteLine("=============================================================================");
                    Console.WriteLine("          생성할 캐릭터의 직업을 선택해주세요 (전사 : 1 / 마법사 : 2)        ");
                    Console.WriteLine("=============================================================================");
                    Console.ResetColor();
                    isInt = int.TryParse(Console.ReadLine(), out command);
                } while (isInt == false || command > 2 || command < 1);


                if (command == 1)
                    GameManager.player = new Warrior(name);
                else if (command == 2)
                    GameManager.player = new Mage(name);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.WriteLine("=============================================================================");
                Console.WriteLine("                        캐릭터를 생성하고 있습니다..                         ");
                Console.WriteLine("=============================================================================");
                Thread.Sleep(300);
                Console.ResetColor();
            }

        }
    }
}
