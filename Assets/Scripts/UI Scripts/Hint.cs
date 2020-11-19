using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint
{
    internal bool playerHasAlreadySeen;
    internal enum HintType
    {
        Move,
        Shoot,
        Jump,
        Lock,
        Aim
    }
    internal readonly HintType hintType;
    internal readonly string hintText;
    internal readonly float hintDelay;

    internal Hint(HintType _hintType, string _hintText, float _hintDelay)
    {
        hintType = _hintType;
        hintText = _hintText;
        hintDelay = _hintDelay;
        playerHasAlreadySeen = false;
    }
}
