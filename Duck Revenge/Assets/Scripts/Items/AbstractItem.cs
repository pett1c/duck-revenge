using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Rarity
{
    public string name;
    public Color color;
}

public abstract class AbstractItem
{
    private string description { get; }

    private Rarity rarity { get; }

    public abstract void Use();
}
