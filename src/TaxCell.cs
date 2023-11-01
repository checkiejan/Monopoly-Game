using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class TaxCell: Cell
    {
        private int _tax;
        public TaxCell(string name, float x, float y, int width, int height, Direction direction, int position) : base(name, x, y, width, height, direction, position)
        {
            _tax = 100;
        }
        public override string Description
        {
            get
            {
                return string.Format("You will be taxed ${0}",_tax);
            }
        }
        public override void PlayerVisit(Player p)
        {
            if (!p.AblePayBack(_tax))
            {
                p.IsBankrupt = true;
            }
            p.ChangeMoney(-_tax);
        }
    }
}
