using System;
using System.Collections;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Networking;

using static UnityEngine.GUILayout;
using static UnityEngine.GUI;
using static UnityEngine.GameObject;
using static UnityEngine.Debug;
using static HarmonyLib.Harmony;

namespace Operation_Gensis_v1
{
    public class Class1 : MonoBehaviour
    {
        // ============================================================
        // WINDOW
        // ============================================================

        private bool showWindow = true;

        private Rect windowRect =
            new Rect(100, 100, 400, 300);


        // ============================================================
        // HARMONY
        // ============================================================

        private static Harmony harmony = null;
        private static AvatarController avatarController = null;


        // ============================================================
        // MENU BACKGROUND
        // ============================================================

        private Texture2D menuBackground;

        private const string MenuBackgroundURL =
            "https://iili.io/3a7fXNS.png";


        // ============================================================
        // MENU COLORS
        // ============================================================

        // ------------------------------------------------------------
        // Open OG button
        // ------------------------------------------------------------

        private Color openButtonColor =
            new Color(
                0.05f,
                0.05f,
                0.05f,
                0.85f
            );

        private Color openButtonHoverColor =
            new Color(
                0.10f,
                0.10f,
                0.10f,
                0.90f
            );

        private Color openButtonTextColor =
            Color.white;


        // ------------------------------------------------------------
        // Menu rectangles
        // ------------------------------------------------------------

        private Color rectangleColor =
            new Color(
                0f,
                0f,
                0f,
                0f
            );


        // ============================================================
        // MENU STYLE
        // ============================================================

        private GUIStyle menuOptionStyle;


        // ============================================================
        // INITIALIZATION
        // ============================================================

        public static void Initialize()
        {
            Log(
                "[Operation Genesis] Initializing..."
            );


            // Prevent duplicate initialization
            if (harmony != null)
            {
                Log(
                    "[Operation Genesis] Harmony already initialized."
                );

                return;
            }


            // Create Harmony instance
            harmony =
                new Harmony(
                    "Operation_Gensis_v1"
                );


            // Apply Harmony patches
            harmony.PatchAll();


            Log(
                "[Operation Genesis] Harmony initialized."
            );


            // Create Unity GameObject
            GameObject obj =
                new GameObject(
                    "Operation_Gensis_v1_GUI"
                );


            // Survive scene changes
            UnityEngine.Object.DontDestroyOnLoad(
                obj
            );


            // Add GUI component
            Class1 gui =
                obj.AddComponent<Class1>();


            // Load menu background
            gui.StartCoroutine(
                gui.LoadMenuBackground()
            );


            Log(
                "[Operation Genesis] GUI initialized."
            );
        }


        // ============================================================
        // LOAD MENU BACKGROUND
        // ============================================================

        private IEnumerator LoadMenuBackground()
        {
            Log(
                "[Operation Genesis] Loading menu background..."
            );


            using (
                UnityWebRequest request =
                UnityWebRequestTexture.GetTexture(
                    MenuBackgroundURL
                )
            )
            {
                yield return request.SendWebRequest();


#if UNITY_2020_1_OR_NEWER

                if (
                    request.result !=
                    UnityWebRequest.Result.Success
                )

#else

                if (
                    request.isNetworkError ||
                    request.isHttpError
                )

#endif
                {
                    Log(
                        "[Operation Genesis] Failed to load background: " +
                        request.error
                    );

                    yield break;
                }


                menuBackground =
                    DownloadHandlerTexture.GetContent(
                        request
                    );


                if (menuBackground != null)
                {
                    Log(
                        "[Operation Genesis] Menu background loaded successfully."
                    );
                }
                else
                {
                    Log(
                        "[Operation Genesis] Background texture is null."
                    );
                }
            }
        }


        // ============================================================
        // GUI
        // ============================================================

        private void OnGUI()
        {
            // --------------------------------------------------------
            // Closed menu
            // --------------------------------------------------------

            if (!showWindow)
            {
                DrawOpenButton();

                return;
            }


            // --------------------------------------------------------
            // Main window
            // --------------------------------------------------------

            windowRect =
                Window(
                    12345,
                    windowRect,
                    DrawWindow,
                    "",
                    GUIStyle.none
                );
        }


        // ============================================================
        // DRAW OPEN BUTTON
        // ============================================================

        private void DrawOpenButton()
        {
            Rect buttonRect =
                new Rect(
                    10,
                    10,
                    150,
                    30
                );


            // --------------------------------------------------------
            // Check hover
            // --------------------------------------------------------

            bool hovered =
                buttonRect.Contains(
                    Event.current.mousePosition
                );


            // --------------------------------------------------------
            // Select color
            // --------------------------------------------------------

            Color buttonColor =
                hovered
                ? openButtonHoverColor
                : openButtonColor;


            // --------------------------------------------------------
            // Save GUI color
            // --------------------------------------------------------

            Color oldColor =
                GUI.color;


            // --------------------------------------------------------
            // Draw flat rectangle
            // --------------------------------------------------------

            GUI.color =
                buttonColor;


            GUI.DrawTexture(
                buttonRect,
                Texture2D.whiteTexture
            );


            // Restore color
            GUI.color =
                oldColor;


            // --------------------------------------------------------
            // Text
            // --------------------------------------------------------

            GUIStyle textStyle =
                new GUIStyle(
                    GUI.skin.label
                );


            textStyle.alignment =
                TextAnchor.MiddleCenter;


            textStyle.normal.textColor =
                openButtonTextColor;


            GUI.Label(
                buttonRect,
                "Open OG v1",
                textStyle
            );


            // --------------------------------------------------------
            // Click
            // --------------------------------------------------------

            if (
                Event.current.type ==
                EventType.MouseDown &&

                Event.current.button == 0 &&

                buttonRect.Contains(
                    Event.current.mousePosition
                )
            )
            {
                showWindow =
                    true;

                Event.current.Use();
            }
        }


