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

Imports BGlobal

Public Class BMessage

#Region "Types"
    Public Enum enm_MessageImages
        Ok
        Cancel
        Warning
        Critical
        Information
        Question
        None
    End Enum
#End Region

#Region "Declarations"
    Private Shared objInstance As BMessage
    Private clrBaseColor As Brush
    Private clrBackground As Brush
    Private clrForeground As Brush
    Private clrTitleForeground As Brush
    Private objFont As FontFamily
    Private blnLog As Boolean
    Private strLogPath As String
    Private strOk As String
    Private strCancel As String
    Private strYes As String
    Private strNo As String
#End Region

#Region "Constructor"
    Private Sub New()
        Try
            clrBaseColor = Brushes.Purple
            clrBackground = Brushes.White
            objFont = New FontFamily("Arial")
            blnLog = ""
            strLogPath = ""
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Events"

#End Region

#Region "Functions"
    Public Shared Sub sbInitialize(p_clrBaseColor As Brush, p_clrBackground As Brush, p_clrForeground As Brush, p_clrTitleForeground As Brush, p_objFont As FontFamily, p_strLogPath As String)
        Try
            Call sbInitialize(p_clrBaseColor, p_clrBackground, p_clrForeground, p_clrTitleForeground, p_objFont, p_strLogPath, "Ok", "Cancel", "Yes", "No")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Shared Sub sbInitialize(p_clrBaseColor As Brush, p_clrBackground As Brush, p_clrForeground As Brush, p_clrTitleForeground As Brush, p_objFont As FontFamily, p_strLogPath As String, p_strButtonOk As String, p_strButtonCancel As String, p_strButtonYes As String, p_strButtonNo As String)
        Try

            If objInstance Is Nothing Then
                objInstance = New BMessage
            End If

            objInstance.BaseColor = p_clrBaseColor
            objInstance.Background = p_clrBackground
            objInstance.Foreground = p_clrForeground
            objInstance.TitleForeground = p_clrTitleForeground
            objInstance.Font = p_objFont
            objInstance.LogPath = p_strLogPath
            objInstance.blnLog = (objInstance.LogPath.Trim <> "")
            objInstance.Ok = p_strButtonOk
            objInstance.Cancel = p_strButtonCancel
            objInstance.Yes = p_strButtonYes
            objInstance.No = p_strButtonNo

            If objInstance.blnLog Then
                BLogger.sbInitialize(objInstance.LogPath.Trim)
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function fnErrorMessage(p_objException As Exception) As MessageBoxResult
        Try
            Return fnErrorMessage(p_objException, p_objException.Source, enm_MessageImages.Critical)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function fnErrorMessage(p_objException As Exception, p_strTitle As String) As MessageBoxResult
        Try
            Return fnErrorMessage(p_objException, p_strTitle, enm_MessageImages.Critical)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function fnErrorMessage(p_objException As Exception, p_strTitle As String, p_enmImage As enm_MessageImages) As MessageBoxResult

        Dim objAuxImage As BitmapImage

        Try

            Select Case p_enmImage
                Case enm_MessageImages.Ok
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Ok)
                Case enm_MessageImages.Cancel
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Critical)
                Case enm_MessageImages.Warning
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Warning)
                Case enm_MessageImages.Information
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Info)
                Case enm_MessageImages.Question
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Help)
                Case enm_MessageImages.Critical
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Critical)
                Case Else
                    objAuxImage = Nothing
            End Select


            Return fnErrorMessage(p_objException, p_strTitle, objAuxImage)

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function fnErrorMessage(p_objException As Exception, p_strTitle As String, p_objImage As ImageSource) As MessageBoxResult

        Dim enmReturn As MessageBoxResult
        Dim objMessage As BCriticalMessage

        Try

            If blnLog Then
                BLogger.Instance.sbLog(p_objException)
            End If

            enmReturn = MessageBoxResult.OK

            objMessage = New BCriticalMessage(p_objException, p_strTitle, strOk, p_objImage, clrBaseColor, clrBackground, clrForeground, clrTitleForeground, objFont)

            objMessage.ShowDialog()

            enmReturn = objMessage.Result

            Return enmReturn

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function fnMessage(p_strMessage As String, p_strTitle As String, p_enmStyle As MessageBoxButton) As MessageBoxResult
        Try
            Return fnMessage(p_strMessage, p_strTitle, p_enmStyle, enm_MessageImages.None)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function fnMessage(p_strMessage As String, p_strTitle As String, p_strButons As List(Of String), p_enmResults As List(Of MessageBoxResult)) As MessageBoxResult
        Try
            Return fnMessage(p_strMessage, p_strTitle, p_strButons, p_enmResults, enm_MessageImages.None)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function fnMessage(p_strMessage As String, p_strTitle As String, p_enmStyle As MessageBoxButton, p_enmImage As enm_MessageImages) As MessageBoxResult

        Dim objAuxImage As BitmapImage

        Try

            Select Case p_enmImage
                Case enm_MessageImages.Ok
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Ok)
                Case enm_MessageImages.Cancel
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Critical)
                Case enm_MessageImages.Warning
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Warning)
                Case enm_MessageImages.Information
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Info)
                Case enm_MessageImages.Question
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Help)
                Case enm_MessageImages.Critical
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Critical)
                Case Else
                    objAuxImage = Nothing
            End Select

            Return fnMessage(p_strMessage, p_strTitle, p_enmStyle, objAuxImage)

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function fnMessage(p_strMessage As String, p_strTitle As String, p_strButons As List(Of String), p_enmResults As List(Of MessageBoxResult), p_enmImage As enm_MessageImages) As MessageBoxResult

        Dim objAuxImage As ImageSource

        Try

            Select Case p_enmImage
                Case enm_MessageImages.Ok
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Ok)
                Case enm_MessageImages.Cancel
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Critical)
                Case enm_MessageImages.Warning
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Warning)
                Case enm_MessageImages.Information
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Info)
                Case enm_MessageImages.Question
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Help)
                Case enm_MessageImages.Critical
                    objAuxImage = GlobalFunctions.fnBitmapToBitmapImage(My.Resources.Critical)
                Case Else
                    objAuxImage = Nothing
            End Select

            Return fnMessage(p_strMessage, p_strTitle, p_strButons, p_enmResults, objAuxImage)

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function fnMessage(p_strMessage As String, p_strTitle As String, p_enmStyle As MessageBoxButton, p_objImage As ImageSource) As MessageBoxResult

        Dim enmReturn As MessageBoxResult
        Dim objMessage As BMessageBox

        Try

            enmReturn = MessageBoxResult.OK

            Select Case p_enmStyle
                Case MessageBoxButton.OK
                    objMessage = New BMessageBox(p_strMessage, p_strTitle, {strOk}.ToList, {MessageBoxResult.OK}.ToList, p_objImage, clrBaseColor, clrBackground, clrForeground, clrTitleForeground, objFont)
                Case MessageBoxButton.OKCancel
                    objMessage = New BMessageBox(p_strMessage, p_strTitle, {strOk, strCancel}.ToList, {MessageBoxResult.OK, MessageBoxResult.Cancel}.ToList, p_objImage, clrBaseColor, clrBackground, clrForeground, clrTitleForeground, objFont)
                Case MessageBoxButton.YesNo
                    objMessage = New BMessageBox(p_strMessage, p_strTitle, {strYes, strNo}.ToList, {MessageBoxResult.Yes, MessageBoxResult.No}.ToList, p_objImage, clrBaseColor, clrBackground, clrForeground, clrTitleForeground, objFont)
                Case MessageBoxButton.YesNoCancel
                    objMessage = New BMessageBox(p_strMessage, p_strTitle, {strYes, strNo, strCancel}.ToList, {MessageBoxResult.Yes, MessageBoxResult.No, MessageBoxResult.Cancel}.ToList, p_objImage, clrBaseColor, clrBackground, clrForeground, clrTitleForeground, objFont)
                Case Else
                    Throw New Exception("Invalid message box style.")
            End Select

            objMessage.ShowDialog()

            enmReturn = objMessage.Result

            Return enmReturn

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function fnMessage(p_strMessage As String, p_strTitle As String, p_strButons As List(Of String), p_enmResults As List(Of MessageBoxResult), p_objImage As ImageSource) As MessageBoxResult

        Dim enmReturn As MessageBoxResult
        Dim objMessage As BMessageBox

        Try

            enmReturn = MessageBoxResult.OK

            objMessage = New BMessageBox(p_strMessage, p_strTitle, p_strButons, p_enmResults, p_objImage, clrBaseColor, clrBackground, clrForeground, clrTitleForeground, objFont)

            objMessage.ShowDialog()

            enmReturn = objMessage.Result

            Return enmReturn

        Catch ex As Exception
            Throw
        End Try
    End Function
