

namespace Konvolucio.MCAN120803.GUI
{

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public interface IEventAggregator
    {
        event EventHandler SubscribeChanged;
        event EventHandler PublishEvent;

        void Publish<T>(T message) where T : IApplicationEvent;
        void Subscribe<T>(Action<T> action) where T : IApplicationEvent;
        void Unsubscribe<T>(Action<T> action) where T : IApplicationEvent;
        void Dispose();
    }

    public interface IApplicationEvent
    {

    }


    class EventAggregator : IEventAggregator
    {
        private static readonly IEventAggregator instance = new EventAggregator();
        public static IEventAggregator Instance { get { return instance; } }
        public event EventHandler SubscribeChanged;
        public event EventHandler PublishEvent;

        private readonly ConcurrentDictionary<Type, List<object>> subscriptions = new ConcurrentDictionary<Type, List<object>>();
        

        public void Publish<T>(T message) where T : IApplicationEvent
        {
            List<object> subscribers;
            OnPublishEvent(message);
            if (subscriptions.TryGetValue(typeof(T), out subscribers))
            {
                foreach (var subscriber in subscribers.ToArray())
                {
                    ((Action<T>)subscriber)(message);
                }
            }
        }

        public void Subscribe<T>(Action<T> action) where T : IApplicationEvent
        {
            OnSubscirbeChanged(action);
            var subscribers = subscriptions.GetOrAdd(typeof(T), t => new List<object>());
            lock (subscribers)
            {
                subscribers.Add(action);
            }
        }

        public void Unsubscribe<T>(Action<T> action) where T : IApplicationEvent
        {
            List<object> subscribers;
            if (subscriptions.TryGetValue(typeof(T), out subscribers))
            {
                lock (subscribers)
                {
                    OnSubscirbeChanged(action);
                    subscribers.Remove(action);
                }
            }
        }

        public void Dispose()
        {
            subscriptions.Clear();
        }

        protected void OnSubscirbeChanged(object sender)
        {
            if (SubscribeChanged != null)
                SubscribeChanged(sender, EventArgs.Empty);
        }

        protected void OnPublishEvent(object sender)
        {
            if (PublishEvent != null)
                PublishEvent(sender, EventArgs.Empty);
        }
    }
}
