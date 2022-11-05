using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using SDRSharp;
using SDRSharp.Common;
using SDRSharp.Radio;

namespace Douniwan5788.SDRSharpPlugin
{
    public class FrontendsPlugin : ISharpPlugin
    {
        private ControlPanel _gui;
        private ISharpControl _control;

        public string DisplayName => "Extend Frontends Plugin";

        // IExtendedNameProvider not avalabke before 1901
        public string Category => "misc";

        public string MenuItemName => DisplayName;

        public bool IsActive => _gui != null && _gui.Visible;

        public UserControl Gui
        {
            get
            {
                LoadGui();
                return _gui;
            }
        }

        public void LoadGui()
        {
            if (_gui == null)
            {
                _gui = new ControlPanel(_control);
            }
        }

        public void Initialize(ISharpControl control)
        {
            // using StreamWriter streamWriter2 = File.AppendText("ModuleInitializer.log");
            // streamWriter2.WriteLine("Initialize" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            // MessageBox.Show("Initialize");

            _control = control;
            if (_control == null)
                MessageBox.Show(string.Format("control: {0}", control != null ? control.ToString() : "(null)"), "N");

            // SDRSharp.SharpControlProxy
            Object mainform = control.GetType().GetField(
                "_owner"
                , BindingFlags.NonPublic | BindingFlags.Instance
            ).GetValue(control);

            if (mainform == null)
                MessageBox.Show(string.Format("mainform: {0}", mainform != null ? mainform.ToString() : "(null)"), "N");

            // private void LoadSourceType(string name, Type type, int access)
            MethodInfo LoadSourceType = mainform.GetType().GetMethod(
                "LoadSourceType", BindingFlags.NonPublic | BindingFlags.Instance
            );
            if (LoadSourceType == null)
                MessageBox.Show(string.Format("LoadSourceType: {0}", LoadSourceType != null ? LoadSourceType.ToString() : "(null)"), "N");

            // workingaround for airspy bad "Baseband from Sound Card" coding…
            // pop iqSourceComboBox.Last then added back
            Object iqSourceComboBox = mainform.GetType().GetField(
                "iqSourceComboBox"
                , BindingFlags.NonPublic | BindingFlags.Instance
            ).GetValue(mainform);
            if (iqSourceComboBox == null)
                MessageBox.Show(string.Format("iqSourceComboBox: {0}", iqSourceComboBox != null ? iqSourceComboBox.ToString() : "(null)"), "N");

            Object Items = iqSourceComboBox.GetType().GetProperty("Items").GetValue(iqSourceComboBox);
            if (Items == null)
                MessageBox.Show(string.Format("Items: {0}", Items != null ? Items.ToString() : "(null)"), "N");

            Object last = Items.GetType().GetProperty("Last").GetAccessors()[0].Invoke(Items, null);
            if (last == null)
                MessageBox.Show(string.Format("last: {0}", last != null ? last.ToString() : "(null)"), "N");

            IList items = (IList)Items;

            // MessageBox.Show(string.Format("items: {0}", string.Join(",", (IEnumerable)items), "N"));

            Dictionary<string, Type> dictionary = new Dictionary<string, Type>();

            System.IO.Directory.CreateDirectory("./Frontends");
            LoadFrontendTree("./Frontends", dictionary);

            items.Remove(last);


            foreach (string key in dictionary.Keys)
            {
                try
                {
                    Type type = dictionary[key];
                    // SDRSharp.Program.UpdateSplashStatus("Loading Frontend: " + type.Name);
                    LoadSourceType.Invoke(mainform,
                                    new object[] { key, type, 10 }
                                );
                }
                catch (Exception ex3)
                {
                    // _sharpFrontends.Remove(name);

                    using StreamWriter streamWriter = File.AppendText("FrontendError.log");
                    streamWriter.WriteLine("*** Frontend Load Error - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    streamWriter.WriteLine("Config Key   '" + key + "'");
                    streamWriter.WriteLine("Type         '" + (dictionary[key].AssemblyQualifiedName != null ? dictionary[key].AssemblyQualifiedName : dictionary[key].ToString()) + "'");
                    streamWriter.WriteLine("Message      '" + ex3.Message + "'");
                    if (!string.IsNullOrEmpty(ex3.StackTrace))
                    {
                        streamWriter.WriteLine("Stack Trace");
                        streamWriter.WriteLine(ex3.StackTrace);
                    }
                    streamWriter.WriteLine();

                }
            }

            items.Add(last);

            // SDRSharp.Program.UpdateSplashStatus("Front-ends Loaded");
        }

        private void LoadFrontendTree(string directory, Dictionary<string, Type> frontendTypes)
        {
            if (!Directory.Exists(directory))
            {
                return;
            }
            string[] dllFiles = Directory.GetFiles(directory, "*.dll");
            Array.Sort(dllFiles, (IComparer<string>)StringComparer.Create(System.Globalization.CultureInfo.InvariantCulture, ignoreCase: true));
            foreach (string dllPath in dllFiles)
            {
                if (Path.GetFileName(dllPath)!.StartsWith("_"))
                {
                    continue;
                }
                try
                {
                    Type[] exportedTypes = Assembly.LoadFrom(dllPath).GetExportedTypes();
                    foreach (Type type in exportedTypes)
                    {
                        bool num = Array.Exists(type.GetInterfaces(), (Type i) => i == typeof(IFrontendController));
                        bool flag = !type.IsAbstract && type.GetConstructor(Array.Empty<Type>()) != null;
                        if (num && flag)
                        {
                            string fullkey = type.FullName + "," + dllPath;
                            frontendTypes[fullkey] = type;
                        }
                    }
                }
                catch (BadImageFormatException)
                {
                }
                catch (Exception ex2)
                {
                    using StreamWriter streamWriter = File.AppendText("FrontendError.log");
                    streamWriter.WriteLine("*** Frontend Load Error - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    streamWriter.WriteLine("DLL          '" + dllPath + "'");
                    streamWriter.WriteLine("Message      '" + ex2.Message + "'");
                    if (!string.IsNullOrEmpty(ex2.StackTrace))
                    {
                        streamWriter.WriteLine("Stack Trace");
                        streamWriter.WriteLine(ex2.StackTrace);
                    }
                    streamWriter.WriteLine();
                }
            }
            string[] subDirectories = Directory.GetDirectories(directory);
            foreach (string subDir in subDirectories)
            {
                if (!Path.GetFileName(subDir)!.StartsWith("_"))
                {
                    LoadFrontendTree(subDir, frontendTypes);
                }
            }
        }
        public void Close()
        {
        }
    }
}
