using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class UtensilioBase : MonoBehaviour
{//TODO cambiar a clase de base a olla/sarten/freidora/ns que más puede ser


    private int _state;
    private const int IDLE = 1;
    private const int COOKING = 2;
    private const int OVERCOOKING = 3;

    private float _timer;
    private float _totalTimer;
    private float _overCookTimer;

    //TODO añadir clase base de comida para que el esta clase pueda cocinarlo y transformarlo en otro.
    //private "NOMBRE_CLASE_COMIDA"
    //TODO, para transformarlo tengo que enviar una señal a una clase que me retornará el prefab correcto que quiero
    public delegate void OnCookCookedDelegate(/* TODO poner la clase quie toca de comida base */);
    public static event OnCookCookedDelegate OnCookCooked;

    void Start()
    {
        _state = IDLE;
        _timer = 0;
    }

    void Update()
    {
        switch (_state)
        {
            case IDLE:

                break;
            case COOKING:
                _timer += Time.deltaTime;

                if (_timer >= _totalTimer)
                {
                    _timer = 0;
                }
                break;
            case OVERCOOKING:
                _timer += Time.deltaTime;

                if (_timer > _overCookTimer)
                {

                }
                break;
            default:

                break;
        }
    }
}
