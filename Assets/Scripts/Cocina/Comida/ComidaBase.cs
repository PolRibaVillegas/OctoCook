using UnityEngine;

[CreateAssetMenu(fileName = "ComidaBase", menuName = "Scriptable Objects/ComidaBase")]
public class ComidaBase : ScriptableObject
{
    private float _time;

    public float GetTime()
    {
        return this._time;
    }
}

