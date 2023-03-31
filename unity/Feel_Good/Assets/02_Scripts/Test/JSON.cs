using UnityEngine;
using System.Collections;
using System.IO;

public static class JsonHelper
{
    // 제네릭 타입의 리스트를 제이슨 형식의 문자열로 변환하는 함수
    public static string ToJson<T>(this T obj)
    {
        return JsonUtility.ToJson(obj);
    }

    // 제이슨 형식의 문자열을 제네릭 타입의 리스트로 변환하는 함수
    public static T FromJson<T>(this string json)
    {
        return JsonUtility.FromJson<T>(json);
    }

    // 제이슨 파일을 읽어와서 제네릭 타입의 리스트로 변환하는 함수
    public static T[] FromJsonFile<T>(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        return FromJson<T[]>(jsonString);
    }

    // 제네릭 타입의 리스트를 제이슨 파일로 저장하는 함수
    public static void ToJsonFile<T>(this T[] array, string filePath)
    {
        string jsonString = ToJson(array);
        File.WriteAllText(filePath, jsonString);
    }
}