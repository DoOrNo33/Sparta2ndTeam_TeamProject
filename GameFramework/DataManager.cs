using Newtonsoft.Json;
using Sparta2ndTeam_TeamProject.GuildInfo;
using Sparta2ndTeam_TeamProject.Items;
using Sparta2ndTeam_TeamProject.PlayerInfo;
using Sparta2ndTeam_TeamProject.Scenes;
using Sparta2ndTeam_TeamProject.Tower;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject.GameFramework
{
    class DataManager
    {
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static string playerDataName = "playerStatData.json";
        static string itemDataName = "itemData.json";
        static string questDataName = "questData.json";
        static string petDataName = "petData.json";
        static string playerDataPath = Path.Combine(path, playerDataName);
        static string itemDataPath = Path.Combine(path, itemDataName);
        static string questDataPath = Path.Combine(path, questDataName);
        static string petDataPath = Path.Combine(path, petDataName);

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

            string playerJson = JsonConvert.SerializeObject(GameManager.player, Formatting.Indented);
            File.WriteAllText(playerDataPath, playerJson);


            string itemJson = JsonConvert.SerializeObject(GameManager.items, Formatting.Indented);
            File.WriteAllText(itemDataPath, itemJson);

            string questJson = JsonConvert.SerializeObject(GameManager.quests, Formatting.Indented);
            File.WriteAllText(questDataPath, questJson);

            string petJson = JsonConvert.SerializeObject(GameManager.pets, Formatting.Indented);
            File.WriteAllText(petDataPath, petJson);

            Console.WriteLine("=============================================================================");
            Console.WriteLine("                     데이터를 성공적으로 저장하였습니다!                     ");
            Console.WriteLine("=============================================================================");
            Console.ResetColor();
            Thread.Sleep(500);
        }


        static public void LoadData()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=============================================================================");
            Console.WriteLine("                       플레이어 데이터를 불러옵니다...                       ");
            Console.WriteLine("=============================================================================");

            Thread.Sleep(500);
            string playerJson = File.ReadAllText(playerDataPath);

            Player temp = JsonConvert.DeserializeObject<Player>(playerJson);

            if (temp.Job == 1)
                GameManager.player = JsonConvert.DeserializeObject<Warrior>(playerJson);
            else if(temp .Job == 2)
                GameManager.player = JsonConvert.DeserializeObject<Mage>(playerJson);

            // GameManager.player = JsonConvert.DeserializeObject<Player>(playerJson);
            GameManager.tower.TowerLv = GameManager.player.towerLv;

            string itemJson = File.ReadAllText(itemDataPath);
            GameManager.items = JsonConvert.DeserializeObject<List<Item>>(itemJson);

            Console.WriteLine("=============================================================================");
            Console.WriteLine("                   플레이어의 아이템 데이터를 불러옵니다...                  ");
            Console.WriteLine("=============================================================================");
            Thread.Sleep(500);

            string questJson = File.ReadAllText(questDataPath);
            GameManager.quests = JsonConvert.DeserializeObject<List<Quest>>(questJson);

            Console.WriteLine("=============================================================================");
            Console.WriteLine("                  플레이어의 퀘스트 데이터를 불러옵니다...                   ");
            Console.WriteLine("=============================================================================");
            Thread.Sleep(500);

            string petJson = File.ReadAllText(petDataPath);
            GameManager.pets = JsonConvert.DeserializeObject<List<Pet>>(petJson);
            Console.ResetColor();
            
        }

        static public void GameStart()
        {
            Console.Clear();


            if (File.Exists(playerDataPath) && File.Exists(itemDataPath) && File.Exists(questDataPath) && File.Exists(petDataPath))
            {
                Console.Clear();
                int command = -1;

                while (true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("=============================================================================");
                    Console.WriteLine("                           기존 데이터가 존재합니다.                         ");
                    Console.WriteLine("                   1. 새 게임                   2. 불러오기                  ");
                    Console.WriteLine("=============================================================================\n");
                    Console.ResetColor();

                    command = ConsoleUtility.PromptMenuChoice(1, 2);

                    if (command != -1)
                        break;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor();
                }


                switch (command)
                {
                    case 1:
                        NewGame(); 
                        
                        break;
                    case 2: LoadData(); 
                        break;
                }

                ConsoleUtility.PrintGameHeader();
            }

            else
            {

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Clear();
                Console.WriteLine("=============================================================================");
                Console.WriteLine("                     저장된 플레이어 데이터가 없습니다.                      ");
                Console.WriteLine("=============================================================================");

                Thread.Sleep(500);

                Console.WriteLine("=============================================================================");
                Console.WriteLine("                     캐릭터 생성 화면으로 이동합니다...                      ");
                Console.WriteLine("=============================================================================");

                Thread.Sleep(500);

                NewGame();
            }

        }

        static public void NewGame()
        {
           
            Console.Clear();
         
            Console.ForegroundColor= ConsoleColor.Yellow;

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
            Console.Clear();

            IntroScene intro = new IntroScene();
            intro.PlayIntro();
        }
    }
}
