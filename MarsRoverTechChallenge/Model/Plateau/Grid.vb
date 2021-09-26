

Namespace Model.Plateau
    Public Class Grids


#Region "Properties"

        ''' <summary>
        ''' x cooridinates
        ''' </summary>
        ''' <returns></returns>
        Public Property x As Int32
        ''' <summary>
        ''' y coordinates
        ''' </summary>
        ''' <returns></returns>
        Public Property y As Int32

        ''' <summary>
        ''' Accessable indicates if a rover can enter that grid
        ''' Reasons for not been accessable
        '''   * another rover is already in that grid 
        '''   * there is an obstacle in the way not allowing the rover to pass, example a moutain
        ''' </summary>
        ''' <returns></returns>
        Public Property isAccessable As Boolean = True

        ''' <summary>
        ''' current rover in the grid
        ''' </summary>
        ''' <returns></returns>
        Public Property Rover As Model.Rover.Rover

        ''' <summary>
        ''' keeps record of all the rovers and instrument readings that have been taken
        ''' </summary>
        ''' <returns></returns>
        Public Property Rovers As New List(Of Model.Rover.Rover)


        Public Property Information As New List(Of Model.Rover.Information)

#End Region

#Region "Business"


        Public Sub New(x As Int32, y As Int32)
            Me.x = x
            Me.y = y

        End Sub

#End Region


    End Class
End Namespace