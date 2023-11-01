using Newtonsoft.Json.Linq;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class PropertyCell : OwnableCell
    {
        private int _housePrice;
        private int _houseNumber;
        private string _cellColor;
        private bool _houseAllowed;
        public PropertyCell(string name, float x, float y, int width, int height, Direction direction, int position) : base(name, x, y, width, height, direction, position)
        {
            _houseNumber = 0;
            _houseAllowed = false;
           
        }
        public override void NotifyChangeOwner()
        {
            _board.CheckPropertyOwner(this);
        }
       
        public override string Description => throw new NotImplementedException();
        public override void LoadData(JObject ob)
        {
            base.LoadData(ob);
            this.AddTax((int)ob["tax0"]);
            this.AddTax((int)ob["tax1"]);
            this.AddTax((int)ob["tax2"]);
            this.AddTax((int)ob["tax3"]);
            this.AddTax((int)ob["tax4"]);
            _housePrice = (int)ob["housePrice"];
            CellColor = (string)ob["propertyColor"];

        }
        public int HousePrice
        {
            get
            {
                return _housePrice;
            }
        }
        public string CellColor
        {
            get
            {
                return _cellColor;
            }
            set
            {
                _cellColor = value;
            }
        }
        public override int Tax
        {
            get
            {
                return _taxes[_houseNumber];
            }
        }
        public int HouseNumber
        {
            get
            {
                return _houseNumber;
            }
            set
            {
                _houseNumber = value;
            }
        }
        public override void Draw()
        {
            DrawOutline();
            if(IsMortage)
            {
                SplashKit.FillRectangle(Color.LightPink, X + 1, Y + 1, Width - 2, Height - 2);
            }
            else
            {
                SplashKit.FillRectangle(_background, X + 1, Y + 1, Width - 2, Height - 2);
            }

            _drawStrategy.DrawColorBox(X, Y, Width, Height, CellColor);
            _drawStrategy.DrawName(X, Y, Width, Height, Name);
        }
        public bool HouseAllowed
        {
            get
            {
                return _houseAllowed;
            }
            set
            {
                _houseAllowed = value;
            }
        }
        public override void Reset()
        {
            base.Reset();
            HouseNumber = 0;
        }
    }
}
