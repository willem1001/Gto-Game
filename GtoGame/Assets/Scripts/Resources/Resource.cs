using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Resource : MonoBehaviour
{

    [SerializeField] private float Quantity;
    [SerializeField] private float InitialQuantity;
    public UnityEvent OnChange = new UnityEvent();

    void Start()
    {
        Quantity = InitialQuantity;
    }

    public void Add(float amount)
    {
        Quantity += amount;
        UpdateUi();
    }

    public void Remove(float amount)
    {
        Quantity -= amount;
        UpdateUi();
    }

    void UpdateUi()
    {
        OnChange.Invoke();
    }

    public float GetQuantity()
    {
        return Quantity; 
    }

    public float GetInitialQuantity()
    {
        return InitialQuantity;
    }

    public bool CanAfford(float cost)
    {
        return Quantity >= cost;
    }
	

}
