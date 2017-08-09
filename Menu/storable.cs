using UnityEngine;
using System.Collections;
using System; //This allows the IComparable Interface
using System.Collections.Generic;

//This is the class you will be storing
//in the different collections. In order to use
//a collection's Sort() method, this class needs to
//implement the IComparable interface.
public class storable : MonoBehaviour
{
    //quest items, tools, 
    public string itemname;
    private GameObject item;
    private int ItemID;
    private int Quantity=0;
    private string ItemGroup; //Usable, Utility, Ingredient,
    private string ItemDescription;
    private GameController Gmaster;
    public storable(int nID, string newName, string NewItemGroup,string NIdesc)
    {
        ItemID = nID;
        itemname = newName;
        ItemDescription = NIdesc;
        ItemGroup = NewItemGroup;


    }
    void Awake()
    {
        Gmaster = GameObject.Find("GameController").GetComponent<GameController>();
        storable temp;
        Debug.Log(itemname);
        Gmaster.GlobalitemDataBaseDictionary.TryGetValue(itemname, out temp);
        if(temp != null) {
            ItemID = temp.getItemID();
            ItemGroup = temp.getItemGroup();
            ItemDescription = temp.getItemDesc();
        }
       else
        {
            Debug.Log("THIS IS NOT A VALID ITEM NAME");
        }


    }

 
    //Use for checking if there is enough of an ingredient
    public bool isQGreater(storable other)
    {
        if (other == null)
        {
            Debug.Log("The pararmeter storable 'other' for 'CompareWeight' is set to NULL. You Suck.");
            return false;
        }

        if (Quantity < other.Quantity)
            return false;
        else
            return true;
    }
    public GameObject getItem()
    {
        return item;

    }
    //Use for Crafting
    public int QuantityDif(storable other)
    {
            return Quantity - other.Quantity;

    }
    //Use for Grouping in Menu
    public int getItemID()
    {
        return ItemID;

    }
    //Use for Grouping in Menu
    public void setItemID(int newitemid)
    {
        ItemID = newitemid;

    }
    public string getItemDesc()
    {
        return ItemDescription;

    }
    public string getItemGroup()
    {
        return ItemGroup;

    }
    public int getItemQuantity()
    {
        return Quantity;

    }
    //use for labeling etc
    public string getItemName()
    {
        return itemname;

    }
    public void setQuantity(int NewQuan)
    {

        Quantity = NewQuan;
    }

}