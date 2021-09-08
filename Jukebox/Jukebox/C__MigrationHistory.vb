Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("__MigrationHistory")>
Partial Public Class C__MigrationHistory
    <Key>
    <Column(Order:=0)>
    <StringLength(150)>
    Public Property MigrationId As String

    <Key>
    <Column(Order:=1)>
    <StringLength(300)>
    Public Property ContextKey As String

    <Required>
    Public Property Model As Byte()

    <Required>
    <StringLength(32)>
    Public Property ProductVersion As String
End Class
