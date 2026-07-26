using UnityEngine;

namespace AllIn13DShader
{
	public class ConversionProperty : ScriptableObject
	{
		public ConversionPropertyType propertyType;
		public string propertyName;
		
		[Header("Effect")]
		public string belongingToEffect;
		public bool requiredProperty;

		[Header("Alternative Names")]
		public string[] alternativeNames;
	}
}