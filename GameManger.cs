using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
namespace Custom
{
    public class GameManger
    {
        private Board _board; 
        private List<Player> _players; // list of players
        private int _playerInTurn; //the order of which player is in turn
        private NoticeBox _noticeBox; //the notice box in the middile of the board
        private DisplayPlayerInfo _displayPlayer;  //display the players and their money
        private DisplayInfo _displayInfo; //display detailed information of the cells or player
        public GameManger()
        {
            LoadResources(); //load all resource
            _board = new Board();
            _players = new List<Player>();
            CreatePlayers(); //create player
            _playerInTurn = 0; //set the current player to the first one
            MoveCommand move = MoveCommand.Instance;
            move.AddBoard(_board); //subscribe the board to the move command
            _noticeBox = new NoticeBox(this); 
            _displayPlayer = new DisplayPlayerInfo(_players);
            _displayInfo = new DisplayInfo();
            _board.AddDisplay(_displayInfo); //subscribe displayInfo to the board
            _displayPlayer.AddInfoObserve(_displayInfo);  //subscribe displayInfo to the displayPlayer
        }
        public void Draw()
        {
            _board.Draw();
            foreach(Player player in _players)
            {
                player.Draw();
            }
            _noticeBox.Draw();
            _displayPlayer.Draw();
            _displayInfo.Draw();
        }
        public void AddPlayer(Player p)
        {
            _players.Add(p);
        }
        public Player PlayerInTurn //return the id of which player is in turn
        {
            get
            {
                return _players[_playerInTurn];
            }
        }
        public void NextTurn() //next turn for the game
        {
            do 
            {
                if (_playerInTurn == _players.Count - 1) //if it is the last player then the next player will be the first one
                {
                    _playerInTurn = 0;
                    _players[0].IsInTurn = true;
                }
                else
                {
                    _playerInTurn++;
                    _players[_playerInTurn].IsInTurn = true;
                }
            } while (_players[_playerInTurn].IsBankrupt); //make sure the current player is not bankrupt. if yes, repeat the above procedure
        }
        public void Update() // to check if the game ends where there is only one player left without being bankrupt
        {
            if (GameEnd())
            {
               
              for(int i=0; i < _players.Count; i++)
                {
                    if (!_players[i].IsBankrupt)
                    {
                        _playerInTurn = i;
                        _players[_playerInTurn].IsInTurn = false; // set the player not in turn
                        _noticeBox.ChangeState(new FinishState(_noticeBox)); //finish the game with finish state
                    }
                }
            }
        }
        public bool GameEnd() //check if there is only one player left without being bankrupt
        {
            int sum = 0;
            foreach(Player p in _players)
            {
                if (!p.IsBankrupt)
                {
                    sum++;
                }
            }
            if(sum == 1)
            {
                return true;
            }
            return false;
        }
        public void LoadResources() //load all bitmaps needed
        {
            SplashKit.LoadFont("GameFont", "bold.ttf");
            SplashKit.LoadBitmap("1", "1.png");
            SplashKit.LoadBitmap("2", "2.png");
            SplashKit.LoadBitmap("3", "3.png");
            SplashKit.LoadBitmap("4", "4.png");
            SplashKit.LoadBitmap("5", "5.png");
            SplashKit.LoadBitmap("6", "6.png");
        }
        public void CreatePlayers()
        {
            Cell c = _board.FetchCell(0); //set all the players to the first cell
            float x = (float)c.CenterPoint().X;
            float y = (float)c.CenterPoint().Y + 7;
            Player p = new Player(x, y, "player1", 0, Color.IndianRed);
            p.IsInTurn = true;
            AddPlayer(p);
            Player p1 = new Player(x, y, "player2", 1, Color.BlueViolet);
            AddPlayer(p1);
            Player p2 = new Player(x, y, "player3", 2, Color.Gold);
            AddPlayer(p2);
            Player p3 = new Player(x, y, "player4", 3, Color.DarkGray);
            AddPlayer(p3);
        }
        public Cell FindCell(int position) //fectch necessary cell
        {
            return _board.FetchCell(position);
        }
        public void HandleInput(Point2D pt) // to check if any components of the below is clicked then process
        {
            _noticeBox.HandleClick(pt); 
            _board.HandleClick(pt);
            _displayPlayer.HandleCLick(pt);
        }
    }
}
