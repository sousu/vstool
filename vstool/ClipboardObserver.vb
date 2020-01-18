
' --- ---
' クリップボード動作を起点とするためツールのメインとなるクラス
' クリップボードハンドラの利用のためだけのフォーム作成を行うが表示させない
' ------

Imports System.Security.Permissions
Imports System.Threading

Public Class ClipboardObserver
    Private xmlparser As XmlParser
    Private ieopener As IEopener
    Private component As Component

    Public Sub New(i As IEopener, c As Component)
        xmlparser = New XmlParser
        ieopener = i
        component = c
        ' イベントハンドラ登録
        Dim viewer = New ClipboardViewer(Me)
        AddHandler viewer.ClipboardHandler, AddressOf OnClipBoardChanged
        InitializeComponent() 'フォームとしての初期化を明示的に起動
    End Sub

    ' クリップボードにテキストがコピー時の呼び出し
    Public Sub OnClipBoardChanged(ByVal sender As Object, ByVal args As ClipboardEventArgs)
        Console.WriteLine("--- copy ---")
        Console.WriteLine(args.Text)
        If xmlparser.parse(args.Text) Then
            ieopener.open(xmlparser.path())
        Else
            '反応がわかるようにアイコン変化
            component.NotifyIcon.Icon = My.Resources.musimegane_red
            Thread.Sleep(1000)
            component.NotifyIcon.Icon = My.Resources.musimegane_black
        End If
    End Sub

    'フォームのCreateParamsプロパティをオーバーライドして非表示化
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        <SecurityPermission(SecurityAction.Demand, Flags:=SecurityPermissionFlag.UnmanagedCode)>
        Get
            Const WS_EX_TOOLWINDOW As Int32 = &H80
            Const WS_POPUP As Int32 = &H80000000
            Const WS_VISIBLE As Int32 = &H10000000
            Const WS_SYSMENU As Int32 = &H80000
            Const WS_MAXIMIZEBOX As Int32 = &H10000
            Dim cp As CreateParams
            cp = MyBase.CreateParams
            cp.ExStyle = WS_EX_TOOLWINDOW
            cp.Style = WS_POPUP Or WS_VISIBLE Or
                WS_SYSMENU Or WS_MAXIMIZEBOX
            cp.Height = 0
            cp.Width = 0
            Return cp
        End Get
    End Property

End Class
