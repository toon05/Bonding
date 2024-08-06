using System.Collections.Generic;
using UnityEngine;

public static class MessageSplitter
{
    public static List<string> SplitMessage(string message, int maxWords)
    {
        Debug.Log("SplitMessage");
        List<string> result = new List<string>();
        int start = 0;

        while (start < message.Length)
        {
            int end = FindSplitPoint(message, start, maxWords);
            result.Add(message.Substring(start, end - start).Trim());
            start = end;
        }

        return result;
    }

    private static int FindSplitPoint(string message, int start, int maxWords)
    {
        int end = start + maxWords;

        if (end >= message.Length)
        {
            return message.Length; // If the remaining characters are less than or equal to maxWords, return the end of the message
        }

        while (end > start && !IsPunctuation(message[end - 1]))
        {
            end--;
        }

        // If no punctuation found within maxWords, just return maxWords
        return end > start ? end : start + maxWords;
    }

    private static bool IsPunctuation(char c)
    {
        return c == '。' || c == '、' || c == '.' || c == ',' || c == '！' || c == '？' || c == '!' || c == '?' || char.IsPunctuation(c);
    }
}