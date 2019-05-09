using System;

namespace KillOrHeal.Domain.Processing
{
    public interface IPlayerEventProcessorResolver
    {
        IPlayerEventProcessor Resolve(Type evenType);
    }
}