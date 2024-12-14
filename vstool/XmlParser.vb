Imports System.Text.RegularExpressions

Public Class XmlParser
    Private latestpath As String

    Public Sub New()
    End Sub

    Public Function parse(txt As String)
        Dim r As New Regex("(?<x><\?xml.+?\?>.+)", RegexOptions.Singleline)  'グループ化しておく
        Dim m As Match = r.Match(txt)
        If m.Success Then
            Try
                Console.WriteLine(m.Groups("x").Value)
                latestpath = ShareOptionXML.Instance.savepath + "\" + Now.ToString("yyyyMMdd_HHmmss") + ".xml"
                Dim sw As New IO.StreamWriter(latestpath, False, Text.Encoding.GetEncoding("shift_jis"))
                sw.Write(m.Groups("x").Value)
                sw.Close()
                Return True
            Catch ex As Exception
                Console.WriteLine(ex.ToString)
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    Public Function path()
        Return latestpath
    End Function

End Class
