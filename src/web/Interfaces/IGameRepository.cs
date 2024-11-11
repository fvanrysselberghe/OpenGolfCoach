using Microsoft.AspNetCore.SignalR;

namespace OpenGolfCoach.Interfaces
{
    /// <summary>
    /// Repository with all golf rounds
    /// </summary> 
    interface IGolfRoundRepository
    {

        /// <summary>
        /// Adds a new round to the repository
        /// </summary> 
        void Insert();
    }
}