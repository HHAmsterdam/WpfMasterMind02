using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMasterMind02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // show initional txt in Infobox
            txtInfo.Text = ShowMm.Info();
            // init the number of positions 
            TxtPos.Text = InitMm.Pos;
            // init the number of characters per position
            TxtChr.Text = InitMm.Chr;
            // init the number of trails
            TxtTrl.Text = InitMm.Trl;
            // init the fisrt line of the combobox
            ComboPlayer.Items.Add(InitMm.InitCombo);
            // set the first line of the combobox as selected
            ComboPlayer.SelectedIndex = 0;
        }

        // instance initional values of the positions, Characters and trails
        InitMm InitMm = new InitMm();
        // instance of the list of players
        List<Player> list_players = new List<Player>();
        // instance shows info in all info textboxes
        ShowMm ShowMm = new ShowMm();
        // instance of the game
        GameMm GameMm = new GameMm();
        // instance of the code handler
        CodeMm CodeMm = new CodeMm();
        
        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            // only act if a game is live with an existing player
            if (GameMm.Live && GameMm.CurrPlay >= 0)
            {
                // check if the entered code is valid
                if (!CodeMm.CheckValid(txtCode.Text, GameMm))
                    // no valid code entered, some help text for valid codes
                    txtInfo.Text = ShowMm.Info(GameMm);
                else if (GameMm.InterpCode(txtCode.Text))
                {
                    // valid code is entered
                    // player has won, set the game on not live
                    GameMm.Live = false;
                    // update the player score, player loses
                    list_players[GameMm.CurrPlay].Update(GameMm, false);
                    // player has lost, some txt in InfoBox 
                    txtInfo.Text = InitMm.Won + GameMm.CodeSecret;
                    // update and show score
                    txtScore.Text = ShowMm.Score(list_players[GameMm.CurrPlay]);
                    // display the game board
                    txtBoard.Text = GameMm.DisplayGameBoard();
                    return;
                }
                     
                if (GameMm.CheckTrails())
                {
                    // check if player has lost with valid or non valid code
                    // player has lost, set the game on not live
                    GameMm.Live = false;
                    // update the player score, player loses
                    list_players[GameMm.CurrPlay].Update(GameMm, true);
                    // player has lost, some txt in InfoBox 
                    txtInfo.Text = InitMm.Lost + GameMm.CodeSecret;
                    // update and show score
                    txtScore.Text = ShowMm.Score(list_players[GameMm.CurrPlay]);
                }
                // display the game board
                txtBoard.Text = GameMm.DisplayGameBoard();
                // go the the next Rank for next entered code 
                GameMm.CodeRank++;
            }
        }

        private void ButtonRst_Click(object sender, RoutedEventArgs e)
        {
            // reset the game, clear the list of players
            list_players.Clear();
            // empty the Score board
            txtScore.Text = string.Empty;
            // set Player to non existing player
            GameMm.CurrPlay = -1;
            // set the game to no live game
            GameMm.Live = false;
            // show initional info in info board
            txtInfo.Text = ShowMm.Info();
            // reset the combobox
            ComboPlayer.Items.Clear();
            ComboPlayer.Items.Add(InitMm.InitCombo);
            ComboPlayer.SelectedIndex = 0;
            // clear the game board
            txtBoard.Text = string.Empty;
        }

        // start a new game
        private void ButtonStrt_Click(object sender, RoutedEventArgs e)
        {   // check for filtering on positive number input only
            if (GameMm.CheckInput(TxtPos.Text,TxtChr.Text,TxtTrl.Text))
                // import the txtboxes positions, characters and trails if possible
                GameMm.SetGame(int.Parse(TxtPos.Text), int.Parse(TxtChr.Text), int.Parse(TxtTrl.Text), InitMm.FirstChr);
            else
            {
                txtInfo.Text = ShowMm.Info("input_int");
                return;
            }
            // generate a new secret code
            GameMm.CodeSecret = CodeMm.GenCode(GameMm);
            // display start txt in InfoBox
            if (GameMm.CurrPlay>-1)
                // game has started, player is chosen
                txtInfo.Text = ShowMm.Info("start_game");
            else
                // first select a player
                txtInfo.Text = ShowMm.Info("choose_player_first");
            // display the GameBoard
            txtBoard.Text = GameMm.DisplayGameBoard();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            // make the txtbox for new player visible if needed
            if (TxtNewPlayer.Visibility == Visibility.Hidden)
                TxtNewPlayer.Visibility = Visibility.Visible;
            else
            {
                // add new player to combobox, show player as selected
                ComboPlayer.SelectedIndex = ComboPlayer.Items.Add(TxtNewPlayer.Text);
                // hide the new players textbox
                TxtNewPlayer.Visibility = Visibility.Hidden;
                // add new player to the list_players (first player has index 0)
                list_players.Add(new Player(TxtNewPlayer.Text));
                // show the score of the new player in score board
                txtScore.Text = ShowMm.Score(list_players[ComboPlayer.SelectedIndex-1]);
                // set the current player
                GameMm.CurrPlay = ComboPlayer.SelectedIndex - 1;
                // clear the info board
                txtInfo.Text= string.Empty;
                // show some startup text
                txtInfo.Text = ShowMm.Info("player_added");
            }
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            // delete the player that is selected in the combobox
            if (ComboPlayer.SelectedIndex > 0)
            {
                // delete the player from the list_players
                list_players.RemoveAt(ComboPlayer.SelectedIndex - 1);
                // delete the player from the combobox items
                ComboPlayer.Items.RemoveAt(ComboPlayer.SelectedIndex);
                // set the 'choose a player' item as current in the combobox
                ComboPlayer.SelectedIndex = 0;
            }   
            // no player is selected, show the start up txt
            txtScore.Text = ShowMm.Score();
            // set the current player to non existing player
            GameMm.CurrPlay = -1;
            // show some startup text
            txtInfo.Text = ShowMm.Info("player_deleted");
        }

        private void ComboPlayer_DropDownClosed(object sender, EventArgs e)
        {
            // show score of selected player, or startup txt if no player selected
            if (ComboPlayer.SelectedIndex > 0)
            {
                // show the score of the selected player in score board
                txtScore.Text = ShowMm.Score(list_players[ComboPlayer.SelectedIndex - 1]);
                // set the current player in GameMm
                GameMm.CurrPlay = ComboPlayer.SelectedIndex - 1;
                // show some startup text
                txtInfo.Text = ShowMm.Info("player_chosen");
            }
            else
            {
                // no player is selected, show the start up txt
                txtScore.Text = ShowMm.Score();
                // set the current player in GameMm to non existing player
                GameMm.CurrPlay = - 1;
            }
        }

        private void TxtNewPlayer_MouseEnter(object sender, MouseEventArgs e)
        {
            // clears the NewPlayer input when mouse enters
            TxtNewPlayer.Text = string.Empty;
        }
    }
}
