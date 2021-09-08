Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class Song
    Public Property ID As Integer

    Public Property Name As String

    Public Property Author As String

    Public Property DurationMinutes As Integer

    Public Property DurationSeconds As Integer

    Public Property Playlists_ID As Integer?

    Public Overridable Property Playlist As Playlist
End Class
