using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMasterMind02
{
    public class InitMm
    {
        // define the inti of number of positions, tharacters and trails
        // and other settings

        // fill in here:
        // number of positions in each code
        static readonly int positions = 4;
        // nummber of posible characters on each position
        static readonly int characters = 6;
        // max number of trails for a player 
        static readonly int trails = 10;
        // first character of the allowed characters in a code
        static readonly char firstChr = 'A';
        // init text for the combobox
        static readonly string initCombo = "Choose a player";
        // init the info text if player has won
        static readonly string won =  "You have found the secret code!"+Environment.NewLine+
                                      " the secret code was indeed: ";
        static readonly string lost = "You run out of trails, pitty" + Environment.NewLine +
                                      " the secret code was : ";


        // asignment of the properties
        public string Pos = positions.ToString();
        public string Chr = characters.ToString();
        public string Trl = trails.ToString();
        public char FirstChr = firstChr;
        public string InitCombo = initCombo;
        public string Won = won;
        public string Lost = lost;

    }
}
