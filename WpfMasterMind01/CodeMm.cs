using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMasterMind02
{   /// <summary>
    /// handles a code in the game 
    /// </summary>
    public class CodeMm
    {
        /// <summary>
        /// checks if a valid code is entered
        /// </summary>
        /// <param name="codeStr"></param>
        /// <param name="gameMm"></param>
        /// <returns>True if the code is valid</returns>
        public bool CheckValid(string codeStr, GameMm gameMm )
        {
            // check if code has obliged nmb of characters
            if (codeStr.Length != gameMm.Pos)
                return false;
            
            // check if only vaild characters are used
            byte[] asciiBytes = Encoding.ASCII.GetBytes(codeStr);
            if (asciiBytes.Min() < (int)gameMm.FirstChr || asciiBytes.Max() > (int)gameMm.FirstChr + gameMm.Chr - 1)
                return false;
            // code is valid, return true
            return true;
        }

        /// <summary>
        /// Generate a random valid code
        /// </summary>
        /// <param name="gameMm"></param>
        /// <returns>a random valid code</returns>        
        public string GenCode(GameMm gameMm)
        {
            // get an instance of a random object
            Random rd = new Random();
            // init CodeStr as empty
            string CodeStr = string.Empty;
            
            // generate the random valid code
            for (int i = 0; i < gameMm.Pos; i++)
            {
                // random next character
                int rand_num = rd.Next((int)gameMm.FirstChr, (int)gameMm.FirstChr + gameMm.Chr);
                // add to the code sofar
                CodeStr += (char)rand_num;
            }
            // return the valid secret code
            return CodeStr;
        }
    }
}
