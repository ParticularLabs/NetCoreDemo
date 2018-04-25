namespace ITOps.Shared
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.MessageMutator;

    public class RemoveAssemblyInfoFromMessageMutator : IMutateIncomingTransportMessages
    {
        public Task MutateIncoming(MutateIncomingTransportMessageContext context)
        {
            var types = context.Headers[Headers.EnclosedMessageTypes];
            var enclosedTypes = types.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < enclosedTypes.Length; i++)
            {
                // Remove the assembly information for all of the enclosed types, because the message types are being
                // declared locally. This is to avoid having to include the dll that contains the message schema as a reference.
                var messageType = enclosedTypes[i].Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
                if (messageType.Length > 0)
                {
                    enclosedTypes[i] = messageType[0];
                }
            }

            context.Headers[Headers.EnclosedMessageTypes] = string.Join(";", enclosedTypes);
            return Task.CompletedTask;
        }
    }
}