using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMover : MonoBehaviour
{
    public float _currentScrollSpeed = 0.5f;
    public Renderer mat;
    float offset;

    private void Start() {
    }

private void Update() {
    move();
}
    void move()
    {
        offset += (_currentScrollSpeed * Time.deltaTime);
       mat.material.mainTextureOffset = new Vector2(0, offset);
    }

}
