using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Stat 
{
    [SerializeField] private int baseValue;

    public List<int> modifiers;
    public int GetValue()
    {
        int FinalValue = baseValue;

        foreach (int modifier in modifiers)
        {
            FinalValue += modifier;
        }

        return FinalValue;
    }

    public void SetDefaultValue(int _value)
    {
        baseValue = _value;
    }

    public void AddModifier(int _modifier)
    {
        modifiers.Add(_modifier);
    }

    public void RemoveModifier(int _modifier)
    {
        modifiers.RemoveAt(_modifier);
    }
}
