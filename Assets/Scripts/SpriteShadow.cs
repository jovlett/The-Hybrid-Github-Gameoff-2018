using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour {

    public Vector2 offset = new Vector2(-0.2f, -0.2f);

    private SpriteRenderer srCaster;
    private SpriteRenderer srShadow;

    private Transform transCaster;
    private Transform transShadow;

    public Material shadowMaterial;
    public Color shadowColor = new Color (0.19f, 0.19f, 0.19f, 1);

    void Start () {
        transCaster = transform;
        transShadow = new GameObject().transform;
        transShadow.parent = transCaster;
        transShadow.gameObject.name = transCaster.name + ": Shadow";
        transShadow.localRotation = Quaternion.identity;
        transShadow.localScale = Vector3.one;
        
        srCaster = GetComponent<SpriteRenderer>();
        srShadow = transShadow.gameObject.AddComponent<SpriteRenderer>();

        srShadow.material = shadowMaterial;
        srShadow.color = shadowColor;
        srShadow.sortingLayerName = srCaster.sortingLayerName;
        srShadow.sortingOrder = srCaster.sortingOrder - 100;
    }

	void LateUpdate () {
        transShadow.position = new Vector2(transCaster.position.x + offset.x, transCaster.position.y + offset.y);
        srShadow.sprite = srCaster.sprite;

    }
}
