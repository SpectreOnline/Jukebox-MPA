Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class Playlist
    Public Sub New()
        Songs = New HashSet(Of Song)()
    End Sub

    Public Property ID As Integer

    Public Property Creator As String

    Public Overridable Property Songs As ICollection(Of Song)
End Class
