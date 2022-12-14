using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receipt : MonoBehaviour
{
    public void WriteProducts(List<Product> products)
    {
        TextWriter.Clean();
        //Writing receipt for the products that are purchased. Issue is to reach their child object dynamically to Invoke one specific method for each child class.
        //Better solution for readability or modularity has not yet to be developed.
        for (int i = 0; i < products.Count; i++)
        {
            if (products[i] is Book)
            {
                Book x = (Book)products[i];
                TextWriter.Write("Product Type: " + products[i].GetType().Name + "," + x.GetTypeOf() + "\nName: " + products[i].GetName() + "\nPrice: " + products[i].GetPrice());
            }
            else if (products[i] is Magazine)
            {
                Magazine x = (Magazine)products[i];
                TextWriter.Write("Product Type: " + products[i].GetType().Name + "," + x.GetTypeOf() + "\nName: " + products[i].GetName() + "\nPrice: " + products[i].GetPrice());
            }
            else if (products[i] is CD)
            {
                CD x = (CD)products[i];
                TextWriter.Write("Product Type: " + products[i].GetType().Name + "," + x.GetTypeOf() + "\nName: " + products[i].GetName() + "\nPrice: " + products[i].GetPrice());
            }
            else if (products[i] is Food)
            {
                Food x = (Food)products[i];
                TextWriter.Write("Product Type: " + products[i].GetType().Name + "," + x.GetTypeOf() + "\nName: " + products[i].GetName() + "\nPrice: " + products[i].GetPrice());
            }
        }

        TextWriter.Write("\nTotal: " + TotalCost(products));
    }
    float TotalCost(List<Product> products)
    {
        float cost = 0;
        foreach (Product product in products) cost += product.GetPrice();
        return cost;
    }
}
