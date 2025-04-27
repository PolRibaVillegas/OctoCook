using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int _maxharms = 8;
    private int _harms;

    public delegate void OnPlaceIngridientPlateDelegate(IngridientBase _ingridient);
    public static event OnPlaceIngridientPlateDelegate OnPlaceIngridientPlate;

    public delegate void OnPlaceIngridientCutTableDelegate(IngridientBase _ingridient);
    public static event OnPlaceIngridientCutTableDelegate OnPlaceIngridientCutTable;

    void Start()
    {
        _harms = _maxharms;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    void Update()
    {
        
    }
}
