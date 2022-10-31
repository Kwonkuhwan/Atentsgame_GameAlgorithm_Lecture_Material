using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Test : MonoBehaviour, IBattle
{
   Text text;

    public float AttackPower => throw new System.NotImplementedException();

    public float DefencePower => throw new System.NotImplementedException();

    public void Attack(IBattle target)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        text.text = $"Hello";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
