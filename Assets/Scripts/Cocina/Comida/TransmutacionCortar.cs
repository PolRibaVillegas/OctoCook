using UnityEngine;

public class TransmutacionCortar : MonoBehaviour
{
    [SerializeField]
    private IngredienteBase _cutedOnion;

    public delegate void OnCookCookedReturnDelegate(IngredienteBase ingrediente);
    public static event OnCookCookedReturnDelegate OnCookCookedReturn;

    private void OnEnable()
    {
        PlanchaCortar.OnCookCooked += DiccionarioComidaCortada;
    }

    private void OnDisable()
    {
        PlanchaCortar.OnCookCooked -= DiccionarioComidaCortada;
    }

    private void DiccionarioComidaCortada(IngredienteBase ingridient)
    {
        Debug.Log(ingridient.name);
        switch (ingridient.name)
        {
            case "Onion":
                OnCookCookedReturn?.Invoke(_cutedOnion);
                break;
            default:
                break;
        }


    }

}
