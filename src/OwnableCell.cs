using Newtonsoft.Json.Linq;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public abstract class OwnableCell: Cell
    {
        private Player _owner;
        protected List<int> _taxes; //list of taxes that the cell has
        private int _price; //price of the cell
        private int _stateTax; //current state tax
        private int _mortageValue; 
        private int _redeemPrice;
        private bool _isMortage;
        protected Board _board;
        public OwnableCell(string name, float x, float y, int width, int height, Direction direction, int position) : base(name, x, y, width, height, direction, position)
        {
            _taxes = new List<int>();
            _isMortage = false;
            _stateTax = 0; //state tax starts at 0
            _owner = null; //set the owner to null
        }
       
        public Player Owner //change owner or get the owner
        {
            get
            {
                return _owner;
            }
            set
            {
                _owner = value;
                NotifyChangeOwner(); //notify the board of the change of owner
                if (value == null)
                {
                    StateTax = 0;
                }
            }
        }
        public bool IsMortage
        {
            get
            {
                return _isMortage;
            }
            set
            {
                _isMortage = value;
            }
        }
        public bool IsOwned
        {
            get
            {
                if(_owner != null)
                {
                    return true;
                }
                return false;
            }
        }
        public int Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
            }
        }
        public override void PlayerVisit(Player p)
        {
            if (IsOwned && !IsMortage) //check if the property is owned
            {              
                if(Owner.Name != p.Name)
                {
                    if (p.AblePayBack(Tax)) //check able to pay the tax
                    {
                        p.ChangeMoney(-Tax);
                        _owner.ChangeMoney(Tax);
                    }
                    else  //if not set to bankrupt
                    {
                        p.IsBankrupt = true; //declare bankrupt
                        _owner.ChangeMoney(p.Money); //pay the owner with the left money
                        p.ChangeMoney(p.Money);
                    }
                }
               
            }
        }
        public void AddBoardObserve(Board b)
        {
            _board = b;
        }
        public void AddTax(int tax) //add tax to the cell
        {
            _taxes.Add(tax);
        }
        public abstract void NotifyChangeOwner();
       
        public virtual void Reset() //reset owner and all state of the cell
        {
            Owner = null;
            StateTax = 0;
            IsMortage = false;
        }
        public virtual int Tax //tax is based on the statetax
        {
            get
            {
                return _taxes[_stateTax];
            }
               
        }
        public int StateTax
        {
            get
            {
                return _stateTax;
            }
            set
            {
                _stateTax = value;
            }
        }
        public int MortageValue
        {
            get
            {
                return _mortageValue;
            }
            set
            {
                _mortageValue = value;
            }
        }
        public int RedeemPrice
        {
            get
            {
                return _redeemPrice;
            }
            set
            {
                _redeemPrice = value;
            }
        }
        public List<int> TaxData
        {
            get
            {
                return _taxes;
            }
        }
        public override void LoadData(JObject ob) // load the data into the cell
        {
            MortageValue = (int)ob["mortageValue"];
            RedeemPrice = (int)ob["redeemPrice"];
            Price = (int)ob["price"];
            
        }
        public override void Draw() 
        {
            DrawOutline();
            if (IsMortage) //if is mortage draw different color for the cell
            {
                SplashKit.FillRectangle(Color.LightPink, X + 1, Y + 1, Width - 2, Height - 2);
            }
            else
            {
                SplashKit.FillRectangle(_background, X + 1, Y + 1, Width - 2, Height - 2);
            }
            _drawStrategy.DrawName(X, Y, Width, Height, Name);
        }
    }
}
