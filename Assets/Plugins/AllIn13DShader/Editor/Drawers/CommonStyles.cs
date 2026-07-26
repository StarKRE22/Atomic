using UnityEditor;
using UnityEngine;

namespace AllIn13DShader
{
	public class CommonStyles
	{
		public const int BUTTON_WIDTH = 600;
		public const int BIG_FONT_SIZE = 16;

		public GUIStyle style;
		public GUIStyle bigLabel;
		public GUIStyle warningLabel;
		public GUIStyle okLabel;
		public GUIStyle linkStyle;
		public GUIStyle boxStyle;

		public void InitStyles()
		{
			if(style == null)
			{
				style = new GUIStyle(EditorStyles.helpBox);
				style.margin = new RectOffset(0, 0, 0, 0);
			}

			if(bigLabel == null)
			{
				bigLabel = new GUIStyle(EditorStyles.boldLabel);
				bigLabel.fontSize = BIG_FONT_SIZE;
			}

			if(okLabel == null)
			{
				okLabel = new GUIStyle(EditorStyles.label);
				okLabel.fontSize = BIG_FONT_SIZE;
				okLabel.normal.textColor = Color.green;
				okLabel.wordWrap = true;
				okLabel.richText = true;
			}

			if(warningLabel == null)
			{
				warningLabel = new GUIStyle(EditorStyles.label);
				warningLabel.fontSize = BIG_FONT_SIZE;
				warningLabel.normal.textColor = Color.yellow;
				warningLabel.wordWrap = true;
				warningLabel.richText = true;
			}

			if(linkStyle == null)
			{
				linkStyle = new GUIStyle(EditorStyles.linkLabel);
			}

			if(boxStyle == null)
			{
				boxStyle = new GUIStyle(EditorStyles.helpBox);
				boxStyle.margin = new RectOffset(0, 0, 0, 0);
			}
		}
	}
}