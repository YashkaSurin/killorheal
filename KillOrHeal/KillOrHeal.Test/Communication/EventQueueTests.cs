using System;
using KillOrHeal.Domain.Communication;
using KillOrHeal.Domain.Communication.Events.Server;
using Xunit;

namespace KillOrHeal.Test.Communication
{
    public class EventQueueTests
    {
        [Fact]
        public void OrderOfItemsShouldBeCorrect()
        {
            var queue = new EventQueue<ServerEvent>();
            queue.Enqueque(new ServerEvent("Event 1", new DateTime(2018, 1, 2)));
            queue.Enqueque(new ServerEvent("Event 2", new DateTime(2018, 1, 1)));
            queue.Enqueque(new ServerEvent("Event 3", new DateTime(2018, 1, 3)));

            Assert.True(queue.TryDequeue(out var dequeued));
            Assert.Equal("Event 2", dequeued.Message);
            Assert.True(queue.TryDequeue(out dequeued));
            Assert.Equal("Event 1", dequeued.Message);
            Assert.True(queue.TryDequeue(out dequeued));
            Assert.Equal("Event 3", dequeued.Message);
        }

        [Fact]
        public void CannotDequeueFromEmptyQueue()
        {
            var queue = new EventQueue<ServerEvent>();
            Assert.False(queue.TryDequeue(out var _));
        }
    }
}
