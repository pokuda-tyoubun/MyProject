※ドキュメント生成方法
FxCommonLib.csprojの以下の一行を一時的に削除してビルドする必要がある。
<Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />

【理由】
docfx.consoleのバグなのか?$(MSBuildToolsPath)がdocfx.consoleパッケージのパスに置き換わるため、
ビルドに失敗してしまう。