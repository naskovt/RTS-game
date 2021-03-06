﻿using UnityEngine;
using UnityEditor;
using System;

public static class extension_methods
{
    public static void DoForEach<T>(this T[] a, Action<T> b, int count)
    {
        for (int i = 0; i < count; i++)
        {
            b(a[i]);
        }
    }

    public static void DoForEach<T>(this T[] a, Action<T> b)
    {
        DoForEach(a, b, a.Length);
    }

    //public static string GetNamesFromObjArray(GameObject[] array, Func<string, string> b ) 
    //{
    //    string str = "";

    //    for (int i = 0; i < array.Length; i++)
    //    {
    //        str += b(array[i].name);
    //    }

    //    return str;
    //}
}