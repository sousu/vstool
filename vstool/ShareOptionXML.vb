
Imports System.IO
Imports System.Text

<Serializable()>
Public Class ShareOptionXML

    ' 設定値及びデフォルト定義をここに列挙
    Property savepath As String = Environment.SpecialFolder.Desktop


    <NonSerialized()>
    Private Shared _instance As ShareOptionXML
    <System.Xml.Serialization.XmlIgnore()>
    Public Shared Property Instance() As ShareOptionXML
        Get
            If _instance Is Nothing Then
                _instance = New ShareOptionXML
            End If
            Return _instance
        End Get
        Set(ByVal Value As ShareOptionXML)
            _instance = Value
        End Set
    End Property

    '設定をXMLファイルから読み込み復元する
    Public Shared Sub LoadFromXmlFile()
        Dim p As String = GetSettingPath()
        If Not File.Exists(p) Then
            Return '設定が無い場合は読み込まずコンストラクタのままとする
        End If

        Dim sr As New StreamReader(p, New UTF8Encoding(False))
        Dim xs As New System.Xml.Serialization.XmlSerializer(GetType(ShareOptionXML))
        '読み込んで逆シリアル化する
        Dim obj As Object = xs.Deserialize(sr)
        sr.Close()

        Instance = CType(obj, ShareOptionXML)
    End Sub

    '現在の設定をXMLファイルに保存する
    Public Shared Sub SaveToXmlFile()
        Dim p As String = GetSettingPath()

        Dim sw As New StreamWriter(p, False, New UTF8Encoding(False))
        Dim xs As New Xml.Serialization.XmlSerializer(GetType(ShareOptionXML))
        'シリアル化して書き込む
        xs.Serialize(sw, Instance)
        sw.Close()
    End Sub

    Private Shared Function GetSettingPath() As String
        '実行ファイルパスに保存
        Dim p As String = My.Application.Info.DirectoryPath + "\" + Application.ProductName + ".setting"
        'アプリ環境に保存
        'Dim p As String = Path.Combine(
        '    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        '    "\" + Application.ProductName + ".setting")

        Return p
    End Function

End Class