        // ============================================================
        // DRAW RECTANGLE
        // ============================================================

        private void DrawRectangle(
            Rect rect,
            Color color)
        {
            Color oldColor =
                GUI.color;


            GUI.color =
                color;


            GUI.DrawTexture(
                rect,
                Texture2D.whiteTexture
            );


            GUI.color =
                oldColor;
        }


        // ============================================================
        // MENU STYLE
        // ============================================================

        private void InitializeMenuStyles()
        {
            menuOptionStyle =
                new GUIStyle(
                    GUI.skin.button
                );


            // --------------------------------------------------------
            // Completely transparent
            // --------------------------------------------------------

            menuOptionStyle.normal.background =
                null;

            menuOptionStyle.hover.background =
                null;

            menuOptionStyle.active.background =
                null;

            menuOptionStyle.focused.background =
                null;


            // --------------------------------------------------------
            // Text
            // --------------------------------------------------------

            menuOptionStyle.normal.textColor =
                Color.white;

            menuOptionStyle.hover.textColor =
                Color.white;

            menuOptionStyle.active.textColor =
                Color.gray;


            // --------------------------------------------------------
            // Alignment
            // --------------------------------------------------------

            menuOptionStyle.alignment =
                TextAnchor.MiddleLeft;


            // --------------------------------------------------------
            // Padding
            // --------------------------------------------------------

            menuOptionStyle.padding =
                new RectOffset(
                    30,
                    15,
                    5,
                    5
                );
        }


        // ============================================================
        // CREATE TEXT
        // ============================================================

        private void CreateText(
            string? text,
            float x,
            float y,
            params GUILayoutOption[]? options)
        {
            Space(y);

            BeginHorizontal();

            Space(x);

            Label(
                text,
                options
            );

            EndHorizontal();
        }


        // ============================================================
        // MENU OPTION
        // ============================================================

        private void MenuOption(
            string textOption,
            Action function)
        {
            if (menuOptionStyle == null)
            {
                InitializeMenuStyles();
            }


            if (
                GUILayout.Button(
                    textOption,
                    menuOptionStyle,
                    GUILayout.Height(40)
                )
            )
            {
                function?.Invoke();
            }
        }


        // ============================================================
        // DRAW WINDOW
        // ============================================================

        private void DrawWindow(
            int windowID)
        {
            // --------------------------------------------------------
            // Background image
            // --------------------------------------------------------

            if (menuBackground != null)
            {
                GUI.DrawTexture(
                    new Rect(
                        0,
                        0,
                        windowRect.width,
                        windowRect.height
                    ),
                    menuBackground,
                    ScaleMode.ScaleAndCrop
                );
            }


            // --------------------------------------------------------
            // Example transparent rectangle
            //
            // You can add/remove these whenever you want.
            // --------------------------------------------------------

            //DrawRectangle(
            //    new Rect(
            //        0,
            //        0,
            //        windowRect.width,
            //        55
            //    ),
            //    new Color(
            //        0f,
            //        0f,
            //        0f,
            //        0.35f
            //    )
            //);


            // --------------------------------------------------------
            // Menu contents
            // --------------------------------------------------------

            BeginVertical();


            Space(15);


            CreateText(
                "Operation Genesis v1",
                30,
                15
            );


            Space(10);


            // --------------------------------------------------------
            // Fly Mode
            // --------------------------------------------------------

            MenuOption(
                "Toggle Fly Mode",
                FlyMode
            );


            // --------------------------------------------------------
            // God Mode
            // --------------------------------------------------------

            MenuOption(
                "Toggle God Mode",
                GodMode
            );


            Space(3);


            // --------------------------------------------------------
            // Close Window
            // --------------------------------------------------------

            MenuOption(
                "Close Window",
                () =>
                {
                    showWindow =
                        false;
                }
            );


            EndVertical();


            // --------------------------------------------------------
            // DRAG WINDOW
            // --------------------------------------------------------

            DragWindow();
        }


        // ============================================================
        // FLY MODE
        // ============================================================

        private void FlyMode()
        {
            AvatarLocalPlayerController
                avatarLocalPlayerController =
                FindObjectOfType<
                    AvatarLocalPlayerController
                >();


            if (
                avatarLocalPlayerController != null &&

                avatarLocalPlayerController.Entity != null
            )
            {
                bool currentState =
                    avatarLocalPlayerController
                        .Entity
                        .IsFlyMode
                        .Value;


                bool newState =
                    !currentState;


                avatarLocalPlayerController
                    .Entity
                    .IsFlyMode
                    .Value =
                    newState;


                Log(
                    "[Operation Genesis] Fly Mode: " +
                    currentState +
                    " -> " +
                    newState
                );
            }
        }


        // ============================================================
        // GOD MODE
        // ============================================================

        private void GodMode()
        {
            AvatarLocalPlayerController
                avatarLocalPlayerController =
                FindObjectOfType<
                    AvatarLocalPlayerController
                >();


            if (
                avatarLocalPlayerController != null &&

                avatarLocalPlayerController.Entity != null
            )
            {
                bool currentState =
                    avatarLocalPlayerController
                        .Entity
                        .IsGodMode
                        .Value;


                bool newState =
                    !currentState;


                avatarLocalPlayerController
                    .Entity
                    .IsGodMode
                    .Value =
                    newState;


                Log(
                    "[Operation Genesis] God Mode: " +
                    currentState +
                    " -> " +
                    newState
                );
            }
        }
    }
}
