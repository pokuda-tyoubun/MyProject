; -- Example1.iss --
; Demonstrates copying 3 files and creating an icon.

; SEE THE DOCUMENTATION FOR DETAILS ON CREATING .ISS SCRIPT FILES!

#define AppVer GetFileVersion("..\bin\release\PokudaSearch.exe")

[Setup]
AppId=PokudaSoft.PokudaSearch
UsePreviousAppDir=yes
AppName=PokudaSearch
AppVersion={#AppVer}
WizardStyle=modern
DefaultDirName={autopf}\PokudaSearch
DefaultGroupName=PokudaSoft
UninstallDisplayIcon={app}\PokudaSearch.exe
Compression=lzma2
SolidCompression=yes
OutputBaseFilename=Setup PokudaSearch v{#AppVer}
OutputDir=..\bin
PrivilegesRequired=admin

[Languages]
Name: jp; MessagesFile: "compiler:Languages\Japanese.isl"

[Dirs]
Name: "{app}\IndexStore";Permissions:everyone-modify
Name: "{app}\GPUCache";Permissions:everyone-modify
Name: "{app}\DB";Permissions:everyone-modify
Name: "{app}\SQLSrc"
Name: "{app}\x64"
Name: "{app}\x86"

[Files]
//Source: "Readme.txt"; DestDir: "{app}"; Flags: isreadme
//Source: "GPUCache\*"; DestDir: "{app}"
//Source: "IndexStore\Index58\*"; DestDir: "{app}"
Source: "..\bin\Installer\bin\DB\*"; DestDir: "{app}\DB";Permissions:everyone-modify; Flags: ignoreversion
Source: "..\bin\Installer\bin\SQLSrc\*"; DestDir: "{app}\SQLSrc"; Flags: ignoreversion
Source: "..\bin\Installer\bin\x64\*"; DestDir: "{app}\x64"; Flags: ignoreversion
Source: "..\bin\Installer\bin\x86\*"; DestDir: "{app}\x86"; Flags: ignoreversion
Source: "..\bin\Installer\bin\C1.Win.4.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\C1.Win.C1Chart.4.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\C1.Win.C1Chart.4.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\C1.Win.C1Command.4.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\C1.Win.C1Command.4.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\C1.Win.C1FlexGrid.4.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\C1.Win.C1FlexGrid.4.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\C1.Win.C1Input.4.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\C1.Win.C1Input.4.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\C1.Win.C1Ribbon.4.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\C1.Win.C1Ribbon.4.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\C1.Win.C1SuperTooltip.4.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\C1.Win.C1SuperTooltip.4.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\ExcelApi.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\ExcelApi.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\ExcelDataReader.DataSet.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\ExcelDataReader.DataSet.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\ExcelDataReader.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\ExcelDataReader.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\FlexLucene.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\FxCommonLib.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\FxCommonLib.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.AWT.WinForms.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Beans.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Charsets.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Cldrdata.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Corba.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Jdbc.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Localedata.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Management.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Media.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Misc.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Naming.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Nashorn.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Remoting.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Security.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.SwingAWT.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Text.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Tools.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.Util.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.XML.API.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.XML.Bind.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.XML.Crypto.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.XML.Parse.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.XML.Transform.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.XML.WebServices.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.OpenJDK.XML.XPath.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.Runtime.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\IKVM.Runtime.JNI.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\log4net.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\log4net.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\LumenWorks.Framework.IO.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\LumenWorks.Framework.IO.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\MathNet.Numerics.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\MathNet.Numerics.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\Microsoft.WindowsAPICodePack.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\Microsoft.WindowsAPICodePack.Shell.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\NetOffice.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\NetOffice.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\Newtonsoft.Json.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\OfficeApi.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\OfficeApi.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\PokudaSearch.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\PokudaSearch.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\PokudaSearch.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\PokudaSearch.vshost.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\PokudaSearch.vshost.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\PokudaSearch.vshost.exe.manifest"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\PowerPointApi.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\PowerPointApi.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\stdole.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\System.Data.SQLite.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\System.Data.SQLite.dll.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\System.Data.SQLite.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\TikaOnDotNet.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\TikaOnDotNet.TextExtraction.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\TikaOnDotNet.TextExtraction.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\UserDictionary.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\VBIDEApi.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\VBIDEApi.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\WordApi.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Installer\bin\WordApi.xml"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\PokudaSearch"; Filename: "{app}\PokudaSearch.exe"
Name: "{autodesktop}\PokudaSearch"; Filename: "{app}\PokudaSearch.exe"

[UninstallDelete]
Type: filesandordirs; Name: "{app}\*"
Type: filesandordirs; Name: "{app}\IndexStore\*"
Type: filesandordirs; Name: "{app}\GPUCache\*"
Type: filesandordirs; Name: "{app}"


