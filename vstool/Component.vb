'通知アイコンに紐付いたメインメニュー各項目の選択時動作

Public Class Component

    Private Sub itemHelpClick(sender As Object, e As EventArgs) Handles ToolStripMenuItemHelp.Click
        Dim help = <Query>
 - コピペしたテキストがXMLを含んでいる場合、自動でIEを開き表示
 - コピペ結果は指定した履歴フォルダに日時を含むファイル名にて自動で保存
 - IEでローカルファイルを読み込む関係から管理者権限での実行が必要
                   </Query>
        MessageBox.Show(help)
    End Sub

    Private Sub itemPathClick(sender As Object, e As EventArgs) Handles ToolStripMenuItemPath.Click
        Dim fbd As New FolderBrowserDialog
        fbd.Description = "履歴を保存するフォルダを指定してください。"
        fbd.RootFolder = Environment.SpecialFolder.Desktop
        fbd.SelectedPath = ShareOptionXML.Instance.savepath
        fbd.ShowNewFolderButton = True
        If fbd.ShowDialog() = DialogResult.OK Then
            ShareOptionXML.Instance.savepath = fbd.SelectedPath
        End If
    End Sub

    Private Sub itemExitClick(sender As Object, e As EventArgs) Handles ToolStripMenuItemExit.Click
        NotifyIcon.Dispose()
        Application.Exit()
    End Sub

End Class
