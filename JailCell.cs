using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class JailCell: Cell
    {
        public JailCell(string name, float x, float y, int width, int height, Direction direction, int position) : base(name, x, y, width, height, direction, position)
        {

        }
        public override string Description
        {
            get
            {
                return "You can visit the jail\nYou will stay in jail for 3 turn\nget out on the fourth";
            }
        }
        public override void PlayerVisit(Player p) //player can visit the jail without having any problem
        {
            
        }
    }
}
