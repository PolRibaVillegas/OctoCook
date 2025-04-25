using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlanchaCortar : MonoBehaviour
{
    private int _state;
    private const int IDLE = 1;
    private const int COOKING = 2;
    private const int FINISH = 3;

    private float _timer;
    private float _totalTimer;

    private GameObject _ingridient;

    private bool _mouse = false;

    private BoxCollider2D _box;
    private SpriteRenderer _sp;

    void Start()
    {
        _state = IDLE;
        _timer = 0;

        _box = GetComponent<BoxCollider2D>();
        _sp = GetComponent<SpriteRenderer>();

        _ingridient = this.transform.Find("ingridient").gameObject;
    }

    void Update()
    {
        switch (_state)
        {
            case IDLE:
                //doing idle animation
                break;
            case COOKING:
                //doing cooking animation
                _timer += Time.deltaTime;

                if (_timer >= _totalTimer)
                {
                    _timer = 0;
                    _state = FINISH;

                    _ingridient.GetComponent<Ingridient>().SetIngridient(DiccionarioComidaCortada(_ingridient.GetComponent<Ingridient>().GetIngridient()));
                    _ingridient.GetComponent<SpriteRenderer>().sprite = _ingridient.GetComponent<Ingridient>().GetIngridient().GetSprite();

                    if (_mouse)
                        _ingridient.GetComponent<SpriteRenderer>().color = Color.gray;
                }
                break;
            case FINISH:
                //doing finish animation
                break;
        }
    }

    private void OnMouseEnter()
    {
        _mouse = true;
        if (_state == IDLE)
            _sp.color = Color.gray;
        else if (_state == FINISH)
            _ingridient.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private void OnMouseExit()
    {
        _mouse = false;
        if (_state == IDLE)
        {
            _sp.color = Color.white;
        }
        else if (_state == FINISH)
            _ingridient.GetComponent<SpriteRenderer>().color = Color.white;

    }

    private void OnClick()
    {
        if (!_mouse)
            return;

        switch (_state)
        {
            case IDLE:
                _state = COOKING;

                //introducir el ingrediente, en el testing es una cebollaaah
                _ingridient.GetComponent<Ingridient>().SetIngridient(_onion);
                //añadir la imagen del ingrediente
                _ingridient.GetComponent<SpriteRenderer>().sprite = _ingridient.GetComponent<Ingridient>().GetIngridient().GetSprite();

                _totalTimer = _ingridient.GetComponent<Ingridient>().GetIngridient().GetTime();

                _sp.color = Color.white;
                break;
            case FINISH:
                _state = IDLE;
                //TODO dar el objeto al jugador

                _ingridient.GetComponent<Ingridient>().SetIngridient(null);
                _ingridient.GetComponent<SpriteRenderer>().sprite = null;
                _ingridient.GetComponent<SpriteRenderer>().color = Color.white;
                break;
        }
    }

    //TODO método en el que el jugador arrastra un ingrediente y lo suelta justo dentro de la tabla
    private void StartCooking(IngredienteBase ingridient)
    {
        if (_state != IDLE)
            return;

        _state = COOKING;

        _ingridient.GetComponent<Ingridient>().SetIngridient(ingridient);
        _ingridient.GetComponent<SpriteRenderer>().sprite = _ingridient.GetComponent<Ingridient>().GetIngridient().GetSprite();
        _totalTimer = _ingridient.GetComponent<Ingridient>().GetIngridient().GetTime();

        _sp.color = Color.white;
    }

    //TODO todos los posibles resultados después de añadir un ingrediente

    [SerializeField]
    private IngredienteBase _onion;

    [SerializeField]
    private IngredienteBase _cutedOnion;

    private IngredienteBase DiccionarioComidaCortada(IngredienteBase ingridient)
    {
        switch (ingridient.name)
        {
            case "Onion":
                return _cutedOnion;
            default:
                return null;
        }
    }
}
