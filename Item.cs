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
        ItemType _type;
        string _MarkAtk, _MarkDef;
        public string Name { get; }
        public string Desc { get; }

        public int Atk { get; }
        public int Def { get; }
        public int Price { get; }

        public bool isEquipped { get; private set; }
        public bool isPurchased { get; private set; }

        public Item(string Name, string Desc, int Atk, int Def, int Price, 
            ItemType _type)
        {
            this.Name = Name; this.Desc = Desc; this.Atk = Atk;
            this.Def = Def; this.Price = Price; this._type = _type;

            //아이템 장착 여부와 구매 여부에 대한 초기 값은 false로 지정
            isEquipped = false; isPurchased = false;
        }
    }
}