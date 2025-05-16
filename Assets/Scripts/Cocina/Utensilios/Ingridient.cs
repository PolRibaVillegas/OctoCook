using UnityEngine;

public class Ingridient : MonoBehaviour
{
    [SerializeField]
    private IngridientBase _ingridient;

    public IngridientBase GetIngridient()
    {
        return _ingridient;
    }

    public void SetIngridient(IngridientBase Ingridient)
    {
        _ingridient = Ingridient;
    }

    public void DeleteIngridient()
    {
        _ingridient = null;

        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<SpriteRenderer>().sprite = null;
    }
}
