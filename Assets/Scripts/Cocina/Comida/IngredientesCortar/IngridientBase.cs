using UnityEngine;

[CreateAssetMenu(fileName = "IngredienteBase", menuName = "Scriptable Objects/IngredienteBase")]
public class IngridientBase : ScriptableObject
{
    [SerializeField]
    private Sprite _sprite;

    public Sprite GetSprite()
    {
        return this._sprite;
    }

    [SerializeField]
    private float _time;

    public float GetTime()
    {
        return this._time;
    }

    [SerializeField]
    private int _method; 

    public static int _TABLE = 1;
    public static int _PAN = 2;
    public static int _CASSEROLE = 3;
    public static int _FRY = 4;

    public int GetMethod()
    {
        return this._method;
    }

    [SerializeField]
    private int _save;

    public static int _FRIDGE = 1;
    public static int _FREEZER = 2;

    public int GetSave()
    {
        return this._save;
    }
}
