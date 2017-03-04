using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T i;
    public static T I
    {
        get
        {
            if (i == null)
            {
                i = (T)FindObjectOfType(typeof(T));
                if (i == null)
                {
                    Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
                }
            }
            return i;
        }
    }
}