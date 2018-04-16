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

Public Class BCriticalMessage
#Region "Declarations"
    Private strMessage As String
    Private strTitle As String
    Private enmResult As MessageBoxResult
#End Region

#Region "Constructor"
    Public Sub New(p_objException As Exception, p_strTitle As String, p_strButton As String, p_objImage As ImageSource, p_clrBaseColor As Brush, p_clrBackground As Brush, p_clrForeground As Brush, p_clrTitleForeground As Brush, p_objFont As FontFamily)

        Dim objAuxInner As Exception

        Try
            InitializeComponent()

            brdBorder.BorderBrush = p_clrBaseColor
            brdDetails.BorderBrush = p_clrBaseColor
            grdTitle.Background = p_clrBaseColor
            Me.FontFamily = p_objFont
            Me.Foreground = p_clrForeground
            lblTitle.Foreground = p_clrTitleForeground
            lblTitle.Content = p_strTitle

            btnInfo.Foreground = p_clrTitleForeground
            btnInfo.Background = p_clrBaseColor

            lblMessage.Foreground = p_clrForeground
            lblMessage.Text = p_objException.Message
            txtDetais.Foreground = p_clrForeground
            txtDetais.Text = p_objException.Message
            btn1.Background = p_clrBaseColor

            objAuxInner = p_objException.InnerException
            txtDetais.Text = ""

            Do While objAuxInner IsNot Nothing
                txtDetais.Text = txtDetais.Text & objAuxInner.Message & vbNewLine & "-----------------" & objAuxInner.StackTrace & vbNewLine
                objAuxInner = objAuxInner.InnerException
            Loop

            btn1.Content = p_strButton

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
                Call sbExit()
                e.Handled = True
            ElseIf e.Key = Key.Escape Then
                Call sbExit()
            ElseIf e.Key = Key.C And e.KeyboardDevice.Modifiers = ModifierKeys.Control Then
                Clipboard.SetText(lblTitle.Content.ToString.Trim & vbNewLine & "---------------" & vbNewLine & lblMessage.Text & vbNewLine & txtDetais.Text)
                e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btn1_Click(sender As Object, e As RoutedEventArgs) Handles btn1.Click
        Try
            Call sbExit()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnInfo_Click(sender As Object, e As RoutedEventArgs) Handles btnInfo.Click
        Try
            Call sbShowInfo()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

#Region "Functions"
    Private Sub sbShowInfo()
        Try
            If brdDetails.Visibility = Visibility.Collapsed Then
                btnInfo.Content = "-"
                brdDetails.Visibility = Visibility.Visible
            Else
                btnInfo.Content = "+"
                brdDetails.Visibility = Visibility.Collapsed
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub sbExit()
        Try

            enmResult = MessageBoxResult.OK

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
