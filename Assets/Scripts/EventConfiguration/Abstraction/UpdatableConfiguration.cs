using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EventConfiguration.Abstraction
{
    public abstract class UpdatableConfiguration<TK, TV> : ScriptableObject
        where TK : Enum
    {
        [SerializeField] private List<SerializedPair<TK, TV>> _content;

        protected IReadOnlyList<SerializedPair<TK, TV>> Content => _content;

        private void OnValidate()
        {
            UpdateContent(_content);
            OnEndValidate();
        }

        protected virtual void OnEndValidate()
        {
        }

        protected void UpdateContent<T1, T2>(List<SerializedPair<T1, T2>> content)
        {
            foreach (SerializedPair<T1, T2> pair in Create<T1, T2>())
            {
                if (content.Exists(item => Equals(item.Key, pair.Key)) == true)
                    continue;

                content.Add(pair);
            }
        }

        private List<SerializedPair<T1, T2>> Create<T1, T2>()
        {
            List<SerializedPair<T1, T2>> result = new();
            T2 value = default;

            foreach (T1 type in Enum.GetValues(typeof(T1)))
                result.Add(new SerializedPair<T1, T2>(type, value));

            return result;
        }
    }
}