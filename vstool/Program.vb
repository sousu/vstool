
' タスクトレイツールプログラムメイン
' - 起動をフォームのデフォルトではなく自前で必要コンポーネントやメイン処理を用意して起動する
' - configからアプリケーションフレームワークを無効にして当クラスのMainをスタートアップに設定している

Public Class Program
    Shared i As IEopener

    '起動処理
    <STAThread()>
    Shared Sub Main()
        '終了イベントハンドラの設定
        AddHandler Application.ApplicationExit, AddressOf appExit
        '共有設定ファイル読込
        ShareOptionXML.LoadFromXmlFile()
        '各オブジェクト用意と起動
        i = New IEopener
        Dim c = New Component
        Dim o = New ClipboardObserver(i, c)
        Clipboard.Clear()
        Application.Run(o) 'ダミーフォームを持つオブジェクトのメッセージループ処理開始
    End Sub

    '終了処理
    Shared Sub appExit(ByVal sender As Object, ByVal e As EventArgs)
        i.close()
        '設定を保存する
        ShareOptionXML.SaveToXmlFile()
    End Sub

End Class