using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
[SerializeField]
class CreatureList : ScriptableObject
{
		public List<CreatureSetting> creatures = new List<CreatureSetting>();
}