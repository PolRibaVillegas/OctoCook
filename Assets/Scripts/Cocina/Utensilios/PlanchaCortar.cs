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

    //INFO este es el gameObject que se corta en la tabla de cortar, es un gameObject hijo de la tabla y se rellena cuando el jugador pone un ingrediente en la tabla y se bacia cuando se ha cocinado y el jugador lo saca de la tabla
    private GameObject _ingridient;

    private bool _mouse = false; //TODO repasar

    private BoxCollider2D _box;
    private SpriteRenderer _sp;

    void Start()
    {
        _state = IDLE;
        _timer = 0;

        _box = GetComponent<BoxCollider2D>();
        _sp = GetComponent<SpriteRenderer>();

        _ingridient = this.transform.Find("ingrediente").gameObject;
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

                    _ingridient.GetComponent<Ingrediente>().ChangeIngrediente(DiccionarioComidaCortada(_ingridient.GetComponent<Ingrediente>().GetIngrediente()));
                    _ingridient.GetComponent<SpriteRenderer>().sprite = _ingridient.GetComponent<Ingrediente>().GetIngrediente().GetSprite();
                }
                break;
            case FINISH:
                //doing finish animation
                break;
            default:
                Debug.Log("wtf is this cut table state?");
                break;
        }
    }

    private void OnMouseEnter()
    {
        _mouse = true;
        _sp.color = Color.gray;
    }

    private void OnMouseExit()
    {
        _mouse = false;
        _sp.color = Color.white;
    }

    private void OnClick()
    {
        switch (_state)
        {
            case IDLE:
                _state = COOKING;
                //_totalTimer = _ingridient.GetTime();
                //TODO mostrar el sprite del ingrediente actual
                break;
            case FINISH:
                _state = IDLE;
                //TODO dar el objeto al jugador
                break;
        }
    }

    //TODO método en el que el jugador arrastra un ingrediente y lo suelta justo dentro de la tabla
    private void StartCooking(IngredienteBase ingridient)
    {
        if (_state != IDLE)
            return;
        
        _ingridient.GetComponent<Ingrediente>().ChangeIngrediente(ingridient);
        _ingridient.GetComponent<SpriteRenderer>().sprite = _ingridient.GetComponent<Ingrediente>().GetIngrediente().GetSprite();
        _totalTimer = _ingridient.GetComponent<Ingrediente>().GetIngrediente().GetTime();
    }

    //TODO todos los posibles resultados después de añadir un ingrediente

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
