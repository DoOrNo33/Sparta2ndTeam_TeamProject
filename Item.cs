
namespace Sparta2ndTeam_TeamProject
{
    public enum ItemType
    {
        ARMOR,
        WEAPON,
        PORTION,
        MONSTER_DROP,
        Pet
    }
    internal class Item
    {
        public ItemType _type;
        string _MarkAtk = "", _MarkDef = "";
        public string Name { get; protected set; }
        public string Desc { get; protected set; }

        public int Atk { get; }
        public int Def { get; }
        public int HP { get; }  
        public int MP { get; }  
        public int Price { get; protected set; }
    

        public bool isEquipped { get; protected set; }
        public bool isPurchased { get; protected set; }
        public bool isInitItem { get; protected set; }

        public Item(string Name, string Desc, int Atk, int Def, int HP, int MP, int Price,
            ItemType _type, bool isEquipped = false, bool isPurchased = false, bool isInitItem = false)
        {
            this.Name = Name; this.Desc = Desc; this.Atk = Atk; this.Def = Def;
            this.HP = HP; this.MP = MP;  this.Price = Price; this._type = _type;
            this.isEquipped = isEquipped; this.isPurchased = isPurchased; this.isInitItem = isInitItem;

        }

        internal void PrintItemStatDesc(bool withNumber = false, int idx = 0)
        {
            string _menuHead = "";
            if (!withNumber) _menuHead = "- ";
            else _menuHead = $"{idx:00}. ";

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{_menuHead}");
            Console.ResetColor();

            if (isEquipped)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[E] ");
                Console.ResetColor();
                Console.Write(ConsoleUtility.PadRightForMixedText(Name, 17));
            }
            else Console.Write(ConsoleUtility.PadRightForMixedText(Name, 20));

            if (this._type != ItemType.Pet)
            {
                Console.Write(" | ");

            _MarkAtk = Atk > 0 ? "+" : "";
            _MarkDef = Def > 0 ? "+" : "";

            
                //아이템이 공격력과 방어력 모두에게 영향을 미치는 경우,
                if (Atk != 0 && Def != 0)
                {
                    Console.Write(ConsoleUtility.PadRightForMixedText(($"공격력 {_MarkAtk}{Atk}, 방어력 {_MarkDef}{Def}"), 22));
                }
                else
                {
                    if (Atk != 0)
                        Console.Write(ConsoleUtility.PadRightForMixedText(($"공격력 {_MarkAtk}{Atk}"), 22));
                    if (Def != 0)
                        Console.Write(ConsoleUtility.PadRightForMixedText(($"방어력 {_MarkDef}{Def}"), 22));
                    if (HP != 0)
                        Console.Write(ConsoleUtility.PadRightForMixedText(($"체  력 +{HP}"), 22));
                    if (MP != 0)
                        Console.Write(ConsoleUtility.PadRightForMixedText(($"마  나 +{MP}"), 22));

                }
            }
            

            Console.Write(" | ");
            Console.Write(ConsoleUtility.PadRightForMixedText(Desc, 57));
        }

        internal void ToggleEquipStatus()
        {
            isEquipped = !isEquipped;
        }
        internal void TogglePurchaseStatus()
        {
            isPurchased = !isPurchased;
        }

        internal void DropItemActive()
        {
            isPurchased = true;
        }
    }
}