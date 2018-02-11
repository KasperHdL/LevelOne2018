using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayer : MonoBehaviour {
	public Renderer[] renderer;

    public void SetMaterial(Material material){
        for(int i = 0 ;i < renderer.Length; i++)
            renderer[i].material = material;
    }
}
