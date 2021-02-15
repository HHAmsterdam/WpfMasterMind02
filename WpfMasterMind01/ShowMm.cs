using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMasterMind02
{
    class ShowMm
    {
        // define new line just for abbreviation
        private readonly string nline = Environment.NewLine;

        public string Score()
        {
            // display the score board after a reset
            string s =  "Choose a player" + nline +
                        "if no player is available" + nline +
                        "add a player first";
            return s;
        }

        public string Score(Player player)
        {
            // display the score board of a player
            string s =  "Score " + player.Name + nline +
                        "number of games: " + player.NmbGames + nline +
                        "average trails : " + player.AvgTrail + nline +
                        "succes rate (%): " + player.SuccessRate;
            return s;
        }

        public string Info()
        {
            // display the initional info board after a cold start
            string s =  "Add one or more players" + nline +
                        "alter the 'positions', 'letters'" + nline + 
                        "and 'trails' if you like" + nline +
                        "press the 'start new game' button next";
            return s;
        }

        public string Info(string SwitcStr)
        {
            String s;

            switch (SwitcStr)
            {
                case "player_chosen":
                    s = "press 'start new game' " + nline +
                        "and fill in a next code";
                    return s;

                case "player_added":
                    s = "press 'start new game' " + nline +
                        "and fill in a next code" + nline +
                        "or add more players";
                    return s;

                case "player_deleted":
                    s = "player is deleted " + nline +
                        "choose or add a new player";
                    return s;

                case "start_game":
                    s = "Let's go and play! " + nline +
                        "fill in a next code and press 'go'";
                    return s;

                case "choose_player_first":
                    s = "Almost ready " + nline +
                        "choose or add a player first";
                    return s;

                case "input_int":
                    s = "Make all input for " + nline +
                        "positions, letters and trails" + nline + 
                        "a number greater than zero";
                    return s;

                default:
                    return "WARNING ShowMm, not defined: " + SwitcStr;
            }
        }

        public string Info(GameMm gameMm)
        {
            // show help text for a valid code
            string s = "You may only enter codes of:" + nline ;
            // add number of positions
            s += gameMm.Pos.ToString() + " characters" + nline + "you may choose from:" + nline;
            for (int i = (int)gameMm.FirstChr; i < (int)gameMm.FirstChr + gameMm.Chr; i++)
                s+=" " + (char)i;
            // return the help string
            return s;
        }
    }
}
