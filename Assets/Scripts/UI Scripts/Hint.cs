using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint
{
    [Tooltip("True = Player has already seen this hint, False = Player hasn't seen this hint yet. (Bool)")]
    internal bool playerHasAlreadySeen;
    /// <summary>
    /// List of possible hint types.
    /// </summary>
    internal enum HintType
    {
        Move,
        Shoot,
        Jump,
        Lock,
        Aim
    }
    [Tooltip("The type of this hint. (Readonly)")]
    internal readonly HintType hintType;
    [Tooltip("The text to display for this hint. (Readonly)")]
    internal readonly string hintText;
    [Tooltip("The amount of time to wait before showing this hint. (Readonly")]
    internal readonly float hintDelay;
    /// <summary>
    /// Construct a new Hint with the specified parameters, and set playerHasAlreadySeen to false.
    /// </summary>
    /// <param name="_hintType">The type of hint to be created.</param>
    /// <param name="_hintText">The text to be shown for this hint. (String)</param>
    /// <param name="_hintDelay">The amount of time to wait before showing this hint. (Float)</param>
    internal Hint(HintType _hintType, string _hintText, float _hintDelay)
    {
        hintType = _hintType;
        hintText = _hintText;
        hintDelay = _hintDelay;
        playerHasAlreadySeen = false;
    }
}
