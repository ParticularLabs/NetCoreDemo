namespace ITOps.ViewModelComposition
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class DynamicViewModel : DynamicObject
    {
        // Needed for registering parameterized callbacks from subscribers. 
        public delegate Task EventHandler<TEvent>(dynamic pageViewModel, TEvent @event, RouteData routeData,
            IQueryCollection query);

        // Keep a list of subcribers of the same route interested in getting called when events are raised.
        readonly IDictionary<Type, List<EventHandler<object>>> callbackRegistrations = new Dictionary<Type, List<EventHandler<object>>>();

        // To extend and keep track of properties as part of the dynamic object.
        readonly IDictionary<string, object> properties = new Dictionary<string, object>();

        readonly IQueryCollection query;

        // Keep track of the routeData and the query collection that's being passed in.
        readonly RouteData routeData;

        public DynamicViewModel(RouteData routeData, IQueryCollection query)
        {
            this.routeData = routeData;
            this.query = query;
        }

        public void RegisterCallback<TEvent>(EventHandler<TEvent> handler)
        {
            if (!callbackRegistrations.TryGetValue(typeof(TEvent), out var handlers))
            {
                handlers = new List<EventHandler<object>>();
                callbackRegistrations.Add(typeof(TEvent), handlers);
            }

            handlers.Add((pageViewModel, @event, routeData, query) =>
                handler(pageViewModel, (TEvent) @event, routeData, query));
        }

        public void ClearCallbackRegistrations() => callbackRegistrations.Clear();

        Task RaiseEventAsync(object @event)
        {
            if (callbackRegistrations.TryGetValue(@event.GetType(), out var handlers))
            {
                var tasks = new List<Task>();
                foreach (var handler in handlers)
                {
                    tasks.Add(handler.Invoke(this, @event, routeData, query));
                }

                return Task.WhenAll(tasks);
            }

            return Task.CompletedTask;
        }


        // Methods to extend the dynamic object, since we are customizing it cannot just extend the ExpandoObject as it's a sealed class.
        public override bool TryGetMember(GetMemberBinder binder, out object result) => properties.TryGetValue(binder.Name, out result);

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            properties[binder.Name] = value;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null;

            if (binder.Name == nameof(RaiseEventAsync))
            {
                result = RaiseEventAsync(args[0]);
                return true;
            }

            return false;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            foreach (var propertyName in properties.Keys)
            {
                yield return propertyName;
            }

            yield return nameof(RaiseEventAsync);
        }
    }
}