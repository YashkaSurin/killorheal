namespace KillOrHeal.Domain.Communication.Events.Player
{
    public class PlayerActionEvent: PlayerEvent
    {
        public int ActorId { get; }
        public int TargetId { get; }

        public PlayerActionEvent(int actorId, int targetId)
        {
            ActorId = actorId;
            TargetId = targetId;
        }

        public override int? SourceId => ActorId;
    }    
}