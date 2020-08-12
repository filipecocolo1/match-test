using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class transformExtensions 
{
    public static IEnumerator Move(this Transform t, Vector3 target, float duration) {
        Vector3 diffVector = (target - t.position);
        float diffTamanho = diffVector.magnitude;
        diffVector.Normalize();
        float contador=0;
        while (contador < duration) {
            float movAmount = (Time.deltaTime * diffTamanho / duration);
            t.position += diffVector * movAmount;
            contador += Time.deltaTime;
            yield return null;
        
        }
        t.position = target;        
    }
}
