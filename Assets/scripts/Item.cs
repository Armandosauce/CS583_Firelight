using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    private string type;

    public Item(string t)
    {
        Id = t;
    }

    public string Id
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public override string ToString()
    {
        return this.gameObject.name + ", Type: " + type;
    }




}
