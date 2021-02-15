using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMasterMind02
{
    public class ShowMm
    {
        // define new line just for abbreviation
        private readonly string nline = Environment.NewLine;

        /// <summary>
        /// display the score board after a reset
        /// </summary>
        public string Score()
        {
            return
$@"Choose a player
if no player is available
add a player first";
        }

        /// <summary>
        /// display the score board of a player
        /// </summary>
        /// <param name="player"></param>
        public string Score(Player player)
        {
            return
$@"Score {player.Name}
number of games: {player.NmbGames}
average trails : {player.AvgTrail}
succes rate (%): {player.SuccessRate}";
        }

        public string Info()
        {
            // display the initional info board after a cold start
            string s = "Add one or more players" + nline +
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
            var sb = new StringBuilder();
            sb.AppendLine("You may only enter codes of:");

            // add number of positions
            sb.AppendLine($"{gameMm.Pos} characters");
            sb.AppendLine("you may choose from:");
            for (int i = (int)gameMm.FirstChr; i < (int)gameMm.FirstChr + gameMm.Chr; i++)
                sb.Append(" " + (char)i);
            // return the help string
            return sb.ToString();
        }
    }
}
