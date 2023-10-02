using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Figure : ScriptableObject
{
    public static readonly int MaxHeight = 2;
    public static readonly int MaxWidth = 4;

    [SerializeField] private Color _color;
    [SerializeField] private bool[] _cell = new bool[MaxHeight * MaxWidth];

    public bool[] Cell => _cell;
    public Color Color => _color;
}
