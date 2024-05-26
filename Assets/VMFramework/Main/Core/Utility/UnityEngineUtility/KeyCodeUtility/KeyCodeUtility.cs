using System;
using UnityEngine;
using VMFramework.Core;

public static class KeyCodeUtility
{
    public static KeyCode ConvertToKeyCode(this int number, bool isKeypad)
    {
        number.AssertIsAboveOrEqual(0, nameof(number));
        number.AssertIsBelowOrEqual(9, nameof(number));

        if (isKeypad)
        {
            return number switch
            {
                0 => KeyCode.Keypad0,
                1 => KeyCode.Keypad1,
                2 => KeyCode.Keypad2,
                3 => KeyCode.Keypad3,
                4 => KeyCode.Keypad4,
                5 => KeyCode.Keypad5,
                6 => KeyCode.Keypad6,
                7 => KeyCode.Keypad7,
                8 => KeyCode.Keypad8,
                9 => KeyCode.Keypad9,
                _ => KeyCode.None
            };
        }

        return number switch
        {
            0 => KeyCode.Alpha0,
            1 => KeyCode.Alpha1,
            2 => KeyCode.Alpha2,
            3 => KeyCode.Alpha3,
            4 => KeyCode.Alpha4,
            5 => KeyCode.Alpha5,
            6 => KeyCode.Alpha6,
            7 => KeyCode.Alpha7,
            8 => KeyCode.Alpha8,
            9 => KeyCode.Alpha9,
            _ => KeyCode.None
        };
    }

    public static string ConvertToString(this KeyCode keyCode, KeyCodeToStringMode mode)
    {
        switch (mode)
        {
            case KeyCodeToStringMode.Full:
                return keyCode switch
                {
                    KeyCode.Exclaim => "!",
                    KeyCode.DoubleQuote => "\"",
                    KeyCode.Hash => "#",
                    KeyCode.Dollar => "$",
                    KeyCode.Ampersand => "&",
                    KeyCode.Quote => "'",
                    KeyCode.LeftParen => "(",
                    KeyCode.RightParen => ")",
                    KeyCode.Asterisk => "*",
                    KeyCode.Plus => "+",
                    KeyCode.Comma => ",",
                    KeyCode.Minus => "-",
                    KeyCode.Period => ".",
                    KeyCode.Slash => "/",
                    KeyCode.Colon => ":",
                    KeyCode.Semicolon => ";",
                    KeyCode.Less => "<",
                    KeyCode.Equals => "=",
                    KeyCode.Greater => ">",
                    KeyCode.Question => "?",
                    KeyCode.At => "@",
                    KeyCode.LeftBracket => "[",
                    KeyCode.Backslash => "\\",
                    KeyCode.RightBracket => "]",
                    KeyCode.Caret => "^",
                    KeyCode.Underscore => "_",
                    KeyCode.BackQuote => "`",
                    KeyCode.UpArrow => "↑",
                    KeyCode.DownArrow => "↓",
                    KeyCode.RightArrow => "→",
                    KeyCode.LeftArrow => "←",
                    _ => keyCode.ToString()
                };
            case KeyCodeToStringMode.Short:
                return keyCode switch
                {
                    KeyCode.Exclaim => "!",
                    KeyCode.DoubleQuote => "\"",
                    KeyCode.Hash => "#",
                    KeyCode.Dollar => "$",
                    KeyCode.Ampersand => "&",
                    KeyCode.Quote => "'",
                    KeyCode.LeftParen => "(",
                    KeyCode.RightParen => ")",
                    KeyCode.Asterisk => "*",
                    KeyCode.Plus => "+",
                    KeyCode.Comma => ",",
                    KeyCode.Minus => "-",
                    KeyCode.Period => ".",
                    KeyCode.Slash => "/",
                    KeyCode.Alpha0 => "0",
                    KeyCode.Alpha1 => "1",
                    KeyCode.Alpha2 => "2",
                    KeyCode.Alpha3 => "3",
                    KeyCode.Alpha4 => "4",
                    KeyCode.Alpha5 => "5",
                    KeyCode.Alpha6 => "6",
                    KeyCode.Alpha7 => "7",
                    KeyCode.Alpha8 => "8",
                    KeyCode.Alpha9 => "9",
                    KeyCode.Keypad0 => "0",
                    KeyCode.Keypad1 => "1",
                    KeyCode.Keypad2 => "2",
                    KeyCode.Keypad3 => "3",
                    KeyCode.Keypad4 => "4",
                    KeyCode.Keypad5 => "5",
                    KeyCode.Keypad6 => "6",
                    KeyCode.Keypad7 => "7",
                    KeyCode.Keypad8 => "8",
                    KeyCode.Keypad9 => "9",
                    KeyCode.KeypadPeriod => ".",
                    KeyCode.KeypadDivide => "/",
                    KeyCode.KeypadMultiply => "*",
                    KeyCode.KeypadMinus => "-",
                    KeyCode.KeypadPlus => "+",
                    KeyCode.KeypadEnter => "Enter",
                    KeyCode.KeypadEquals => "=",
                    KeyCode.UpArrow => "↑",
                    KeyCode.DownArrow => "↓",
                    KeyCode.RightArrow => "→",
                    KeyCode.LeftArrow => "←",
                    KeyCode.Colon => ":",
                    KeyCode.Semicolon => ";",
                    KeyCode.Less => "<",
                    KeyCode.Equals => "=",
                    KeyCode.Greater => ">",
                    KeyCode.Question => "?",
                    KeyCode.At => "@",
                    KeyCode.LeftBracket => "[",
                    KeyCode.Backslash => "\\",
                    KeyCode.RightBracket => "]",
                    KeyCode.Caret => "^",
                    KeyCode.Underscore => "_",
                    KeyCode.BackQuote => "`",
                    _ => keyCode.ToString()
                };
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }
}
