
Namespace Common
    Public Module Logs


#Region "Properties"
        Public Property Message As String

#End Region

#Region "Business"

        ''' <summary>
        ''' clear out the properties
        ''' </summary>
        Public Sub Clear()
            Message = ""
        End Sub

        ''' <summary>
        ''' save the message to the text file
        ''' </summary>
        ''' <param name="messages"></param>
        Public Sub Save(messages As String)
            Message = messages
            Save()
        End Sub

        ''' <summary>
        ''' save the message property to the text file
        ''' </summary>
        Public Sub Save()

            'save the date and time followed by the log message 
            Message = $"{Now.ToString("yyyy/MM/dd HH:mm")}: {Message}"

            'TODO: add save to text file here

            Clear()
        End Sub



#End Region



    End Module
End Namespace