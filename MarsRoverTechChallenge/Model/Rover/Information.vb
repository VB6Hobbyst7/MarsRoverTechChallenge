
Namespace Model.Rover

    ''' <summary>
    ''' holds the Instrumental readings capturted by the rover when travelling through that grid
    ''' </summary>
    Public Class Information

        ''' <summary>
        ''' this is the rovers ID
        ''' </summary>
        ''' <returns></returns>
        Public Property Roverid As Int32

        ''' <summary>
        ''' the date time used will be the one synced with the command center
        ''' </summary>
        ''' <returns></returns>
        Public Property DateTimeCaptured As DateTime

        ''' <summary>
        ''' capture a 360 degree image of the grid
        ''' </summary>
        ''' <returns></returns>
        Public Property Photo As Byte()

        ''' <summary>
        ''' capture wind speed
        ''' </summary>
        ''' <returns></returns>
        Public Property Anemometer As Decimal

        ''' <summary>
        ''' Measures air pressure
        ''' </summary>
        ''' <returns></returns>
        Public Property Barometer As Decimal

        ''' <summary>
        ''' Measures the water vapor content of air or the humidity
        ''' </summary>
        ''' <returns></returns>
        Public Property Hygrometer As Decimal


        Public Function TakeMeasurements()


        End Function

    End Class
End Namespace