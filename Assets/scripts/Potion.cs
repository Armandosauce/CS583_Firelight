using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item {

    private int amt;
    
	public Potion(int amt, string id) 
        : base(id)
    {
        this.amt = amt;
    }
    
}
