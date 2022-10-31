using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GlobalVariables
{
    /*private static Dictionary<string, GlobalVariable<int>> intVariables;
    public class GlobalVariable<T>
    {
        private T value = default;
        public UnityEvent<T> OnChange { get; private set; }

        public GlobalVariable()
        {
            OnChange = new();
            value = default;
        }

        public T Value
        {
            get => value; set
            {
                this.value = value;
                if(typeof(T) == typeof(int))
                {
                    PlayerPrefs.SetInt();
                }
                OnChange.Invoke(value);
            }
        }


    }

    public static GlobalVariable<int> GetInt(string tag)
    {
        if (!intVariables.ContainsKey(tag))
            intVariables.Add(tag, new GlobalVariable<int>());
        return intVariables[tag];
    }*/

}