#End Region

#Region "Properties"
    Public Shared ReadOnly Property Instance As BMessage
        Get
            If objInstance IsNot Nothing Then
                Return objInstance
            Else
                Throw New Exception("Must be initialized.")
            End If
        End Get
    End Property

    Public Property BaseColor As Brush
        Get
            Return clrBaseColor
        End Get
        Set(value As Brush)
            clrBaseColor = value
        End Set
    End Property

    Public Property Font As FontFamily
        Get
            Return objFont
        End Get
        Set(value As FontFamily)
            objFont = value
        End Set
    End Property

    Public Property Log As Boolean
        Get
            Return blnLog
        End Get
        Set(value As Boolean)
            blnLog = value
        End Set
    End Property

    Public Property LogPath As String
        Get
            Return strLogPath
        End Get
        Set(value As String)
            strLogPath = value
        End Set
    End Property

    Public Property Background As Brush
        Get
            Return clrBackground
        End Get
        Set(value As Brush)
            clrBackground = value
        End Set
    End Property

    Public Property Foreground As Brush
        Get
            Return clrForeground
        End Get
        Set(value As Brush)
            clrForeground = value
        End Set
    End Property

    Public Property TitleForeground As Brush
        Get
            Return clrTitleForeground
        End Get
        Set(value As Brush)
            clrTitleForeground = value
        End Set
    End Property

    Public Property Ok As String
        Get
            Return strOk
        End Get
        Set(value As String)
            strOk = value
        End Set
    End Property

    Public Property Cancel As String
        Get
            Return strCancel
        End Get
        Set(value As String)
            strCancel = value
        End Set
    End Property

    Public Property Yes As String
        Get
            Return strYes
        End Get
        Set(value As String)
            strYes = value
        End Set
    End Property

    Public Property No As String
        Get
            Return strNo
        End Get
        Set(value As String)
            strNo = value
        End Set
    End Property
#End Region

End Class
