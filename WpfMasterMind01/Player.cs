using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMasterMind02
{
    class Player
    {
        // the neame of the player
        public string Name
        { get; set; }

        // nmb of games played since resest
        public int NmbGames
        { get; set; }

        // average nbr of trails per succeed guesses code
        public double AvgTrail
        { get; set; }

        // total number of trails
        public int TotalTrails
        { get; set; }

        // total of lost games
        public int TotalLost
        { get; set; }

        // nmber of succeed games / totol number of games
        public double SuccessRate
        { get; set; }

        // the constructor
        public Player(string name)
        {
            Name = name;
            NmbGames = 0;
            AvgTrail = 0;
            TotalTrails = 0;
            TotalLost = 0;
            SuccessRate = 0;
        } 

        // update the score of a player
        public void Update(GameMm gameMm, bool lost)
        {
            // update the number of playd games
            NmbGames += 1;
            // opdate the total number of trails
            TotalTrails += gameMm.CodeRank+1;
            // update the average number of trails per game
            AvgTrail = TotalTrails / NmbGames;
            // update total number of lost games
            if (lost)
                TotalLost += 1;
            // update the succes rate
            SuccessRate = 100 * (NmbGames - TotalLost) / NmbGames;
        }
    }
}
