using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditorInternal;
using UnityEngine;

namespace AllIn13DShader
{
	public class AllIn13DShaderWindow : EditorWindow
	{
		private static AllIn13DShaderWindow instance;

		private Vector2 scrollPosition;

		private int currTab = 0;
		private string[] tabsNames;

		private CommonStyles commonStyles;
		private GlobalConfiguration globalConfiguration;

		private Texture imageInspector;

		private TextureEditorTool textureEditorTool;
		private TextureEditorValuesDrawer textureEditorValuesDrawer;

		private NormalMapCreatorTool normalMapCreatorTool;
		private NormalMapCreatorDrawer normalMapCreatorDrawer;

		private GradientCreatorTool gradientCreatorTool;
		private GradientCreatorDrawer gradientCreatorDrawer;

		private AtlasPackerTool atlasPackerTool;
		private AtlasPackerDrawer atlasPackerDrawer;

		private NoiseCreatorTool noiseCreatorTool;
		private NoiseCreatorDrawer noiseCreatorDrawer;

		private OverrideMaterialsTabDrawer overrideMaterialsTabDrawer;
		private OtherTabDrawer otherTabDrawer;
		private URPSettingsDrawer urpSettingsDrawer;


		private bool initialized;


		[MenuItem("Tools/AllIn1/3DShaderWindow")] 
		public static void ShowAllIn13DShaderWindow()
		{
			if(instance == null)
			{
				instance = GetWindow<AllIn13DShaderWindow>("All In 1 3DShader Window");
			}
		} 

		private void Init()
		{
			//EditorApplication.wantsToQuit += OnWantsToQuit;

			commonStyles = new CommonStyles();
			globalConfiguration = EditorUtils.FindAssetByName<GlobalConfiguration>("GlobalConfiguration");


			scrollPosition = Vector2.zero;

#if ALLIN13DSHADER_URP
			tabsNames = new string[] { "Save Paths", "Texture Editor", "Texture Creators", "Override Materials", "Default Look", "URP Settings"};
#else
			tabsNames = new string[] { "Save Paths", "Texture Editor", "Texture Creators", "Override Materials", "Default Look"};
#endif

			if (imageInspector == null)
			{
				imageInspector = AllIn13DShaderConfig.GetInspectorImage();
			}

			textureEditorTool = new TextureEditorTool();
			textureEditorValuesDrawer = new TextureEditorValuesDrawer();
			textureEditorValuesDrawer.Setup(textureEditorTool);

			normalMapCreatorTool = new NormalMapCreatorTool();
			normalMapCreatorDrawer = new NormalMapCreatorDrawer(normalMapCreatorTool, commonStyles, Repaint);

			gradientCreatorTool = new GradientCreatorTool();
			gradientCreatorDrawer = new GradientCreatorDrawer(gradientCreatorTool, commonStyles);

			atlasPackerTool = new AtlasPackerTool();
			atlasPackerDrawer = new AtlasPackerDrawer(atlasPackerTool, commonStyles);
			
			noiseCreatorTool = new NoiseCreatorTool();
			noiseCreatorDrawer = new NoiseCreatorDrawer(noiseCreatorTool, commonStyles);

			otherTabDrawer = new OtherTabDrawer();
			otherTabDrawer.Setup(globalConfiguration, commonStyles);

			overrideMaterialsTabDrawer = new OverrideMaterialsTabDrawer();
			overrideMaterialsTabDrawer.Setup(commonStyles, this);

			urpSettingsDrawer = new URPSettingsDrawer();
			urpSettingsDrawer.Setup(commonStyles);

			initialized = true;
		}

		private void OnEnable()
		{
			Init();
		}

		private void OnDisable()
		{

		}

		private void OnDestroy()
		{
			WindowClosed();
		}

		private void WindowClosed()
		{
			overrideMaterialsTabDrawer.Close();
			Repaint();
		}

		private void OnGUI()
		{
			if (!initialized)
			{
				Init();
			}

			commonStyles.InitStyles();

#if ALLIN13DSHADER_URP
			bool urpCorrectlyConfigured = URPConfigurator.IsURPCorrectlyConfigured();
			if (!urpCorrectlyConfigured)
			{
				Draw_URPError();
			}
			else
			{
				Draw();
			}
#else
			Draw();
#endif

		}

