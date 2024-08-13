using UnityEngine;
using UnityEditor;

namespace ReadMe
{
	/// <summary>
	/// Editor to render <see cref="ReadMe"/> in the Inspector
	/// </summary>
	[CustomEditor(typeof(ReadMe))]
	[InitializeOnLoad]
	public class ReadMeEditor : UnityEditor.Editor
	{
		public string IconPath = "Assets/ReadMeEditor/Gizmos/Icon.png";

		private static float VSpaceBeforeAllSections = 5;
		private static float vSpaceAfterEachSection = 10f;
		private static float hSpace1 = 8;
		private static float hSpace2 = 16;
		private static float hSpace3 = 25;
		private static float LayoutMinWidht = 250;
		private static float LayoutMaxWidth = 500;

		[SerializeField] private GUIStyle TitleStyle;
		[SerializeField] private GUIStyle IconStyle;
		[SerializeField] private GUIStyle TextHeadingStyle;
		[SerializeField] private GUIStyle TextSubheadingStyle;
		[SerializeField] private GUIStyle TextBodyStyle;
		[SerializeField] private GUIStyle LinkTextStyle;

		private bool _isInitialized = false;
		
		static ReadMeEditor()
		{
			EditorApplication.delayCall += Load;
		}

		private static void Load()
		{
			var readme = SelectReadme();
			//SessionState.SetBool(kShowedReadmeSessionStateName, true);
		}
		
		static ReadMe SelectReadme() 
		{
			var ids = AssetDatabase.FindAssets("ReadMe t:ReadMe");
			if (ids.Length == 1)
			{
				var readmeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(ids[0]));
				
				Selection.objects = new UnityEngine.Object[]{readmeObject};
				
				return (ReadMe)readmeObject;
			}
			else
			{
				Debug.LogError("Couldn't find a ReadMe file");
				return null;
			}
		}
		
		private void Initialize()
		{
			if (_isInitialized)
			{
				return;
			}
			
			_isInitialized = true;

			//Declare Styles
			TitleStyle = new GUIStyle(EditorStyles.label);
			TitleStyle.stretchHeight = true;
			TitleStyle.wordWrap = true;
			TitleStyle.fontSize = 20;
			TitleStyle.margin.left = 10;
			TitleStyle.alignment = TextAnchor.MiddleLeft;
			
			//Icon
			IconStyle = new GUIStyle(EditorStyles.iconButton);
			IconStyle.normal.background = null;
			IconStyle.hover.background = null;
			IconStyle.active.background = null;
			IconStyle.margin = new RectOffset(5, 5, 5, 5);
			IconStyle.alignment = TextAnchor.MiddleCenter;

			//TextHeading
			TextHeadingStyle = new GUIStyle(TitleStyle);
			TextHeadingStyle.wordWrap = true;
			TextHeadingStyle.fontSize = 20;
			
			//TextSubheadingStyle
			TextSubheadingStyle = new GUIStyle(TextHeadingStyle);
			TextSubheadingStyle.wordWrap = true;
			TextSubheadingStyle.fontStyle = FontStyle.Bold;
			TextSubheadingStyle.fontSize = 18;
			
			//TextBodyStyle - Supports richText (https://docs.unity3d.com/2021.3/Documentation/Manual/StyledText.html)
			TextBodyStyle = new GUIStyle(TextHeadingStyle);
			TextBodyStyle.wordWrap = true;
			TextBodyStyle.richText = true;
			TextBodyStyle.fontSize = 12;
			TextBodyStyle.border = new RectOffset(0, 0, 0, 0);
			
			//LinkTextStyle
			LinkTextStyle = new GUIStyle(EditorStyles.linkLabel);
			LinkTextStyle.wordWrap = false;
			LinkTextStyle.stretchWidth = false;
		}

		/// <summary>
		/// All for "\n" in the source to be a line break in the result
		/// </summary>
		private string ProcessText(string s)
		{
			return s.Replace("\\n", "\n");
		}
		
		private bool ProcessLink(GUIContent label, params GUILayoutOption[] options)
		{
			var position = GUILayoutUtility.GetRect(label, LinkTextStyle, options);

			Handles.BeginGUI();
			Handles.color = LinkTextStyle.normal.textColor;
			Handles.DrawLine(new Vector3(position.xMin, position.yMax), new Vector3(position.xMax, position.yMax));
			Handles.color = Color.white;
			Handles.EndGUI();

			EditorGUIUtility.AddCursorRect(position, MouseCursor.Link);

			return GUI.Button(position, label, LinkTextStyle);
		}

