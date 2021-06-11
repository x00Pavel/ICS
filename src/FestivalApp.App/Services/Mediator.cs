﻿using System;
using System.Collections.Generic;
using System.Linq;
using FestivalApp.App.Messages;

namespace FestivalApp.App.Services
{
    public class Mediator : IMediator
    {
        private readonly Dictionary<Type, List<Delegate>> _registeredActions = new();

        public void Register<TMessage>(Action<TMessage> action) where TMessage : IMessage
        {
            var key = typeof(TMessage);
            if (!_registeredActions.TryGetValue(key, out _))
            {
                _registeredActions[key] = new List<Delegate>();
            }
            _registeredActions[key].Add(action);
        }

        public void Send<TMessage>(TMessage message) where TMessage : IMessage
        {
            if (!_registeredActions.TryGetValue(message.GetType(), out var actions)) return;

            foreach (var action in actions.Where(action => action != null))
            {
                action.DynamicInvoke(message);
            }
        }

        public void UnRegister<TMessage>(Action<TMessage> action) where TMessage : IMessage
        {
            throw new NotImplementedException();
        }
    }
}
