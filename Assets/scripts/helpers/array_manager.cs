using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class array_manager : MonoBehaviour {

    //------------the custom picked value 128 is used as a key, that indicates NULL, helps the logic between methods----------//
    const byte customByteNull = 128;


    //remove obj from array
    public static void removeObjFromArray(GameObject obj, GameObject[] arr)
    {
        byte objPos = FindObjPosInArray(obj, arr);

        if (objPos != customByteNull)
        {
            arr[FindObjPosInArray(obj, arr)] = null;
        }
    }

    //add obj in array
    public static void addObjInArray(GameObject obj, GameObject[] arr)
    {

        if (!IsArrayFull(arr) && arr!= null && obj!=null)
        {
            arr[EmptyPosInArray(arr)] = obj;
        }
    }

    //is array full?
    public static bool IsArrayFull(GameObject[] arr)
    {
        if (arr != null)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == null)
                {
                    return false;
                }
            }
            return true;

        }
        else
        {
            return false;
            //throw new MissingReferenceException("The passed array is null!");
        }
    }

    //is array empty?
    public static bool IsArrayEmpty(GameObject[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != null)
            {
                return false;
            }
        }
        return true;
    }

    //find and return empty position in array
    public static byte EmptyPosInArray(GameObject[] arr)
    {

        if (arr != null)
        {

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == null)
            {
                return (byte)i;
            }
        }
        }
        return 0;
        //throw new KeyNotFoundException("no such an object position in array");
    }

    //find and return object's position in array
    public static byte FindObjPosInArray(GameObject obj, GameObject[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != null && arr[i].Equals(obj))
            {
                return (byte)i;    
            }
        }
        //throw new KeyNotFoundException("no such an object position in array");
        return customByteNull;
    }

    //print in one line array
    public static void PrintArray(GameObject[] arr)
    {
        string oneLineArray = "";
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != null)
            {
                oneLineArray += arr[i].name;

            }
            else
            {
                oneLineArray += "0";
            }
        }
        print(oneLineArray);
    }

    //is array with one element inside
    public static bool IsArrayWithOneObj(GameObject[] arr)
    {
        byte count = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != null)
            {
                count++;
                if (count > 1)
                {
                    return false;
                }
            }
        }
        return true;
    }

    //return number of elements in array
    public static int objectsCountInArray(GameObject[] arr)
    {
        int count = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != null)
            {
                count++;
            }
        }
        return count;
    }

}

