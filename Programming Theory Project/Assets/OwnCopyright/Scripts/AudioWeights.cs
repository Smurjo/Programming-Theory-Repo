using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioWeights", menuName = "ScriptableObjects/AudioWeights")]

public class AudioWeights : ScriptableObject
{
   public float[] clipVolumes;
}
