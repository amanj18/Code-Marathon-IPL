namespace IPLCODE.Models
{
    public class MatchDetails
    {
        public int MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public string Venue { get; set; }
        public string Team1Name { get; set; }
        public string Team2Name { get; set; }
        public int FanEngagementCount { get; set; }
    }

    public class PlayerStats
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int MatchesPlayed { get; set; }
    }

    public class MatchDetailsbyRange
    {
        public int MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public string Venue { get; set; }
        public string Team1Name { get; set; }
        public string Team2Name { get; set; }
    }

    public class CreatePlayerRequest
    {
        public string PlayerName { get; set; }
        public int TeamId { get; set; }
        public string Role { get; set; }
        public int Age { get; set; }
        public int MatchesPlayed { get; set; }
    }

}
