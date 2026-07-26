using UnityEngine;

namespace AllIn13DShader
{
	public class PropertyTween
	{
		protected int propID;
		protected Material mat;
		protected float currentValue;

		[SerializeField] protected string propertyName;

		public virtual void Init(Material mat)
		{
			this.mat = mat;
			propID = Shader.PropertyToID(propertyName);
		}

		public virtual void Update(float deltaTime)
		{
			Tween(deltaTime);
			UpdateMaterial();
		}

		protected virtual void Tween(float deltaTime)
		{

		}

		private void UpdateMaterial()
		{
			mat.SetFloat(propID, currentValue);
		}
	}
}