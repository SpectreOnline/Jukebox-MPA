Imports System
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Linq

Partial Public Class Model1
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=JukeboxContext")
    End Sub

    Public Overridable Property C__MigrationHistory As DbSet(Of C__MigrationHistory)
    Public Overridable Property Playlists As DbSet(Of Playlist)
    Public Overridable Property Songs As DbSet(Of Song)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        modelBuilder.Entity(Of Playlist)() _
            .HasMany(Function(e) e.Songs) _
            .WithOptional(Function(e) e.Playlist) _
            .HasForeignKey(Function(e) e.Playlists_ID)
    End Sub
End Class
