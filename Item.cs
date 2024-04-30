
namespace Sparta2ndTeam_TeamProject
{
    public enum ItemType
    {
        ARMOR,
        WEAPON,
        PORTION,
    }
    internal class Item
    {
        public ItemType _type;
        string _MarkAtk = "", _MarkDef = "";
        public string Name { get; }
        public string Desc { get; }

        public int Atk { get; }
        public int Def { get; }
        public int HP { get; }  
        public int Price { get; }

        public bool isEquipped { get; private set; }
        public bool isPurchased { get; private set; }

        public Item(string Name, string Desc, int Atk, int Def, int HP, int Price,
            ItemType _type, bool isEquipped = false, bool isPurchased = false)
        {
            this.Name = Name; this.Desc = Desc; this.Atk = Atk;
            this.Def = Def; this.HP = HP; this.Price = Price; this._type = _type;
            this.isEquipped = isEquipped; this.isPurchased = isPurchased;
        }

        internal void PrintItemStatDesc(bool withNumber = false, int idx = 0)
        {
            string _menuHead = "";
            if (!withNumber) _menuHead = "- ";
            else _menuHead = $"{idx}. ";

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

            }

            Console.Write(" | ");
            Console.Write(ConsoleUtility.PadRightForMixedText(Desc, 55));
            //Console.Write(Desc);
        }

        internal void ToggleEquipStatus()
        {
            isEquipped = !isEquipped;
        }
        internal void TogglePurchaseStatus()
        {
            isPurchased = !isPurchased;
        }
    }
}