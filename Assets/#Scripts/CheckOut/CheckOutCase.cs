using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Receipt))]
public class CheckOutCase : MonoBehaviour
{
    [Header("Text Reference")]
    [SerializeField]
    private TextMeshProUGUI costText;

    //References
    PlayerDistanceToAction distanceToAction;
    DropArea checkOutArea;
    DropArea playerArea;
    Receipt receipt;

    private bool _isProductsTransfered = false;
    private float methodExecutionTimer = 0.5f;
    private float executionTimerElapsed = 0;

    #region Unity Main
    void Start() => InitializeScript();
    void Update() => MainLogic();
    #endregion
    #region Initializers
    void InitializeScript()
    {
        InitializeReferences();
    }
    void InitializeReferences()
    {
        distanceToAction = FindObjectOfType<PlayerDistanceToAction>();
        playerArea = distanceToAction.GetComponent<DropArea>();
        receipt = GetComponent<Receipt>();
        checkOutArea = GetComponentInChildren<DropArea>();
    }
    #endregion
    #region Main Logic
    void MainLogic()
    {
        UpdateCost();
        TransferProducts();
    }
    void TransferProducts()
    {
        if (distanceToAction.canCheckOut())
        {
            if (!_isProductsTransfered && playerArea._products.Count > 0)
            {
                List<GameObject> copy = playerArea._products.ToList();

                for (int i = 0; i < copy.Count; i++)
                {
                    var product = copy[i];
                    product.transform.position = playerArea.ResetPos(copy[i].gameObject, checkOutArea.ListPos(copy[i]));
                }
                UpdateCost();
                _isProductsTransfered = true;
            }
        }
        else
        {
            _isProductsTransfered = false;
        }
    }
    #endregion
    #region Update Text Logic
    public void UpdateCost()
    {
        //Event system could be use to decouple this method.
        //Adding timer to not execute to execute everyframe.
        bool canExecute = (executionTimerElapsed += Time.deltaTime) >= methodExecutionTimer;
        if (canExecute)
        {
            costText.text = "Cost: " + CurrentCost();
            executionTimerElapsed = 0;
        }
    }
    float CurrentCost()
    {
        float cost = 0;
        foreach (Product product in ToProducts(checkOutArea._products)) cost += product.GetPrice();
        return cost;
    }
    #endregion
    #region Check Out Logic
    public void CheckOut()
    {
        var products = ToProducts(checkOutArea._products);
        receipt.WriteProducts(products);
    }
    List<Product> ToProducts(List<GameObject> products)
    {
        List<Product> x = new List<Product>();
        for (int i = 0; i < products.Count; i++)
        {
            x.Add(products[i].GetComponent<Product>());
        }
        return x;
    }
    #endregion
}
