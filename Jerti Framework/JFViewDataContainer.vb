
Public Class JFViewDataContainer(Of T)
    Implements IViewDataContainer
    Public Sub New(viewData As ViewDataDictionary(Of T))
        Me.ViewData = viewData
    End Sub

    Public Property ViewData As ViewDataDictionary Implements IViewDataContainer.ViewData
End Class
