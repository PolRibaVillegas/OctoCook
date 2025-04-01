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

    private GameObject _cosa;

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
                    Debug.Log("_timer+=Time.deltaTime");
                    _timer = 0;
                    _state = FINISH;

                    TransmutacionCortar.OnCookCookedReturn += TransmuteIngridient;
                    OnCookCooked?.Invoke(_ingridient);
                    TransmutacionCortar.OnCookCookedReturn -= TransmuteIngridient;

                    _cosa.GetComponent<SpriteRenderer>().sprite = _ingridient.GetSprite();
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
        //_cosa.GetComponent<SpriteRenderer>().sprite = _ingridient.GetSprite(); 
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
        if (!_mouse)
            return;

        switch (_state)
        {
            case IDLE:
                _state = COOKING;
                _ingridient = _onion;
                _totalTimer = _ingridient.GetTime();
                Debug.Log("_cosa");
                _cosa = CreateNewSpriteRenderer(_ingridient.GetSprite()); //TODO


                break;
            case FINISH:
                _state = IDLE;
                //_ingridient = null;
                //TODO poner gameObject null
                break;
        }
    }
    private GameObject CreateNewSpriteRenderer(Sprite newSprite)
    {
        GameObject _ingridient = new GameObject("ExtraSprite");
        _ingridient.transform.position = transform.position; // Opcional: Ajustar posición al objeto actual
        //_ingridient.transform.parent = transform; // Opcional: Hacerlo hijo de este objeto

        SpriteRenderer newSpriteRenderer = _ingridient.AddComponent<SpriteRenderer>();
        newSpriteRenderer.sprite = newSprite;
        newSpriteRenderer.sortingOrder = 1; // Asegura que se dibuje sobre el objeto base

        return _ingridient;
    }
}
