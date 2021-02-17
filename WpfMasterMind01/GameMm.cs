using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMasterMind02
{   /// <summary>
    /// contains all info of a game 
    /// </summary>
    public class GameMm
    {
        // number of positions in a code
        public int Pos { get; set; }
        // number of allowed characters on a position
        public int Chr { get; set; }
        // number of trails allowed for a player
        public int Trl { get; set; }
        // true if a game is being played
        public bool Live { get; set; }
        // rank number of the current player
        // so, the index of the list_players
        // 0 is first player, 1 is second player, etc
        public int CurrPlay { get; set; }
        // the rank ofthe code to be guessed by the player, start at 0
        public int CodeRank { get; set; }
        // the code which is entered by the player
        public string Code { get; set; }
        // the secret code to be guessed by the player
        public string CodeSecret { get; set; }
        // the list with all positions and black/white interpretations of 
        // the entered codes
        private List<string> GameBoard = new List<string>();
        // the first character of the allowed characters (e.g. 'A')
        public char FirstChr { get; set; }

        /// <summary>
        /// Constructor, init the game 
        /// </summary>
        public GameMm()
        {
            //set live false at start, no game is being played yet
            Live = false;
            // init with non existing Player
            // will be set by new game or reset button
            CurrPlay = -1;
            // SetGame the current guess, to be entered by player
            CodeRank = 0;
            // init the code to be entered by user as empty
            Code = string.Empty;
            // init the secret code as empty
            CodeSecret = string.Empty;
        }

        /// <summary>
        /// check the input of Pos, Chr and Trl for valid
        /// </summary>
        /// <param name="Pos"></param>
        /// <param name="Chr"></param>
        /// <param name="Trl"></param>
        /// <returns>True if all parameters are integer greater than 0</returns>
        public bool CheckInput(string Pos, String Chr, String Trl)
        {
            // return true if Pos, Chr and Trl are greater than 0
            if (int.TryParse(Pos, out int GPos) &&
                int.TryParse(Chr, out int GChr) &&
                int.TryParse(Trl, out int GTrl))
                if ((GPos > 0) && (GChr > 0) && (GTrl > 0))
                    return true;
            return false;
        }


        /// <summary>
        /// start a new game, with empty list gameboard 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="chr"></param>
        /// <param name="trl"></param>
        /// <param name="firstChr"></param>
        public void SetGame(int pos, int chr, int trl, char firstChr)
        {
            // read the settings of the game
            Pos = pos;
            Chr = chr;
            Trl = trl;
            // clear the GameBoard
            GameBoard.Clear();
            // build new GameBoard with dots
            string s = new string('.', Pos) + "  ..." + Environment.NewLine;
            for (int i = 0; i < Trl; i++)
                GameBoard.Add(s);
            // reset the rank of the first code to be entered by player
            CodeRank = 0;
            // make the first cahr accessable via this class
            FirstChr = firstChr;
            // set live to true, a game is started
            Live = true;
        }

        /// <summary>
        /// returns a string that can be written to the UI gameboard
        /// </summary>
        /// <returns>string which can be used as text for the gameBoardBlock</returns>
        public string DisplayGameBoard()
        {
            // build string s that contains the dots for the gameboard
            string s = string.Empty;
            for (int i = 0; i < Trl; i++)
                s += GameBoard[i];
            return s;
        }

        /// <summary>
        /// updates the gameboard with entered code
        /// ads B and W
        /// Black right char right place, White is right char wrong place
        /// </summary>
        /// <param name="codeTry"></param>
        /// <returns>True if won</returns>
        public bool InterpCode(string codeTry)
        {
            // find the white characters, number of characters 
            // both in secret code and guessed codeTry
            string score = string.Empty;
            for (int i = 0; i < Chr; i++)
                for (int j = 0; j < Math.Min(GetNmbChar(i, CodeSecret), GetNmbChar(i, codeTry)); j++)
                    score += "W";
            // check for characters on the right place
            // alter the W into a B
            int cnt_B = 0; // keep track of number of right char at right place
            for (int i = 0; i < Pos; i++)
                if (string.Equals(CodeSecret[i], codeTry[i]))
                {
                    // alter most left W into B
                    StringBuilder sb = new StringBuilder(score);
                    sb[score.IndexOf('W')] = 'B';
                    score = sb.ToString();
                    // track number of B
                    cnt_B++;
                }
            // add the Black/White string to the list of guesses    
            // add the entered code to gameboard at line CodeRank
            GameBoard[CodeRank] = codeTry + "    ";
            GameBoard[CodeRank] += (string.IsNullOrEmpty(score)) ? "..." : score;
            GameBoard[CodeRank] += Environment.NewLine;

            // true if code is found
            return (cnt_B == Pos);
        }


        /// <summary>
        /// get the number of char in the code with the requested rank
        /// e.g. charRank=0 get number of 'A',charRank=1 get number of 'B', charRank=2 ...
        /// </summary>
        /// <param name="charRank"></param>
        /// <param name="code"></param>
        /// <returns>the number of chars of charRank in code</returns>
        public int GetNmbChar(int charRank, string code)
        {
            // init the number of char
            int nmbRankChar = 0;
            // count the characters with charRank
            for (int i = 0; i < code.Length; i++)
                if (code[i].Equals((char)((int)FirstChr + charRank)))
                    nmbRankChar++;
            // return the number of chars of charRank in code
            return nmbRankChar;
        }

        /// <summary>
        /// check if a player has run out of trails and loses
        /// </summary>
        /// <returns>True if player has lost</returns>
        public bool CheckTrails()
        {
            // true if player has lost
            return Trl <= CodeRank + 1;
        }

    }
}
