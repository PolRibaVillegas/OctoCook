using UnityEngine;

public class Ingridient : MonoBehaviour
{
    [SerializeField]
    private IngredienteBase _ingridient;

    public IngredienteBase GetIngridient()
    {
        return _ingridient;
    }

    public void SetIngridient(IngredienteBase Ingridient)
    {
        _ingridient = Ingridient;
    }
}
