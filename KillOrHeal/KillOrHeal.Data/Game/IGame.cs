using KillOrHeal.Data.Entities;

namespace KillOrHeal.Data.Game
{
    /// <summary>
    /// Game state.
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Get player by ID
        /// </summary>
        PlayerState GetPlayerStateById(int id);

        /// <summary>
        /// Update player.
        /// </summary>
        void UpdatePlayerState(PlayerState player);

        /// <summary>
        /// Get full game state.
        /// </summary>
        /// <returns></returns>
        GameStateDto GetFullState();

        /// <summary>
        /// Spawn new player. Location and faction are random.
        /// </summary>
        /// <returns></returns>
        PlayerState SpawnNewPlayer();
    }
}