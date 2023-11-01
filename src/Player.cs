using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
namespace Custom
{
    public class Player: DrawObject, ICanBeDisplayed
    {
        private int[,] PlayerPosOnCell = new int[4, 2] //determine where to place player on the cell based on playerID
        {
            {-10, 10},
            {-10, -10},
            {10, -10},
            {10, 10}
        };
        private int _money; // the current money
        private int _position; // the  position on the board
        private int _playerID; // the player id
        private float _radius; 
        private Color _color; 
        private bool _isInTurn; 
        private bool _isBankrupt; // check if the player has been bankrupt
        private bool _isInJail;
        private PlayerDisplayBox _displayStatus;
        private int _turnsInJail; 
        private List<OwnableCell> _properties;
        public Player(float x, float y, string name, int playerID, Color color):  base(x,y,name)
        {
            
            _money = 2000; //money initialized
            _position = 0;
            _isInTurn = false;
            _isBankrupt = false;
            _isInJail = false;
            _turnsInJail = 0;
            _playerID = playerID;
            _color = color;
            _radius = 5;
            _properties = new List<OwnableCell>();
        }
        public override void Draw()
        {
            if(!IsBankrupt)
            {
                SplashKit.FillCircle(_color, X + PlayerPosOnCell[_playerID, 0], Y + PlayerPosOnCell[_playerID, 1], _radius);
            }
        }
        public Boolean AblePayBack(float money)
        {
            float sum = _money;
            foreach(OwnableCell o in _properties)
            {
                sum += o.RedeemPrice;
            }
            if (sum > money)
            {
                return true;
            }
            return false;
        }
        public void AddDisplayObserve(PlayerDisplayBox displayBox)
        {
            _displayStatus = displayBox;
        }
        private void NoticeChangeMoney(int money)
        {
            _displayStatus.NotifyMoneyChange(money);
        }
        public void AddProperty(OwnableCell c)
        {
            _properties.Add(c);
        }
        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public int PlayerID
        {
            get
            {
                return _playerID;
            }
        }
        public void BuyProperty(OwnableCell oc)
        {
            AddProperty(oc);
            oc.Owner = this;
            ChangeMoney(-oc.Price);
        }
        public int Money
        {
            get { return _money; }
        }
        public void ChangeMoney(int money)
        {
            _money += money;
            NoticeChangeMoney(money);
        }
        public List<OwnableCell> Property
        {
            get
            {
                return _properties;
            }
        }
        public bool IsInTurn
        {
            get
            {
                return _isInTurn;
            }
            set
            {
                _isInTurn = value;
            }
        }
        public bool IsInJail
        {
            get
            {
                return _isInJail;
            }
            set
            {
                _isInJail = value;
            }
        }
        public int TurnInJail
        {
            get
            {
                return _turnsInJail;
            }
            set
            {
                _turnsInJail = value;
            }
        }
        public void GetOutJail()
        {
            IsInJail = false;
            _turnsInJail = 0;
        }
        public string Description
        {
            get
            {
                if (IsBankrupt)
                {
                    return String.Format("{0} is bankrupt", Name);
                }
                return Name;
            }
        }
        public bool IsBankrupt
        {
            get
            {
                return _isBankrupt;
            }
            set
            {
                _isBankrupt = value;
                if(!value)
                {
                    foreach(OwnableCell c in _properties)
                    {
                        c.Reset();
                    }
                    _properties = new List<OwnableCell>();
                }
            }
        }
       
        public Color Color
        {
            get
            {
                return _color;
            }
        }
    }
}