		protected override void OnHeaderGUI()
		{
			var readMe = (ReadMe)target;

			Initialize();

			var iconWidth = Mathf.Min(EditorGUIUtility.currentViewWidth / 3f - 20f, 100);
			var labelWidth = EditorGUIUtility.currentViewWidth - iconWidth;
			var labelMinWidth = 200;
			var headerHeight = 85;
			iconWidth = 80;
			
			GUILayout.BeginHorizontal(GUILayout.MaxHeight(headerHeight));
			{
				IconStyle.fixedWidth = iconWidth;
				IconStyle.fixedHeight = iconWidth;
				GUILayout.Box(AssetDatabase.LoadAssetAtPath<Texture2D>(IconPath), IconStyle);
				GUILayout.Label(ProcessText(readMe.Title), TitleStyle, GUILayout.MaxWidth(labelWidth), GUILayout.MinWidth(labelMinWidth));
			}
			
			GUILayout.EndHorizontal();
			GUIDividerLine();
		}
		
		private void GUIDividerLine( int height = 1 )
		{
			Rect rect = EditorGUILayout.GetControlRect(false, height );
			rect.height = height;
			//Line
			EditorGUI.DrawRect(rect, new Color ( 0.4f,0.4f,0.4f, .8f ) );
			
			//Dropshadow
			rect.y +=2 ;
			EditorGUI.DrawRect(rect, new Color ( 0.2f,0.2f,0.2f, .4f ) );

		}

		public override void OnInspectorGUI()
		{
			var readMe = (ReadMe)target;
			
			Initialize();

			var MinWidth = Mathf.Clamp(EditorGUIUtility.currentViewWidth, LayoutMinWidht, LayoutMaxWidth);

			if (readMe != null && readMe.Sections != null)
			{
				GUILayout.Space(VSpaceBeforeAllSections);
				
				foreach (var section in readMe.Sections)
				{
					if (section == null)
					{
						continue;
					}

					if (!string.IsNullOrEmpty(section.TextHeading))
					{
						GUILayout.Space(5);
						GUILayout.BeginHorizontal(GUILayout.Width(MinWidth));
						GUILayout.Space(hSpace1);
						GUILayout.Label(section.TextHeading, TextHeadingStyle);
						GUILayout.EndHorizontal();
						GUILayout.Space(3);
					}

					if (!string.IsNullOrEmpty(section.TextSubheading))
					{
						GUILayout.Space(5);
						GUILayout.BeginHorizontal(GUILayout.Width(MinWidth));
						GUILayout.Space(hSpace2);
						GUILayout.Label(section.TextSubheading, TextSubheadingStyle);
						GUILayout.EndHorizontal();
						GUILayout.Space(3);
					}

					if (!string.IsNullOrEmpty(section.TextBody))
					{
						
						GUILayout.BeginHorizontal(GUILayout.Width(MinWidth));
						GUILayout.Space(hSpace3);
						GUILayout.TextField(ProcessText(section.TextBody), TextBodyStyle);
						GUILayout.EndHorizontal();
						GUILayout.Space(3);
					}

					if (!string.IsNullOrEmpty(section.LinkName))
					{
						GUILayout.BeginHorizontal();
						GUILayout.Space(hSpace3);
						GUILayout.Label("▶");
						if (ProcessLink(new GUIContent(section.LinkName)))
						{
							Application.OpenURL(section.LinkUrl);
						}

						GUILayout.Space(1000);
						GUILayout.EndHorizontal();

					}

					/*if (!string.IsNullOrEmpty(section.PingObjectName))
					{
						GUILayout.BeginHorizontal();
						GUILayout.Space(hSpace3);
						GUILayout.Label("▶");
						if (ProcessLink(new GUIContent(section.PingObjectName)))
						{
							string path = AssetDatabase.GUIDToAssetPath(section.PingObjectGuid);
							var objectToSelect = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
							EditorGUIUtility.PingObject(objectToSelect);

							// Do not select it.
							// Since For most users that would un-select the ReadMe.asset and disorient user
							// Selection.activeObject = objectToSelect;
						}

						GUILayout.Space(1000);
						GUILayout.EndHorizontal();
					}*/

					if (!string.IsNullOrEmpty(section.MenuItemName))
					{
						GUILayout.BeginHorizontal();
						GUILayout.Space(hSpace3);
						GUILayout.Label("▶");
						if (ProcessLink(new GUIContent(section.MenuItemName)))
						{
							EditorApplication.ExecuteMenuItem(section.MenuItemPath);
						}

						GUILayout.Space(1000);
						GUILayout.EndHorizontal();
					}

					GUILayout.Space(vSpaceAfterEachSection);
				}
			}
			
			
			
		}
	}
}
