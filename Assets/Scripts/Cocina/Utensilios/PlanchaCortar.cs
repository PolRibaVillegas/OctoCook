using System.Runtime.CompilerServices;
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

    private SpriteRenderer _sprite; //TODO repasar, sprite renderer del ingrediente que se corta

    private bool _mouse = false; //TODO repasar
    private BoxCollider2D _box;

    public delegate void OnCookCookedDelegate(IngredienteBase ingrediente);
    public static event OnCookCookedDelegate OnCookCooked;

    void Start()
    {
        _box = GetComponent<BoxCollider2D>();
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
                    _state = FINISH;
                }
                break;
            case FINISH:
                TransmutacionCortar.OnCookCookedReturn += TransmuteIngridient;
                OnCookCooked?.Invoke(_ingridient);
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
    }

    private void OnMouseExit()
    {
        _mouse = false;
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
                _ingridient = null;
                _sprite = null;
                break;
        }
    }
    private void CreateNewSpriteRenderer(Sprite newSprite)
    {
        GameObject _ingridient = new GameObject("ExtraSprite");
        _ingridient.transform.position = transform.position; // Opcional: Ajustar posici�n al objeto actual
        _ingridient.transform.parent = transform; // Opcional: Hacerlo hijo de este objeto

        SpriteRenderer newSpriteRenderer = _ingridient.AddComponent<SpriteRenderer>();
        newSpriteRenderer.sprite = newSprite;
        newSpriteRenderer.sortingOrder = 1; // Asegura que se dibuje sobre el objeto base
    }
}
