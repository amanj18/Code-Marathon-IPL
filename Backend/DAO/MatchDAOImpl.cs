using IPLCODE.Models;
using Npgsql;
namespace IPLCODE.DAO
{
    // interface implementation
    public class MatchDAOImpl : IMatchDAO
    {
        // The readonly keyword indicates that the value of the field can only be assigned during its declaration or
        // within the constructor(s) of the class. Once assigned, the value cannot be modified.
        private readonly string _connectionString;

        //IConfiguration is an interface from the Microsoft.Extensions.Configuration namespace that provides access to configuration settings.
        //This interface is typically used to read configuration values from various sources like configuration files (appsettings.json),
        //environment variables, or other sources.
        public MatchDAOImpl (IConfiguration connectionString)
        {
            //_connectionString will now hold the connection string value "Host=myserver;Database=mydb;Username=myuser;Password=mypassword;",
            //which can be used later in the class methods to connect to a database.
            _connectionString = connectionString.GetConnectionString("PostgreDB"); ;
        }

        // Method Initialization
        public async Task<IEnumerable<MatchDetails>> GetMatchDetailsWithEngagementsAsync()
        {
            var matchDetails = new List<MatchDetails>();
            // The using keyword is used to create a scope for an object, ensuring that the object is disposed of properly when the scope is
            // exited, even if an exception occurs.

            //Objects that implement the IDisposable interface (like NpgsqlConnection) often use using to automatically manage resource cleanup.
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                // await connection.OpenAsync(); is used to open a database connection asynchronously.
                await connection.OpenAsync();

                var query = @"
                SELECT 
                    m.match_id AS ""MatchId"",
                    m.match_date AS ""MatchDate"",
                    m.venue AS ""Venue"",
                    t1.team_name AS ""Team1Name"",
                    t2.team_name AS ""Team2Name"",
                    COALESCE(fan_engagements.engagement_count, 0) AS ""FanEngagementCount""
                FROM ipl_sch.Matches m
                JOIN ipl_sch.Teams t1 ON m.team1_id = t1.team_id
                JOIN ipl_sch.Teams t2 ON m.team2_id = t2.team_id
                LEFT JOIN (
                    SELECT match_id, COUNT(*) AS engagement_count
                    FROM ipl_sch.Fan_Engagement
                    GROUP BY match_id
                ) fan_engagements ON m.match_id = fan_engagements.match_id
                ";

            // query: The SQL query string that will be executed against the database.
            // connection: The NpgsqlConnection object that represents the connection to the PostgreSQL database.
                using (var command = new NpgsqlCommand(query, connection))
                {
                    // Executes the SQL query asynchronously and returns an NpgsqlDataReader object to read the results.
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Reads each row from the result set returned by the query.
                        // The await keyword here asynchronously waits for the ReadAsync method to complete,
                        // which means it reads the next row from the result set without blocking the thread.
                        while (await reader.ReadAsync())
                        {
                            matchDetails.Add(new MatchDetails
                            {
                                // Creates a new MatchDetails object for each row in the result set and adds it to the matchDetails list.
                                MatchId = reader.GetInt32(reader.GetOrdinal("MatchId")),
                                MatchDate = reader.GetDateTime(reader.GetOrdinal("MatchDate")),
                                Venue = reader.GetString(reader.GetOrdinal("Venue")),
                                Team1Name = reader.GetString(reader.GetOrdinal("Team1Name")),
                                Team2Name = reader.GetString(reader.GetOrdinal("Team2Name")),
                                FanEngagementCount = reader.GetInt32(reader.GetOrdinal("FanEngagementCount")),
                            });
                        }
                    }
                }
            }
            return matchDetails;
        }

        public async Task<IEnumerable<PlayerStats>> GetTop5PlayersByMatchesAndEngagementsAsync()
        {
            var playerStats = new List<PlayerStats>();

            var query = @"
        WITH TopMatches AS (
            SELECT
                m.match_id,
                COALESCE(fan_engagements.engagement_count, 0) AS engagement_count
            FROM ipl_sch.Matches m
            LEFT JOIN (
                SELECT match_id, COUNT(*) AS engagement_count
                FROM ipl_sch.Fan_Engagement
                GROUP BY match_id
            ) fan_engagements ON m.match_id = fan_engagements.match_id
            ORDER BY engagement_count DESC
            LIMIT 5
        ),
        PlayerStats AS (
            SELECT
                p.player_id,
                p.player_name,
                COUNT(p.player_id) AS matches_played
            FROM ipl_sch.Players p
            JOIN ipl_sch.Matches m ON p.team_id IN (m.team1_id, m.team2_id)
            JOIN TopMatches tm ON m.match_id = tm.match_id
            GROUP BY p.player_id, p.player_name
        )
        SELECT
            player_id,
            player_name,
            matches_played
        FROM PlayerStats
        ORDER BY matches_played DESC
        LIMIT 5;
        ";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            playerStats.Add(new PlayerStats
                            {
                                PlayerId = reader.GetInt32(reader.GetOrdinal("player_id")),
                                PlayerName = reader.GetString(reader.GetOrdinal("player_name")),
                                MatchesPlayed = reader.GetInt32(reader.GetOrdinal("matches_played"))
                            });
                        }
                    }
                }
            }

            return playerStats;
        }

        public async Task<IEnumerable<MatchDetailsbyRange>> GetMatchesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var matchDetails = new List<MatchDetailsbyRange>();

            var query = @"
            SELECT
                m.match_id AS ""MatchId"",
                m.match_date AS ""MatchDate"",
                m.venue AS ""Venue"",
                t1.team_name AS ""Team1Name"",
                t2.team_name AS ""Team2Name""
            FROM ipl_sch.Matches m
            JOIN ipl_sch.Teams t1 ON m.team1_id = t1.team_id
            JOIN ipl_sch.Teams t2 ON m.team2_id = t2.team_id
            WHERE m.match_date BETWEEN @StartDate AND @EndDate
            ORDER BY m.match_date;
            ";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            matchDetails.Add(new MatchDetailsbyRange
                            {
                                MatchId = reader.GetInt32(reader.GetOrdinal("MatchId")),
                                MatchDate = reader.GetDateTime(reader.GetOrdinal("MatchDate")),
                                Venue = reader.GetString(reader.GetOrdinal("Venue")),
                                Team1Name = reader.GetString(reader.GetOrdinal("Team1Name")),
                                Team2Name = reader.GetString(reader.GetOrdinal("Team2Name")),
                            });
                        }
                    }
                }
            }

            return matchDetails;
        }

        public async Task AddPlayerAsync(CreatePlayerRequest player)
        {
            var query = @"
            INSERT INTO ipl_sch.Players (player_name, team_id, role, age, matches_played)
            VALUES (@PlayerName, @TeamId, @Role, @Age, @MatchesPlayed);
            ";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PlayerName", player.PlayerName);
                    command.Parameters.AddWithValue("@TeamId", player.TeamId);
                    command.Parameters.AddWithValue("@Role", player.Role);
                    command.Parameters.AddWithValue("@Age", player.Age);
                    command.Parameters.AddWithValue("@MatchesPlayed", player.MatchesPlayed);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
