using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMasterMind02
{
    /// <summary>
    /// handles all the text used in the GUI
    /// </summary>
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
               "Choose a player" + nline +
               "if no player is available" + nline +
               "add a player first";
        }

        /// <summary>
        /// display the score board of a player
        /// </summary>
        /// <param name="player"></param>
        /// <returns>the string to be displayed in the ScoreBox</returns>
        public string Score(Player player)
        {
            return
                $"Score {player.Name}" + nline +
                $"number of games: {player.NmbGames}" + nline +
                $"average trails : {player.AvgTrail}" + nline +
                $"succes rate (%): {player.SuccessRate}";
        }

        /// <summary>
        /// display the initional info board after a cold start
        /// </summary>
        /// <returns> the string to be displayed in the InfoBox</returns>
        public string Info()
        {
            return
                "Add a player first";
        }

        /// <summary>
        /// different text for the InfoBox
        /// </summary>
        /// <param name="SwitchStr"></param>
        /// <returns> the string to be displayed in the InfoBox</returns>
        public string Info(string SwitchStr)
        {
            switch (SwitchStr)
            {
                case "player_chosen":
                    return
                        "press 'start new game' " + nline +
                        "and fill in a next code";

                case "player_added":
                    return
                        "add more players" + nline +
                        "or"+ nline +
                        "press 'start new game' ";

                case "player_deleted":
                    return
                        "player is deleted " + nline +
                        "choose or add a new player";

                case "start_game":
                    return 
                        "Let's go and play! " + nline +
                        "fill in a next code and press 'go'";

                case "choose_player_first":
                    return
                        "Almost ready " + nline +
                        "choose or add a player first";

                case "input_int":
                    return
                        "Make all input for " + nline +
                        "positions, letters and trails" + nline +
                        "a number greater than zero";

                case "place_name":
                    return
                        "write the name of the player " + nline +
                        "to be added to the game" + nline +
                        "at least one character";

                default:
                    return "WARNING ShowMm, not defined: " + SwitchStr;
            }
        }

        /// <summary>
        /// generates a text to help the player
        /// to enter a valid code
        /// </summary>
        /// <param name="gameMm"></param>
        /// <returns>the string to be displayed in the InfoBox</returns>
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
