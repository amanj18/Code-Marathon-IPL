using IPLCODE.Models;

namespace IPLCODE.DAO
{
    public interface IMatchDAO
    {
        // IEnumerable<T> is an interface that defines a forward-only cursor over a sequence of elements of type T.
        // It is used to represent a collection of objects that can be enumerated (iterated) over, but not modified.

        // Task is a type from the System.Threading.
        // Tasks namespace that represents an asynchronous operation.
        // It allows you to work with asynchronous methods and manage their completion.

        // Method Declaration
        Task<IEnumerable<MatchDetails>> GetMatchDetailsWithEngagementsAsync();

        Task<IEnumerable<PlayerStats>> GetTop5PlayersByMatchesAndEngagementsAsync();

        Task<IEnumerable<MatchDetailsbyRange>> GetMatchesByDateRangeAsync(DateTime startDate, DateTime endDate);

        Task AddPlayerAsync(CreatePlayerRequest player);
    }
}
