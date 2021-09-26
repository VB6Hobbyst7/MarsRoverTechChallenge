
Namespace Model.Rover
    Public Class Rover

        ''' <summary>
        ''' set the _x to -1 means that the x coordinate has not been set yet
        ''' </summary>
        Private _x As String = -1
        Private _y As String = -1
        Private _direction As String

#Region "Properties"


        ''' <summary>
        ''' this is the rovers id
        ''' </summary>
        ''' <returns></returns>
        Public Property Id As Int32

        ''' <summary>
        ''' last copy of the plateau from central command
        ''' </summary>
        ''' <returns></returns>
        Public Property Plateau As Model.Plateau.Grids(,)

        ''' <summary>
        ''' current x position on the plateau of the rover
        ''' </summary>
        ''' <returns></returns>
        Public Property X As Int32
            Get
                Return _x
            End Get
            Set(value As Int32)
                'Check that the x coordinate is inside of the plateau
                If value < 0 OrElse value > Plateau.GetUpperBound(0) Then
                    Common.Logs.Save($"X coordinate is outside of the plateau ""{value}""")
                    Throw New IndexOutOfRangeException("-1 -1")
                Else
                    If _y > -1 Then
                        If CheckPlateauPosition(_x, _y) Then
                            _x = value
                        Else
                            Common.Logs.Save($"Stopping commands another rover currently in grid ""X:{_x} Y:{_y}""")
                            Throw New Exception("-1 -1")
                        End If
                    Else
                        _x = value
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' current y position on the plateau of the rover
        ''' </summary>
        ''' <returns></returns>
        Public Property Y As Int32
            Get
                Return _y
            End Get
            Set(value As Int32)
                'Check that the x coordinate is inside of the plateau
                If value < 0 OrElse value > Plateau.GetUpperBound(1) Then
                    Common.Logs.Save($"Y coordinate is outside of the plateau ""{value}""")
                    Throw New IndexOutOfRangeException("-1 -1")
                Else
                    If _x > -1 Then
                        If CheckPlateauPosition(_x, _y) Then
                            _y = value
                        Else
                            Common.Logs.Save($"Stopping commands another rover currently in grid ""X:{_x} Y:{_y}""")
                            Throw New Exception("-1 -1")
                        End If
                    Else
                        _y = value
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' get the direction the rover is pointing in
        ''' </summary>
        ''' <returns></returns>
        Public Property Direction As String
            Get
                Return _direction
            End Get
            Set(value As String)
                If value.Length = 1 AndAlso value.ToUpper.IndexOfAny(New Char() {"N", "E", "S", "W"}) > -1 Then
                    _direction = value
                Else
                    Common.Logs.Save($"A non direction character was used ""{value}""")
                    Throw New Exception("-1 -1")
                End If
            End Set
        End Property

        Public Property Commands As String


#End Region

#Region "Business"

        Public Sub New(Plateaus As Model.Plateau.Grids(,))
            Me.Plateau = Plateaus
        End Sub

        ''' <summary>
        ''' make sure another rover is not currently in the grid
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="y"></param>
        ''' <returns></returns>
        Private Function CheckPlateauPosition(x As Int32, y As Int32)
            Dim isValid As Boolean = True
            'Check that the x and y values have been set
            'then check that another rover does not already exist in the grid
            If x >= 0 AndAlso y >= 0 AndAlso Not Plateau(x, y).Rover Is Nothing Then isValid = False

            Return isValid
        End Function

        ''' <summary>
        ''' execute the rover commands to move in the grid
        ''' </summary>
        ''' <param name="plateau"></param>
        ''' <returns></returns>
        Public Function Execute(plateau As Model.Plateau.Grids(,)) As Model.Plateau.Grids(,)
            Me.Plateau = plateau

            Try

                For Each Movement As Char In Me.Commands.ToUpper.ToCharArray

                    Select Case Movement
                        Case "M"
                            Select Case Me.Direction
                                Case "N" : Me.Y += 1
                                Case "E" : Me.X += 1
                                Case "S" : Me.Y -= 1
                                Case "W" : Me.X -= 1
                            End Select

                            MoveRoverExecution(_x, _y)

                        Case "R"
                            Select Case Me.Direction
                                Case "N" : Me.Direction = "E"
                                Case "E" : Me.Direction = "S"
                                Case "S" : Me.Direction = "W"
                                Case "W" : Me.Direction = "N"
                            End Select

                        Case "L"
                            Select Case Me.Direction
                                Case "N" : Me.Direction = "W"
                                Case "E" : Me.Direction = "N"
                                Case "S" : Me.Direction = "E"
                                Case "W" : Me.Direction = "S"
                            End Select
                    End Select

                Next Movement

            Catch ex As Exception

            End Try

            Return plateau
        End Function

        ''' <summary>
        ''' Take instrament readings and photos of grid
        ''' </summary>
        Private Function ExecuteMeasurements() As Model.Rover.Information
            Dim Info As New Model.Rover.Information

            With Info
                .Roverid = Me.Id
                'TODO: add instrument readings code here
            End With

            Return Info
        End Function

        ''' <summary>
        ''' Moves the rover in the grid and removes the rover from the last position in the grid
        ''' And executes the intrument readings on the rover
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="y"></param>
        Private Sub MoveRoverExecution(x As String, y As String)

            'remove rover from old current grid
            For i As Int32 = 0 To Plateau.GetUpperBound(0)
                For o As Int32 = 0 To Plateau.GetUpperBound(1)
                    If Plateau(i, o).Rover IsNot Nothing AndAlso Plateau(i, o).Rover.Id = Me.Id Then
                        Plateau(i, o).Rover = Nothing
                        Exit For
                    End If
                Next o
            Next i

            'Add rover to new current grid
            With Plateau(_x, _y)
                .Rover = Me
                'capture instrument readings and assign it to the grid
                .Information.Add(ExecuteMeasurements())
            End With

        End Sub

        ''' <summary>
        ''' combines the X and Y and direction cooridinates
        ''' </summary>
        ''' <returns></returns>
        Public Shadows Function ToString() As String
            Return $"{Me.X} {Me.Y} {Me.Direction}"
        End Function



#End Region



    End Class

End Namespace