		private void Draw()
		{
			using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPosition, GUILayout.Width(position.width), GUILayout.Height(position.height)))
			{
				scrollPosition = scrollView.scrollPosition;

				if (imageInspector)
				{
					Rect rect = EditorGUILayout.GetControlRect(GUILayout.Height(50));
					GUI.DrawTexture(rect, imageInspector, ScaleMode.ScaleToFit, true);
				}

				EditorUtils.DrawThinLine();
				int newTab = GUILayout.Toolbar(currTab, tabsNames);
				if (newTab != currTab)
				{
					if (newTab == 3)
					{
						overrideMaterialsTabDrawer.Show();
					}
					else if (currTab == 3)
					{
						overrideMaterialsTabDrawer.Hide();
					}

					currTab = newTab;
				}
				EditorUtils.DrawThinLine();

				switch (currTab)
				{
					case 0:
						DrawTabSavePaths();
						break;
					case 1:
						DrawTabTextureEditor();
						break;
					case 2:
						DrawTabTextureCreators();
						break;
					case 3:
						DrawTabOverrideMaterials();
						break;
					case 4:
						DrawTabOthers();
						break;
#if ALLIN13DSHADER_URP
					case 5:
						DrawTabURPSettings();
						break;
#endif
				}

				GUILayout.Space(10);
				EditorUtils.DrawThinLine();
				GUILayout.Label("Current asset version is " + Constants.VERSION, EditorStyles.boldLabel);
			}
		}

		private void Draw_URPError()
		{
			EditorGUILayout.LabelField(CommonMessages.URP_PIPELINE_NOT_ASSIGNED, commonStyles.warningLabel);
			EditorUtils.DrawButtonLink(CommonMessages.URP_PIPELINE_NOT_ASSIGNED_DOC_LINK);
		}

		private void DrawTabSavePaths()
		{
			GUILayout.Label("Material Save Path", commonStyles.bigLabel);
			GUILayout.Space(20);
			GUILayout.Label("Select the folder where new Materials will be saved when the Save Material To Folder button of the asset component is pressed", EditorStyles.boldLabel);
			GlobalConfiguration.instance.MaterialSavePath = EditorUtils.DrawSelectorFolder(GlobalConfiguration.instance.MaterialSavePath, /*GlobalConfiguration.MATERIAL_SAVE_PATH_DEFAULT,*/ "New Material Folder");

			EditorUtils.DrawThinLine();
			GUILayout.Label("Render Material to Image Save Path", commonStyles.bigLabel);
			GUILayout.Space(20);

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Rendered Image Texture Scale", GUILayout.MaxWidth(190));
			GlobalConfiguration.instance.RenderImageScale = EditorGUILayout.Slider(GlobalConfiguration.instance.RenderImageScale, 0.2f, 5f, GUILayout.MaxWidth(200));
			if (GUILayout.Button("Default Value", GUILayout.MaxWidth(100)))
			{
				GlobalConfiguration.instance.RenderImageScale = 1f;
			}
			EditorGUILayout.EndHorizontal();

			GlobalConfiguration.instance.RenderImageSavePath = EditorUtils.DrawSelectorFolder(GlobalConfiguration.instance.RenderImageSavePath, /*GlobalConfiguration.RENDER_IMAGE_SAVE_PATH_DEFAULT,*/"New Images Folder");
		}

		private void DrawTabTextureEditor()
		{
			EditorGUI.BeginChangeCheck();
			textureEditorTool.editorTexInput = EditorGUILayout.ObjectField("Image to Edit", textureEditorTool.editorTexInput, typeof(Texture2D), false, GUILayout.Width(300), GUILayout.Height(50)) as Texture2D;
			if (EditorGUI.EndChangeCheck())
			{
				if (textureEditorTool.editorTexInput != null)
				{
					textureEditorTool.Setup();
				}
			}

			EditorUtils.DrawThinLine();

			if (textureEditorTool.editorTex != null)
			{
				textureEditorValuesDrawer.Draw();
			}
			else
			{
				GUILayout.Label("Please select an Image to Edit above", EditorStyles.boldLabel);
			}
		}

		private void DrawTabTextureCreators()
		{
			normalMapCreatorDrawer.Draw();

			EditorUtils.DrawThinLine();

			gradientCreatorDrawer.Draw();

			EditorUtils.DrawThinLine();

			atlasPackerDrawer.Draw();

			EditorUtils.DrawThinLine();

			noiseCreatorDrawer.Draw();
		}

		private void DrawTabOverrideMaterials()
		{
			overrideMaterialsTabDrawer.Draw();
		}

		private void DrawTabOthers()
		{
			otherTabDrawer.Draw();
		}

		private void DrawTabURPSettings()
		{
			urpSettingsDrawer.Draw();
		}

		//[InitializeOnLoadMethod]
		//private static void InitializeOnLoad()
		//{
		//	CheckURPCorrectlyConfigured();
		//}

		public static void AllAssetProcessed()
		{
			CheckURPCorrectlyConfigured();
		}

		private static void CheckURPCorrectlyConfigured()
		{
#if ALLIN13DSHADER_URP
			if (!URPConfigurator.IsURPCorrectlyConfigured())
			{
				ShowAllIn13DShaderWindow();
			}
#endif
		}
	}
}