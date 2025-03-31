using UnityEngine;

[CreateAssetMenu(fileName = "IngredienteBase", menuName = "Scriptable Objects/IngredienteBase")]
public class IngredienteBase : ScriptableObject
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
    private int _metodo; 

    public static int _CORTAR = 1;
    public static int _SARTEN = 2;
    public static int _CAZUELA = 3;
    public static int _FREIR = 4;

    public int GetMetodo()
    {
        return this._metodo;
    }

    [SerializeField]
    private int _guardar;

    public static int _NEVERA = 1;
    public static int _CONGELADOR = 2;

    public int GetGuardar()
    {
        return this._guardar;
    }
}
