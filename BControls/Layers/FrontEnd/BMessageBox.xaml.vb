'Copyright 2018 Kelvys B. Pantaleão

'This file is part of BControls

'BControls Is free software: you can redistribute it And/Or modify
'it under the terms Of the GNU General Public License As published by
'the Free Software Foundation, either version 3 Of the License, Or
'(at your option) any later version.

'This program Is distributed In the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty Of
'MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License For more details.

'You should have received a copy Of the GNU General Public License
'along with this program.  If Not, see <http://www.gnu.org/licenses/>.


'Este arquivo é parte Do programa BControls

'BControls é um software livre; você pode redistribuí-lo e/ou 
'modificá-lo dentro dos termos da Licença Pública Geral GNU como 
'publicada pela Fundação Do Software Livre (FSF); na versão 3 da 
'Licença, ou(a seu critério) qualquer versão posterior.

'Este programa é distribuído na esperança de que possa ser  útil, 
'mas SEM NENHUMA GARANTIA; sem uma garantia implícita de ADEQUAÇÃO
'a qualquer MERCADO ou APLICAÇÃO EM PARTICULAR. Veja a
'Licença Pública Geral GNU para maiores detalhes.

'Você deve ter recebido uma cópia da Licença Pública Geral GNU junto
'com este programa, Se não, veja <http://www.gnu.org/licenses/>.

'GitHub: https://github.com/Kelvysb/BControls

Public Class BMessageBox

#Region "Declarations"
    Private strMessage As String
    Private strTitle As String
    Private enmResults As List(Of MessageBoxResult)
    Private enmResult As MessageBoxResult
#End Region

#Region "Constructor"
    Public Sub New(p_strMessage As String, p_strTitle As String, p_strButons As List(Of String), p_enmReturns As List(Of MessageBoxResult), p_objImage As ImageSource, p_clrBaseColor As Brush, p_clrBackground As Brush, p_clrForeground As Brush, p_clrTitleForeground As Brush, p_objFont As FontFamily)
        Try
            InitializeComponent()


            enmResults = New List(Of MessageBoxResult)

            brdBorder.BorderBrush = p_clrBaseColor
            grdTitle.Background = p_clrBaseColor
            Me.FontFamily = p_objFont
            Me.Foreground = p_clrForeground
            lblTitle.Foreground = p_clrTitleForeground
            lblTitle.Content = p_strTitle
            lblMessage.Foreground = p_clrForeground
            lblMessage.Text = p_strMessage
            btn1.Background = p_clrBaseColor
            btn2.Background = p_clrBaseColor
            btn3.Background = p_clrBaseColor

            If p_strButons.Count <> p_enmReturns.Count Then
                Throw New Exception("Wrong number of returns.")
            End If

            If p_strButons.Count = 1 Then
                btn1.Content = p_strButons(0)
                enmResults.Add(p_enmReturns(0))

                btn2.Visibility = Visibility.Collapsed
                btn3.Visibility = Visibility.Collapsed
            ElseIf p_strButons.Count = 2 Then
                btn1.Content = p_strButons(0)
                enmResults.Add(p_enmReturns(0))

                btn2.Content = p_strButons(1)
                enmResults.Add(p_enmReturns(1))

                btn3.Visibility = Visibility.Collapsed
            ElseIf p_strButons.Count >= 3 Then
                btn1.Content = p_strButons(0)
                enmResults.Add(p_enmReturns(0))

                btn2.Content = p_strButons(1)
                enmResults.Add(p_enmReturns(1))

                btn3.Content = p_strButons(2)
                enmResults.Add(p_enmReturns(2))
            Else
                btn1.Content = "Ok"
                enmResults.Add(MessageBoxResult.OK)
                btn2.Visibility = Visibility.Collapsed
                btn3.Visibility = Visibility.Collapsed
            End If

            btn1.Focus()

            If p_objImage IsNot Nothing Then
                imgIcon.Visibility = Visibility.Visible
                imgIcon.Source = p_objImage
            Else
                imgIcon.Visibility = Visibility.Collapsed
            End If

            enmResult = MessageBoxResult.None
        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region

#Region "Events"
    Private Sub grdTitle_MouseMove(sender As Object, e As MouseEventArgs) Handles grdTitle.MouseMove
        Try
            If e.LeftButton = MouseButtonState.Pressed Then
                Cursor = Cursors.SizeAll
                Me.DragMove()
            Else
                Cursor = Cursors.Arrow
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BMessageBox_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles Me.PreviewKeyDown
        Try
            If e.Key = Key.Enter Then
                Call sbExit(0)
                e.Handled = True
            ElseIf e.Key = Key.Escape Then
                If enmResults.Count > 1 Then
                    Call sbExit(enmResults.Count - 1)
                    e.Handled = True
                End If
            ElseIf e.Key = Key.C And e.KeyboardDevice.Modifiers = ModifierKeys.Control Then
                Clipboard.SetText(lblTitle.Content.ToString.Trim & vbNewLine & "---------------" & vbNewLine & lblMessage.Text)
                e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btn1_Click(sender As Object, e As RoutedEventArgs) Handles btn1.Click
        Try
            Call sbExit(0)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btn2_Click(sender As Object, e As RoutedEventArgs) Handles btn2.Click
        Try
            Call sbExit(1)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btn3_Click(sender As Object, e As RoutedEventArgs) Handles btn3.Click
        Try
            Call sbExit(2)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

#Region "Functions"
    Private Sub sbExit(p_intButtonIndex As Integer)
        Try
            If p_intButtonIndex <= enmResults.Count - 1 Then
                enmResult = enmResults(p_intButtonIndex)
            Else
                Throw New Exception("Wrong number of results.")
            End If
            Me.Close()
        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property Result As MessageBoxResult
        Get
            Return enmResult
        End Get
    End Property
#End Region

End Class
