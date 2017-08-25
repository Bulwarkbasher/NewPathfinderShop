using UnityEngine;
using UnityEditor;
using System;
using System.Linq.Expressions;

public class TableEditorSettings : EditorWindow
{
    public static float columnSpacing = EditorGUIUtility.standardVerticalSpacing;
    public static float rowSpacing = EditorGUIUtility.standardVerticalSpacing;
    public static float rowHeight = EditorGUIUtility.singleLineHeight;
    public static Color oddRowColor = new Color(80f / 255f, 80f / 255f, 80f / 255f, 255f);
    public static Color columnDivisionColor = Color.black;


    private readonly static float defaultColumnSpacing = EditorGUIUtility.standardVerticalSpacing;
    private readonly static float defaultRowSpacing = EditorGUIUtility.standardVerticalSpacing;
    private readonly static float defaultRowHeight = EditorGUIUtility.singleLineHeight;
    private readonly static Color defaultOddRowColor = new Color(80f/255f, 80f / 255f, 80f / 255f, 255f);
    private readonly static Color defaultColumnDivisionColor = Color.black;


    private readonly static string columnSpacingName;
    private readonly static string rowSpacingName;
    private readonly static string rowHeightName;
    private readonly static string oddRowColorName;
    private readonly static string columnDivisionColorName;


    private const float windowWidth = 350f;
    private const int settingsCount = 5;
    private const float additionalWindowHeight = 15f;
    private const float additionalLabelWidth = 50f;
    private const float resetButtonWidth = 50f;
    private const string prefsClassName = "TableEditorSettings.";


    static TableEditorSettings ()
    {
        columnSpacingName = GetMemberName(() => columnSpacing);
        rowSpacingName = GetMemberName(() => rowSpacing);
        rowHeightName = GetMemberName(() => rowHeight);
        oddRowColorName = GetMemberName(() => oddRowColor);
        columnDivisionColorName = GetMemberName(() => columnDivisionColor);

        Load();
    }


    [MenuItem("Edit/Table Editor Settings...")]
    private static void Init ()
    {
        float windowHeight = settingsCount * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) + additionalWindowHeight;
        Rect position = new Rect(0f, 0f, windowWidth, windowHeight);
        GetWindowWithRect<TableEditorSettings>(position, true).Show();
    }


    private void OnDestroy ()
    {
        Save();
    }


    private void OnGUI ()
    {
        float oldLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth += additionalLabelWidth;

        DrawSetting("Space between columns", ref columnSpacing, defaultColumnSpacing, EditorGUILayout.FloatField);
        DrawSetting("Space between rows", ref rowSpacing, defaultRowSpacing, EditorGUILayout.FloatField);
        DrawSetting("Row height", ref rowHeight, defaultRowHeight, EditorGUILayout.FloatField);
        DrawSetting("Background colour of add rows", ref oddRowColor, defaultOddRowColor, EditorGUILayout.ColorField);
        DrawSetting("Colour of column divisions", ref columnDivisionColor, defaultColumnDivisionColor, EditorGUILayout.ColorField);

        EditorGUIUtility.labelWidth = oldLabelWidth;
    }


    private void DrawSetting <TSetting> (string label, ref TSetting value, TSetting defaultValue, Func<string, TSetting, GUILayoutOption[], TSetting> guiCall)
    {
        EditorGUILayout.BeginHorizontal();
        value = guiCall(label, value, new GUILayoutOption[0]);
        if (GUILayout.Button("Reset", GUILayout.Width(resetButtonWidth)))
            value = defaultValue;
        EditorGUILayout.EndHorizontal();
    }


    private static void Save ()
    {
        SaveSetting(columnSpacingName, columnSpacing);
        SaveSetting(rowSpacingName, rowSpacing);
        SaveSetting(rowHeightName, rowHeight);
        SaveSetting(oddRowColorName, oddRowColor);
        SaveSetting(columnDivisionColorName, columnDivisionColor);
    }


    private static void SaveSetting(string settingName, float value)
    {
        string prefName = prefsClassName + settingName;
        EditorPrefs.SetFloat(prefName, value);
    }


    private static void SaveSetting(string settingName, Color value)
    {
        EditorPrefs.SetFloat(prefsClassName + settingName + ".r", value.r);
        EditorPrefs.SetFloat(prefsClassName + settingName + ".g", value.g);
        EditorPrefs.SetFloat(prefsClassName + settingName + ".b", value.b);
        EditorPrefs.SetFloat(prefsClassName + settingName + ".a", value.a);
    }


    private static void Load ()
    {
        LoadSetting(columnSpacingName, ref columnSpacing, defaultColumnSpacing);
        LoadSetting(rowSpacingName, ref rowSpacing, defaultRowSpacing);
        LoadSetting(rowHeightName, ref rowHeight, defaultRowHeight);
        LoadSetting(oddRowColorName, ref oddRowColor, defaultOddRowColor);
        LoadSetting(columnDivisionColorName, ref columnDivisionColor, defaultColumnDivisionColor);
    }


    private static void LoadSetting (string settingName, ref float value, float defaultValue)
    {
        string prefName = prefsClassName + settingName;
        value = EditorPrefs.GetFloat(prefName, defaultValue);
    }


    private static void LoadSetting (string settingName, ref Color value, Color defaultValue)
    {
        value.r = EditorPrefs.GetFloat(prefsClassName + settingName + ".r", defaultValue.r);
        value.g = EditorPrefs.GetFloat(prefsClassName + settingName + ".g", defaultValue.g);
        value.b = EditorPrefs.GetFloat(prefsClassName + settingName + ".b", defaultValue.b);
        value.a = EditorPrefs.GetFloat(prefsClassName + settingName + ".a", defaultValue.a);        
    }

    private static string GetMemberName<T>(Expression<Func<T>> memberExpression)
    {
        MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
        return expressionBody.Member.Name;
    }
}
