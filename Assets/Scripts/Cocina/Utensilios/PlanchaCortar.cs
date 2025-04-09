using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlanchaCortar : MonoBehaviour
{
    private int _state;
    private const int IDLE = 1;
    private const int COOKING = 2;
    private const int FINISH = 3;

    private float _timer;
    private float _totalTimer;

    [SerializeField]
    private IngredienteBase _onion; //TODO QUITAR --> esto, solo para testing inicial
    private IngredienteBase _ingridient;

    private bool _mouse = false; //TODO repasar
    
    private BoxCollider2D _box;
    private SpriteRenderer _sp;

    public delegate void OnCookCookedDelegate(IngredienteBase ingrediente);
    public static event OnCookCookedDelegate OnCookCooked;

    void Start()
    {
        _state = IDLE;
        _timer = 0;
        
        _ingridient.AddComponent<Transform>();

        _box = GetComponent<BoxCollider2D>();
        _sp = GetComponent<SpriteRenderer>();
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
                    _state = FINISH;

                    TransmutacionCortar.OnCookCookedReturn += TransmuteIngridient;
                    OnCookCooked?.Invoke(_ingridient);
                    TransmutacionCortar.OnCookCookedReturn -= TransmuteIngridient;
                }
                break;
            case FINISH:
                
                break;
            default:

                break;
        }
    }

    private void TransmuteIngridient(IngredienteBase ingridient)
    {
        _ingridient = ingridient;
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
        //TODO QUITAR
        Debug.Log("Click");

        if (!_mouse)
            return;

        Debug.Log("Inside");

        switch (_state)
        {
            case IDLE:
                _state = COOKING;
                _ingridient = _onion;
                _totalTimer = _ingridient.GetTime();

                CreateNewSpriteRenderer(_ingridient.GetSprite()); //TODO

                break;
            case FINISH:
                _state = IDLE;
                Destroy(_ingridient);
                break;
        }
    }
    private void CreateNewSpriteRenderer(Sprite newSprite)
    {
        _ingridient.GetComponent<Transform>().position = transform.position; // Opcional: Ajustar posición al objeto actual
        _ingridient.GetComponent<Transform>().position.parent = transform; // Opcional: Hacerlo hijo de este objeto

        SpriteRenderer newSpriteRenderer = _ingridient.AddComponent<SpriteRenderer>();
        newSpriteRenderer.sprite = newSprite;
        newSpriteRenderer.sortingOrder = 1; // Asegura que se dibuje sobre el objeto base
    }
}
