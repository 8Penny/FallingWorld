using System;
using System.Collections.Generic;

namespace Game.Components.Core
{
    public class FWEvent
    {
        public List<Action> _actions = new List<Action>();
        public void Invoke()
        {
            foreach (var action in _actions)
            {
                if (action == null)
                {
                    continue;
                }
                action();
            }
        }

        public void Clear()
        {
            _actions.Clear();
        }

        public void Add(Action ac)
        {
            _actions.Add(ac);
        }
    }
}