using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta2ndTeam_TeamProject.Items
{
    internal class Pet : Item
    {
        public Pet(string Name, string Desc, int Atk, int Def, int HP, int MP, int Price,
    ItemType _type, bool isEquipped = false, bool isPurchased = false, bool isInitItem = false) : base(Name, Desc, Atk, Def, HP, MP, Price, _type, isEquipped, isPurchased, isInitItem)
        {
            this.Name = Name; 
            this.Desc = Desc; 
            this._type = _type;
            this.isEquipped = isEquipped; 
            this.isPurchased = isPurchased; 
            this.isInitItem = isInitItem;

        }

        public virtual void PetSkill()
        {

        }
    }
}
