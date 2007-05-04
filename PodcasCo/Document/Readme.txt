PodcasCo
---


【主な機能】
PodcasCoはMicrosoft Windows Mobile 5.0 Pocket PC上で動作するPodcastのヘッドライ
ンをインターネットより取得、端末内に保存し、インターネットに接続されていない環
境でPodcastを再生するツールです。


【必要環境】
Microsoft Windows Mobile 5.0 Pocket PC
.NET Compact Framework 1.0 SP3
TCPMP（推奨）

.NET Compact Frameworkについては
http://www.microsoft.com/japan/msdn/vstudio/device/netcf/
を参照してください。


【インストール】
ZIPファイルを解凍し、PodcasCo.exe・MiscPocketCompactLibrary.dll・
FileDialog.dll・GetFileInfo.dllを適当なフォルダに置いてください。


【アップデート】
ZIPファイルを解凍し、PodcasCo.exe・MiscPocketCompactLibrary.dll・
FileDialog.dll・GetFileInfo.dllを適当なフォルダに置いてください。


【アンインストール】
PodcasCo.exe・MiscPocketCompactLibrary.dll・FileDialog.dll・GetFileInfo.dll、同
じフォルダにあるSetting.xmlおよびSetting.*.xmlを削除してください。
PodcasCoはレジストリを使用しません。


【使い方】
・PodcasCoの使い方
Updateボタンを押し、配信中のヘッドラインを取得します。
保存したい番組を選択してClipボタンを押すとダウンロードを開始します。
クリップした番組にはタイトルの頭に [C] が表示されます。
クリップした番組を選択し、Playボタンで番組を再生します。

・PodcasCoの設定
トップ画面から[メニュー]->[PodcasCo設定]を選択するとPodcasCoの設定を行うことが
できます。
[Podcastをクリップするフォルダ]はダウンロードした番組を保存するフォルダを設定
します。
[メディアプレーヤーのパス]は番組を再生するためのメディアプレーヤーのファイルパ
スを設定します。
メディアプレーヤーにはTCPMP（http://tcpmp.corecodec.org/about）をおすすめしま
す。
[ブラウザのパス]は番組のウェブサイトにアクセスするためのブラウザのファイルパス
を設定します。

・放送局の追加
トップ画面から[メニュー]->[放送局に追加と削除]を選択するとPodcasCoの放送局の設
定を行うことができます。
PodcastのURLを指定し[追加]ボタンで放送局を追加することができます。


【ライセンス】
本ソフトウェアは使用にあたり無料で使用することができますが、本ソフトウェアの使
用に起因する一切の損害について開発者及び関係者が責任を負わないものとします。


【その他】
開発にあたりW-ZERO3でのみテストしており、他のデバイスや環境で正常に動作するか
についてはテストしていません。


【バージョン履歴】
Version 0.6
・同こんのFileDialogを1.14にバージョンアップ
・リジュームに対応した

Version 0.5
・デバイスの解像度に依存しないように変更

Version 0.4
・プロキシ設定を「プロキシに接続しない」「OSで設定したプロキシを使用する」「プ
　ロキシを設定する」から選択するように変更
・エラー終了時にログを出力するようにした
・アイコンをやめた。反省している。

Version 0.3
・クリップできないPodcastがあった場合にメッセージボックスを出すようにした
・UIの細かな修正
・アイコンをつけた。反省している。

Version 0.2
・「Podcastをクリップするフォルダ」設定を変更した後に、クリップ済みの番組のデー
　タと保存しているファイルに整合性がとれなくなっていたのを修正
・クリップするフォルダ英語に以外の文字が入っていた場合にプレイヤーで再生できな
　かったのを修正
・ダウンロードの進捗をプログレスバーで表示するようにした
・クリップボードの内容がテキスト以外の場合に、ペーストをすると落ちていたのを
　（多分）修正
・放送局の追加と削除画面でエンターキーを押して入力するとビープ音が鳴っていたの
　を鳴らないように変更
・メディアプレイヤーとブラウザのパス、Podcastをクリップするフォルダ設定をファ
　イルダイアログから設定できるようにした
・クリップボードのキーボードショートカットに対応（テキストボックスのみ）
・UIの細かな修正

Version 0.1
・初版
