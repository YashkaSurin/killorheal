using KillOrHeal.Data.Entities;

namespace KillOrHeal.Domain.Services
{
    public interface IGameService
    {
        PlayerState SpawnNewPlayer();
    }
}
