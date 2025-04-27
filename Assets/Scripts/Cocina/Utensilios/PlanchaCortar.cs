using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlanchaCortar : MonoBehaviour
{
    private int _state;
    private const int IDLE = 1;
    private const int COOKING = 2;
    private const int FINISH = 3;
    private const int MOVING = 4;

    private float _timer;
    private float _totalTimer;

    private GameObject _ingridient;

    private bool _mouse = false;

    private MovibleIngridient _movIngridient;

    private Rigidbody2D _rb;
    private BoxCollider2D _box;
    private SpriteRenderer _sp;

    void Start()
    {
        _state = IDLE;
        _timer = 0;
        
        _rb = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();
        _sp = GetComponent<SpriteRenderer>();

        _ingridient = this.transform.Find("Ingridient").gameObject;
        _movIngridient = null;
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
            case MOVING:
                //doing moving animation
                //Vector3 position = this.transform.position;
                Vector3 newPosition = Input.mousePosition;
                newPosition = Camera.main.ScreenToWorldPoint(newPosition);
                newPosition.z = this.transform.position.z;
                //newPosition = newPosition - position;
                this.transform.position = newPosition;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ingridient")
        {
            _movIngridient = collision.gameObject.GetComponent<MovibleIngridient>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ingridient")
        {
            _movIngridient = null;
        }
    }

    private void OnLClickPress()
    {
        if (!_mouse)
            return;

        if (_state == FINISH)
        {
            _state = IDLE;
            //TODO dar el objeto al jugador

            _ingridient.GetComponent<Ingridient>().SetIngridient(null);
            _ingridient.GetComponent<SpriteRenderer>().sprite = null;
            _ingridient.GetComponent<SpriteRenderer>().color = Color.white;

            _sp.color = Color.gray;
        }
    }

    private void OnLClickRelease()
    {
        if (_movIngridient == null)
            return;

        StartCooking(_movIngridient.getIngridient());

        Destroy(_movIngridient.gameObject);
    }

    private void OnRClickPress()
    {
        if (!_mouse||_state != IDLE)
            return;

        _state = MOVING;
        //TODOcambiar color o algo
    }

    private void OnRClickRelease()
    {
        if (_state != MOVING)
            return;

        _state = IDLE;
    }

    //TODO método en el que el jugador arrastra un ingrediente y lo suelta justo dentro de la tabla
    private void StartCooking(IngridientBase ingridient)
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

    //[SerializeField]
    //private IngridientBase _cuted01;

    //[SerializeField]
    //private IngridientBase _cuted02;

    //[SerializeField]
    //private IngridientBase _cuted03;

    [SerializeField]
    private IngridientBase _cutedOnion;

    private IngridientBase DiccionarioComidaCortada(IngridientBase ingridient)
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
