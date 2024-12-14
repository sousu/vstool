' IEの自動起動

Imports System.Threading
Imports System.Runtime.InteropServices

Public Class IEopener
    Private ie As SHDocVw.InternetExplorer
    Private hwnd As Integer

    Public Sub New()
        createIE()
    End Sub

    Public Sub open(url As String)
        Try
            Dim exist As Boolean = False
            For Each app In CreateObject("Shell.Application").Windows()
                If hwnd = app.HWND Then exist = True
            Next
            If Not exist Then createIE()
            '表示
            activateIE()
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
        Try
            ie.Navigate(url)
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try

    End Sub

    Private Sub createIE()
        ie = New SHDocVw.InternetExplorer
        hwnd = ie.HWND
    End Sub

    Public Sub close()
        Try
            ie.Quit()
            Marshal.ReleaseComObject(ie)
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

    '表示し前に出す
    <DllImport("USER32.DLL")>
    Private Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As Boolean
    End Function
    Private Sub activateIE()
        Try
            Thread.Sleep(300)
            For Each ps As Process In Process.GetProcesses()
                If 0 <= ps.MainWindowTitle.IndexOf("Internet Explorer") Then
                    SetForegroundWindow(ps.MainWindowHandle)
                    Exit For
                End If
            Next
            AppActivate("Internet Explorer")
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
        Try
            ie.Visible = True
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

End Class
