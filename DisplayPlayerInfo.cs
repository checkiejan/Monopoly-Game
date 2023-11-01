using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class DisplayPlayerInfo //display all the players' money
    {
        private List<PlayerDisplayBox> _boxes; //box of information of each player
        private int _x = 1050;
        private int _y = 0;
        private DisplayInfo _displayInfo;
        public DisplayPlayerInfo(List<Player> players)
        {
            _boxes = new List<PlayerDisplayBox>();
            foreach(Player player in players)
            {
                PlayerDisplayBox box = new PlayerDisplayBox(_x, _y, player.Name, player);
                player.AddDisplayObserve(box); //subscribe each player to their box so that they can notify the box when the money changes
                AddBox(box);
                _y += 50;
            }
        }
        public void AddInfoObserve(DisplayInfo displayInfo) //use observer to notify the display information to display detailed information of the player
        {
            _displayInfo = displayInfo; //subscribe to the displayInfo
        }
        public void AddBox(PlayerDisplayBox box)
        {
            _boxes.Add(box);
        }
        public void Draw()
        {
            foreach(PlayerDisplayBox box in _boxes)
            {
                box.Draw();
            }
        }
        public void HandleCLick(Point2D pt)
        {
            foreach(PlayerDisplayBox box in _boxes)
            {
                if(box.IsAt(pt)) // if any box is clicked notify the displayInfo to display
                {
                    _displayInfo.StrategyDisplay(new DisplayPlayer());  // change the strategy of the displayInfo to display player
                    _displayInfo.AddDisplay(box.Player); //add the player to display
                    break;
                }
            }
        }
    }
}
