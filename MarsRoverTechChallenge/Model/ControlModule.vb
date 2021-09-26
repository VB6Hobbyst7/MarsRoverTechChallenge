
Namespace Model
    Public Class ControlModule


        Public Property Plateau As Model.Plateau.Grids(,)

        ''' <summary>
        ''' This is the current rover occuping the grid
        ''' if nothing then no rovers  currently inside of grid
        ''' </summary>
        ''' <returns></returns>
        Public Property Rover As Model.Rover.Rover = Nothing

        ''' <summary>
        ''' This is a list of all rovers that have previously occupied the grid
        ''' </summary>
        ''' <returns></returns>
        Public Property Rovers As New List(Of Model.Rover.Rover)



        Public Function Process(commands As String) As String
            Dim Results As String = ""
            Dim Seporators As Char() = New Char() {vbCrLf, vbLf}
            Dim Processs As List(Of String) = commands.Split(Seporators, options:=StringSplitOptions.RemoveEmptyEntries).ToList

            If Processs.Count > 0 Then
                Dim i As Int32 = 1
                Dim isValid As Boolean = True
                Dim Id As Int32 = 1
                For Each Proces As String In Processs
                    'If i equals to row 1 then Create the Plateau grids array
                    If i = 1 Then isValid = ProcessPlateau(Proces)
                    If i Mod 2 = 0 Then
                        ProcessRoversCoordinates(Proces, Id)
                        Id += 1
                    ElseIf Not i = 1 Then
                        ProcessRoversCommands(Proces)
                    End If
                    i += 1
                    If Not isValid Then Exit For
                Next Proces

                'if Plateau failed to create send -1 -1 to indicate error
                If Not isValid Then
                    Results = "-1 -1"
                Else
                    Results = ExecuteRoverCommand()
                End If

            Else
                Results = "-1 -1"
            End If

            Return Results
        End Function

        ''' <summary>
        ''' Create the Plateau grids array
        ''' </summary>
        ''' <param name="grids"></param>
        Private Function ProcessPlateau(grids As String) As Boolean
            Dim isValid As Boolean = True
            Dim XY As String() = grids.Split(" ", options:=StringSplitOptions.RemoveEmptyEntries)
            'Check that XY has data
            'Then make sure there are a minimum 2 inputs
            'Then make both inputs are numeric
            If XY IsNot Nothing AndAlso XY.Count >= 2 AndAlso IsNumeric(XY(0)) AndAlso IsNumeric(XY(1)) Then
                Dim Plateau(XY(0), XY(1)) As Model.Plateau.Grids

                For x As Int32 = 0 To XY(0)
                    For y As Int32 = 0 To XY(1)
                        'Initilize the grids
                        Plateau(x, y) = New Model.Plateau.Grids(x, y)
                    Next y
                Next x

                Me.Plateau = Plateau
            Else
                isValid = True
            End If

            Return isValid
        End Function

        ''' <summary>
        ''' create the rover and set up grid position and direction N E S W
        ''' </summary>
        ''' <param name="rover"></param>
        ''' <returns></returns>
        Private Function ProcessRoversCoordinates(rovers As String, Id As Int32) As Boolean
            Dim isValid As Boolean = True
            Dim XYP As String() = rovers.Split(" ", options:=StringSplitOptions.RemoveEmptyEntries)
            Dim Directions() As Char = {"N", "E", "S", "W"}

            'Check that XY has data
            'Then make sure there are a minimum 2 inputs
            'Then make both inputs are numeric
            'Then make sure the direction
            If XYP IsNot Nothing AndAlso XYP.Count >= 3 AndAlso IsNumeric(XYP(0)) AndAlso IsNumeric(XYP(1)) Then
                Dim Rover As New Model.Rover.Rover(Plateau)

                'set up rover coordinates
                With Rover
                    Try
                        .Id = Id
                        .X = XYP(0)
                        .Y = XYP(1)
                        .Direction = XYP(2)
                    Catch ex As Exception
                        isValid = False
                    End Try

                    If isValid Then
                        Me.Plateau(.X, .Y).Rovers.Add(Rover)
                        Me.Rovers.Add(Rover)
                    End If
                End With

            Else
                Common.Logs.Save($"Rover command in wrong format ""rovers""")
                isValid = False
            End If

            'insert empty rover so when commands get processed it will see that creation of the rover errored out
            If Not isValid Then Me.Rovers.Add(Nothing)

            Return isValid
        End Function

        ''' <summary>
        ''' add the rover commands to the rover
        ''' </summary>
        ''' <param name="commands"></param>
        ''' <returns></returns>
        Private Function ProcessRoversCommands(commands As String) As Boolean
            Dim isValid As Boolean

            Me.Rovers.Last.Commands = commands

            Return isValid
        End Function

        ''' <summary>
        ''' Executes the rover commands in sequace that the rover was set up
        ''' </summary>
        ''' <returns></returns>
        Private Function ExecuteRoverCommand() As String
            Dim Results As New List(Of String)

            For Each Rover As Model.Rover.Rover In Me.Rovers
                Rover.Execute(Me.Plateau)
                Results.Add(Rover.ToString)
            Next Rover

            Return String.Join(vbCrLf, Results)
        End Function




    End Class
End Namespace
