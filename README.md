# SDRSharp_Frontends_Plugin

SDRSharp Plugin for Load Frontends(Add back SDRSharp feature that were removed above v1732)

## Tested on

- Windows7/11 x86/x64
- SDRSharp v1901 (.NET 6.0)
<!-- - SDRSharp v1885 (.NET 6.0)
- SDRSharp v1807 (.NET 5.0)
- SDRSharp v1793 (.NET 5.0)
- SDRSharp v1784 (.NET Framework 4.6)
- SDRSharp v1777 (.NET Framework 4.6) -->
<!-- - SDRSharp v1732 (.NET Framework 4.6) -->

## Setup

- download and add the Plugin to SDR#
  - put the Plugin file `Douniwan5788.SDRSharpPlugin.dll` in the SDR# `Plugins` folder(folder path can be found with `core.pluginsDirectory` key in `SDRSharp.config`)
  - OR the same folder with `Sdrsharp.exe`, and edit `Plugins.xml` add: `<add key="FrontendsPlugin" value="Douniwan5788.SDRSharpPlugin.FrontendsPlugin,Douniwan5788.SDRSharpPlugin" />`
- put the Frontend Dlls in `Frontens` folder(auto created in the CWD when this plugin first loaded. `Start in` of the desttop shortcut)
  - SDRPlay(https://github.com/F6FLT/SDRSharp_v17xx-Plugin-for-SDRPlay)
  - ExtIO
